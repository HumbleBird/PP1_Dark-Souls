using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterLocomotionManager : CharacterLocomotionManager
{
    AICharacterManager aiCharacter;

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 1; // °È±â
    [SerializeField]
    float walkingSpeed = 1.5f; // ¶Ù±â
    [SerializeField]
    float sprintSpeed = 3; // ´ë½¬

    public float horizontal;
    public float vertical;
    public float moveAmount;

    protected override void Awake()
    {
        base.Awake();
        aiCharacter = GetComponent<AICharacterManager>();
    }

    protected override void Update()
    {
        base.Update();

        horizontal = aiCharacter.animator.GetFloat("Horizontal");
        vertical = aiCharacter.animator.GetFloat("Vertical");
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        //HandleGroundedMovement();
    }

    public void HandleGroundedMovement()
    {
        if (aiCharacter.isInteracting)
            return;

        moveDirection = transform.forward * vertical;
        moveDirection = moveDirection + transform.right * horizontal;
        //moveDirection = transform.forward * vertical * horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        // Two Hand Animation Rig
        //if (aiCharacter.twoHandFlag)
        //{
        //    if (aiCharacter.isSprinting && moveAmount > 0.1f)
        //    {
        //        aiCharacter.aiCharacterWeaponSlotManager.LoadTwoHandIKTargtets(true);
        //    }
        //    else
        //    {
        //        aiCharacter.aiCharacterWeaponSlotManager.LoadTwoHandIKTargtets(false);

        //    }
        //}

        if (moveAmount > 0.5f)
        {
            aiCharacter.isSprinting = true;
        }

        // AI Movement
        if (aiCharacter.isSprinting && moveAmount > 0.5f)
        {
            aiCharacter.characterController.Move(moveDirection * sprintSpeed * Time.deltaTime);
        }
        else
        {
            if (moveAmount > 0.5f)
            {
                aiCharacter.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
            }
            else if (moveAmount <= 0.5f)
            {
                aiCharacter.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }
}
