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
    public ItemType m_EItemType;
}
