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
    public AICharacterAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    private void Awake()
    {
        rotateTowardsTargetState = GetComponent<RotateTowardsTargetState>();
        combatStanceState = GetComponent<CombatStanceState>();
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        //FlagCheck(aiCharacter);

        float distancefromTarget = Vector3.Distance(aiCharacter.currentTarget.transform.position, aiCharacter.transform.position);

        RotateTowardsTargetWhilstAttacking(aiCharacter);

        if (distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if(willDoComboOnNextAttack && aiCharacter.canDoCombo)
        {
            // Attack with combo
            AttackTargetWithCombo(aiCharacter);

            // set cool down time
        }

        if (!hasPerformedAttack && currentAttack != null)
        {
            //Attack
            AttackTarget(aiCharacter);

            // Roll for a combo change
            RollForComboChance(aiCharacter);
        }

        if(willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this; // goes back up to perform the combo
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(AICharacterManager enemy)
    {
        enemy.isUsingRightHand = currentAttack.isRightHandedAction;
        enemy.isUsingLeftHand = !currentAttack.isRightHandedAction;
        enemy.aiCharacterAnimationManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemy.aiCharacterAnimationManager.PlayWeaponTrailFX();
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(AICharacterManager enemy)
    {
        enemy.isUsingRightHand = currentAttack.isRightHandedAction;
        enemy.isUsingLeftHand = !currentAttack.isRightHandedAction;
        willDoComboOnNextAttack = false;
        enemy.aiCharacterAnimationManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemy.aiCharacterAnimationManager.PlayWeaponTrailFX();
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }


    private void RotateTowardsTargetWhilstAttacking(AICharacterManager enemyManager)
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

    private void RollForComboChance(AICharacterManager enemyManager)
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

    void FlagCheck(AICharacterManager aiCharacter)
    {
        if (willDoComboOnNextAttack && hasPerformedAttack && aiCharacter.canDoCombo == false)
        {
            willDoComboOnNextAttack = false;
            hasPerformedAttack = false;
        }
    }
}
