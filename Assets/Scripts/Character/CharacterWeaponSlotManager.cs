using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSlotManager : MonoBehaviour
{
    protected CharacterManager character;

    [Header("Unarmed Weapon")]
    public WeaponItem unarmWeapon;

    [Header("Weapon Slots")]
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    protected WeaponHolderSlot backSlot;

    [Header("Damage Colliders")]
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;


    [Header("Hand IK Targets")]
    public RightHandIKTarget rightHandIKTarget;
    public LeftHandIKTarget leftHandIKTarget;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

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
        LoadWeaponOnSlot(character.characterInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(character.characterInventoryManager.leftWeapon, true);
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
                if (character.isTwoHandingWeapon)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    character.characterAnimatorManager.PlayerTargetAnimation("Left Arm Empty", false, true);
                }
                else
                {

                    backSlot.UnloadWeaponAndDestroy();

                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                LoadTwoHandIKTargtets(character.isTwoHandingWeapon);
                character.animator.runtimeAnimatorController = weaponItem.weaponController;
            }

        }
        else
        {
            weaponItem = unarmWeapon;

            if (isLeft)
            {
                character.characterInventoryManager.leftWeapon = unarmWeapon;
                leftHandSlot.currentWeapon = unarmWeapon;
                leftHandSlot.LoadWeaponModel(unarmWeapon);
                LoadLeftWeaponDamageCollider();
                character.characterAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                character.characterInventoryManager.rightWeapon = unarmWeapon;
                rightHandSlot.currentWeapon = unarmWeapon;
                rightHandSlot.LoadWeaponModel(unarmWeapon);
                LoadRightWeaponDamageCollider();
                character.animator.runtimeAnimatorController = weaponItem.weaponController;

            }
        }
    }

    protected virtual void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        leftHandDamageCollider.physicalDamage = character.characterInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = character.characterInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.characterManager = character;
        leftHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseBreak = character.characterInventoryManager.leftWeapon.poiseBreak;
        character.characterEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }

    protected virtual void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        if (rightHandDamageCollider == null)
            return;

        rightHandDamageCollider.physicalDamage = character.characterInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = character.characterInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.characterManager = character;
        rightHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseBreak = character.characterInventoryManager.rightWeapon.poiseBreak;
        character.characterEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }

    public virtual void LoadTwoHandIKTargtets(bool isTwoHandingWeapon)
    {
        leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();

        character.characterAnimatorManager.SetHandIKForWeapon(rightHandIKTarget, leftHandIKTarget, isTwoHandingWeapon);
    }

    public virtual void OpenDamageCollider()
    {
        if (character.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (character.isUsingLeftHand)
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
        WeaponItem currentWeaponBeingUsed = character.characterInventoryManager.currentItemBeingUsed as WeaponItem;
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.totalPoiseDefence + currentWeaponBeingUsed.offensivePoiseBonus;
    }

    public virtual void ResetWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.armorPoiseBonus;
    }
}
