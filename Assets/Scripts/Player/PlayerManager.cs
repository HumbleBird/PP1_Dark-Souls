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
    public GameUIManager uiManager;

    [Header("Player")]
    public PlayerStatsManager playerStatsManager;
    public PlayerWeaponSlotManager playerWeaponSlotManager;
    public PlayerCombatManager playerCombatManager;
    public PlayerInventoryManager playerInventoryManager;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerEffectsManager playerEffectsManager;


    [Header("Interactables")]
    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableUIGameObject;

    protected override void Awake()
    {
        base.Awake();

        cameraHandler = FindObjectOfType<CameraHandler>();
        uiManager = FindObjectOfType<GameUIManager>();
        interactableUI = FindObjectOfType<InteractableUI>();


        animator = GetComponentInChildren<Animator>();
        inputHandler = GetComponent<InputHandler>();

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();

        WorldSaveGameManager.instance.player = this;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        isFiringSpell = animator.GetBool("isFiringSpell");
        isPerformingFullyChargedAttack = animator.GetBool("isPerformingFullyChargedAttack");

        animator.SetBool("isInAir", isInAir);

        inputHandler.TickInput();
        playerLocomotionManager.HandleRollingAndSprinting();
        playerLocomotionManager.HandleJumping();
        playerStatsManager.RegenerateStamina();

        CheckForInteractableObject();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        playerLocomotionManager.HandleFalling(playerLocomotionManager.moveDirection);
        playerLocomotionManager.HandleMovement();
        playerLocomotionManager.HandleRotation();
        playerEffectsManager.HandleAllBuildUpEffects();
    }

    private void LateUpdate()
    {
        inputHandler.d_Pad_Up = false;
        inputHandler.d_Pad_Down = false;
        inputHandler.d_Pad_Left = false;
        inputHandler.d_Pad_Right = false;
        inputHandler.a_Input = false;
        inputHandler.inventory_Input = false;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget();
            cameraHandler.HandleCameraRotation();
        }

        if (isInAir)
        {
            playerLocomotionManager.inAirTimer += Time.deltaTime;
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
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if (itemInteractableUIGameObject != null && inputHandler.a_Input)
            {
                itemInteractableUIGameObject.SetActive(false);
            }
        }
    }

    public void OpenChestInteraction(Transform playerStandingHereWhenOpingChest)
    {
        playerLocomotionManager.rigidbody.velocity = Vector3.zero;
        transform.position = playerStandingHereWhenOpingChest.transform.position;
        playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
    }

    public void PassThroughFogWallInteraction(Transform fogWallEnterance)
    {
        playerLocomotionManager.rigidbody.velocity = Vector3.zero;

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

        currentCharacterSaveData.currentRightHandWeaponID = playerInventoryManager.rightWeapon.itemID;
        currentCharacterSaveData.currentLeftHandWeaponID  = playerInventoryManager.leftWeapon.itemID ;

        if(playerInventoryManager.currentHelmetEquipment != null)
        {
            currentCharacterSaveData.currentHeadGearItemID     = playerInventoryManager.currentHelmetEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentHeadGearItemID = -1;
        }

        if(playerInventoryManager.currentTorsoEquipment != null)
        {
            currentCharacterSaveData.currentChestGearItemID = playerInventoryManager.currentTorsoEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentChestGearItemID = -1;
        }

        if(playerInventoryManager.currentLegEquipment != null)
        {
            currentCharacterSaveData.currentLegGearItemID = playerInventoryManager.currentLegEquipment.itemID;
        }
        else
        {
            currentCharacterSaveData.currentLegGearItemID = -1;
        }

        if(playerInventoryManager.currentHandEquipment != null)
        {
            currentCharacterSaveData.currentHandGearItemID = playerInventoryManager.currentHandEquipment.itemID;
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

        playerInventoryManager.rightWeapon = WorldItemDataBase.Instance.GetWeaponItemByID(currentCharacterSaveData.currentRightHandWeaponID);
        playerInventoryManager.leftWeapon = WorldItemDataBase.Instance.GetWeaponItemByID(currentCharacterSaveData.currentLeftHandWeaponID);
        playerWeaponSlotManager.LoadBothWeaponsOnSlots();

        EquipmentItem headEquipment = WorldItemDataBase.Instance.GetEquipmentItemByID(currentCharacterSaveData.currentHeadGearItemID);

        if(headEquipment != null)
        {
            playerInventoryManager.currentHelmetEquipment = headEquipment;
        }

        EquipmentItem bpduEquipment = WorldItemDataBase.Instance.GetEquipmentItemByID(currentCharacterSaveData.currentChestGearItemID);

        if(bpduEquipment != null)
        {
            playerInventoryManager.currentTorsoEquipment = bpduEquipment;
        }

        EquipmentItem legEquipment = WorldItemDataBase.Instance.GetEquipmentItemByID(currentCharacterSaveData.currentLegGearItemID);

        if(legEquipment != null)
        {
            playerInventoryManager.currentLegEquipment = legEquipment;
        }

        EquipmentItem handEquipment = WorldItemDataBase.Instance.GetEquipmentItemByID(currentCharacterSaveData.currentHandGearItemID);

        if(handEquipment != null)
        {
            playerInventoryManager.currentHandEquipment = handEquipment;
        }

        playerEquipmentManager.EquipAllEquipmentModelsOnStart();
    }       
}
