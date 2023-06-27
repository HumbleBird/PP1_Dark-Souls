using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    CameraHandler cameraHandler;
    PlayerManager playerManager;
    PlayerStatsManager playerStatsManager;
    Transform cameraObject;
    InputHandler inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public PlayerAnimatorManager playerAnimatorManager;

    public new Rigidbody rigidbody;
    //public GameObject normalCamera;

    [Header("Ground & Air Ditections States")]
    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField]
    float groundDirectionRayDistance = 0.2f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float walkingSpeed = 3;
    [SerializeField]
    float sprintSpeed = 7;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float fallingSpeed = 70;

    [Header("Stamina Costs")]
    [SerializeField]
    int rollStaminaCost = 15;
    int backstepStaminaCost = 12;
    int sprintStaminaCost = 1;

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;

    Vector3 normalVector;
    Vector3 targetPosition;

    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
        playerManager = GetComponent<PlayerManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        
    }

    private void Start()
    {

        cameraObject = Camera.main.transform;
        myTransform = transform;
        playerAnimatorManager.Initialize();

        playerManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 10);
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }

    #region Movement


    public void HandleRotation(float delta)
    {

        if (playerAnimatorManager.canRotate)
        {

            if (inputHandler.lockOnFlag)
            {
                if (inputHandler.sprintFlag || inputHandler.rollFlag)
                {
                    Vector3 targetDir = Vector3.zero;

                    targetDir = cameraHandler.cameraTransform.forward * inputHandler.vertical;
                    targetDir += cameraHandler.cameraTransform.right * inputHandler.horizontal;
                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                        targetDir = transform.forward;

                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

                    myTransform.rotation = targetRotation;
                }
                else
                {
                    Vector3 rotationDirection = moveDirection;

                    rotationDirection = cameraHandler.m_trCurrentLockOnTarget.transform.position - transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();

                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

                    myTransform.rotation = targetRotation;
                }
            }
            else
            {
                Vector3 targetDir = Vector3.zero;

                targetDir = cameraObject.forward * inputHandler.vertical;
                targetDir += cameraObject.right * inputHandler.horizontal;

                targetDir.Normalize();
                targetDir.y = 0;

                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;

                float rs = rotationSpeed;

                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);

                myTransform.rotation = targetRotation;
            }
        }

    }

    public void HandleMovement(float delta)
    {
        if (inputHandler.rollFlag)
            return;

        if (playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;

        if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
        {
            speed = sprintSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
            playerStatsManager.TakeStaminsDamage(sprintStaminaCost);
        }
        else
        {
            if(inputHandler.moveAmount < 0.5)
            {
                moveDirection *= walkingSpeed;
                playerManager.isSprinting = false;
            }
            else
            {
                moveDirection *= speed;
                playerManager.isSprinting = false;

            }
        }

        Vector3 projecteedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projecteedVelocity;

        if(inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
        {
            playerAnimatorManager.UpdateAnimatorValues(inputHandler.moveAmount, inputHandler.horizontal, playerManager.isSprinting);
        }
        else
        {
            playerAnimatorManager.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);
        }


    }

    public void HandleRollingAndSprinting(float delta)
    {
        if (playerAnimatorManager.animator.GetBool("isInteracting"))
            return;

        if (playerStatsManager.currentStamina <= 0)
            return; 

        if (inputHandler.rollFlag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if (inputHandler.moveAmount > 0)
            {
                playerAnimatorManager.PlayerTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
                playerStatsManager.TakeStaminsDamage(rollStaminaCost);
            }
            else
            {
                playerAnimatorManager.PlayerTargetAnimation("BackStep", true);
                playerStatsManager.TakeStaminsDamage(backstepStaminaCost);

            }

        }
    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if (playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up * fallingSpeed);
            rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if (playerManager.isInAir)
            {
                if(inAirTimer > 0.5)
                {
                    Debug.Log("YOu were in the air for " + inAirTimer);
                    playerAnimatorManager.PlayerTargetAnimation("Land", true);
                    inAirTimer = 0;
                }
                else
                {
                    playerAnimatorManager.PlayerTargetAnimation("Empty", false);
                    inAirTimer = 0;
                }

                playerManager.isInAir = false;
            }
        }
        else
        {
            if(playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if(playerManager.isInAir == false)
            {
                if(playerManager.isInteracting == false)
                {
                    playerAnimatorManager.PlayerTargetAnimation("Falling", true);
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
        }

        //if(playerManager.isGrounded)
        {
            if (playerManager.isInteracting || inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            }
            else
            {
                myTransform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if (playerManager.isInteracting)
            return;


        if (playerStatsManager.currentStamina <= 0)
            return;

        if (inputHandler.jump_Input)
        {
            if(inputHandler.moveAmount > 0 )
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;
                playerAnimatorManager.PlayerTargetAnimation("Jump", true);
                moveDirection.y = 0;
                Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = jumpRotation;
            }
        }
    }

    #endregion
}
