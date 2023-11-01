using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassGear
{


    [Header("Class Name")]
    public string className;

    [Header("Weapons")]
    public WeaponItem primaryWeapon;
    public WeaponItem offHandWeapon;
    //public WeaponItem secondaryWeapon;

    [Header("Armor")]
    public HelmEquipmentItem headEquipment;
    public TorsoEquipmentItem chestEquipment;
    public LeggingsEquipmentItem legEquipment;
    public GantletsEquipmentItem handEquipment;

    public SpellItem m_SpellItem_1;
    public SpellItem m_SpellItem_2;
    public RingItem m_RingItem;
    public ToolItem m_ToolItem1;
}
