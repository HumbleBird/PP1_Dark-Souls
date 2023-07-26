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
        // detection radius �ݰ� ���� ���� Ž��
        Collider[] colliders = Physics.OverlapSphere(transform.position, aiCharacter.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            // ���� ã�� �´ٸ� ���� ������ �ƴ����� �Ǻ� �� ���� ��������
            if (targetCharacter != null)
            {
                if (targetCharacter.characterStatsManager.teamIDNumber != aiCharacter.enemyStatsManager.teamIDNumber)
                {
                    Vector3 TargetDirection = targetCharacter.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                    // ���� ã�Ұ�, �� ��ü�� �þ� �ݰ� ��(��)�� �ִ���
                    if (viewableAngle > aiCharacter.minimumDetectionAngle && viewableAngle < aiCharacter.maximumDetectionAngle)
                    {
                        // ���� A.I ���̿� ��ֹ��� �ִٸ� current Target�� �߰����� ����.
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
