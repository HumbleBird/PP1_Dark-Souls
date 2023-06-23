using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

[CreateAssetMenu(menuName = "Items/Eqipment/Armor")]
public class EquipmentItem : Item
{
    public EquipmentArmorParts equipmentArmorParts;

    [Header("Defense Bonus")]
    public float physicalDefense;
}
