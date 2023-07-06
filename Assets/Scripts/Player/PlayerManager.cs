using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    Animator animator;
    CameraHandler cameraHandler;
    PlayerStatsManager playerStatsManager;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerLocomotionManager playerLocomotionManager;
    PlayerEffectsManager playerEffectsManager;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableUIGameObject;

    protected override void Awake()
    {
        base.Awake();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();

        cameraHandler = FindObjectOfType<CameraHandler>();

        inputHandler = GetComponent<InputHandler>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();

        animator = GetComponentInChildren<Animator>();

        interactableUI = FindObjectOfType<InteractableUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isFiringSpell = animator.GetBool("isFiringSpell");
        isHoldingArrow = animator.GetBool("isHoldingArrow");

        animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isInAir", isInAir);
        animator.SetBool("isDead", playerStatsManager.isDead);

        inputHandler.TickInput(delta);
        playerAnimatorManager.canRotate = animator.GetBool("canRotate");
        playerLocomotionManager.HandleRollingAndSprinting(delta);
        playerLocomotionManager.HandleJumping();
        playerStatsManager.RegenerateStamina();

        CheckForInteractableObject();
    }


    protected override void FixedUpdate() 
    {
        base.FixedUpdate();

        float delta = Time.fixedDeltaTime;


        playerLocomotionManager.HandleFalling(delta, playerLocomotionManager.moveDirection);
        playerLocomotionManager.HandleMovement(delta);
        playerLocomotionManager.HandleRotation(delta);
        playerEffectsManager.HandleAllBuildUpEffects();
    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        inputHandler.lt_Input = false;
        inputHandler.d_Pad_Up = false;
        inputHandler.d_Pad_Down = false;
        inputHandler.d_Pad_Left = false;
        inputHandler.d_Pad_Right = false;
        inputHandler.a_Input = false;
        inputHandler.jump_Input = false;
        inputHandler.inventory_Input = false;

        float delta = Time.deltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollwTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
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
        playerAnimatorManager.PlayerTargetAnimation("Open Chest", true);
    }

    public void PassThroughFogWallInteraction(Transform fogWallEnterance)
    {
        playerLocomotionManager.rigidbody.velocity = Vector3.zero;

        //Vector3 rotationDirection = fogWallEnterance.transform.forward;
        //Quaternion turnRoation = Quaternion.LookRotation(rotationDirection);
        //transform.rotation = turnRoation;

        playerAnimatorManager.PlayerTargetAnimation("Pass Through Fog", true);
    }

    #endregion
}
