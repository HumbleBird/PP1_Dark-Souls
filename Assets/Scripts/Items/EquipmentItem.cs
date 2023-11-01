using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

[CreateAssetMenu(menuName = "Items/Eqipment/Armor")]
public class EquipmentItem : Item
{
    public EquipmentItem(int id)
    {

    }

    protected string playerGender
    {
        get {
            string gender = null;

            if (Managers.Object.m_MyPlayer.playerEquipmentManager.m_bIsFemale)
                gender = "Female_";
            else
                gender = "Male_";

            return gender; }
    }

    [Header("Defense Bonus")]
    public float m_fPhysicalDamageAbsorption     ;
    public float m_fVSStrikeDamageAbsorption           ;
    public float m_fVSSlashDamageAbsorption            ;
    public float m_fVSThrustDamageAbsorption           ;
    public float m_fMagicDamageAbsorption            ;
    public float m_fFireDamageAbsorption             ;
    public float m_fLightningDamageAbsorption            ;
    public float m_fDarkDamageAbsorption         ;

    [Header("Resistances")]
    public float   m_fBleedResistance ;
    public float   m_fPoisonResistance;
    public float   m_fFrostResistance ;
    public float   m_fCurseResistance ;
    public float   m_fPoiseResistance ;

    [Header("Durability")]
    public float m_fDurability = 0;

    [Header("Weight")]
    public float m_fWeight = 0;

    protected void SetInfo(int id)
    {
        Table_Item_Armor.Info data = Managers.Table.m_Item_Armor.Get(id);

        if (data == null)
            return;
        {
            Count = 1;
            m_bStackable = false;

            m_iItemID = data.m_nID; ;
            m_ItemName = data.m_sName; ;
            m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);

            if (data.m_iArmorType == 1)
                m_eItemType = E_ItemType.Helmet;
            if (data.m_iArmorType == 2)
                m_eItemType = E_ItemType.ChestArmor;
            if (data.m_iArmorType == 3)
                m_eItemType = E_ItemType.Gauntlets;
            if (data.m_iArmorType == 4)
                m_eItemType = E_ItemType.Leggings;

            m_fPhysicalDamageAbsorption = data.m_iDamage_Reduction_Physical;
            m_fVSStrikeDamageAbsorption = data.m_iDamage_Reduction_VSStrike;
            m_fVSSlashDamageAbsorption = data.m_iDamage_Reduction_VSSlash;
            m_fVSThrustDamageAbsorption = data.m_iDamage_Reduction_Thrust;
            m_fMagicDamageAbsorption = data.m_iDamage_Reduction_Magic;
            m_fFireDamageAbsorption = data.m_iDamage_Reduction_Fire;
            m_fLightningDamageAbsorption = data.m_iDamage_Reduction_Lightning;
            m_fDarkDamageAbsorption = data.m_iDamage_Reduction_Dark;
            m_fBleedResistance = data.m_iResistance_Bleeding;
            m_fPoisonResistance = data.m_iResistance_Posion;
            m_fFrostResistance = data.m_iResistance_Frost;
            m_fCurseResistance = data.m_iResistance_Curse;
            m_fPoiseResistance = data.m_fResistance_Poise;
            m_fDurability = data.m_iDurability;
            m_fWeight = data.m_fWeight;
        }
    }
}
