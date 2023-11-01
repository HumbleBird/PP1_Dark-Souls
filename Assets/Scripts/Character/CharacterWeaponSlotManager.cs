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

    public void Start()
    {
        LoadBothWeaponsOnSlots();
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
        LoadWeaponOnSlot(character.characterEquipmentManager.m_CurrentHandRightWeapon, false);
        LoadWeaponOnSlot(character.characterEquipmentManager.m_CurrentHandLeftWeapon, true);
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
                    character.characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);
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
                character.characterEquipmentManager.m_CurrentHandLeftWeapon = unarmWeapon;
                leftHandSlot.currentWeapon = unarmWeapon;
                leftHandSlot.LoadWeaponModel(unarmWeapon);
                LoadLeftWeaponDamageCollider();
                //character.characterAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                character.characterEquipmentManager.m_CurrentHandRightWeapon = unarmWeapon;
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

        leftHandDamageCollider.physicalDamage = character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_iPhysicalDamage;
        leftHandDamageCollider.fireDamage = character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_iFireDamage;

        leftHandDamageCollider.characterManager = character;
        leftHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseDamage = character.characterEquipmentManager.m_CurrentHandLeftWeapon.poiseBreak;
        character.characterEffectsManager.leftWeaponManager = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponManager>();
    }

    protected virtual void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        if (rightHandDamageCollider == null)
            return;

        rightHandDamageCollider.physicalDamage = character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iPhysicalDamage;
        rightHandDamageCollider.fireDamage = character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iFireDamage;

        rightHandDamageCollider.characterManager = character;
        rightHandDamageCollider.teamIDNumber = character.characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseDamage = character.characterEquipmentManager.m_CurrentHandRightWeapon.poiseBreak;
        character.characterEffectsManager.rightWeaponManager = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponManager>();
    }

    public virtual void LoadTwoHandIKTargtets(bool isTwoHandingWeapon)
    {
        leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();

        if (leftHandIKTarget == null || rightHandIKTarget == null)
            return;

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

    public virtual void GrantWeaponAttackingPoiseBonus()
    {
        WeaponItem currentWeaponBeingUsed = character.characterEquipmentManager.currentItemBeingUsed as WeaponItem;
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.totalPoiseDefence + currentWeaponBeingUsed.offensivePoiseBonus;
    }

    public virtual void ResetWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.armorPoiseBonus;
    }
}
