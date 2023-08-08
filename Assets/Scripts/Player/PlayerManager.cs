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

        if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    string interactableText = interactable.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if(inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if(itemInteractableUIGameObject != null && inputHandler.a_Input)
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

    public void  SaveCharacterdataToCurrentSaveData(ref CharacterSaveData currentCharacterSaveData)
    {
        currentCharacterSaveData.characterName = playerStatsManager.characterName;
        currentCharacterSaveData.characterLevel = playerStatsManager.playerLevel;

        currentCharacterSaveData.xPosition = transform.position.x;
        currentCharacterSaveData.yPosition = transform.position.y;
        currentCharacterSaveData.zPosition = transform.position.z;
    }
}
