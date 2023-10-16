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
    public GameSceneUI m_GameSceneUI;

    [Header("Model")]
    public GameObject m_goModel;

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
    }

    public void StartGame()
    {
        // 위치
        m_StartPos = new Vector3(36.36f, 4.95f, -14.23298f);
        m_StartRo = new Vector3(0f, -90f, 0f);

        //if (Managers.Game.m_isNewGame)
        //{
        //    // 위치
        //    m_StartPos = new Vector3(36.36f, 4.95f, -14.23298f);
        //    m_StartRo = new Vector3(0f, -90f, 0f);
        //}
        //// 정보 로드
        //else
        //{

        //}

        cameraHandler = FindObjectOfType<CameraHandler>();
        m_GameSceneUI = FindObjectOfType<GameSceneUI>();

        Managers.Camera.m_Camera.StartGame();
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
                    interactable.ShowInteractUI();
                }
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

    #region Player Save & Load Data
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

    #endregion

    // 초기화
    public override void InitCharacterManager()
    {
        base.InitCharacterManager();

        //  two hand 해제

        if(inputHandler.twoHandFlag)
        {
            inputHandler.twoHandFlag = false;

            isTwoHandingWeapon = false;
            playerWeaponSlotManager.LoadWeaponOnSlot(playerEquipmentManager.m_CurrentHandRightWeapon, false);
            playerWeaponSlotManager.LoadWeaponOnSlot(playerEquipmentManager.m_CurrentHandLeftWeapon, true);
            playerWeaponSlotManager.LoadTwoHandIKTargtets(false);
        }

        m_GameSceneUI.RefreshUI();
    }

    public override void Dead()
    {
        base.Dead();

        // 소울 초기화
        playerStatsManager.currentSoulCount = 0;

        StopCoroutine(Managers.Game.PlayerDead());
        StartCoroutine(Managers.Game.PlayerDead()); 
    }


}
