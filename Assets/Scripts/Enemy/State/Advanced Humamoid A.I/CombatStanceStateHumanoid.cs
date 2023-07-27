using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CombatStanceStateHumanoid : State
{
    public AttackStateHumanoid attackState;
    public ItemBasedAttackAction[] enemyAttacks;
    public PursueTargetStateHumanoid pursueTargetState;

    protected bool randomDestinationSet = true;
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
        pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
        attackState = GetComponent<AttackStateHumanoid>();
    }

    public override State Tick(EnemyManager enemy)
    {
        if(enemy.combatStyle == AICombatStyle.swordAndShield)
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

    private State ProcessSwordAndShieldCombatStyle(EnemyManager enemy)
    {
        enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

        // AI가 추락중이거나 어떤 action을 취하는 중이라면 모든 움직임을 멈춤
        if (enemy.isInteracting || !enemy.isGrounded)
        {
            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);
            return this;
        }

        // A.I로부터 목표물이 너무 멀리 떨어져 있다면 다시 pursue 모드로
        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            return pursueTargetState;
        }

        // 플레이어를 기준으로 원으로 돌면서 랜덤 공격 방식 획득
        if (!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(enemy.enemyAnimationManager);
        }

        if (enemy.allowAIToPerformParry)
        {
            if (enemy.currentTarget.canBeRiposted)
            {
                CheckForRipsote(enemy);
                return this;
            }
        }

        if (enemy.allowAIToPerformBlock)
        {
            RollForBlockChance(enemy);
        }

        if(enemy.allowAIToPerformDodge)
        {
            RollForDodgeChance(enemy);
        }

        if(enemy.allowAIToPerformParry)
        {
            RollForParryChance(enemy);
        }

        if(enemy.currentTarget.isAttacking)
        {
            if(willPerformParry && !hasPerformedParry)
            {
                ParryCurrentTarget(enemy);
                return this;
            }
        }

        if (willPerformBlock)
        {
            BlockUsingOffHand(enemy);
        }

        if (willPerformDodge && enemy.isAttacking)
        {
            Dodge(enemy);
        }

        HandleRotateTowardTarget(enemy);

        if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            ResetStateFlags();
            return attackState;
        }
        else
        {
            GetNewAttack(enemy);
        }

        return this;
    }

    private State ProcessArcherCombatSyle(EnemyManager enemy)
    {
        enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

        // AI가 추락중이거나 어떤 action을 취하는 중이라면 모든 움직임을 멈춤
        if (enemy.isInteracting || !enemy.isGrounded)
        {
            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);
            return this;
        }

        // A.I로부터 목표물이 너무 멀리 떨어져 있다면 다시 pursue 모드로
        if (enemy.distancefromTarget > enemy.MaximumAggroRadius)
        {
            ResetStateFlags();
            return pursueTargetState;
        }

        // 플레이어를 기준으로 원으로 돌면서 랜덤 공격 방식 획득
        if (!randomDestinationSet)
        {
            randomDestinationSet = true;
            DecideCirclingAction(enemy.enemyAnimationManager);
        }

        if (enemy.allowAIToPerformDodge)
        {
            RollForDodgeChance(enemy);
        }

        if (willPerformDodge && enemy.isAttacking)
        {
            Dodge(enemy);
        }

        HandleRotateTowardTarget(enemy);

        if(!hasAmmoLoaded)
        {
            DrawArrow(enemy);
            AimAtTargetBeforeFiring(enemy);
        }

        if (enemy.currentRecoveryTime <= 0 && hasAmmoLoaded)
        {
            ResetStateFlags();
            return attackState;
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

    protected virtual void GetNewAttack(EnemyManager enemy)
    {
        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

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
            ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

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

    // AI Rolls
    private void RollForBlockChance(EnemyManager enemy)
    {
        int Chance = Random.Range(0, 100);

        if(Chance <= enemy.blockLikelyHood)
        {
            willPerformBlock = true;
        }
        else
        {
            willPerformBlock = false;
        }
    }

    private void RollForDodgeChance(EnemyManager enemy)
    {
        int Chance = Random.Range(0, 100);

        if (Chance <= enemy.dodgeLikelyHood)
        {
            willPerformDodge = true;
        }
        else
        {
            willPerformDodge = false;
        }
    }

    private void RollForParryChance(EnemyManager enemy)
    {
        int Chance = Random.Range(0, 100);

        if (Chance <= enemy.ParryLikelyHood)
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

    private void BlockUsingOffHand(EnemyManager enemy)
    {
        if(enemy.isBlocking == false)
        {
            if(enemy.allowAIToPerformBlock)
            {
                enemy.isBlocking = true;
                enemy.characterInventoryManager.currentItemBeingUsed = enemy.characterInventoryManager.leftWeapon;
                enemy.characterCombatManager.SetBlockingAbsorptionsFromBlockingWeapon();
            }
        }
    }

    private void Dodge(EnemyManager enemy)
    {
        if(!hasPerformedDodge)
        {
            if(!hasRandomDodgeDirection)
            {
                float randomDodgeDirection;

                hasRandomDodgeDirection = true;
                randomDodgeDirection = Random.Range(0, 360);
                targetDodgeDirection = Quaternion.Euler(enemy.transform.eulerAngles.x, randomDodgeDirection, enemy.transform.eulerAngles.z);
            }

            if(enemy.transform.rotation != targetDodgeDirection)
            {
                Quaternion targetRotation = Quaternion.Slerp(enemy.transform.rotation, targetDodgeDirection, 1f);
                enemy.transform.rotation = targetRotation;

                float targetYRotation = targetDodgeDirection.eulerAngles.y;
                float currentYRotation = enemy.transform.eulerAngles.y;
                float rotationDifference = Mathf.Abs(targetYRotation - currentYRotation);

                if(rotationDifference <= 5)
                {
                    hasPerformedDodge = true;
                    enemy.transform.rotation = targetDodgeDirection;
                    enemy.characterAnimatorManager.PlayTargetAnimation("Roll_01", true);
                }
            }
        }
    }

    private void DrawArrow(EnemyManager enemy)
    {
        if(!enemy.isTwoHandingWeapon)
        {
            enemy.isTwoHandingWeapon = true;
            enemy.characterWeaponSlotManager.LoadBothWeaponsOnSlots();
        }
        else
        {
            hasAmmoLoaded = true;
            enemy.characterInventoryManager.currentItemBeingUsed = enemy.characterInventoryManager.rightWeapon;
            enemy.characterInventoryManager.rightWeapon.th_hold_RB_Action.PerformAction(enemy);
        }
    }

    private void AimAtTargetBeforeFiring(EnemyManager enemy)
    {
        float timeUntileAmmoIsShortAtTarget = Random.Range(enemy.minimumTimeToAimAtTarget, enemy.maximumTimeToAimAtTarget);
        enemy.currentRecoveryTime = timeUntileAmmoIsShortAtTarget;
    }

                
    private void ParryCurrentTarget(EnemyManager enemy)
    {
        if(enemy.currentTarget.canBeParryied)
        {
            if(enemy.distancefromTarget <= 2)
            {
                hasPerformedParry = true;
                enemy.isParrying = true;
                enemy.enemyAnimationManager.PlayTargetAnimation("Parry_01", true);
            }
        }
    }

    private void CheckForRipsote(EnemyManager enemy)
    {
        if (enemy.isInteracting)
        {
            enemy.animator.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return;
        }
        if (enemy.distancefromTarget >= 1.0)
        {
            HandleRotateTowardTarget(enemy);
            enemy.animator.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
        }
        else
        {
            enemy.isBlocking = false;

            if(!enemy.isInteracting && !enemy.currentTarget.isBeingRiposted && !enemy.currentTarget.isBeingBackstabbed)
            {
                enemy.enemyRigidbody.velocity = Vector3.zero;
                enemy.animator.SetFloat("Vertical", 0);
            }
        }
    }
}
