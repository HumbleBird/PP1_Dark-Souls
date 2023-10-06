using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("Input")]
    public InputHandler inputHandler;

    [Header("Camera")]
    public CameraHandler cameraHandler;

    [Header("UI")]
    public GameUIManager m_GameUIManager;

    [Header("Player")]
    public PlayerStatsManager playerStatsManager;
    public PlayerWeaponSlotManager playerWeaponSlotManager;
    public PlayerCombatManager playerCombatManager;
    public PlayerInventoryManager playerInventoryManager;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerEffectsManager playerEffectsManager;

    protected override void Awake()
    {
        base.Awake();

        cameraHandler = FindObjectOfType<CameraHandler>();
        m_GameUIManager = FindObjectOfType<GameUIManager>();

        animator            = GetComponentInChildren<Animator>();
        inputHandler        = GetComponent<InputHandler>();

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();

        Managers.Object.m_MyPlayer = this;
        Managers.Object.Add(1, gameObject);
    }

    public void ReStart()
    {
        cameraHandler = Managers.Camera.m_Camera;
        m_GameUIManager = FindObjectOfType<GameUIManager>();
        //m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.RefreshUI();

        Managers.Camera.m_Camera.ReStart();
    }


    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (cameraHandler == null)
            return;

            inputHandler.TickInput();
        playerLocomotionManager.HandleRollingAndSprinting();
        playerLocomotionManager.HandleJumping();
        playerStatsManager.RegenerateStamina();

        playerLocomotionManager.HandleGroundedMovement();
        playerLocomotionManager.HandleRotation();

        CheckForInteractableObject();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void LateUpdate()
    {
        inputHandler.d_Pad_Up = false;
        inputHandler.d_Pad_Down = false;
        inputHandler.d_Pad_Left = false;
        inputHandler.d_Pad_Right = false;
        inputHandler.a_Input = false;
        inputHandler.select_Input = false;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget();
            cameraHandler.HandleCameraRotation();
        }
    }

    #region Player Interations

    public void CheckForInteractableObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    string interactableText = interactable.interactableText;
                    InteractablePopupUI ui = Managers.UI.ShowPopupUI<InteractablePopupUI>();
                    ui.m_InteractionText.text = interactableText;
                    m_GameUIManager.m_bIsShowingPopup = true;
                    m_GameUIManager.m_InteractablePopupUI = ui;

                    if (inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (m_GameUIManager.m_bIsShowingPopup  == true && inputHandler.a_Input)
            {
                Managers.UI.ClosePopupUI();
                m_GameUIManager.m_bIsShowingPopup = false;
            }
        }
    }

    public void OpenChestInteraction(Transform playerStandingHereWhenOpingChest)
    {
        playerLocomotionManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = playerStandingHereWhenOpingChest.transform.position;
        playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
    }

    public void PassThroughFogWallInteraction(Transform fogWallEnterance)
    {
        playerLocomotionManager.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Vector3 rotationDirection = fogWallEnterance.transform.forward;
        //Quaternion turnRoation = Quaternion.LookRotation(rotationDirection);
        //transform.rotation = turnRoation;

        playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
    }

    #endregion

    public void SaveCharacterdataToCurrentSaveData(ref CharacterSaveData currentCharacterSaveData)
    {
        currentCharacterSaveData.characterName  = playerStatsManager.characterName;
        currentCharacterSaveData.characterLevel = playerStatsManager.playerLevel;

        currentCharacterSaveData.xPosition      = transform.position.x;
        currentCharacterSaveData.yPosition      = transform.position.y;
        currentCharacterSaveData.zPosition      = transform.position.z;

        currentCharacterSaveData.currentRightHandWeaponID = playerEquipmentManager.m_CurrentHandRightWeapon.itemID;
        currentCharacterSaveData.currentLeftHandWeaponID  = playerEquipmentManager.m_CurrentHandLeftWeapon.itemID ;

        if(playerEquipmentManager.m_HelmetEquipment != null)
        {
            currentCharacterSaveData.currentHeadGearItemID     = playerEquipmentManager.m_HelmetEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentHeadGearItemID = -1;
        }

        if(playerEquipmentManager.m_TorsoEquipment != null)
        {
            currentCharacterSaveData.currentChestGearItemID = playerEquipmentManager.m_TorsoEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentChestGearItemID = -1;
        }

        if(playerEquipmentManager.m_LegEquipment != null)
        {
            currentCharacterSaveData.currentLegGearItemID = playerEquipmentManager.m_LegEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentLegGearItemID = -1;
        }

        if(playerEquipmentManager.m_HandEquipment != null)
        {
            currentCharacterSaveData.currentHandGearItemID = playerEquipmentManager.m_HandEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentHandGearItemID = -1;
        }

    }

    public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterSaveData)
    {
        playerStatsManager.characterName  =     currentCharacterSaveData.characterName ;
        playerStatsManager.playerLevel = currentCharacterSaveData.characterLevel;

        transform.position = new Vector3(currentCharacterSaveData.xPosition, currentCharacterSaveData.yPosition, currentCharacterSaveData.zPosition);

        playerEquipmentManager.m_CurrentHandRightWeapon = Managers.ItemData.GetWeaponItemByID(currentCharacterSaveData.currentRightHandWeaponID);
        playerEquipmentManager.m_CurrentHandLeftWeapon = Managers.ItemData.GetWeaponItemByID(currentCharacterSaveData.currentLeftHandWeaponID);
        playerWeaponSlotManager.LoadBothWeaponsOnSlots();

        HelmEquipmentItem headEquipment = (HelmEquipmentItem)Managers.ItemData.GetEquipmentItemByID(currentCharacterSaveData.currentHeadGearItemID);

        if(headEquipment != null)
        {
            playerEquipmentManager.m_HelmetEquipment = headEquipment;
        }

        TorsoEquipmentItem bodyEquipment = (TorsoEquipmentItem)Managers.ItemData.GetEquipmentItemByID(currentCharacterSaveData.currentChestGearItemID);

        if(bodyEquipment != null)
        {
            playerEquipmentManager.m_TorsoEquipment = bodyEquipment;
        }

        LeggingsEquipmentItem legEquipment = (LeggingsEquipmentItem)Managers.ItemData.GetEquipmentItemByID(currentCharacterSaveData.currentLegGearItemID);

        if(legEquipment != null)
        {
            playerEquipmentManager.m_LegEquipment = legEquipment;
        }

        GantletsEquipmentItem handEquipment = (GantletsEquipmentItem)Managers.ItemData.GetEquipmentItemByID(currentCharacterSaveData.currentHandGearItemID);

        if(handEquipment != null)
        {
            playerEquipmentManager.m_HandEquipment = handEquipment;
        }

        playerEquipmentManager.EquipAllEquipmentModel();
    }       
}
