using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipmentSlotUI : EquipmentSlotUI
{
    protected GameUIManager uiManager;

    public Image icon;
    WeaponItem equipmentItem;

    private void Awake()
    {
        uiManager = FindObjectOfType<GameUIManager>();
    }

    public void AddItem(WeaponItem newEquipmentItem)
    {
        if (newEquipmentItem != null)
        {
            equipmentItem = newEquipmentItem;
            icon.sprite = equipmentItem.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
    }

    public void ClearItem()
    {
        equipmentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool leftHandSlot01;
    public bool leftHandSlot02;

    public  void SelectThisSlot()
    {
        uiManager.ResetAllSelectedSlots();

        if(rightHandSlot01)
        {
            uiManager.rightHandSlot01Selected = true;
        }
        else if (rightHandSlot02)
        {
            uiManager.rightHandSlot02Selected = true;

        }
        else if (leftHandSlot01)
        {
            uiManager.leftHandSlot01Selected = true;

        }
        else
        {
            uiManager.leftHandSlot02Selected = true;

        }

        uiManager.itemStatWindowUI.UpdateWeaponItemStats(equipmentItem);
    }
}
