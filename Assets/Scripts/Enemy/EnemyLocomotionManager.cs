using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimationManager enemyAnimationManager;
    NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;

    public LayerMask detectionLayer;

    public float distancefromTarget;
    public float stoppingdistance = 1f;

    public float rotationSpeed = 25f;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
    }



    public void HandleMoveToTarget()
    {
        if (enemyManager.isPreformingAction)
            return;

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if(enemyManager.isPreformingAction)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }
        else
        {
            if(distancefromTarget > stoppingdistance)
            {
                enemyAnimationManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            else if (distancefromTarget <= stoppingdistance)
            {
                enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardTarget()
    {
        // Rotate manually
        if(enemyManager.isPreformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward; 
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }

        // Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidbody.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyRigidbody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }
}
