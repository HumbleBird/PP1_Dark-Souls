using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateHumanoid : State
{
    public PursueTargetStateHumanoid pursueTargetState;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
    }

    public LayerMask detectionLayer;
    public LayerMask layerThatBlockLineOfSight;

    public override State Tick(EnemyManager aiCharacter)
    {
        // detection radius 반경 내의 적을 탐색
        Collider[] colliders = Physics.OverlapSphere(transform.position, aiCharacter.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            // 적을 찾아 냈다면 같은 팀인지 아닌지를 판별 후 다음 스텝으로
            if (targetCharacter != null)
            {
                if (targetCharacter.characterStatsManager.teamIDNumber != aiCharacter.enemyStatsManager.teamIDNumber)
                {
                    Vector3 TargetDirection = targetCharacter.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                    // 적을 찾았고, 이 객체의 시야 반경 안(앞)에 있는지
                    if (viewableAngle > aiCharacter.minimumDetectionAngle && viewableAngle < aiCharacter.maximumDetectionAngle)
                    {
                        // 적과 A.I 사이에 장애물이 있다면 current Target에 추가하지 않음.
                        if(Physics.Linecast(aiCharacter.lockOnTransform.position, targetCharacter.lockOnTransform.position, layerThatBlockLineOfSight))
                        {
                            return this;
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
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
