using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

[CreateAssetMenu(menuName = "Items/Eqipment/Armor")]
public class EquipmentItem : Item
{
    [Header("Defense Bonus")]
    public float m_fPhysicalDefense;
    public float m_fStrikeDefense;
    public float m_fSlashDefense;
    public float m_fThrustDefense;
    public float m_fMagicDefense;
    public float m_fFireDefense;
    public float m_fLightningDefense;
    public float m_fDarkDefense;

    [Header("Resistances")]
    public float m_fBleedResistance;
    public float m_fPoisonResistance;
    public float m_fFrostResistance;
    public float m_fCurseResistance;
    public float m_fPoiseResistance;

    [Header("Durability")]
    public float m_fDurability = 0;

    [Header("Weight")]
    public float m_fWeight = 0;
}
