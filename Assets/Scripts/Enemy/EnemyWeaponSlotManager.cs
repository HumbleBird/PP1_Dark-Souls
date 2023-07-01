using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
{
    EnemyStatsManager enemyStatsManager;
    EnemyEffectsManager enemyEffectsManager;

    public WeaponItem rightHandWeapon;
    public WeaponItem leftHandWeapon;

    private void Awake()
    {
        enemyStatsManager = GetComponent<EnemyStatsManager>();
        enemyEffectsManager = GetComponent<EnemyEffectsManager>();
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

            leftHandDamageCollider.physicalDamage = leftHandWeapon.physicalDamage;
            leftHandDamageCollider.fireDamage = leftHandWeapon.fireDamage;

            leftHandDamageCollider.teamIDNumber = enemyStatsManager.teamIDNumber;

            enemyEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
        }
        else
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();


            rightHandDamageCollider.physicalDamage = rightHandWeapon.physicalDamage;
            rightHandDamageCollider.fireDamage = rightHandWeapon.fireDamage;

            rightHandDamageCollider.teamIDNumber = enemyStatsManager.teamIDNumber;

            enemyEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
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
        enemyStatsManager.totalPoiseDefence = enemyStatsManager.totalPoiseDefence + enemyStatsManager.offensivePoiseBonus;
    }


    public void ResetWeaponAttackingPoiseBonus()
    {
        enemyStatsManager.totalPoiseDefence = enemyStatsManager.armorPoiseBonus;
    }

    #endregion
}
