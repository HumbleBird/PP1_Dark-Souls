using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateHumanoid : State
{
    public PursueTargetStateHumanoid pursueTargetState;

    public LayerMask detectionLayer;
    public LayerMask layerThatBlockLineOfSight;

    public bool patrolComplete;
    public bool repeatPatrol;

    [Header("Patrol Rest Time")]
    public float endOfPatrolRestTime;
    public float endOfPatrolTimer;

    [Header("Patrol Position")]
    public int patrolDestinationIndex;
    public bool hasPatrolDestination;
    public Transform currentPatrolDestination;
    public float distanceFromCurrentPatrolPoint;
    public List<Transform> listOfPatrolDestinations = new List<Transform>();

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        SearchForTargetWhilstPatroling(aiCharacter);

        if (aiCharacter.isInteracting)
        {
            aiCharacter.animator.SetFloat("Vertical", 0);
            aiCharacter.animator.SetFloat("Horizontal", 0);
            return this;
        }

        if(aiCharacter.currentTarget != null)
        {
            return pursueTargetState;
        }

        // 한 사이클의 경로를 완수했고, 다시 Patrol을 원한다면 실행
        if(patrolComplete && repeatPatrol)
        {
            // 다시 시작 시간을 카운트, patrol flags all reset
            if(endOfPatrolRestTime > endOfPatrolTimer)
            {
                aiCharacter.animator.SetFloat("Vertical", 0f, 0.2f, Time.deltaTime);
                endOfPatrolTimer += Time.deltaTime;
                return this;
            }
            else if (endOfPatrolTimer >= endOfPatrolRestTime)
            {
                patrolDestinationIndex = -1;
                hasPatrolDestination = false;
                currentPatrolDestination = null;
                patrolComplete = false;
                endOfPatrolTimer = 0;
            }
        }

        else if (patrolComplete && !repeatPatrol)
        {
            aiCharacter.navMeshAgent.enabled = false;
            aiCharacter.animator.SetFloat("Vertical", 0f, 0.2f, Time.deltaTime);
            return this;
        }

        if (hasPatrolDestination)
        {
            if (currentPatrolDestination != null)
            {
                distanceFromCurrentPatrolPoint = Vector3.Distance(aiCharacter.transform.position, currentPatrolDestination.transform.position);

                if (distanceFromCurrentPatrolPoint > 1)
                {
                    aiCharacter.navMeshAgent.enabled = true;
                    aiCharacter.navMeshAgent.destination = currentPatrolDestination.transform.position;
                    Quaternion targetRotation = Quaternion.Lerp(aiCharacter.transform.rotation, aiCharacter.navMeshAgent.transform.rotation, 0.5f);
                    aiCharacter.transform.rotation = targetRotation;
                    aiCharacter.animator.SetFloat("Vertical", 0.5f, 0.2f, Time.deltaTime);
                }
                else
                {
                    currentPatrolDestination = null;
                    hasPatrolDestination = false;
                }
            }
        }

        if(!hasPatrolDestination)
        {
            patrolDestinationIndex += 1;
            
            if(patrolDestinationIndex > listOfPatrolDestinations.Count - 1)
            {
                patrolComplete = true;
                return this;
            }

            currentPatrolDestination = listOfPatrolDestinations[patrolDestinationIndex];
            hasPatrolDestination = true;
        }

        return this;
    }

    private void SearchForTargetWhilstPatroling(AICharacterManager aiCharacter)
    {
        // detection radius 반경 내의 적을 탐색
        Collider[] colliders = Physics.OverlapSphere(transform.position, aiCharacter.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            // 적을 찾아 냈다면 같은 팀인지 아닌지를 판별 후 다음 스텝으로
            if (targetCharacter != null)
            {
                if (targetCharacter.characterStatsManager.teamIDNumber != aiCharacter.aiCharacterStatsManager.teamIDNumber)
                {
                    Vector3 TargetDirection = targetCharacter.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                    // 적을 찾았고, 이 객체의 시야 반경 안(앞)에 있는지
                    if (viewableAngle > aiCharacter.minimumDetectionAngle && viewableAngle < aiCharacter.maximumDetectionAngle)
                    {
                        // 적과 A.I 사이에 장애물이 있다면 current Target에 추가하지 않음.
                        if (Physics.Linecast(aiCharacter.lockOnTransform.position, targetCharacter.lockOnTransform.position, layerThatBlockLineOfSight))
                        {
                            return;
                        }
                        else
                        {
                            aiCharacter.currentTarget = targetCharacter;
                        }
                    }
                }
            }
        }

        if (aiCharacter.currentTarget != null)
        {
            return;
        }
        else
        {
            return;
        }
    }
}
