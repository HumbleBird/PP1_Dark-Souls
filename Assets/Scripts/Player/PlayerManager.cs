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

    [Header("Player")]
    public PlayerStatsManager playerStatsManager;
    public PlayerWeaponSlotManager playerWeaponSlotManager;
    public PlayerCombatManager playerCombatManager;
    public PlayerInventoryManager playerInventoryManager;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerEffectsManager playerEffectsManager;
    public PlayerSoundFXManager playerSoundFXManager;

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
        playerSoundFXManager = GetComponent<PlayerSoundFXManager>();

        Managers.Object.m_MyPlayer = this;
    }

    public void StartGame()
    {
        if(Managers.Game.m_Develing)
        {

        }
        else
        {
            if (Managers.Game.m_isNewGame)
            {
                // 위치
                m_StartPos = new Vector3(-25.688f, -4.981f, 4.910397f);
                m_StartRo = new Vector3(0f, 90f, 0f);

                transform.position = m_StartPos;
                transform.eulerAngles = m_StartRo;
            }
            else
            {
                LodeData();
            }
        }


        cameraHandler = FindObjectOfType<CameraHandler>();
        m_GameSceneUI = FindObjectOfType<GameSceneUI>();

        playerStatsManager.InitAbility();

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
                // 다른 팝업창이 켜져 있다면 제거
                if (Managers.GameUI.m_isShowingInteratablePopup)
                    return;

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.CanInteractable();
                }
            }
        }
        else
        {
            if (Managers.GameUI.m_InteractableAnnouncementPopupUI != null)
            {
                Managers.GameUI.ClosePopupUI();
                Managers.GameUI.m_InteractableAnnouncementPopupUI = null;
                Managers.Game.m_Interactable = null;
            }
        }
    }

    #endregion

    #region Player Save & Load Data
    public void SaveData()
    {
        Table_SaveData_Character.Info saveCharacterInfo = Managers.Table.m_SaveData_Character.Get(1);

        // Name
        saveCharacterInfo.m_sCharacterName = playerStatsManager.m_sCharacterName;

        // Player Level
        saveCharacterInfo.m_iPlayerLevel = playerStatsManager.playerLevel;

        // Stat Level
        saveCharacterInfo.m_iVigorLevel          = playerStatsManager.m_iVigorLevel;
        saveCharacterInfo.m_iAttunementLevel     = playerStatsManager.m_iAttunementLevel   ;
        saveCharacterInfo.m_iEnduranceLevel      = playerStatsManager.m_iEnduranceLevel    ;
        saveCharacterInfo.m_iVitalityLevel       = playerStatsManager.m_iVitalityLevel     ;
        saveCharacterInfo.m_iStrengthLevel       = playerStatsManager.m_iStrengthLevel     ;
        saveCharacterInfo.m_iDexterityLevel      = playerStatsManager.m_iDexterityLevel    ;
        saveCharacterInfo.m_iIntelligenceLevel   = playerStatsManager.m_iIntelligenceLevel ;
        saveCharacterInfo.m_iFaithLevel          = playerStatsManager.m_iFaithLevel        ;
        saveCharacterInfo.m_iLuckLevel           = playerStatsManager.m_iLuckLevel;

        // Inventory


        // Equipment

        // Right Hand Slots
        if (playerEquipmentManager.m_RightWeaponsSlots[0] != null)
          saveCharacterInfo.m_iRightHand1Id =  playerEquipmentManager.m_RightWeaponsSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iRightHand1Id = 0;
        if (playerEquipmentManager.m_RightWeaponsSlots[1] != null)
          saveCharacterInfo.m_iRightHand2Id =  playerEquipmentManager.m_RightWeaponsSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iRightHand2Id = 0;
        if (playerEquipmentManager.m_RightWeaponsSlots[2] != null)
          saveCharacterInfo.m_iRightHand3Id = playerEquipmentManager.m_RightWeaponsSlots[2].m_iItemID;
        else
            saveCharacterInfo.m_iRightHand3Id = 0;

        // Left Hand Slots
        if (playerEquipmentManager.m_LeftWeaponsSlots[0] != null)
              saveCharacterInfo.m_iLeftHand1Id =  playerEquipmentManager.m_LeftWeaponsSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iLeftHand1Id = 0;
        if (playerEquipmentManager.m_LeftWeaponsSlots[1] != null)
              saveCharacterInfo.m_iLeftHand2Id =  playerEquipmentManager.m_LeftWeaponsSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iLeftHand2Id = 0;
        if (playerEquipmentManager.m_LeftWeaponsSlots[2] != null)
              saveCharacterInfo.m_iLeftHand3Id = playerEquipmentManager.m_LeftWeaponsSlots[2].m_iItemID;
        else
            saveCharacterInfo.m_iLeftHand3Id = 0;

        // Ammo

        if (playerEquipmentManager.m_ArrowAmmoSlots[0] != null)
              saveCharacterInfo.m_iArrow1ID= playerEquipmentManager.m_ArrowAmmoSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iArrow1ID = 0;
        if (playerEquipmentManager.m_ArrowAmmoSlots[1] != null)
             saveCharacterInfo.m_iArrow2ID= playerEquipmentManager.m_ArrowAmmoSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iArrow2ID = 0;

        if (playerEquipmentManager.m_BoltAmmoSlots[0] != null)
             saveCharacterInfo.m_iBolt1ID= playerEquipmentManager. m_BoltAmmoSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iBolt1ID = 0;
        if (playerEquipmentManager.m_BoltAmmoSlots[1] != null)
            saveCharacterInfo.m_iBolt2ID = playerEquipmentManager.m_BoltAmmoSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iBolt2ID = 0;

        // Armor
        if (playerEquipmentManager.m_HelmetEquipment != null)
             saveCharacterInfo.m_iHeadArmorId = playerEquipmentManager.m_HelmetEquipment.m_iItemID;
        else
            saveCharacterInfo.m_iHeadArmorId = 0;
        if (playerEquipmentManager.m_TorsoEquipment != null)
             saveCharacterInfo.m_iChestArmorId = playerEquipmentManager.m_TorsoEquipment.m_iItemID;
        else
            saveCharacterInfo.m_iChestArmorId = 0;
        if (playerEquipmentManager.m_LegEquipment != null)
          saveCharacterInfo.m_iHandArmorId = playerEquipmentManager.m_LegEquipment.m_iItemID;
        else
            saveCharacterInfo.m_iHandArmorId = 0;
        if (playerEquipmentManager.m_HandEquipment != null)
            saveCharacterInfo.m_iLegArmorId = playerEquipmentManager.m_HandEquipment.m_iItemID;
        else
            saveCharacterInfo.m_iLegArmorId = 0;

        // Ring

        if (playerEquipmentManager.m_RingSlots[0] != null)
          saveCharacterInfo.m_iRing1Id= playerEquipmentManager.m_RingSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iRing1Id = 0;
        if (playerEquipmentManager.m_RingSlots[1] != null)
          saveCharacterInfo.m_iRing2Id = playerEquipmentManager .m_RingSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iRing2Id = 0;
        if (playerEquipmentManager.m_RingSlots[2] != null)
           saveCharacterInfo.m_iRing3Id= playerEquipmentManager .m_RingSlots[2].m_iItemID;
        else
            saveCharacterInfo.m_iRing3Id = 0;
        if (playerEquipmentManager.m_RingSlots[3] != null)
           saveCharacterInfo.m_iRing4Id = playerEquipmentManager.m_RingSlots[3].m_iItemID;
        else
            saveCharacterInfo.m_iRing4Id = 0;

        // Consumable
        if (playerEquipmentManager.m_ConsumableItemSlots[0] != null)
         saveCharacterInfo.m_iToolItemId1 = playerEquipmentManager.m_ConsumableItemSlots[0].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId1 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[1] != null)
          saveCharacterInfo.m_iToolItemId2 = playerEquipmentManager  .m_ConsumableItemSlots[1].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId2 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[2] != null)
          saveCharacterInfo.m_iToolItemId3 = playerEquipmentManager  .m_ConsumableItemSlots[2].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId3 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[3] != null)
           saveCharacterInfo.m_iToolItemId4 = playerEquipmentManager  .m_ConsumableItemSlots[3].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId4 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[4] != null)
           saveCharacterInfo.m_iToolItemId5 = playerEquipmentManager  .m_ConsumableItemSlots[4].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId5 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[5] != null)
          saveCharacterInfo.m_iToolItemId6 = playerEquipmentManager  .m_ConsumableItemSlots[5].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId6 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[6] != null)
            saveCharacterInfo.m_iToolItemId7 = playerEquipmentManager  .m_ConsumableItemSlots[6].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId7 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[7] != null)
         saveCharacterInfo.m_iToolItemId8 = playerEquipmentManager  .m_ConsumableItemSlots[7].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId8 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[8] != null)
            saveCharacterInfo.m_iToolItemId9 = playerEquipmentManager  .m_ConsumableItemSlots[8].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId9 = 0;
        if (playerEquipmentManager.m_ConsumableItemSlots[9] != null)
          saveCharacterInfo.m_iToolItemId10 = playerEquipmentManager.m_ConsumableItemSlots[9].m_iItemID;
        else
            saveCharacterInfo.m_iToolItemId10 = 0;

        // Spell
        if (playerEquipmentManager.m_CurrentHandSpell != null)
           saveCharacterInfo.m_iSpellId = playerEquipmentManager.m_CurrentHandSpell.m_iItemID;
        else
            saveCharacterInfo.m_iSpellId = 0;

        // Pledge
        if (playerEquipmentManager.m_CurrentPledge != null)
           saveCharacterInfo.m_iCurrentPledgeID = playerEquipmentManager.m_CurrentPledge.m_iItemID;
        else
            saveCharacterInfo.m_iCurrentPledgeID = 0;

        // Pos
        saveCharacterInfo.xPosition= transform.position.x;
        saveCharacterInfo.yPosition= transform.position.y;
        saveCharacterInfo.zPosition= transform.position.z;

        // index
        saveCharacterInfo.m_iCurrentRightWeaponIndex  = playerEquipmentManager.m_iCurrentRightWeaponIndex  ;
        saveCharacterInfo.m_iCurrentLeftWeaponIndex   = playerEquipmentManager.m_iCurrentLeftWeaponIndex   ;
        saveCharacterInfo.m_iCurrentAmmoArrowIndex    = playerEquipmentManager.m_iCurrentAmmoArrowIndex    ;
        saveCharacterInfo.m_iCurrentAmmoBoltIndex     = playerEquipmentManager.m_iCurrentAmmoBoltIndex     ;
        saveCharacterInfo.m_iCurrentConsumableItemndex= playerEquipmentManager.m_iCurrentConsumableItemndex;

        Managers.Table.Save();
    }   
    
    public void LodeData()
    {
        return;
        Table_SaveData_Character.Info saveCharacterInfo = Managers.Table.m_SaveData_Character.Get(1);

        // Name
        playerStatsManager.m_sCharacterName = saveCharacterInfo.m_sCharacterName;

        // Player Level
        playerStatsManager.playerLevel = saveCharacterInfo.m_iPlayerLevel;

        // Stat Level
        playerStatsManager.m_iVigorLevel = saveCharacterInfo.m_iVigorLevel;
        playerStatsManager.m_iAttunementLevel  =saveCharacterInfo.m_iAttunementLevel    ;
        playerStatsManager.m_iEnduranceLevel   =saveCharacterInfo.m_iEnduranceLevel     ;
        playerStatsManager.m_iVitalityLevel    =saveCharacterInfo.m_iVitalityLevel      ;
        playerStatsManager.m_iStrengthLevel    =saveCharacterInfo.m_iStrengthLevel      ;
        playerStatsManager.m_iDexterityLevel   =saveCharacterInfo.m_iDexterityLevel     ;
        playerStatsManager.m_iIntelligenceLevel= saveCharacterInfo.m_iIntelligenceLevel  ;
        playerStatsManager.m_iFaithLevel       =saveCharacterInfo.m_iFaithLevel         ;
        playerStatsManager.m_iLuckLevel        =  saveCharacterInfo.m_iLuckLevel           ;

        // Inventory


        // Equipment

        // Right Hand Slots
        if (saveCharacterInfo.m_iRightHand1Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iRightHand1Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[0] = null;

        if (saveCharacterInfo.m_iRightHand2Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iRightHand2Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[1] = null;

        if (saveCharacterInfo.m_iRightHand3Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[2] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iRightHand3Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[2] = null;



        // Left Hand Slots
        if (saveCharacterInfo.m_iLeftHand1Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iLeftHand1Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[0] = null;

        if (saveCharacterInfo.m_iLeftHand2Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iLeftHand2Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[1] = null;

        if (saveCharacterInfo.m_iLeftHand3Id != 0)
            playerEquipmentManager.m_RightWeaponsSlots[2] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iLeftHand3Id) as WeaponItem;
        else
            playerEquipmentManager.m_RightWeaponsSlots[2] = null;

        // Ammo

        if (saveCharacterInfo.m_iArrow1ID != 0)
            playerEquipmentManager.m_ArrowAmmoSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iArrow1ID) as AmmoItem;
        else
            playerEquipmentManager.m_ArrowAmmoSlots[0] = null;

        if (saveCharacterInfo.m_iArrow2ID != 0)
            playerEquipmentManager.m_ArrowAmmoSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iArrow2ID) as AmmoItem;
        else
            playerEquipmentManager.m_ArrowAmmoSlots[1] = null;


        if (saveCharacterInfo.m_iBolt1ID != 0)
            playerEquipmentManager.m_BoltAmmoSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iBolt1ID) as AmmoItem;
        else
            playerEquipmentManager.m_BoltAmmoSlots[0] = null;

        if (saveCharacterInfo.m_iBolt2ID != 0)
            playerEquipmentManager.m_BoltAmmoSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iBolt2ID) as AmmoItem;
        else
            playerEquipmentManager.m_BoltAmmoSlots[1] = null;


        // Armor
        if (saveCharacterInfo.m_iHeadArmorId != 0)
            playerEquipmentManager.m_HelmetEquipment = Managers.Game.MakeItem(Define.E_ItemType.MeleeWeapon, saveCharacterInfo.m_iHeadArmorId) as HelmEquipmentItem;
        else
            playerEquipmentManager.m_HelmetEquipment = null;

        if (saveCharacterInfo.m_iChestArmorId != 0)
            playerEquipmentManager.m_TorsoEquipment = Managers.Game.MakeItem(Define.E_ItemType.ChestArmor, saveCharacterInfo.m_iChestArmorId) as TorsoEquipmentItem;
        else
            playerEquipmentManager.m_TorsoEquipment = null;

        if (saveCharacterInfo.m_iHandArmorId != 0)
            playerEquipmentManager.m_HandEquipment = Managers.Game.MakeItem(Define.E_ItemType.Gauntlets, saveCharacterInfo.m_iHandArmorId) as GantletsEquipmentItem;
        else
            playerEquipmentManager.m_HandEquipment = null;

        if (saveCharacterInfo.m_iLegArmorId != 0)
            playerEquipmentManager.m_LegEquipment = Managers.Game.MakeItem(Define.E_ItemType.Leggings, saveCharacterInfo.m_iLegArmorId) as LeggingsEquipmentItem;
        else
            playerEquipmentManager.m_LegEquipment = null;

        // Ring

        if (saveCharacterInfo.m_iRing1Id != 0)
            playerEquipmentManager.m_RingSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.Ring, saveCharacterInfo.m_iRing1Id) as RingItem;
        else
            playerEquipmentManager.m_RingSlots[0] = null;

        if (saveCharacterInfo.m_iRing2Id != 0)
            playerEquipmentManager.m_RingSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.Ring, saveCharacterInfo.m_iRing2Id) as RingItem;
        else
            playerEquipmentManager.m_RingSlots[1] = null;

        if (saveCharacterInfo.m_iRing3Id != 0)
            playerEquipmentManager.m_RingSlots[2] = Managers.Game.MakeItem(Define.E_ItemType.Ring, saveCharacterInfo.m_iRing3Id) as RingItem;
        else
            playerEquipmentManager.m_RingSlots[2] = null;

        if (saveCharacterInfo.m_iRing4Id != 0)
            playerEquipmentManager.m_RingSlots[3] = Managers.Game.MakeItem(Define.E_ItemType.Ring, saveCharacterInfo.m_iRing4Id) as RingItem;
        else
            playerEquipmentManager.m_RingSlots[3] = null;


        // Consumable

        if (saveCharacterInfo.m_iToolItemId1 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[0] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId1) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[0] = null;

        if (saveCharacterInfo.m_iToolItemId2 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[1] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId2) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[1] = null;

        if (saveCharacterInfo.m_iToolItemId3 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[2] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId3) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[2] = null;

        if (saveCharacterInfo.m_iToolItemId4 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[3] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId4) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[3] = null;

        if (saveCharacterInfo.m_iToolItemId5 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[4] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId5) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[4] = null;

        if (saveCharacterInfo.m_iToolItemId6 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[5] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId6) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[5] = null;

        if (saveCharacterInfo.m_iToolItemId7 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[6] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId7) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[6] = null;

        if (saveCharacterInfo.m_iToolItemId8 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[7] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId8) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[7] = null;

        if (saveCharacterInfo.m_iToolItemId9 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[8] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId9) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[8] = null;

        if (saveCharacterInfo.m_iToolItemId10 != 0)
            playerEquipmentManager.m_ConsumableItemSlots[9] = Managers.Game.MakeItem(Define.E_ItemType.Tool, saveCharacterInfo.m_iToolItemId10) as ToolItem ;
        else
            playerEquipmentManager.m_ConsumableItemSlots[9] = null;


        // Spell
        if (saveCharacterInfo.m_iSpellId != 0)
            playerEquipmentManager.m_CurrentHandSpell = Managers.Game.MakeItem(Define.E_ItemType.Magic, saveCharacterInfo.m_iSpellId) as SpellItem;
        else
            playerEquipmentManager.m_CurrentHandSpell = null;


        // Pledge
        if (saveCharacterInfo.m_iCurrentPledgeID != 0)
            playerEquipmentManager.m_CurrentPledge = Managers.Game.MakeItem(Define.E_ItemType.Pledge, saveCharacterInfo.m_iCurrentPledgeID) as PledgeItem;
        else
            playerEquipmentManager.m_CurrentPledge = null;

        // Pos
        transform.position = new Vector3(saveCharacterInfo.xPosition, saveCharacterInfo.yPosition, saveCharacterInfo.zPosition);

        // index
        playerEquipmentManager.m_iCurrentRightWeaponIndex  =   saveCharacterInfo.m_iCurrentRightWeaponIndex  ;
        playerEquipmentManager.m_iCurrentLeftWeaponIndex   =   saveCharacterInfo.m_iCurrentLeftWeaponIndex   ;
        playerEquipmentManager.m_iCurrentAmmoArrowIndex    =   saveCharacterInfo.m_iCurrentAmmoArrowIndex    ;
        playerEquipmentManager.m_iCurrentAmmoBoltIndex     =   saveCharacterInfo.m_iCurrentAmmoBoltIndex     ;
        playerEquipmentManager.m_iCurrentConsumableItemndex= saveCharacterInfo.m_iCurrentConsumableItemndex;

        playerEquipmentManager.EquipAllEquipmentModel();
        playerWeaponSlotManager.LoadBothWeaponsOnSlots();
        StartGame();
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

        // UI Refresh
        m_GameSceneUI.RefreshUI();

        // Player Input Clear
        inputHandler.Clear();
    }

    public override void Dead()
    {
        base.Dead();

        // 소울 초기화
        Managers.Game.m_iPlayerDeadSoul = playerStatsManager.currentSoulCount;
        playerStatsManager.currentSoulCount = 0;
        Managers.GameUI.m_GameSceneUI.SoulsRefreshUI(false, 0, false);

        StopCoroutine(Managers.Game.PlayerDead());
        StartCoroutine(Managers.Game.PlayerDead()); 
    }


}
