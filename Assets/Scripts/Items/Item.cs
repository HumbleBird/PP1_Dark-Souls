using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Item : ScriptableObject
{
    [Header("Item Infomation")]
    public Sprite itemIcon;
    public string itemName;
    public int itemID;
    public E_ItemType m_EItemType;
    public bool m_isEquiping = false;
    public E_EquipmentSlotsPartType m_EquipingType;
    public int m_iEquipSlotNum;
}
