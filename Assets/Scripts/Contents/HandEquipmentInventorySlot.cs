using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentInventorySlot : MonoBehaviour
{
    GameUIManager uiManager;

    public Image icon;
    EquipmentItem item;

    private void Awake()
    {
        uiManager = GetComponentInParent<GameUIManager>();
    }

    public void AddItem(EquipmentItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if (uiManager.handEquipmentSlotSelected)
        {
            if (uiManager.player.playerInventoryManager.currentHandEquipment != null)
            {
                uiManager.player.playerInventoryManager.handEquipmentInventory.Add(uiManager.player.playerInventoryManager.currentHandEquipment);
            }

            uiManager.player.playerInventoryManager.currentHandEquipment = (GantletsEquipmentItem)item;
            uiManager.player.playerInventoryManager.handEquipmentInventory.Remove((GantletsEquipmentItem)item);
            uiManager.player.playerEquipmentManager.EquipAllEquipmentModel();
        }
        else
        {
            return;
        }

        uiManager.m_HUDUI.equipmentWindowUI.LoadArmorOnEquipmentScreen(uiManager.player.playerInventoryManager);
        uiManager.ResetAllSelectedSlots();
    }
}
