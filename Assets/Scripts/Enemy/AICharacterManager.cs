using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class AICharacterManager : CharacterManager
{
    public EnemyBossManager aiCharacterBossManager;
    public AICharacterLocomotionManager aiCharacterLocomotionManager;
    public AICharacterAnimationManager aiCharacterAnimationManager;
    public AICharacterStatsManager   aiCharacterStatsManager;
    public AICharacterEffectsManager aiCharacterEffectsManager;

    public State currentState;
    public CharacterManager currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody aiCharacterRigidbody;

    public bool isPreformingAction;
    public float rotationSpeed = 25f;
    public float MaximumAggroRadius   = 1.5f;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;
    public float stoppingDistance = 1.2f; // �׵� �տ� ���߰ų� ������ ���ư��� ���� �츮�� ��ǥ���� �󸶳� �����ϴ°�

    [Header("ADVANCED A.I SETTING")]
    public bool allowAIToPerformBlock;
    public int blockLikelyHood = 50; // 0~100% ���� Ȯ��. 100%�� �� ���� Block��, 0%�� block ���� ����.
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

    [Header("A.I Companion Settings")]
    public float maxDistanceFromCompanion;          // �÷��̾�� ������ �� �ִ� �ִ� �Ÿ�
    public float minimumDistanceFromCompanion;      // �÷��̾�� ������ �� �ִ� �ּ� �Ÿ�
    public float returnDistanceFromCompanion = 2;   // �÷��̾ ���ῡ�� �ʹ� ������ ������ �� ���ᰡ  �������� �Ÿ�
    public float distanceFromCompanion;
    public CharacterManager companion;

    [Header("A.I Target Information")]
    public float distancefromTarget;
    public Vector3 targetDirection;
    public float viewableAngle;

    protected override void Awake()
    {
        base.Awake();
        aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
        aiCharacterBossManager = GetComponent<EnemyBossManager>();
        aiCharacterAnimationManager = GetComponent<AICharacterAnimationManager>();
        aiCharacterRigidbody = GetComponent<Rigidbody>();
        aiCharacterStatsManager = GetComponent<AICharacterStatsManager>();
        aiCharacterEffectsManager = GetComponent<AICharacterEffectsManager>();

        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;

        currentState = GetComponentInChildren<IdleStateHumanoid>();
    }

    protected override void Start()
    {
        base.Start();
        aiCharacterRigidbody.isKinematic = false;
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

        if (companion != null)
        {
            distanceFromCompanion = Vector3.Distance(companion.transform.position, transform.position);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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
