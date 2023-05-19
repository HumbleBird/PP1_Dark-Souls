using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{
    public bool isSleeping;
    public float detectionRadius = 2;
    public string sleepAnimation;
    public string wakeAnimation;

    public LayerMask detectionLayer;

    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        if(isSleeping && enemyManager.isInteracting == false)
        {
            enemyAnimationManager.PlayerTargetAnimation(sleepAnimation, true);
        }

        #region Handle Target Detection

        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStatus characterStatus = colliders[i].transform.GetComponent<CharacterStatus>();

            if(characterStatus != null)
            {
                Vector3 targetDirection = characterStatus.transform.position - enemyManager.transform.position;
                enemyManager.viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if(enemyManager.viewableAngle > enemyManager.minimumDetectionAngle
                    && enemyManager.viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget = characterStatus;
                    isSleeping = false;
                    enemyAnimationManager.PlayerTargetAnimation(wakeAnimation, true);
                }
            }
        }

        #endregion

        #region Handle State Change
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
