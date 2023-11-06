using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    public Vector3 moveDirection;
    public LayerMask groundLayer = 1 << 0;

    [Header("Gravity Settings")]
    public float inAirTimer;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float groundedYVelocity = -7.5f;
    [SerializeField] protected float fallStartYVelocity = -7;
    [SerializeField] protected float gravityForce = -10;
    [SerializeField]  float groundCheckSphereRadius = 0.1f;
    [SerializeField] protected float m_FallingDamageCanTime = 0.5f;
    [SerializeField] protected float m_FallingDeadTime = 1.7f;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, groundCheckSphereRadius);
    }

    public void HandleGroundCheck()
    {
        if (character.isDead)
            return;

        if(character.isGrounded)
        {
            if(inAirTimer > 0)
            {
                // Damage Check
                if (inAirTimer <= m_FallingDeadTime && inAirTimer >= m_FallingDamageCanTime)
                {
                    // Stat
                    character.characterStatsManager.currentHealth -= Mathf.RoundToInt(inAirTimer * 5);

                    // UI Refresh
                    character.characterStatsManager.HealthBarUIUpdate(Mathf.RoundToInt(Mathf.RoundToInt(inAirTimer * 5)));

                    // Sound
                    Managers.Sound.SoundPlayFromCharacter(gameObject, "Character/Common/Fall_Land", character.characterSoundFXManager.m_AudioSource);

                    inAirTimer = 0;
                    fallingVelocity = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            //if(yVelocity.y < 0)
            //{
            //    inAirTimer = 0;
            //    fallingVelocity = false;
            //    yVelocity.y = groundedYVelocity;
            //}
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

            FallingDeadCheck();

            character.animator.SetFloat("inAirTimer", inAirTimer);
            character.characterController.Move(yVelocity * Time.deltaTime);
        }
    }

    protected virtual void FallingDeadCheck()
    {
        // »ç¸Á
        if (inAirTimer >= m_FallingDeadTime)
        {
            character.characterStatsManager.currentHealth = 0;
            character.Dead();

            //Animation
            character.characterAnimatorManager.PlayTargetAnimation("Dead_01", true);

            // UI Refresh
            character.characterStatsManager.HealthBarUIUpdate(Mathf.RoundToInt(Mathf.RoundToInt(inAirTimer * 5)));

        }
    }
}
