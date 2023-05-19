using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표물을 쫓음
// 공격 거리 안에 든다면 Combat State로 변경
// 타겟이 사정 거리에서 벗어난다면 다시 이 상태로 변경후 목표물 쫓음

public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        if (enemyManager.isPreformingAction)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        if (distancefromTarget > enemyManager.maximunAttackRange)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        HandleRotateTowardTarget(enemyManager);

        enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

        if(distancefromTarget<= enemyManager.maximunAttackRange)
        {
            return combatStanceState;
        }
        else
        {
            return this;

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
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }

        // Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}
