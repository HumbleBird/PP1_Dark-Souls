using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ھ� ������ ���� ���ݵ� �� �ϳ� ����
// ������ ������ ����, �Ÿ� ���� ������ �ִٸ� ���Ӱ� �ٽ� ����
// ������ ���� �����ϴٸ�, �������� �������� ���߰ų�, Ÿ���� �����Ѵ�.
// ���� ��Ÿ�� üũ 
// combat State�� ����

public class AttackState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public CombatStanceState combatStanceState;
    public PursueTargetState pursueTargetState;
    public EnemyAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStatsManager, EnemyAnimationManager enemyAnimationManager)
    {
        float distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        RotateTowardsTargetWhilstAttacking(enemyManager);

        if (distancefromTarget > enemyManager.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if(willDoComboOnNextAttack && enemyManager.canDoCombo)
        {
            // Attack with combo
            AttackTargetWithCombo(enemyAnimationManager, enemyManager);

            // set cool down time
        }

        if (!hasPerformedAttack)
        {
            //Attack
            AttackTarget(enemyAnimationManager, enemyManager);

            // Roll for a combo change
            RollForComboChance(enemyManager);
        }

        if(willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this; // goes back up to perform the combo
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyAnimationManager enemyAnimationManager, EnemyManager enemyManager)
    {
        enemyAnimationManager.PlayerTargetAnimation(currentAttack.actionAnimation, true);
        enemyAnimationManager.PlayWeaponTrailFX();
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(EnemyAnimationManager enemyAnimationManager, EnemyManager enemyManager)
    {
        willDoComboOnNextAttack = false;
        enemyAnimationManager.PlayerTargetAnimation(currentAttack.actionAnimation, true);
        enemyAnimationManager.PlayWeaponTrailFX();
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
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
            if(currentAttack.comboAction != null)
            {
                willDoComboOnNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else
            {
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }

        }
    }
}
