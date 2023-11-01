using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격 사정거리 체크
// 플레이어 주변을 원 형태로 걸음
// 공격 사정 거리 안에 있다면 Attack State로 변경
// 공격 쿨타임 중이라면, 이 State로 복귀 후 Circling player 함.
// 플레이어가 사정거리(공격?) 에서 벗어난다면 Pursue State로 변경 

public class CombatStanceState : State
{
    public AttackState attackState;
    public AICharacterAttackAction[] enemyAttacks;
    public PursueTargetState pursueTargetState;

    protected bool randomDestinationSet = true;
    protected float verticalMovementValue = 0;
    protected float horizontalMovementValue = 0;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(AICharacterManager enemy)
    {
        enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        attackState.hasPerformedAttack = false;

        if (enemy.isInteracting)
        {
            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);
            return this;
        }

        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if(!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(enemy.aiCharacterAnimationManager);
        }

        HandleRotateTowardTarget(enemy);

        if(enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            randomDestinationSet = false;
            return attackState;
        }
        else
        {
            GetNewAttack(enemy);
        }

        return this;
    }

    protected void HandleRotateTowardTarget(AICharacterManager enemyManager)
    {
        // Rotate manually
        if (enemyManager.isPreformingAction)
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

        // Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.aiCharacterRigidbody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.aiCharacterRigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    protected void DecideCirclingAction(AICharacterAnimationManager enemyAnimationManager)
    {
        WalkAroundTarget(enemyAnimationManager);
    }

    protected void WalkAroundTarget(AICharacterAnimationManager enemyAnimationManager)
    {
        verticalMovementValue = 0.5f;

        horizontalMovementValue = Random.Range(0, 1);

        if (horizontalMovementValue <= 1 && horizontalMovementValue > 0)
        {
            horizontalMovementValue = 0.5f;
        }
        else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0)
        {
            horizontalMovementValue = -0.5f;
        }

        Debug.Log("WalkAroundTarget " + horizontalMovementValue);

    }

    protected virtual void GetNewAttack(AICharacterManager enemy)
    {
        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            AICharacterAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemy.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemy.distancefromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (enemy.viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && enemy.viewableAngle > enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            AICharacterAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemy.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemy.distancefromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (enemy.viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && enemy.viewableAngle > enemyAttackAction.minimumAttackAngle)
                {
                    if (attackState.currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore >= randomValue)
                    {
                        attackState.currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}
