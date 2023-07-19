using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public WeaponEquipmentSlotUI[] weaponEquipmentSlotsUI;
    public EquipmentSlotUI headEquipmentSlotUI;



    public void LoadWeaponsOnEquipmentScreen(PlayerInventoryManager playerInventoryManager)
    {
        for (int i = 0; i < weaponEquipmentSlotsUI.Length; i++)
        {
            if(weaponEquipmentSlotsUI[i].rightHandSlot01)
            {
                weaponEquipmentSlotsUI[i].AddItem(playerInventoryManager.weaponsInRightHandSlots[0]);
            }
            else if (weaponEquipmentSlotsUI[i].rightHandSlot02)
            {
                weaponEquipmentSlotsUI[i].AddItem(playerInventoryManager.weaponsInRightHandSlots[1]);
            }
            else if (weaponEquipmentSlotsUI[i].leftHandSlot01)
            {
                weaponEquipmentSlotsUI[i].AddItem(playerInventoryManager.weaponsInLeftHandSlots[0]);
            }
            else
            {
                weaponEquipmentSlotsUI[i].AddItem(playerInventoryManager.weaponsInLeftHandSlots[1]);
            }
        }   
    }

    public void LoadArmorOnEquipmentScreen(PlayerInventoryManager playerInventoryManager)
    {
        if(playerInventoryManager.currentHelmetEquipment != null)
        {
            headEquipmentSlotUI.AddItem(playerInventoryManager.currentHelmetEquipment);
        }
        else
        {
            headEquipmentSlotUI.ClearItem();
        }
    }


}
