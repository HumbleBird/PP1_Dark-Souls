using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimationManager enemyAnimationManager;
    public Rigidbody enemyRigidbody;

    public LayerMask detectionLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        enemyRigidbody.isKinematic = false;
    }

}
