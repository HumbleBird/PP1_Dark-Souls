using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public WeaponItem unarmWeapon;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];

    public int currentRightWeaponIndex = -1;
    public int currentLeftWeaponIndex = -1;

    public List<WeaponItem> weaponsInventory;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
        rightWeapon = unarmWeapon;
        leftWeapon = unarmWeapon;
    }

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
                break;
            }
            else if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] == null)
            {
                currentRightWeaponIndex += 1;
            }
        }

        if(currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            currentRightWeaponIndex = -1;
            rightWeapon = unarmWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmWeapon, false);

        }
    }

    public void ChangeLeftWeapon()
    {
        currentLeftWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentLeftWeaponIndex == index && weaponsInLeftHandSlots[index] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
                break;
            }
            else if (currentLeftWeaponIndex == index && weaponsInLeftHandSlots[index] == null)
            {
                currentLeftWeaponIndex += 1;
            }
        }

        if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            currentLeftWeaponIndex = -1;
            leftWeapon = unarmWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmWeapon, true);
        }
    }
}
