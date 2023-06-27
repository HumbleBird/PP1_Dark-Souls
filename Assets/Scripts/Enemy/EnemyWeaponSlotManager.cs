using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{
    EnemyStatus enemyStatus;

    public WeaponItem rightHandWeapon;
    public WeaponItem leftHandWeapon;

    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    private void Awake()
    {
        enemyStatus = GetComponentInParent<EnemyStatus>();
        LoadWeaponHolderSlots();
    }

    private void LoadWeaponHolderSlots()
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
        }
    }

    private void Start()
    {
        LoadWeaponsOnBothHands();
    }

    private void LoadWeaponsOnBothHands()
    {
        if (rightHandWeapon != null)
        {
            LoadWeaponOnSlot(rightHandWeapon, false);
        }
        else if (leftHandWeapon != null)
        {
            LoadWeaponOnSlot(leftHandWeapon, false);
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.currentWeapon = weaponItem;
            leftHandSlot.LoadWeaponModel(weaponItem);
        }
        else
        {
            rightHandSlot.currentWeapon = weaponItem;
            rightHandSlot.LoadWeaponModel(weaponItem);
        }

        LoadWeaponDamageCollider(isLeft);
    }

    private void LoadWeaponDamageCollider(bool isLeft)
    {
        if(isLeft)
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }
        else
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }
    }

    public void OpenDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }


    public void DrainStaminaLightAttack()
    {
    }

    public void DrainStaminaHeavyAttack()
    {
    }

    public void EnableCombo()
    {
        //anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
       // anim.SetBool("canDoCombo", false);

    }


    #region Handle Weapon's Poise Bonus

    public void GrantWeaponAttackingPoiseBonus()
    {
        enemyStatus.totalPoiseDefence = enemyStatus.totalPoiseDefence + enemyStatus.offensivePoiseBonus;
    }


    public void ResetWeaponAttackingPoiseBonus()
    {
        enemyStatus.totalPoiseDefence = enemyStatus.armorPoiseBonus;
    }

    #endregion
}
