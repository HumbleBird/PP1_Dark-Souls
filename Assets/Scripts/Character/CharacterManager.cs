using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    CharacterAnimatorManager characterAnimatorManager;
    CharacterWeaponSlotManager characterWeaponSlotManager;

    [Header("Lock On Tranform")]
    public Transform lockOnTransform;

    [Header("Combat Colliders")]
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("IsInteraction")]
    public bool isInteracting;


    [Header("Combat Flaged")]
    public bool canBeRiposted;
    public bool canDoCombo;
    public bool canBeParryied;
    public bool isParrying;
    public bool isBlocking;
    public bool isInvulnerable;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isAiming;
    public bool isTwoHandingWeapon;

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
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
    }

    protected virtual void FixedUpdate()
    {
        characterAnimatorManager.CheckHandIKWeight(characterWeaponSlotManager.rightHandIKTarget, characterWeaponSlotManager.leftHandIKTarget, isTwoHandingWeapon);
    }
}
 