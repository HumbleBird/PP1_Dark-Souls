using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ֺ� Ÿ�� Ž��
// Ÿ���� ã�Ҵٸ� Pursue State�� ����
// �ٽ� IdleState�� �������� ����

public class IdleState : State
{
    public PursueTargetState pursueTargetState;

    public LayerMask detectionLayer;


    public override State Tick(EnemyManager enemy)
    {
        #region Handle Enemy Target Detection
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStatsManager characterStatus = colliders[i].transform.GetComponent<CharacterStatsManager>();

            if (characterStatus != null)
            {
                if (characterStatus.teamIDNumber != enemy.enemyStatsManager.teamIDNumber)
                {
                    Vector3 TargetDirection = characterStatus.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                    if (viewableAngle > enemy.minimumDetectionAngle && viewableAngle < enemy.maximumDetectionAngle)
                    {
                        enemy.currentTarget = characterStatus;
                    }
                }
            }
        }
        #endregion

        #region Handle Switching To Next State
        if (enemy.currentTarget != null)
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
