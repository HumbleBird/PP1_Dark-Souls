using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public List<WeaponItem> weaponsInventory;

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
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
            rightWeapon = characterWeaponSlotManager.unarmWeapon;
            characterWeaponSlotManager.LoadWeaponOnSlot(characterWeaponSlotManager.unarmWeapon, false);

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
                characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
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
            leftWeapon = characterWeaponSlotManager.unarmWeapon;
            characterWeaponSlotManager.LoadWeaponOnSlot(characterWeaponSlotManager.unarmWeapon, true);
        }
    }
}
