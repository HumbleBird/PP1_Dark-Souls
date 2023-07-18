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
    public EnemyAttackAction[] enemyAttacks;
    public PursueTargetState pursueTargetState;

    protected bool randomDestinationSet = true;
    protected float verticalMovementValue = 0;
    protected float horizontalMovementValue = 0;

    public override State Tick(EnemyManager enemy)
    {
        float distancefromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);
        enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        attackState.hasPerformedAttack = false;

        if (enemy.isInteracting)
        {
            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);
            return this;
        }

        if (distancefromTarget > enemy.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        if(!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(enemy.enemyAnimationManager);
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

    protected void HandleRotateTowardTarget(EnemyManager enemyManager)
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
            Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    protected void DecideCirclingAction(EnemyAnimationManager enemyAnimationManager)
    {
        WalkAroundTarget(enemyAnimationManager);
    }

    protected void WalkAroundTarget(EnemyAnimationManager enemyAnimationManager)
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
    }

    protected virtual void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle > enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle > enemyAttackAction.minimumAttackAngle)
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
