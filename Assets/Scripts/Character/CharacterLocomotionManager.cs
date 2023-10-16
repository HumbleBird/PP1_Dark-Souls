using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    public Vector3 moveDirection;
    public LayerMask groundLayer = 1;

    [Header("Gravity Settings")]
    public float inAirTimer;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float groundedYVelocity = -20;
    [SerializeField] protected float fallStartYVelocity = -7;
    [SerializeField] protected float gravityForce = -25;
    [SerializeField]  float groundCheckSphereRadius = 1f;
    protected bool fallingVelocity = false;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

    }

    protected virtual void Start()
    {

    }


    protected virtual void Update()
    {
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        character.animator.SetBool("isGrounded", character.isGrounded);
        HandleGroundCheck();
    }

    public virtual void HandleGroundCheck()
    {
        if(character.isGrounded)
        {
            if(yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocity = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if(!fallingVelocity)
            {
                fallingVelocity = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer += Time.deltaTime;
            yVelocity.y += gravityForce * Time.deltaTime;
        }

        character.animator.SetFloat("inAirTimer", inAirTimer);
        character.characterController.Move(yVelocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
