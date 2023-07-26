using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Animator animator;
    public CharacterAnimatorManager characterAnimatorManager;
    public CharacterWeaponSlotManager characterWeaponSlotManager;
    public CharacterStatsManager characterStatsManager;
    public CharacterInventoryManager characterInventoryManager;
    public CharacterEffectsManager characterEffectsManager;
    public CharacterSoundFXManager characterSoundFXManager;
    public CharacterCombatManager characterCombatManager;

    [Header("Lock On Tranform")]
    public Transform lockOnTransform;

    [Header("Ray Casts")]
    public Transform criticalAttackRayCastStartPoint;

    [Header("Combat Colliders")]
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("IsInteraction")]
    public bool isInteracting;

    [Header("Status")]
    public bool isDead;


    [Header("Combat Flaged")]
    public bool canBeRiposted;
    public bool canDoCombo;
    public bool canBeParryied;
    public bool isParrying;
    public bool isBlocking;
    public bool isInvulnerable;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isHoldingArrow;
    public bool isAiming;
    public bool isTwoHandingWeapon;
    public bool isPerformingFullyChargedAttack;
    public bool isAttacking;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
    public bool canRotate;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;

    [Header("Spells")]
    public bool isFiringSpell;

    // 애니메이션 이벤트에 맞춰 들어가는 데미지 
    // backstab(뒤잡기) or riposte (패링 데미지?)
    public int pendingCriticalDamage;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterInventoryManager = GetComponent<CharacterInventoryManager>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
    }

    protected virtual void FixedUpdate()
    {
        characterAnimatorManager.CheckHandIKWeight(characterWeaponSlotManager.rightHandIKTarget, characterWeaponSlotManager.leftHandIKTarget, isTwoHandingWeapon);
    }

    public virtual void UpdateWhichHandCharacterIsUsing(bool usingRightHand)
    {
        if (usingRightHand)
        {
            isUsingRightHand = true;
            isUsingLeftHand = false;
        }
        else
        {
            isUsingRightHand = false;
            isUsingLeftHand = true;

        }
    }
}
 