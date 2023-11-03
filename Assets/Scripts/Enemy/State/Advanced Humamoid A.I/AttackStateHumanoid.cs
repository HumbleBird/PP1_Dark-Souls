using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 스코어 점수에 따른 공격들 중 하나 선택
// 선택한 공격이 각도, 거리 상의 문제가 있다면 새롭게 다시 선택
// 공격이 변형 가능하다면, 공격자의 움직임을 멈추거나, 타겟을 공격한다.
// 공격 쿨타임 체크 
// combat State로 복귀

public class AttackStateHumanoid : State
{
    RotateTowardsTargetStateHumanoid rotateTowardsTargetState;
    CombatStanceStateHumanoid combatStanceState;
    PursueTargetStateHumanoid pursueTargetState;
    public ItemBasedAttackAction currentAttack;

    public bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    private void Awake()
    {
        rotateTowardsTargetState = GetComponent<RotateTowardsTargetStateHumanoid>();
        combatStanceState = GetComponent<CombatStanceStateHumanoid>();
        pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
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

        if(aiCharacter.isHoldingArrow == false)
        {
            ResetStateFlags();
            return combatStanceState;
        }

        if (aiCharacter.currentTarget.isDead)
        {
            ResetStateFlags();
            //aiCharacter.currentTarget = null;
            return this;
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


    private void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacter)
    {
        // Rotate manually
        if (aiCharacter.canRotate && aiCharacter.isInteracting)
        {
            Vector3 direction = aiCharacter.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            aiCharacter.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, aiCharacter.rotationSpeed / Time.deltaTime);
        }
    }

    private void RollForComboChance(AICharacterManager aiCharacter)
    {
        float comboChance = Random.Range(0, 100);

        if(aiCharacter.allowAIToPerformCombos && comboChance <= aiCharacter.comboLikelyHood)
        {
            if(currentAttack.actionCanCombo)
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

    private void FireAmmo(AICharacterManager aiCharacter)
    {
        if(aiCharacter.isHoldingArrow)
        {
            hasPerformedAttack = true;
            aiCharacter.characterEquipmentManager.currentItemBeingUsed = aiCharacter.characterEquipmentManager.m_CurrentHandRightWeapon;
            aiCharacter.characterEquipmentManager.m_CurrentHandRightWeapon.th_tap_RB_Action.PerformAction(aiCharacter);
        }
    }
}
