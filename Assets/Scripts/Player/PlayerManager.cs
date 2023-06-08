using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerStatus playerStatus;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerLocomotion playerLocomotion;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableUIGameObject;

    public bool isInteracting;

    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;
    
    private void Awake()
    {
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();

        playerStatus = GetComponent<PlayerStatus>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isUsingRightHand = anim.GetBool("isUsingRightHand");
        isUsingLeftHand = anim.GetBool("isUsingLeftHand");
        isInvulnerable = anim.GetBool("isInvulnerable");

        anim.SetBool("isBlocking", isBlocking);
        anim.SetBool("isInAir", isInAir);
        anim.SetBool("isDead", playerStatus.isDead);

        inputHandler.TickInput(delta);
        playerAnimatorManager.canRotate = anim.GetBool("canRotate");
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleJumping();
        playerStatus.RegenerateStamina();

        CheckForInteractableObject();
    }


    private void FixedUpdate() 
    {
        float delta = Time.fixedDeltaTime;


        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRotation(delta);
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
            playerLocomotion.inAirTimer += Time.deltaTime;
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
        playerLocomotion.rigidbody.velocity = Vector3.zero;
        transform.position = playerStandingHereWhenOpingChest.transform.position;
        playerAnimatorManager.PlayerTargetAnimation("Open Chest", true);
    }

    #endregion
}
