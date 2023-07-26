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
    public RotateTowardsTargetStateHumanoid rotateTowardsTargetState;
    public CombatStanceStateHumanoid combatStanceState;
    public PursueTargetStateHumanoid pursueTargetState;
    //public EatPantState eatPantState;
    public ItemBasedAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    private void Awake()
    {
        //eatPantState = FindObjectOfType<EatPantState>();
        rotateTowardsTargetState = GetComponent<RotateTowardsTargetStateHumanoid>();
        combatStanceState = GetComponent<CombatStanceStateHumanoid>();
        pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
    }

    public override State Tick(EnemyManager enemy)
    {
        if (enemy.combatStyle == AICombatStyle.swordAndShield)
        {
            return ProcessSwordAndShieldCombatStyle(enemy);
        }
        else if (enemy.combatStyle == AICombatStyle.Archer)
        {
            return ProcessArcherCombatSyle(enemy);
        }
        else
        {
            return this;
        }
    }

    private State ProcessSwordAndShieldCombatStyle(EnemyManager enemy)
    {

        RotateTowardsTargetWhilstAttacking(enemy);

        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if (willDoComboOnNextAttack && enemy.canDoCombo)
        {
            // Attack with combo
            AttackTargetWithCombo(enemy);

            // set cool down time
        }

        if (!hasPerformedAttack)
        {
            //Attack
            AttackTarget(enemy);

            // Roll for a combo change
            RollForComboChance(enemy);
        }

        if (willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this; // goes back up to perform the combo
        }

        ResetStateFlags();
        return rotateTowardsTargetState;
    }

    private State ProcessArcherCombatSyle(EnemyManager enemy)
    {
        RotateTowardsTargetWhilstAttacking(enemy);

        if (enemy.isInteracting)
            return this;

        if(enemy.isHoldingArrow == false)
        {
            ResetStateFlags();
            return combatStanceState;
        }

        if (enemy.currentTarget.isDead)
        {
            ResetStateFlags();
            enemy.currentTarget = null;
            return this;
        }

        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            ResetStateFlags();
            return pursueTargetState;
        }

        if (!hasPerformedAttack && enemy.isHoldingArrow)
        {
            FireAmmo(enemy);
        }

        ResetStateFlags();
        return rotateTowardsTargetState;

    }

    private void AttackTarget(EnemyManager enemy)
    {
        currentAttack.PerformAttackAction(enemy);
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(EnemyManager enemy)
    {
        currentAttack.PerformAttackAction(enemy);
        willDoComboOnNextAttack = false;
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }


    private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
    {
        // Rotate manually
        if (enemyManager.canRotate && enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if(enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
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

    private void FireAmmo(EnemyManager enemy)
    {
        if(enemy.isHoldingArrow)
        {
            hasPerformedAttack = true;
            enemy.characterInventoryManager.currentItemBeingUsed = enemy.characterInventoryManager.rightWeapon;
            enemy.characterInventoryManager.rightWeapon.th_tap_RB_Action.PerformAction(enemy);
        }
    }
}
