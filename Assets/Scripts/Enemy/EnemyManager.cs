using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{

    public EnemyBossManager enemyBossManager;
    public EnemyLocomotionManager enemyLocomotionManager;
    public EnemyAnimationManager enemyAnimationManager;
    public EnemyStatsManager enemyStatsManager;
    public EnemyEffectsManager enemyEffectsManager;

    public State currentState;
    public CharacterStatsManager currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;

    public bool isPreformingAction;
    public float rotationSpeed = 25f;
    public float MaximumAggroRadius   = 1.5f;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;

    [Header("A.I Combat Settings")]
    public bool allowAIToPerformCombos;
    public bool isPhaseShifting;
    public float comboLikelyHood;

    protected override void Awake()
    {
        base.Awake();
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyBossManager = GetComponent<EnemyBossManager>();
        enemyAnimationManager = GetComponent<EnemyAnimationManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyStatsManager = GetComponent<EnemyStatsManager>();
        enemyEffectsManager = GetComponent<EnemyEffectsManager>();

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

        isRotatingWithRootMotion = animator.GetBool("isRotatingWithRootMotion");
        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        canRotate = animator.GetBool("canRotate");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isPhaseShifting = animator.GetBool("isPhaseShifting");
        animator.SetBool("isDead", isDead);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        enemyEffectsManager.HandleAllBuildUpEffects();
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
            State nextState = currentState.Tick(this);

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
