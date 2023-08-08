using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class InputHandler : MonoBehaviour
{
    PlayerControls inputActions;
    PlayerManager player;

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
    public bool hold_rt_Input;

    public bool hold_lb_Input;
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

    public bool input_Has_Been_Qued;
    public float current_Qued_Input_Timer;
    public float default_Qued_Input_Time;
    public bool qued_RB_Input;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
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
            inputActions.PlayerActions.HoldRT.performed += i => hold_rt_Input = true;
            inputActions.PlayerActions.HoldRT.canceled += i => hold_rt_Input = false;

            inputActions.PlayerActions.TabLB.performed += i => tap_lb_Input = true;
            inputActions.PlayerActions.LB.performed += i => hold_lb_Input = true;
            inputActions.PlayerActions.LB.canceled += i => hold_lb_Input = false;
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

            inputActions.PlayerActions.QuedRB.performed += i => QueInput(ref qued_RB_Input);
        }

        inputActions.Enable();
    }

    public void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput()
    {
        if (player.isDead)
            return;

        HandleMoveInput();
        HandleRollInput();

        HandleTapRBInput();
        HandleHoldRBInput();
        HandleHoladRTInput();

        HandleTapRTInput();
        HandleTapLTInput();

        HandleTapLBInput();
        HandleHoldLBInput();

        HandleQuickSlotsInput();
        HandleInventoryInput();

        HandleLockOnInput();
        HandleTwoHandInput();
        HandleUseConsumableInput();
        HandleQuedInput();
    }

    private void HandleMoveInput()
    {
        if(player.isHoldingArrow)
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

            if(player.playerStatsManager.currentStamina <= 0 )
            {
                b_Input = false;
                sprintFlag = false;
            }

            if(moveAmount > 0.5f && player.playerStatsManager.currentStamina > 0)
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

            if(player.playerInventoryManager.rightWeapon.oh_tap_RB_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                player.playerInventoryManager.rightWeapon.oh_tap_RB_Action.PerformAction(player);
            }

        }
    }

    private void HandleHoldRBInput()
    {
        if (hold_rb_Input)
        {
            if (player.playerInventoryManager.rightWeapon.oh_hold_RB_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                player.playerInventoryManager.rightWeapon.oh_hold_RB_Action.PerformAction(player);
            }
                
        }
    }

    private void HandleHoladRTInput()
    {
        player.animator.SetBool("isChargingAttack", hold_rt_Input);

        if(hold_rt_Input)
        {
            player.UpdateWhichHandCharacterIsUsing(true);
            player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;

            if(player.isTwoHandingWeapon)
            {
                if (player.playerInventoryManager.rightWeapon.th_hold_RT_Action != null)
                {
                    player.playerInventoryManager.rightWeapon.th_hold_RT_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerInventoryManager.rightWeapon.oh_hold_RT_Action != null)
                {
                    player.playerInventoryManager.rightWeapon.oh_hold_RT_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleTapRTInput()
    {
        if (tab_rt_Input)
        {
            tab_rt_Input = false;

            if (player.playerInventoryManager.rightWeapon.oh_tap_RT_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                player.playerInventoryManager.rightWeapon.oh_tap_RT_Action.PerformAction(player);
            }


        }
    }

    private void HandleTapLBInput()
    {
        if (tap_lb_Input)
        {
            tap_lb_Input = false;

            if (player.isTwoHandingWeapon)
            {
                if (player.playerInventoryManager.rightWeapon.oh_tap_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                    player.playerInventoryManager.rightWeapon.oh_tap_LB_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerInventoryManager.leftWeapon.oh_tap_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.leftWeapon;
                    player.playerInventoryManager.leftWeapon.oh_tap_LB_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleHoldLBInput()
    {
        if (player.isInAir ||
            player.isSprinting ||
            player.isFiringSpell)
        {
            hold_lb_Input = false;
            return;
        }

        if (hold_lb_Input)
        {
            if (player.isTwoHandingWeapon)
            {
                if (player.playerInventoryManager.rightWeapon.oh_hold_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                    player.playerInventoryManager.rightWeapon.oh_hold_LB_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerInventoryManager.leftWeapon.oh_hold_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.leftWeapon;
                    player.playerInventoryManager.leftWeapon.oh_hold_LB_Action.PerformAction(player);
                }
            }

        }
        else if (hold_lb_Input == false)
        {
            if (player.isAiming)
            {
                player.isAiming = false;
                player.uiManager.crossHair.SetActive(false);
                player.cameraHandler.ResetAimCameraRotations();
            }

            if (player.isBlocking)
            {
                player.isBlocking = false;
            }
        }
    }

    private void HandleTapLTInput()
    {
        if (tab_lt_Input)
        {
            tab_lt_Input = false;

            if (player.isTwoHandingWeapon)
            {
                if (player.playerInventoryManager.rightWeapon.oh_tap_LT_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.rightWeapon;
                    player.playerInventoryManager.rightWeapon.oh_tap_LT_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerInventoryManager.leftWeapon.oh_tap_LT_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(true);
                    player.playerInventoryManager.currentItemBeingUsed = player.playerInventoryManager.leftWeapon;
                    player.playerInventoryManager.leftWeapon.oh_tap_LT_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleQuickSlotsInput()
    {
        if(d_Pad_Right)
        {
            player.playerInventoryManager.ChangeRightWeapon();
        }
        else if (d_Pad_Left)
        {
            player.playerInventoryManager.ChangeLeftWeapon();
        }
    }

    private void HandleInventoryInput()
    {
        if(inventoryFlag)
        {
            player.uiManager.UpdateUI();
        }

        if(inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if(inventoryFlag)
            {
                player.uiManager.OpenSelectWindow();
                player.uiManager.UpdateUI();
                player.uiManager.hudWindow.SetActive(false);
            }
            else
            {
                player.uiManager.CloseSelectWindow();
                player.uiManager.CloseAllInventoryWindows();
                player.uiManager.hudWindow.SetActive(true);
            }
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            lockOnInput = false;
            player.cameraHandler.HandleLockOn();
            if(player.cameraHandler.m_trNearestLockOnTarget != null)
            {
                player.cameraHandler.m_trCurrentLockOnTarget = player.cameraHandler.m_trNearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            player.cameraHandler.ClearLockOnTargets();
        }

        if(lockOnFlag && right_Stick_Right_Input)
        {
            right_Stick_Right_Input = false;
            player.cameraHandler.HandleLockOn();
            if (player.cameraHandler.m_trleftLockTarget != null)
            {
                player.cameraHandler.m_trCurrentLockOnTarget = player.cameraHandler.m_trleftLockTarget;
            }
        }

        if(lockOnFlag && right_Stick_Left_Input)
        {
            right_Stick_Left_Input = false;
            player.cameraHandler.HandleLockOn();
            if (player.cameraHandler.m_trRightLockTarget != null)
            {
                player.cameraHandler.m_trCurrentLockOnTarget = player.cameraHandler.m_trRightLockTarget;
            }
        }

        player.cameraHandler.SetCameraHeight();
    }

    private void HandleTwoHandInput()
    {
        if(y_Input)
        {
            y_Input = false;

            twoHandFlag = !twoHandFlag;

            if(twoHandFlag)
            {
                player.isTwoHandingWeapon = true;
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerInventoryManager.rightWeapon, false);
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(true);
            }
            else
            {
                player.isTwoHandingWeapon = false;
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerInventoryManager.rightWeapon, false);
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerInventoryManager.leftWeapon, true);
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(false);
            }
        }
    }

    private void HandleUseConsumableInput()
    {
        if(x_Input)
        {
            x_Input = false;
            player.playerInventoryManager.currentConsumable.AttemptToConsumeItem(player);
        }
    }

    private void QueInput(ref bool quedInput)
    {
        // ��� Queue Input Disable
        // Qued_LB_Input = false;
        // Qued_LT_Input = false;

        // ���� Ű ��� Enabled
        // intereting�̶��, input que ����, �ݴ��� ��Ȳ������queing�� �ʿ�ġ ����

        if(player.isInteracting)
        {
            quedInput = true;
            current_Qued_Input_Timer = default_Qued_Input_Time;
            input_Has_Been_Qued = true;
        }
    }

    private void HandleQuedInput()
    {
        if(input_Has_Been_Qued)
        {
            if(current_Qued_Input_Timer > 0)
            {
                current_Qued_Input_Timer -= Time.deltaTime;
                ProcessQuedInput();
            }
            else
            {
                input_Has_Been_Qued = false;
                current_Qued_Input_Timer = 0;
            }
        }
    }

    private void ProcessQuedInput()
    {
        if(qued_RB_Input)
        {
            tab_rb_Input = true;
        }

        // if Qued Lb Input => Tap LB Input = true;
        // if Qued LT Input => Tap LT Input = true;
    }
}

