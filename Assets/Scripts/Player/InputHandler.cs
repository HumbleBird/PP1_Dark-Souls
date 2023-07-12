using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool a_Input;
    public bool x_Input;
    public bool y_Input;


    public bool tab_rb_Input;
    public bool hold_rb_Input;
    public bool tab_rt_Input;

    public bool lb_Input;
    public bool tap_lb_Input;
    public bool tab_lt_Input;


    public bool jump_Input;
    public bool inventory_Input;
    public bool lockOnInput;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;


    public bool d_Pad_Up;
    public bool d_Pad_Down;
    public bool d_Pad_Left;
    public bool d_Pad_Right;

    public bool rollFlag;
    public bool twoHandFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public bool fireFlag;
    public bool inventoryFlag;

    public float rollInputTimer;

    public Transform criticalAttackRayCastStartPoint;

    PlayerControls inputActions;
    PlayerCombatManager playerCombatManager;
    PlayerInventoryManager playerInventoryManager;
    PlayerManager playerManager;
    PlayerEffectsManager playerEffectsManager;
    PlayerStatsManager playerStatsManager;
    BlockingCollider blockingCollider;
    PlayerWeaponSlotManager playerWeaponSlotManager;
    PlayerAnimatorManager playerAnimatorManager;
    public GameUIManager uiManager;
    CameraHandler cameraHandler;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerManager = GetComponent<PlayerManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();


        playerWeaponSlotManager = GetComponentInChildren<PlayerWeaponSlotManager>();
        blockingCollider = GetComponentInChildren<BlockingCollider>();
        uiManager = FindObjectOfType<GameUIManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
    }

    public void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput  = i.ReadValue<Vector2>();

            inputActions.PlayerActions.RB.performed += i => tab_rb_Input = true;
            inputActions.PlayerActions.HoldRB.performed += i => hold_rb_Input = true;
            inputActions.PlayerActions.HoldRB.canceled += i => hold_rb_Input = false;
            inputActions.PlayerActions.RT.performed += i => tab_rt_Input = true;

            inputActions.PlayerActions.TabLB.performed += i => tap_lb_Input = true;
            inputActions.PlayerActions.LB.performed += i => lb_Input = true;
            inputActions.PlayerActions.LB.canceled += i => lb_Input = false;
            inputActions.PlayerActions.LT.performed += i => tab_lt_Input = true;

            inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

            inputActions.PlayerActions.A.performed += i => a_Input = true;
            inputActions.PlayerActions.X.performed += i => x_Input = true;

            inputActions.PlayerActions.Roll.performed += i => b_Input = true;
            inputActions.PlayerActions.Roll.canceled += i => b_Input = false;

            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
            inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
            inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
            inputActions.PlayerActions.Y.performed += i => y_Input = true;

        }

        inputActions.Enable();
    }

    public void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        if (playerStatsManager.isDead)
            return;

        HandleMoveInput();
        HandleRollInput();

        HandleHoldRBInput();

        HandleHoldLBInput();
        HandleTapRBInput();
        HandleTapRTInput();
        HandleTapLBInput();
        HandleTapLTInput();


        HandleQuickSlotsInput();
        HandleInventoryInput();

        HandleLockOnInput();
        HandleTwoHandInput();
        HandleUseConsumableInput();

    }

    private void HandleMoveInput()
    {
        if(playerManager.isHoldingArrow)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            if(moveAmount > 0.5f)
            {
                moveAmount = 0.5f;
            }

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        else
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }


    }

    private void HandleRollInput()
    {


        if (b_Input)
        {
            rollInputTimer += Time.deltaTime;

            if(playerStatsManager.currentStamina <= 0 )
            {
                b_Input = false;
                sprintFlag = false;
            }

            if(moveAmount > 0.5f && playerStatsManager.currentStamina > 0)
            {
                sprintFlag = true;
            }
        }
        else
        {
            sprintFlag = false;


            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                rollFlag = true;
            }

            rollInputTimer = 0;
        }
    }

    private void HandleTapRBInput()
    {
        if(tab_rb_Input)
        {
            tab_rb_Input = false;

            if(playerInventoryManager.rightWeapon.tap_RB_Action != null)
            {
                playerManager.UpdateWhichHandCharacterIsUsing(true);
                playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                playerInventoryManager.rightWeapon.tap_RB_Action.PerformAction(playerManager);
            }

        }
    }

    private void HandleHoldRBInput()
    {
        if (hold_rb_Input)
        {
            if (playerInventoryManager.rightWeapon.hold_RB_Action != null)
            {
                playerManager.UpdateWhichHandCharacterIsUsing(true);
                playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                playerInventoryManager.rightWeapon.hold_RB_Action.PerformAction(playerManager);
            }
                
        }
    }


    private void HandleTapRTInput()
    {
        if (tab_rt_Input)
        {
            tab_rt_Input = false;

            if (playerInventoryManager.rightWeapon.tap_RT_Action != null)
            {
                playerManager.UpdateWhichHandCharacterIsUsing(true);
                playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                playerInventoryManager.rightWeapon.tap_RT_Action.PerformAction(playerManager);
            }


        }
    }

    private void HandleTapLBInput()
    {
        if (tap_lb_Input)
        {
            tap_lb_Input = false;

            if (playerManager.isTwoHandingWeapon)
            {
                if (playerInventoryManager.rightWeapon.tap_LB_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                    playerInventoryManager.rightWeapon.tap_LB_Action.PerformAction(playerManager);
                }
            }
            else
            {
                if (playerInventoryManager.leftWeapon.tap_LB_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.leftWeapon;
                    playerInventoryManager.leftWeapon.tap_LB_Action.PerformAction(playerManager);
                }
            }
        }
    }

    private void HandleHoldLBInput()
    {
        if (playerManager.isInAir ||
            playerManager.isSprinting ||
            playerManager.isFiringSpell)
        {
            lb_Input = false;
            return;
        }

        if (lb_Input)
        {
            if (playerManager.isTwoHandingWeapon)
            {
                if (playerInventoryManager.rightWeapon.hold_LB_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                    playerInventoryManager.rightWeapon.hold_LB_Action.PerformAction(playerManager);
                }
            }
            else
            {
                if (playerInventoryManager.leftWeapon.hold_LB_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.leftWeapon;
                    playerInventoryManager.leftWeapon.hold_LB_Action.PerformAction(playerManager);
                }
            }

        }
        else if (lb_Input == false)
        {
            if (playerManager.isAiming)
            {
                playerManager.isAiming = false;
                uiManager.crossHair.SetActive(false);
                cameraHandler.ResetAimCameraRotations();
            }

            if (blockingCollider.blockingCollider.enabled)
            {
                playerManager.isBlocking = false;
                blockingCollider.DisableBlockingCollider();
            }
        }
    }


    private void HandleTapLTInput()
    {
        if (tab_lt_Input)
        {
            tab_lt_Input = false;

            if (playerManager.isTwoHandingWeapon)
            {
                if (playerInventoryManager.rightWeapon.tap_LT_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                    playerInventoryManager.rightWeapon.tap_LT_Action.PerformAction(playerManager);
                }
            }
            else
            {
                if (playerInventoryManager.leftWeapon.tap_LT_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerInventoryManager.currentItemBeingUsed = playerInventoryManager.leftWeapon;
                    playerInventoryManager.leftWeapon.tap_LT_Action.PerformAction(playerManager);
                }
            }
        }
    }


    private void HandleQuickSlotsInput()
    {
        if(d_Pad_Right)
        {
            playerInventoryManager.ChangeRightWeapon();
        }
        else if (d_Pad_Left)
        {
            playerInventoryManager.ChangeLeftWeapon();
        }
    }

    private void HandleInventoryInput()
    {
        if(inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if(inventoryFlag)
            {
                uiManager.OpenSelectWindow();
                uiManager.UpdateUI();
                uiManager.hudWindow.SetActive(false);
            }
            else
            {
                uiManager.CloseSelectWindow();
                uiManager.CloseAllInventoryWindows();
                uiManager.hudWindow.SetActive(true);
            }
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            lockOnInput = false;
            cameraHandler.HandleLockOn();
            if(cameraHandler.m_trNearestLockOnTarget != null)
            {
                cameraHandler.m_trCurrentLockOnTarget = cameraHandler.m_trNearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }

        if(lockOnFlag && right_Stick_Right_Input)
        {
            right_Stick_Right_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.m_trleftLockTarget != null)
            {
                cameraHandler.m_trCurrentLockOnTarget = cameraHandler.m_trleftLockTarget;
            }
        }

        if(lockOnFlag && right_Stick_Left_Input)
        {
            right_Stick_Left_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.m_trRightLockTarget != null)
            {
                cameraHandler.m_trCurrentLockOnTarget = cameraHandler.m_trRightLockTarget;
            }
        }

        cameraHandler.SetCameraHeight();
    }

    private void HandleTwoHandInput()
    {
        if(y_Input)
        {
            y_Input = false;

            twoHandFlag = !twoHandFlag;

            if(twoHandFlag)
            {
                playerManager.isTwoHandingWeapon = true;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                playerWeaponSlotManager.LoadTwoHandIKTargtets(true);
            }
            else
            {
                playerManager.isTwoHandingWeapon = false;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.leftWeapon, true);
                playerWeaponSlotManager.LoadTwoHandIKTargtets(false);
            }
        }
    }



    private void HandleUseConsumableInput()
    {
        if(x_Input)
        {
            x_Input = false;
            playerInventoryManager.currentConsumable.AttemptToConsumeItem(playerAnimatorManager, playerWeaponSlotManager, playerEffectsManager);
        }
    }

}

