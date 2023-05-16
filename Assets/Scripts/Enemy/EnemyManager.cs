using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimationManager enemyAnimationManager;
    EnemyStatus enemyStatus;

    public State currentState;
    public CharacterStatus currentTarget;

    public bool isPreformingAction;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        enemyStatus = GetComponent<EnemyStatus>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStatus, enemyAnimationManager);

            if(nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }

    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPreformingAction)
        {
            if(currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }
        }    
    }

    #region Attack

    private void AttackTarget()
    {
        //if (isPreformingAction)
        //    return;

        //if(currentAttack == null)
        //{
        //    GetNewAttack();
        //}
        //else
        //{
        //    isPreformingAction = true;
        //    currentRecoveryTime = currentAttack.recoveryTime;
        //    enemyAnimationManager.PlayerTargetAnimation(currentAttack.actionAnimation, true);
        //    currentAttack = null;
        //}
    }

    private void GetNewAttack()
    {
        //Vector3 targetDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
        //float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        //enemyLocomotionManager.distancefromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

        //int maxScore = 0;

        //for (int i = 0; i < enemyAttacks.Length; i++)
        //{
        //    EnemyAttackAction enemyAttackAction = enemyAttacks[i];

        //    if(enemyLocomotionManager.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
        //        && enemyLocomotionManager.distancefromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
        //    {
        //        if(viewableAngle <= enemyAttackAction.maximumAttackAngle
        //            && viewableAngle > enemyAttackAction.minimumAttackAngle)
        //        {
        //            maxScore += enemyAttackAction.attackScore;
        //        }
        //    }
        //}

        //int randomValue = Random.Range(0, maxScore);
        //int temporaryScore = 0;

        //for (int i = 0; i < enemyAttacks.Length; i++)
        //{
        //    EnemyAttackAction enemyAttackAction = enemyAttacks[i];

        //    if (enemyLocomotionManager.distancefromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
        //        && enemyLocomotionManager.distancefromTarget > enemyAttackAction.minimumDistanceNeededToAttack)
        //    {
        //        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
        //            && viewableAngle > enemyAttackAction.minimumAttackAngle)
        //        {
        //            if (currentAttack != null)
        //                return;

        //            temporaryScore += enemyAttackAction.attackScore;

        //            if(temporaryScore >= randomValue)
        //            {
        //                currentAttack = enemyAttackAction;
        //            }
        //        }
        //    }
        //}

    }

    #endregion
}
