using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimationManager enemyAnimationManager;
    EnemyStatus enemyStatus;

    public State currentState;
    public CharacterStatus currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;

    public bool isPreformingAction;
    public bool isInteracting;
    public float rotationSpeed = 25f;
    public float maximunAttackRange   = 1.5f;

    [Header("Combat Falgs")]
    public bool candoCombo;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;

    [Header("A.I Combat Settings")]
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyStatus = GetComponent<EnemyStatus>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;
    }

    private void Start()
    {
        enemyRigidbody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        candoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
        enemyAnimationManager.anim.SetBool("isDead", enemyStatus.isDead);
    }

    private void LateUpdate()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
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
}
