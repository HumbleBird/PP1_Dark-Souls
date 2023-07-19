using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    protected GameUIManager uiManager;

    public Image icon;
    Item equipmentItem;

    private void Awake()
    {
        uiManager = FindObjectOfType<GameUIManager>();
    }

    public void AddItem(Item newEquipmentItem)
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

    public virtual void SelectThisSlot()
    {

    }
}
