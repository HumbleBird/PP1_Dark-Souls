using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegEquipmentInventorySlot : MonoBehaviour
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
        if (uiManager.legEquipmentSlotSelected)
        {
            if (uiManager.player.playerInventoryManager.currentLegEquipment != null)
            {
                uiManager.player.playerInventoryManager.legEquipmentInventory.Add(uiManager.player.playerInventoryManager.currentLegEquipment);
            }

            uiManager.player.playerInventoryManager.currentLegEquipment = (LeggingsEquipmentItem)item;
            uiManager.player.playerInventoryManager.legEquipmentInventory.Remove((LeggingsEquipmentItem)item);
            uiManager.player.playerEquipmentManager.EquipAllEquipmentModel();
        }
        else
        {
            return;
        }

        uiManager.equipmentWindowUI.LoadArmorOnEquipmentScreen(uiManager.player.playerInventoryManager);
        uiManager.ResetAllSelectedSlots();
    }
}
