using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyEquipmentInventorySlot : MonoBehaviour
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
        if (uiManager.bodyEquipmentSlotSelected)
        {
            if (uiManager.player.playerInventoryManager.currentTorsoEquipment != null)
            {
                uiManager.player.playerInventoryManager.bodyEquipmentInventory.Add(uiManager.player.playerInventoryManager.currentTorsoEquipment);
            }

            uiManager.player.playerInventoryManager.currentTorsoEquipment = item;
            uiManager.player.playerInventoryManager.bodyEquipmentInventory.Remove(item);
            uiManager.player.playerEquipmentManager.EquipAllEquipmentModelsOnStart();
        }
        else
        {
            return;
        }

        uiManager.equipmentWindowUI.LoadArmorOnEquipmentScreen(uiManager.player.playerInventoryManager);
        uiManager.ResetAllSelectedSlots();
    }
}
