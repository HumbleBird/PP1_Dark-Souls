using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스코어 점수에 따른 공격들 중 하나 선택
// 선택한 공격이 각도, 거리 상의 문제가 있다면 새롭게 다시 선택
// 공격이 변형 가능하다면, 공격자의 움직임을 멈추거나, 타겟을 공격한다.
// 공격 쿨타임 체크 
// combat State로 복귀

public class AttackState : State
{
    public CombatStanceState combatStanceState;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    bool isComboing = false;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        if (enemyManager.isInteracting && enemyManager.candoCombo == false)
        {
            return this;

        }
        else if (enemyManager.isInteracting && enemyManager.candoCombo)
        {
            if (isComboing)
            {
                enemyAnimationManager.PlayerTargetAnimation(currentAttack.actionAnimation, true);
                isComboing = false;
            }
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        HandleRotateTowardTarget(enemyManager);

        if (enemyManager.isPreformingAction)
        {
            return combatStanceState;
        }

        if (currentAttack != null)
        {
            // 현재 공격의 최소 사정거리보다 적이 더 가깝다면 새로운 공격을 선택함.
            if(distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }
            // 충분히 접근 했다면 공격 수행
            else if (distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                // 적이 시야 각도 안에 있다면 공격
                if(viewableAngle <= currentAttack.maximumAttackAngle &&
                   viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if(enemyManager.currentRecoveryTime <= 0 && enemyManager.isPreformingAction == false)
                    {
                        enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimationManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                        enemyAnimationManager.PlayerTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPreformingAction = true;

                        if(currentAttack.canCombo)
                        {
                            currentAttack = currentAttack.comboAction;
                            return this;
                        }
                        else
                        {
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }

                    }
                }
            }

        }

        else
        {
            GetNewAttack(enemyManager);
        }

        return combatStanceState;
    }

    private void GetNewAttack(EnemyManager enemyManager)
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
                    if (currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore >= randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }

    private void HandleRotateTowardTarget(EnemyManager enemyManager)
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
}
