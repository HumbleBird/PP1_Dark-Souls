using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

[CreateAssetMenu(menuName = "Items/Eqipment/Armor")]
public class EquipmentItem : Item
{
    public EquipmentArmorParts equipmentArmorParts;

    [Header("Defense Bonus")]
    public float physicalDefense;
    public float magicDefense;

    [Header("Weight")]
    public float weight = 0;

    [Header("Resistances")]
    public float poisonResistance;
}
