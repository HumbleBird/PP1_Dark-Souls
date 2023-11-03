using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{
    public PursueTargetState pursueTargetState;

    public bool isSleeping;
    public float detectionRadius = 2;
    public string sleepAnimation;
    public string wakeAnimation;

    public LayerMask detectionLayer;


    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
        detectionLayer = 1 << 9;
    }

    public override State Tick(AICharacterManager enemy)
    {
        if(isSleeping && enemy.isInteracting == false)
        {
            enemy.aiCharacterAnimationManager.PlayTargetAnimation(sleepAnimation, true);
        }

        #region Handle Target Detection

        Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].transform.GetComponent<PlayerManager>();

            if(character != null)
            {
                Vector3 targetDirection = character.transform.position - enemy.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemy.transform.forward);

                if(viewableAngle > enemy.minimumDetectionAngle
                    && viewableAngle < enemy.maximumDetectionAngle)
                {
                    enemy.currentTarget = character;
                    isSleeping = false;
                    enemy.aiCharacterAnimationManager.PlayTargetAnimation(wakeAnimation, true);
                }
            }
        }

        #endregion

        #region Handle State Change
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
