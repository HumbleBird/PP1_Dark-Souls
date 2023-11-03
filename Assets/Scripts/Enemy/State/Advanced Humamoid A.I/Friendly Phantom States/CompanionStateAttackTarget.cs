using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CompanionStateAttackTarget : State
{
    CompanionStateRotateTowardsTarget rotateTowardsTargetState;
    CompanionStateCombatStance combatStanceState;
    CompanionStatePursueTarget pursueTargetState;
    public ItemBasedAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    private void Awake()
    {
        rotateTowardsTargetState = GetComponent<CompanionStateRotateTowardsTarget>();
        combatStanceState = GetComponent<CompanionStateCombatStance>();
        pursueTargetState = GetComponent<CompanionStatePursueTarget>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        if (aiCharacter.combatStyle == AICombatStyle.swordAndShield)
        {
            return ProcessSwordAndShieldCombatStyle(aiCharacter);
        }
        else if (aiCharacter.combatStyle == AICombatStyle.Archer)
        {
            return ProcessArcherCombatSyle(aiCharacter);
        }
        else
        {
            return this;
        }
    }

    private State ProcessSwordAndShieldCombatStyle(AICharacterManager aiCharacter)
    {

        RotateTowardsTargetWhilstAttacking(aiCharacter);

        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if (willDoComboOnNextAttack && aiCharacter.canDoCombo)
        {
            // Attack with combo
            AttackTargetWithCombo(aiCharacter);

            // set cool down time
        }

        if (!hasPerformedAttack)
        {
            //Attack
            AttackTarget(aiCharacter);

            // Roll for a combo change
            RollForComboChance(aiCharacter);
        }

        if (willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this; // goes back up to perform the combo
        }

        ResetStateFlags();
        return rotateTowardsTargetState;
    }

    private State ProcessArcherCombatSyle(AICharacterManager aiCharacter)
    {
        RotateTowardsTargetWhilstAttacking(aiCharacter);

        if (aiCharacter.isInteracting)
            return this;

        if (aiCharacter.isHoldingArrow == false)
        {
            ResetStateFlags();
            return combatStanceState;
        }

        if (aiCharacter.currentTarget.isDead)
        {
            ResetStateFlags();
            aiCharacter.currentTarget = null;
            return combatStanceState;
        }

        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            ResetStateFlags();
            return pursueTargetState;
        }

        if (!hasPerformedAttack)// && aiCharacter.isHoldingArrow)
        {
            FireAmmo(aiCharacter);
            return this;
        }

        ResetStateFlags();
        return rotateTowardsTargetState;

    }

    private void AttackTarget(AICharacterManager aiCharacter)
    {
        currentAttack.PerformAttackAction(aiCharacter);
        aiCharacter.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(AICharacterManager aiCharacter)
    {
        currentAttack.PerformAttackAction(aiCharacter);
        willDoComboOnNextAttack = false;
        aiCharacter.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }


    private void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacterManager)
    {
        // Rotate manually
        if (aiCharacterManager.canRotate && aiCharacterManager.isInteracting)
        {
            Vector3 direction = aiCharacterManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            aiCharacterManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, aiCharacterManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void RollForComboChance(AICharacterManager aiCharacterManager)
    {
        float comboChance = Random.Range(0, 100);

        if (aiCharacterManager.allowAIToPerformCombos && comboChance <= aiCharacterManager.comboLikelyHood)
        {
            if (currentAttack.actionCanCombo)
            {
                willDoComboOnNextAttack = true;
            }
            else
            {
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }

        }
    }

    private void ResetStateFlags()
    {
        willDoComboOnNextAttack = false;
        hasPerformedAttack = false;
    }

    private void FireAmmo(AICharacterManager aiCharacterManager)
    {
        if (aiCharacterManager.isHoldingArrow)
        {
            hasPerformedAttack = true;
            aiCharacterManager.characterEquipmentManager.currentItemBeingUsed = aiCharacterManager.characterEquipmentManager.m_CurrentHandRightWeapon;
            aiCharacterManager.characterEquipmentManager.m_CurrentHandRightWeapon.th_tap_RB_Action.PerformAction(aiCharacterManager);
        }
    }
}
