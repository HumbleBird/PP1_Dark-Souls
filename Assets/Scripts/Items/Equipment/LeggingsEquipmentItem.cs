using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Leggings Armor")]
public class LeggingsEquipmentItem : EquipmentItem
{
    [Header("Item Parts Name")]
    public string m_HipName;
    public string m_LeftLeggingName;
    public string m_RightLeggingName;
}
