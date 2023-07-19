using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEquipmentInventorySlot : MonoBehaviour
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
        if (uiManager.headEquipmentSlotSelected)
        {
            if(uiManager.player.playerInventoryManager.currentHelmetEquipment != null)
            {
                uiManager.player.playerInventoryManager.headEquipmentInventory.Add(uiManager.player.playerInventoryManager.currentHelmetEquipment);
            }

            uiManager.player.playerInventoryManager.currentHelmetEquipment = item;
            uiManager.player.playerInventoryManager.headEquipmentInventory.Remove(item);
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
