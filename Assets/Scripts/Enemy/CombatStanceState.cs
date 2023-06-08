using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����Ÿ� üũ
// �÷��̾� �ֺ��� �� ���·� ����
// ���� ���� �Ÿ� �ȿ� �ִٸ� Attack State�� ����
// ���� ��Ÿ�� ���̶��, �� State�� ���� �� Circling player ��.
// �÷��̾ �����Ÿ�(����?) ���� ����ٸ� Pursue State�� ���� 

public class CombatStanceState : State
{
    public AttackState attackState;
    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        if (enemyManager.isInteracting)
            return this;

        float distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        HandleRotateTowardTarget(enemyManager);

        if (enemyManager.isPreformingAction)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

        if(enemyManager.currentRecoveryTime <= 0 && distancefromTarget <= enemyManager.maximunAttackRange)
        {
            return attackState;
        }
        else if (distancefromTarget > enemyManager.maximunAttackRange)
        {
            return pursueTargetState;
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
