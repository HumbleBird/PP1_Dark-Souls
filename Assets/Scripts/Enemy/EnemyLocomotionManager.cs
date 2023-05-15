using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;

    public CharacterStatus currentTarget;
    public LayerMask detectionLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStatus characterStatus = colliders[i].transform.GetComponent<CharacterStatus>();

            if(characterStatus != null)
            {
                Vector3 TargetDirection = characterStatus.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(TargetDirection, transform.forward);

                if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    currentTarget = characterStatus;
                }
            }
        }
    }
}
