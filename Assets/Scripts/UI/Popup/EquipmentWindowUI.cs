using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EquipmentWindowUI : MonoBehaviour
{
    public WeaponEquipmentSlotUI[] weaponEquipmentSlotsUI;
    public HeadEquipmentSlotUI headEquipmentSlotUI;
    public BodyEquipmentSlotUI bodyEquipmentSlotUI;
    public LegEquipmentSlotUI legEquipmentSlotUI;
    public HandEquipmentSlotUI handEquipmentSlotUI;

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
        // Head
        if(playerInventoryManager.currentHelmetEquipment != null)
        {
            headEquipmentSlotUI.AddItem(playerInventoryManager.currentHelmetEquipment);
        }
        else
        {
            headEquipmentSlotUI.ClearItem();
        }

        // Body
        if(playerInventoryManager.currentTorsoEquipment != null)
        {
            bodyEquipmentSlotUI.AddItem(playerInventoryManager.currentTorsoEquipment);
        }
        else
        {
            bodyEquipmentSlotUI.ClearItem();
        }

        // Leg
        if(playerInventoryManager.currentLegEquipment != null)
        {
            legEquipmentSlotUI.AddItem(playerInventoryManager.currentLegEquipment);
        }
        else
        {
            legEquipmentSlotUI.ClearItem();
        }

        // Hand
        if(playerInventoryManager.currentHandEquipment != null)
        {
            handEquipmentSlotUI.AddItem(playerInventoryManager.currentHandEquipment);
        }
        else
        {
            handEquipmentSlotUI.ClearItem();
        }
    }
}
