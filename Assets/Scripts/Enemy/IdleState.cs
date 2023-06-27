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


    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        #region Handle Enemy Target Detection
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStatsManager characterStatus = colliders[i].transform.GetComponent<CharacterStatsManager>();

            if (characterStatus != null)
            {
                Vector3 TargetDirection = characterStatus.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget = characterStatus;
                }   
            }
        }
        #endregion

        #region Handle Switching To Next State
        if (enemyManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
        #endregion

    }
}
