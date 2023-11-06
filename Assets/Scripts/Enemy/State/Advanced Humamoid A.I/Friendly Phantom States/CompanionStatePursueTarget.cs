using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CompanionStatePursueTarget : State
{
    CompanionStateIdle idleState;
    CompanionStateCombatStance combatStanceState;
    CompanionStateFollowHost followHostState;

    private void Awake()
    {
        combatStanceState = GetComponent<CompanionStateCombatStance>();
        followHostState = GetComponent<CompanionStateFollowHost>();
        idleState = GetComponent<CompanionStateIdle>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        // »ç¸Á Ã¼Å©
        if (aiCharacter.currentTarget.isDead)
        {
            aiCharacter.currentTarget = null;
            return idleState;
        }

        if (aiCharacter.distanceFromCompanion > aiCharacter.maxDistanceFromCompanion)
        {
            return followHostState;
        }

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
        HandleRotateTowardTarget(aiCharacter);

        if (aiCharacter.isInteracting)
            return this;

        if (aiCharacter.isPreformingAction)
        {
            aiCharacter.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            aiCharacter.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        if (aiCharacter.distancefromTarget <= aiCharacter.MaximumAggroRadius)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }

    private State ProcessArcherCombatSyle(AICharacterManager aiCharacter)
    {


        HandleRotateTowardTarget(aiCharacter);

        if (aiCharacter.isInteracting)
            return this;

        if (aiCharacter.isPreformingAction)
        {
            aiCharacter.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            if (!aiCharacter.isStationaryArcher)
            {
                aiCharacter.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

            }
        }

        if (aiCharacter.distancefromTarget <= aiCharacter.MaximumAggroRadius)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }

    private void HandleRotateTowardTarget(AICharacterManager aiCharacter)
    {
        // Rotate manually
        if (aiCharacter.isPreformingAction)
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

        // Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(aiCharacter.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = aiCharacter.aiCharacterRigidbody.velocity;

            aiCharacter.navMeshAgent.enabled = true;
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.currentTarget.transform.position);
            aiCharacter.aiCharacterRigidbody.velocity = targetVelocity;
            aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, aiCharacter.navMeshAgent.transform.rotation, aiCharacter.rotationSpeed / Time.deltaTime);
        }
    }
}