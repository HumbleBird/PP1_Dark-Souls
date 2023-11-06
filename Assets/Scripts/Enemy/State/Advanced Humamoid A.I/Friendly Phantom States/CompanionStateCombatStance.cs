using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CompanionStateCombatStance : State
{
    public ItemBasedAttackAction[] enemyAttacks;

    CompanionStateIdle idleState;
    CompanionStateFollowHost   followHostState;
    CompanionStatePursueTarget pursueTargetState;
    CompanionStateAttackTarget attackState;

    protected bool randomDestinationSet = false;
    protected float verticalMovementValue = 0;
    protected float horizontalMovementValue = 0;

    [Header("State Flags")]
    bool willPerformBlock = false;
    bool willPerformDodge = false;
    bool willPerformParry = false;

    bool hasPerformedDodge = false;
    bool hasRandomDodgeDirection = false;
    bool hasPerformedParry = false;
    bool hasAmmoLoaded = false;

    Quaternion targetDodgeDirection;


    private void Awake()
    {
        followHostState = GetComponent<CompanionStateFollowHost>();
        pursueTargetState = GetComponent<CompanionStatePursueTarget>();
        attackState         = GetComponent<CompanionStateAttackTarget>();
        idleState = GetComponent<CompanionStateIdle>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        if(aiCharacter.currentTarget.isDead)
        {
            aiCharacter.currentTarget = null;
            return idleState;
        }

        // 동료(플레이어)로부터 멀리 떨어져 있다면 다시 플레이어에게 이동
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


        // AI가 추락중이거나 어떤 action을 취하는 중이라면 모든 움직임을 멈춤
        if (aiCharacter.isInteracting || !aiCharacter.isGrounded)
        {
            aiCharacter.animator.SetFloat("Vertical", 0);
            aiCharacter.animator.SetFloat("Horizontal", 0);
            return this;
        }


        // A.I로부터 목표물이 너무 멀리 떨어져 있다면 다시 pursue 모드로
        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        // 플레이어를 기준으로 원으로 돌면서 랜덤 공격 방식 획득
        if (!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(aiCharacter.aiCharacterAnimationManager);
        }

        if (aiCharacter.allowAIToPerformParry)
        {
            if (aiCharacter.currentTarget.canBeRiposted)
            {
                CheckForRipsote(aiCharacter);
                return this;
            }
        }

        if (aiCharacter.allowAIToPerformBlock)
        {
            RollForBlockChance(aiCharacter);
        }

        if (aiCharacter.allowAIToPerformDodge)
        {
            RollForDodgeChance(aiCharacter);
        }

        if (aiCharacter.allowAIToPerformParry)
        {
            RollForParryChance(aiCharacter);
        }

        if (aiCharacter.currentTarget.isAttacking)
        {
            if (willPerformParry && !hasPerformedParry)
            {
                ParryCurrentTarget(aiCharacter);
                return this;
            }
        }

        if (willPerformBlock)
        {
            BlockUsingOffHand(aiCharacter);
        }

        if (willPerformDodge && aiCharacter.currentTarget.isAttacking)
        {
            Dodge(aiCharacter);
        }

        HandleRotateTowardTarget(aiCharacter);

        if (aiCharacter.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            ResetStateFlags();
            return attackState;
        }
        else
        {
            GetNewAttack(aiCharacter);
        }

        HandleMovement(aiCharacter);

        return this;
    }

    private State ProcessArcherCombatSyle(AICharacterManager aiCharacter)
    {


        // AI가 추락중이거나 어떤 action을 취하는 중이라면 모든 움직임을 멈춤
        if (aiCharacter.isInteracting || !aiCharacter.isGrounded)
        {
            aiCharacter.animator.SetFloat("Vertical", 0);
            aiCharacter.animator.SetFloat("Horizontal", 0);
            return this;
        }



        // A.I로부터 목표물이 너무 멀리 떨어져 있다면 다시 pursue 모드로
        if (aiCharacter.distancefromTarget > aiCharacter.MaximumAggroRadius)
        {
            ResetStateFlags();
            return pursueTargetState;
        }

        // 플레이어를 기준으로 원으로 돌면서 랜덤 공격 방식 획득
        if (!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(aiCharacter.aiCharacterAnimationManager);
        }

        if (aiCharacter.allowAIToPerformDodge)
        {
            RollForDodgeChance(aiCharacter);
        }

        if (willPerformDodge && aiCharacter.currentTarget.isAttacking)
        {
            Dodge(aiCharacter);
        }

        HandleRotateTowardTarget(aiCharacter);

        if (!hasAmmoLoaded)
        {
            DrawArrow(aiCharacter);
            AimAtTargetBeforeFiring(aiCharacter);
        }

        if (aiCharacter.currentRecoveryTime <= 0 && hasAmmoLoaded)
        {
            ResetStateFlags();
            return attackState;
        }

        if (aiCharacter.isStationaryArcher)
        {
            aiCharacter.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            aiCharacter.animator.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
        }
        else
        {
            HandleMovement(aiCharacter);

        }


        return this;

    }

    protected void HandleRotateTowardTarget(AICharacterManager aiCharacter)
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

    protected void DecideCirclingAction(AICharacterAnimationManager enemyAnimationManager)
    {
        WalkAroundTarget();
    }

    protected void WalkAroundTarget()
    {
        verticalMovementValue = 0.5f;

        horizontalMovementValue = Random.Range(-1, 2);

        if (horizontalMovementValue <= 1 && horizontalMovementValue > 0)
        {
            horizontalMovementValue = 0.5f;
        }
        else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0)
        {
            horizontalMovementValue = -0.5f;
        }
    }

    protected virtual void GetNewAttack(AICharacterManager aiCharacter)
    {
        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

            if (aiCharacter.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && aiCharacter.distancefromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (aiCharacter.viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && aiCharacter.viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

            if (aiCharacter.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && aiCharacter.distancefromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (aiCharacter.viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && aiCharacter.viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (attackState.currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore > randomValue)
                    {
                        attackState.currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }

    // AI Rolls
    private void RollForBlockChance(AICharacterManager aiCharacter)
    {
        int Chance = Random.Range(0, 100);

        if (Chance <= aiCharacter.blockLikelyHood)
        {
            willPerformBlock = true;
        }
        else
        {
            willPerformBlock = false;
        }
    }

    private void RollForDodgeChance(AICharacterManager aiCharacter)
    {
        int Chance = Random.Range(0, 100);

        if (Chance <= aiCharacter.dodgeLikelyHood)
        {
            willPerformDodge = true;
        }
        else
        {
            willPerformDodge = false;
        }
    }

    private void RollForParryChance(AICharacterManager aiCharacter)
    {
        int Chance = Random.Range(0, 100);

        if (Chance <= aiCharacter.ParryLikelyHood)
        {
            willPerformParry = true;
        }
        else
        {
            willPerformParry = false;
        }
    }
    // combatstance에서 빠져나가면 실행함
    private void ResetStateFlags()
    {
        hasRandomDodgeDirection = false;
        hasPerformedDodge = false;
        hasPerformedParry = false;
        hasAmmoLoaded = false;

        randomDestinationSet = false;

        willPerformBlock = false;
        willPerformDodge = false;
        willPerformParry = false;
    }

    private void BlockUsingOffHand(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isBlocking == false)
        {
            if (aiCharacter.allowAIToPerformBlock)
            {
                aiCharacter.isBlocking = true;
                aiCharacter.characterEquipmentManager.currentItemBeingUsed = aiCharacter.characterEquipmentManager.m_CurrentHandLeftWeapon;
                aiCharacter.characterCombatManager.SetBlockingAbsorptionsFromBlockingWeapon();
            }
        }
    }

    private void Dodge(AICharacterManager aiCharacter)
    {
        if (!hasPerformedDodge)
        {
            if (!hasRandomDodgeDirection)
            {
                float randomDodgeDirection;

                hasRandomDodgeDirection = true;
                randomDodgeDirection = Random.Range(0, 360);
                targetDodgeDirection = Quaternion.Euler(aiCharacter.transform.eulerAngles.x, randomDodgeDirection, aiCharacter.transform.eulerAngles.z);
            }

            if (aiCharacter.transform.rotation != targetDodgeDirection)
            {
                Quaternion targetRotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetDodgeDirection, 1f);
                aiCharacter.transform.rotation = targetRotation;

                float targetYRotation = targetDodgeDirection.eulerAngles.y;
                float currentYRotation = aiCharacter.transform.eulerAngles.y;
                float rotationDifference = Mathf.Abs(targetYRotation - currentYRotation);

                if (rotationDifference <= 5)
                {
                    hasPerformedDodge = true;
                    aiCharacter.transform.rotation = targetDodgeDirection;
                    aiCharacter.characterAnimatorManager.PlayTargetAnimation("Roll_01", true);
                }
            }
        }
    }

    private void DrawArrow(AICharacterManager aiCharacter)
    {
        if (!aiCharacter.isTwoHandingWeapon)
        {
            aiCharacter.isTwoHandingWeapon = true;
            aiCharacter.characterWeaponSlotManager.LoadBothWeaponsOnSlots();
        }
        else
        {
            hasAmmoLoaded = true;
            aiCharacter.characterEquipmentManager.currentItemBeingUsed = aiCharacter.characterEquipmentManager.m_CurrentHandRightWeapon;
            aiCharacter.characterEquipmentManager.m_CurrentHandRightWeapon.th_hold_RB_Action.PerformAction(aiCharacter);
        }
    }

    private void AimAtTargetBeforeFiring(AICharacterManager aiCharacter)
    {
        float timeUntileAmmoIsShortAtTarget = Random.Range(aiCharacter.minimumTimeToAimAtTarget, aiCharacter.maximumTimeToAimAtTarget);
        aiCharacter.currentRecoveryTime = timeUntileAmmoIsShortAtTarget;
    }


    private void ParryCurrentTarget(AICharacterManager aiCharacter)
    {
        if (aiCharacter.currentTarget.canBeParryied)
        {
            if (aiCharacter.distancefromTarget <= 2)
            {
                hasPerformedParry = true;
                aiCharacter.isParrying = true;
                aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Parry_01", true);
            }
        }
    }

    private void CheckForRipsote(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isInteracting)
        {
            aiCharacter.animator.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
            aiCharacter.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return;
        }
        if (aiCharacter.distancefromTarget >= 1.0)
        {
            HandleRotateTowardTarget(aiCharacter);
            aiCharacter.animator.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
            aiCharacter.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
        }
        else
        {
            aiCharacter.isBlocking = false;

            if (!aiCharacter.isInteracting && !aiCharacter.currentTarget.isBeingRiposted && !aiCharacter.currentTarget.isBeingBackstabbed)
            {
                aiCharacter.aiCharacterRigidbody.velocity = Vector3.zero;
                aiCharacter.animator.SetFloat("Vertical", 0);
                aiCharacter.characterCombatManager.AttemptBackStabOrRiposte();
            }
        }
    }

    private void HandleMovement(AICharacterManager aiCharacter)
    {
        if (aiCharacter.distancefromTarget <= aiCharacter.stoppingDistance)
        {
            aiCharacter.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            aiCharacter.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        }
        else
        {
            aiCharacter.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            aiCharacter.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        }
    }
}