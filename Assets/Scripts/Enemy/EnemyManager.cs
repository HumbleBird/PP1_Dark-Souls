using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class EnemyManager : CharacterManager
{

    public EnemyBossManager enemyBossManager;
    public EnemyLocomotionManager enemyLocomotionManager;
    public EnemyAnimationManager enemyAnimationManager;
    public EnemyStatsManager enemyStatsManager;
    public EnemyEffectsManager enemyEffectsManager;

    public State currentState;
    public CharacterManager currentTarget;
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
    public float stoppingDistance = 1.2f; // 그들 앞에 멈추거나 앞으로 나아가기 전에 우리가 목표물에 얼마나 근접하는가

    [Header("ADVANCED A.I SETTING")]
    public bool allowAIToPerformBlock;
    public int blockLikelyHood = 50; // 0~100% 중의 확률. 100%면 매 순간 Block을, 0%면 block 하지 않음.
    public bool allowAIToPerformDodge;
    public int dodgeLikelyHood = 50;
    public bool allowAIToPerformParry;
    public int ParryLikelyHood = 50;

    [Header("A.I Combat Settings")]
    public bool allowAIToPerformCombos;
    public bool isPhaseShifting;
    public float comboLikelyHood;
    public AICombatStyle combatStyle;

    [Header("A.I Archery Setting")]
    public bool isStationaryArcher;
    public float minimumTimeToAimAtTarget = 3;
    public float maximumTimeToAimAtTarget = 6;

    [Header("A.I Target Information")]
    public float distancefromTarget;
    public Vector3 targetDirection;
    public float viewableAngle;

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

    protected override void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        base.Update();

        isRotatingWithRootMotion = animator.GetBool("isRotatingWithRootMotion");

        isPhaseShifting = animator.GetBool("isPhaseShifting");


        if (currentTarget != null)
        {
            distancefromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            targetDirection = currentTarget.transform.position - transform.position;
            viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        }
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
