using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    public CharacterAnimatorManager characterAnimatorManager;
    public CharacterWeaponSlotManager characterWeaponSlotManager;
    public CharacterStatsManager characterStatsManager;
    public CharacterEffectsManager characterEffectsManager;
    public CharacterSoundFXManager characterSoundFXManager;
    public CharacterCombatManager characterCombatManager;
    public CharacterEquipmentManager characterEquipmentManager;

    [Header("Lock On Tranform")]
    public Transform lockOnTransform;

    [Header("Ray Casts")]
    public Transform criticalAttackRayCastStartPoint;

    [Header("Start Pos")]
    public Vector3 m_StartPos;
    public Vector3 m_StartRo;

    [Header("IsInteraction")]
    public bool isInteracting;

    [Header("Status")]
    public bool isDead;

    [Header("Combat Flaged")]
    public bool canBeRiposted;
    public bool canDoCombo;
    public bool canBeParryied;
    public bool canRoll = true;
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
    public bool isBeingBackstabbed;
    public bool isBeingRiposted;
    public bool isPerformingBackstab;
    public bool isPerformingRipost;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
    public bool canRotate;
    public bool isSprinting;
    public bool isGrounded;

    [Header("Spells")]
    public bool isFiringSpell;

    // �ִϸ��̼� �̺�Ʈ�� ���� ���� ������ 
    // backstab(�����) or riposte (�и� ������?)
    public int pendingCriticalDamage;

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();

        Managers.Object.Add(gameObject);
    }

    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        characterAnimatorManager.CheckHandIKWeight(characterWeaponSlotManager.rightHandIKTarget, characterWeaponSlotManager.leftHandIKTarget, isTwoHandingWeapon);
    }

    protected virtual void Update()
    {
        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        canRotate = animator.GetBool("canRotate");
        isHoldingArrow = animator.GetBool("isHoldingArrow");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isFiringSpell = animator.GetBool("isFiringSpell");
        isPerformingFullyChargedAttack = animator.GetBool("isPerformingFullyChargedAttack");

        animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isDead", isDead);

        characterEffectsManager.ProcessAllTimedEffects();
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

    // �ʱ�ȭ
    public virtual void InitCharacterManager()
    {
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Horizontal", 0);

        // ü�� �� �ʱ�ȭ
        characterStatsManager.FullRecovery();

        // ��ġ �ʱ�ȭ
        transform.position = m_StartPos;
        transform.eulerAngles = m_StartRo;

        // Flag �ʱ�ȭ
        isBlocking = false;
        canBeRiposted= false;
        canDoCombo= false;
        canBeParryied= false;
        canRoll = true;
        isParrying= false;
        isBlocking= false;
        isInvulnerable= false;
        isUsingRightHand= false;
        isUsingLeftHand= false;
        isHoldingArrow= false;
        isAiming= false;
        isTwoHandingWeapon= false;
        isPerformingFullyChargedAttack= false;
        isAttacking= false;
        isBeingBackstabbed= false;
        isBeingRiposted= false;
        isPerformingBackstab= false;
        isPerformingRipost= false;

        isDead = false;

        isInteracting = false;
    }

    // �׾��� ��
    public virtual void Dead()
    {
        characterStatsManager.currentHealth = 0;
        isDead = true;

    }
}
 