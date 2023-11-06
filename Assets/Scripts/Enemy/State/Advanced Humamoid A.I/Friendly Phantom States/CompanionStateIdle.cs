using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionStateIdle : State
{
    CompanionStatePursueTarget pursueTargetState;
    CompanionStateFollowHost followHostState;

    public LayerMask detectionLayer;
    public LayerMask layerThatBlockLineOfSight;

    private void Awake()
    {
        pursueTargetState = GetComponent<CompanionStatePursueTarget>();
        followHostState = GetComponent<CompanionStateFollowHost>();
    }

    public override State Tick(AICharacterManager aiCharacter)
    {
        aiCharacter.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

        if(aiCharacter.distanceFromCompanion > aiCharacter.maxDistanceFromCompanion)
        {
            return followHostState;
        }

        // detection radius �ݰ� ���� ���� Ž��
        Collider[] colliders = Physics.OverlapSphere(transform.position, aiCharacter.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            // ���� ã�� �´ٸ� ���� ������ �ƴ����� �Ǻ� �� ���� ��������
            if (targetCharacter != null)
            {
                Vector3 TargetDirection = targetCharacter.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                // ���� ã�Ұ�, �� ��ü�� �þ� �ݰ� ��(��)�� �ִ���
                if (viewableAngle > aiCharacter.minimumDetectionAngle && viewableAngle < aiCharacter.maximumDetectionAngle)
                {
                    // ���� A.I ���̿� ��ֹ��� �ִٸ� current Target�� �߰����� ����.
                    if (Physics.Linecast(aiCharacter.lockOnTransform.position, targetCharacter.lockOnTransform.position, layerThatBlockLineOfSight))
                    {

                    }
                    else
                    {
                        if (targetCharacter.characterStatsManager.teamIDNumber != aiCharacter.aiCharacterStatsManager.teamIDNumber)
                        {
                            aiCharacter.currentTarget = targetCharacter;

                        }
                        else
                        {
                            if (aiCharacter.companion == null && targetCharacter != aiCharacter)
                                aiCharacter.companion = targetCharacter;
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