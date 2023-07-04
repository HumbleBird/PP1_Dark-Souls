using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSlotManager : MonoBehaviour
{
    protected CharacterManager characterManager;
    protected CharacterInventoryManager characterInventoryManager;
    protected CharacterAnimatorManager characterAnimatorManager;
    protected CharacterStatsManager   characterStatsManager;
    protected CharacterEffectsManager characterEffectsManager;

    [Header("Unarmed Weapon")]
    public WeaponItem unarmWeapon;

    [Header("Weapon Slots")]
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    protected WeaponHolderSlot backSlot;

    [Header("Damage Colliders")]
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    [Header("Attacking Weapon")]
    public WeaponItem attackingWeapon;

    [Header("Hand IK Targets")]
    public RightHandIKTarget rightHandIKTarget;
    public LeftHandIKTarget leftHandIKTarget;

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        characterInventoryManager = GetComponent<CharacterInventoryManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        LoadWeaponHolderSlots();
    }

    protected virtual void LoadWeaponHolderSlots()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }
        }
    }

    public virtual void LoadBothWeaponsOnSlots()
    {
        LoadWeaponOnSlot(characterInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(characterInventoryManager.leftWeapon, true);
    }

    public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (weaponItem != null)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                if (characterManager.isTwoHandingWeapon)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    characterAnimatorManager.PlayerTargetAnimation("Left Arm Empty", false, true);
                }
                else
                {

                    backSlot.UnloadWeaponAndDestroy();

                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                LoadTwoHandIKTargtets(characterManager.isTwoHandingWeapon);
                characterAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }

        }
        else
        {
            weaponItem = unarmWeapon;

            if (isLeft)
            {
                characterInventoryManager.leftWeapon = unarmWeapon;
                leftHandSlot.currentWeapon = unarmWeapon;
                leftHandSlot.LoadWeaponModel(unarmWeapon);
                LoadLeftWeaponDamageCollider();
                characterAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                characterInventoryManager.rightWeapon = unarmWeapon;
                rightHandSlot.currentWeapon = unarmWeapon;
                rightHandSlot.LoadWeaponModel(unarmWeapon);
                LoadRightWeaponDamageCollider();
                characterAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;

            }
        }
    }

    protected virtual void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        leftHandDamageCollider.physicalDamage = characterInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = characterInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseBreak = characterInventoryManager.leftWeapon.poiseBreak;
        characterEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }

    protected virtual void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        rightHandDamageCollider.physicalDamage = characterInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = characterInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseBreak = characterInventoryManager.rightWeapon.poiseBreak;
        characterEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }

    public virtual void LoadTwoHandIKTargtets(bool isTwoHandingWeapon)
    {
        leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();

        characterAnimatorManager.SetHandIKForWeapon(rightHandIKTarget, leftHandIKTarget, isTwoHandingWeapon);
    }

    public virtual void OpenDamageCollider()
    {
        if (characterManager.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (characterManager.isUsingLeftHand)
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
    }

    public virtual void CloseDamageCollider()
    {
        if (rightHandDamageCollider != null)
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        if (leftHandDamageCollider != null)
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
    }

    public virtual void DrainStaminaLightAttack()
    {
    }

    public virtual void DrainStaminaHeavyAttack()
    {
    }

    public virtual void GrantWeaponAttackingPoiseBonus()
    {
        characterStatsManager.totalPoiseDefence = characterStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    public virtual void ResetWeaponAttackingPoiseBonus()
    {
        characterStatsManager.totalPoiseDefence = characterStatsManager.armorPoiseBonus;
    }
}
