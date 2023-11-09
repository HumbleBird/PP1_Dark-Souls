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
    public AICharacterSoundFXManager aICharacterSoundFXManager;

    public State currentState;
    public State m_InitState;
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
    public float stoppingDistance = 1.2f; // 그들 앞에 멈추거나 앞으로 나아가기 전에 우리가 목표물에 얼마나 근접하는가
    public E_InitAICharacterState e_InitAICharacterState;

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

    [Header("A.I Companion Settings")]
    public float maxDistanceFromCompanion;          // 플레이어와 떨어질 수 있는 최대 거리
    public float minimumDistanceFromCompanion;      // 플레이어와 떨어질 수 있는 최소 거리
    public float returnDistanceFromCompanion = 2;   // 플레이어가 동료에게 너무 가깝게 접근할 때 동료가  떨어지는 거리
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
        aICharacterSoundFXManager = GetComponent<AICharacterSoundFXManager>();

        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;

        switch (e_InitAICharacterState)
        {
            case E_InitAICharacterState.Idle:
                {
                    IdleStateHumanoid state = GetComponentInChildren<IdleStateHumanoid>();
                    if (state != null)
                        currentState = state;
                }
                {
                    IdleState state = GetComponentInChildren<IdleState>();
                    if (state != null)
                        currentState = state;
                }
                {
                    CompanionStateIdle state = GetComponentInChildren<CompanionStateIdle>();
                    if (state != null)
                        currentState = state;
                }
                break;
            case E_InitAICharacterState.Patrol:
                currentState = GetComponentInChildren<PatrolStateHumanoid>();
                break;
            case E_InitAICharacterState.Ambush:
                currentState = GetComponentInChildren<AmbushState>();
                ((AmbushState)currentState).isSleeping = true;
                break;
            default:
                break;
        }
    }

    protected override void Start()
    {
        base.Start();

        m_StartPos = transform.position;
        m_StartRo = transform.eulerAngles;

        aiCharacterRigidbody.isKinematic = false;

        m_InitState = currentState;
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
        if (isDead == true)
            return;

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

    public override void InitCharacterManager()
    {
        base.InitCharacterManager();

        gameObject.SetActive(true);

        // 공격 시간 초기화
        currentRecoveryTime = 0;

        // 상태 초기화
        currentState = m_InitState;

        // 몬스터의 현재 타깃 초기화
        currentTarget = null;

        if (aiCharacterStatsManager.isBoss)
            aiCharacterBossManager.Clear();

        companion = null;
    }

    public override void Dead()
    {
        base.Dead();

        Managers.Game.PlayerLockOnCheck();

        StartCoroutine(IDeadEffect());
    }

    // 추후 스르륵 사라지게
    IEnumerator IDeadEffect()
    {
        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);
    }
}
