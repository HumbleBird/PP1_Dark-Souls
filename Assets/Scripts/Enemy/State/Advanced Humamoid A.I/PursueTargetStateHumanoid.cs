using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PursueTargetStateHumanoid : State
{
    public CombatStanceStateHumanoid combatStanceState;


    private void Awake()
    {
        combatStanceState = GetComponent<CombatStanceStateHumanoid>();
    }

    public override State Tick(AICharacterManager enemy)
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

    private State ProcessSwordAndShieldCombatStyle(AICharacterManager enemy) 
    {
        HandleRotateTowardTarget(enemy);

        if (enemy.isInteracting)
            return this;

        if (enemy.isPreformingAction)
        {
            enemy.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            enemy.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        if (enemy.distancefromTarget <= enemy.MaximumAggroRadius)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }

    private State ProcessArcherCombatSyle(AICharacterManager enemy) 
    {
        HandleRotateTowardTarget(enemy);

        if (enemy.isInteracting)
            return this;

        if (enemy.isPreformingAction)
        {
            enemy.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            if(!enemy.isStationaryArcher)
            {
                enemy.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

            }
        }

        if (enemy.distancefromTarget <= enemy.MaximumAggroRadius)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }

    private void HandleRotateTowardTarget(AICharacterManager enemyManager)
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
}
