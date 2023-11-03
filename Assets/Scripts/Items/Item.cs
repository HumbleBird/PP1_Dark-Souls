using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Item : ScriptableObject
{
    [Header("Item Infomation")]
    public Sprite m_ItemIcon;
    public string m_ItemName;
    public int m_iItemID;
    public E_ItemType m_eItemType;
    public bool m_isEquiping = false;
    public E_EquipmentSlotsPartType m_EquipingType;
    public int m_iEquipSlotNum;
    public int Count { get; set; }
    public bool m_bStackable { get; protected set; }
    public bool m_bEquipped { get; set; } = false;
    public bool m_bIsUnarmed = false;
    public string m_sPrefabPath;
    public GameObject m_goPrefab;
}
