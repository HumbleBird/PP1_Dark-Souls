using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주변 타겟 탐색
// 타겟을 찾았다면 Pursue State로 변경
// 다시 IdleState로 변경하지 않음

public class IdleState : State
{
    public PursueTargetState pursueTargetState;

    public LayerMask detectionLayer;
    public LayerMask layerThatBlockLineOfSight;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
        detectionLayer = 1 << 9;
    }

    public override State Tick(AICharacterManager aiCharacter)
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
                        if (Physics.Linecast(aiCharacter.lockOnTransform.position, targetCharacter.lockOnTransform.position, layerThatBlockLineOfSight))
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
