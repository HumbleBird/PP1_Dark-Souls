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
    public bool select_Input;
    public bool lockOnInput;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;

    /// <summary>
    /// ///////////////////////
    /// </summary>
    public bool d_Pad_Up;
    public bool d_Pad_Down;
    public bool d_Pad_Left;
    public bool d_Pad_Right;

    public bool rollFlag;
    public bool twoHandFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public bool fireFlag;
    public bool selectFlag;

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
            inputActions.PlayerQuickSlots.DPadDown.performed += i => d_Pad_Down = true;

            inputActions.PlayerActions.A.performed += i => a_Input = true;
            inputActions.PlayerActions.X.performed += i => x_Input = true;

            inputActions.PlayerActions.Roll.performed += i => b_Input = true;
            inputActions.PlayerActions.Roll.canceled += i => b_Input = false;

            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
            inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
            inputActions.PlayerActions.Y.performed += i => y_Input = true;

            inputActions.PlayerActions.QuedRB.performed += i => QueInput(ref qued_RB_Input);

            // 게임 외 설정
            inputActions.PlayerActions.Select.performed += i => select_Input = true;

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
        HandleSelectUIInput();

        HandleLockOnInput();
        HandleTwoHandInput();
        HandleUseConsumableInput();
        HandleQuedInput();

        HandleAInput();
    }

    private void HandleMoveInput()
    {
        if(player.isHoldingArrow || player.playerStatsManager.encumbranceLevel == EncumbranceLevel.Overloaded)
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
                player.isSprinting = false;
            }

            if(moveAmount > 0.5f && player.playerStatsManager.currentStamina > 0)
            {
                player.isSprinting = true;
            }
        }
        else
        {
            player.isSprinting = false;


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

            if(player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_RB_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_RB_Action.PerformAction(player);
            }

        }
    }

    private void HandleHoldRBInput()
    {
        if (hold_rb_Input)
        {
            if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_RB_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_RB_Action.PerformAction(player);
            }
                
        }
    }

    private void HandleHoladRTInput()
    {
        player.animator.SetBool("isChargingAttack", hold_rt_Input);

        if(hold_rt_Input)
        {
            player.UpdateWhichHandCharacterIsUsing(true);
            player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;

            if(player.isTwoHandingWeapon)
            {
                if (player.playerEquipmentManager.m_CurrentHandRightWeapon.th_hold_RT_Action != null)
                {
                    player.playerEquipmentManager.m_CurrentHandRightWeapon.th_hold_RT_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_RT_Action != null)
                {
                    player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_RT_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleTapRTInput()
    {
        if (tab_rt_Input)
        {
            tab_rt_Input = false;

            if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_RT_Action != null)
            {
                player.UpdateWhichHandCharacterIsUsing(true);
                player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_RT_Action.PerformAction(player);
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
                if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                    player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_LB_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_tap_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandLeftWeapon;
                    player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_tap_LB_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleHoldLBInput()
    {
        if (!player.isGrounded ||
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
                if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                    player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_hold_LB_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_hold_LB_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandLeftWeapon;
                    player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_hold_LB_Action.PerformAction(player);
                }
            }

        }
        else if (hold_lb_Input == false)
        {
            if (player.isAiming)
            {
                player.isAiming = false;
                player.m_GameSceneUI.m_goCrosshair.SetActive(false);
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
                if (player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_LT_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandRightWeapon;
                    player.playerEquipmentManager.m_CurrentHandRightWeapon.oh_tap_LT_Action.PerformAction(player);
                }
            }
            else
            {
                if (player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_tap_LT_Action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    player.characterEquipmentManager.currentItemBeingUsed = player.playerEquipmentManager.m_CurrentHandLeftWeapon;
                    player.playerEquipmentManager.m_CurrentHandLeftWeapon.oh_tap_LT_Action.PerformAction(player);
                }
            }
        }
    }

    private void HandleQuickSlotsInput()
    {
        if (player.isInteracting)
            return;

        if (d_Pad_Left)
        {
            player.playerEquipmentManager.ChangeCurrentEquipmentToNextNumSlotEquipment(0);
        }

        if (d_Pad_Right)
        {
            player.playerEquipmentManager.ChangeCurrentEquipmentToNextNumSlotEquipment(1);
        }


        if (d_Pad_Down)
        {
            player.playerEquipmentManager.ChangeCurrentEquipmentToNextNumSlotEquipment(2);
        }
    }

    private void HandleSelectUIInput()
    {
        // 다른 팝업 창이 켜져 있으면 하나씩 꺼준다.
        // 인벤토리가 켜져 있다면 인벤토리를 닫고, 선택 창을 켜주고 다시 선택하면 꺼주는 걸로.

        if (select_Input)
        {
            if (Managers.UI._popupStack.Count > 0)
            {
                Managers.GameUI.CloseAllPopupUI();

                // TODO

            }
        }

        if(select_Input)
        {
            selectFlag = !selectFlag;

            // 선택 창을 처음 켰다면.
            if(selectFlag)
            {
                Managers.GameUI.m_GameSceneUI.quickSlotsUI.gameObject.SetActive(false);
                Managers.GameUI.ShowPopupUI<SelectUI>();
                Managers.Sound.Play("UI/Popup_ButtonClose");
            }
            // 선택 창을 끄는 거라면
            else
            {
                Managers.GameUI.m_GameSceneUI.quickSlotsUI.gameObject.SetActive(true);
                Managers.GameUI.ClosePopupUI();
                Managers.Sound.Play("UI/Popup_ButtonShow");
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

        if(player.cameraHandler != null)
             player.cameraHandler.SetCameraHeight();
    }

    private void HandleTwoHandInput()
    {
        if (y_Input)
        {
            y_Input = false;

            twoHandFlag = !twoHandFlag;

            if (twoHandFlag)
            {
                player.isTwoHandingWeapon = true;
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerEquipmentManager.m_CurrentHandRightWeapon, false);
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(true);
            }
            else
            {
                player.isTwoHandingWeapon = false;
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerEquipmentManager.m_CurrentHandRightWeapon, false);
                player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerEquipmentManager.m_CurrentHandLeftWeapon, true);
                player.playerWeaponSlotManager.LoadTwoHandIKTargtets(false);

            }
        }

        player.canRoll = true;
    }

    private void HandleUseConsumableInput()
    {
        if(x_Input)
        {
            x_Input = false;

            if(player.playerEquipmentManager.m_CurrentHandConsumable != null)
            {
                player.playerEquipmentManager.m_CurrentHandConsumable.AttemptToConsumeItem(player);
            }
        }
    }

    private void QueInput(ref bool quedInput)
    {
        // 모든 Queue Input Disable
        // Qued_LB_Input = false;
        // Qued_LT_Input = false;

        // 눌린 키 대로 Enabled
        // intereting이라면, input que 가능, 반대의 상황에서는queing이 필요치 않음

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

    private void HandleAInput()
    {
        // UI 선택

        // Interactble
        if (a_Input)
        {
            if (Managers.Game.m_bisWatingButtonClose == true)
            {
                Managers.Game.m_bisWatingButtonClose = false;
                return;
            }

            if (Managers.Game.m_Interactable != null)
            {
                Managers.Game.m_Interactable.Interact(player);
            }
        }

        // Inventory, Equipment, Item ...
    }

    public void Clear()
    {
        //Lock On
        if (lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            player.cameraHandler.ClearLockOnTargets();
        }
    }
}

