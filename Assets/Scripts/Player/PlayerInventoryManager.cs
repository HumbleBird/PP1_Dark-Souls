using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public List<Item> m_Item = new List<Item>();

    public List<WeaponItem> weaponsInventory = new List<WeaponItem>();
    public List<HelmEquipmentItem> headEquipmentInventory = new List<HelmEquipmentItem>();
    public List<TorsoEquipmentItem> bodyEquipmentInventory = new List<TorsoEquipmentItem>();
    public List<LeggingsEquipmentItem> legEquipmentInventory = new List<LeggingsEquipmentItem>();
    public List<GantletsEquipmentItem> handEquipmentInventory = new List<GantletsEquipmentItem>();

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                character.characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
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
            rightWeapon = character.characterWeaponSlotManager.unarmWeapon;
            character.characterWeaponSlotManager.LoadWeaponOnSlot(character.characterWeaponSlotManager.unarmWeapon, false);

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
                character.characterWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
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
            leftWeapon = character.characterWeaponSlotManager.unarmWeapon;
            character.characterWeaponSlotManager.LoadWeaponOnSlot(character.characterWeaponSlotManager.unarmWeapon, true);
        }
    }

    public void InventoryItemClear()
    {
        weaponsInventory.Clear();
        headEquipmentInventory.Clear();
        bodyEquipmentInventory.Clear();
        legEquipmentInventory.Clear();
        handEquipmentInventory.Clear();
    }
}
