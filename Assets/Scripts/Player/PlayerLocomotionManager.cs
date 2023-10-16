using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

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

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
        
    }

    protected override void Start()
    {
        base.Start();
        player.isGrounded = true;
    }

    #region Movement


    public void HandleRotation()
    {

        if (player.canRotate)
        {
            if(player.isAiming)
            {
                Quaternion targetRotation = Quaternion.Euler(0, player.cameraHandler.cameraTransform.eulerAngles.y, 0);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
            else
            {
                if (player.inputHandler.lockOnFlag)
                {
                    if (player.isSprinting|| player.inputHandler.rollFlag)
                    {
                        Vector3 targetDir = Vector3.zero;

                        targetDir = player.cameraHandler.cameraTransform.forward * player.inputHandler.vertical;
                        targetDir += player.cameraHandler.cameraTransform.right * player.inputHandler.horizontal;
                        targetDir.Normalize();
                        targetDir.y = 0;

                        if (targetDir == Vector3.zero)
                            targetDir = transform.forward;

                        float rs = rotationSpeed;

                        Quaternion tr = Quaternion.LookRotation(targetDir);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDirection = moveDirection;

                        rotationDirection = player.cameraHandler.m_trCurrentLockOnTarget.transform.position - transform.position;
                        rotationDirection.y = 0;
                        rotationDirection.Normalize();

                        float rs = rotationSpeed;

                        Quaternion tr = Quaternion.LookRotation(rotationDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                }
                else
                {
                    Vector3 targetDir = Vector3.zero;

                    targetDir = player.cameraHandler.cameraObject.transform.forward * player.inputHandler.vertical;
                    targetDir += player.cameraHandler.cameraObject.transform.right * player.inputHandler.horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                        targetDir = transform.forward;

                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

                    transform.rotation = targetRotation;
                }
            }
        }

    }

    public void HandleGroundedMovement()
    {
        if (player.inputHandler.rollFlag)
            return;

        if (player.isInteracting)
            return;

        moveDirection = player.cameraHandler.transform.forward * player.inputHandler.vertical;
        moveDirection = moveDirection + player.cameraHandler.transform.right * player.inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        // Two Hand Animation Rig
        if (player.inputHandler.twoHandFlag)
        {
            if (player.isSprinting && player.inputHandler.moveAmount > 0.1f)
            {
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(true);
            }
            else
            {
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(false);

            }
        }

        // Player Movement
        if (player.isSprinting && player.inputHandler.moveAmount > 0.5f)
        {
            player.characterController.Move(moveDirection * sprintSpeed * Time.deltaTime);
            player.playerStatsManager.DeductSprintingStamina(sprintStaminaCost);
        }
        else
        {
            if(player.inputHandler.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
            else if (player.inputHandler.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        // Animator Parameters Value Change
        if(player.inputHandler.lockOnFlag && player.isSprinting == false)
        {
            player.playerAnimatorManager.UpdateAnimatorValues(player.inputHandler.moveAmount, player.inputHandler.horizontal, player.isSprinting);
        }
        else
        {
            player.playerAnimatorManager.UpdateAnimatorValues(player.inputHandler.moveAmount, 0, player.isSprinting);
        }
    }

    public void HandleRollingAndSprinting()
    {
        if (player.playerStatsManager.currentStamina <= 0)
        {
            player.inputHandler.rollFlag = false;
            return;
        }

        if (player.inputHandler.rollFlag)
        {
            player.inputHandler.rollFlag = false;

            if (!player.canRoll)
                return;

            moveDirection = player.cameraHandler.cameraObject.transform.forward * player.inputHandler.vertical;
            moveDirection += player.cameraHandler.cameraObject.transform.right * player.inputHandler.horizontal;

            if (player.inputHandler.moveAmount > 0)
            {
                switch (player.playerStatsManager.encumbranceLevel)
                {
                    case Define.EncumbranceLevel.Light:
                        player.playerAnimatorManager.PlayTargetAnimation("Roll_01", true);
                        break;
                    case Define.EncumbranceLevel.Medium:
                        player.playerAnimatorManager.PlayTargetAnimation("Roll_01", true);
                        break;
                    case Define.EncumbranceLevel.Heavy:
                        player.playerAnimatorManager.PlayTargetAnimation("Heavy_Roll_01", true);
                        break;
                    case Define.EncumbranceLevel.Overloaded:
                        player.playerAnimatorManager.PlayTargetAnimation("Heavy_Roll_01", true);
                        break;
                    default:
                        break;
                }

                player.playerAnimatorManager.EraseHandIKForWeapon();
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = rollRotation;
                player.playerStatsManager.DeductStamina(rollStaminaCost);
            }
            else
            {
                switch (player.playerStatsManager.encumbranceLevel)
                {
                    case Define.EncumbranceLevel.Light:
                        player.playerAnimatorManager.PlayTargetAnimation("BackStep_01", true);
                        break;
                    case Define.EncumbranceLevel.Medium:
                        player.playerAnimatorManager.PlayTargetAnimation("BackStep_01", true);
                        break;
                    case Define.EncumbranceLevel.Heavy:
                        player.playerAnimatorManager.PlayTargetAnimation("Heavy_BackStep_01", true);
                        break;
                    case Define.EncumbranceLevel.Overloaded:
                        player.playerAnimatorManager.PlayTargetAnimation("Heavy_BackStep_01", true);
                        break;
                    default:
                        break;
                }

                player.playerAnimatorManager.EraseHandIKForWeapon();
                player.playerStatsManager.DeductStamina(backstepStaminaCost);

            }

        }
    }

    public void HandleJumping()
    {
        if (player.isInteracting)
            return;


        if (player.playerStatsManager.currentStamina <= 0)
            return;

        if (player.inputHandler.jump_Input)
        {
            player.inputHandler.jump_Input = false;

            if(player.inputHandler.moveAmount > 0)
            {
                moveDirection = player.cameraHandler.cameraObject.transform.forward * player.inputHandler.vertical;
                moveDirection += player.cameraHandler.cameraObject.transform.right * player.inputHandler.horizontal;
                player.playerAnimatorManager.PlayTargetAnimation("Jump", true);
                player.playerAnimatorManager.EraseHandIKForWeapon();
                moveDirection.y = 0;
                Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = jumpRotation;
            }
        }
    }

    #endregion
}
