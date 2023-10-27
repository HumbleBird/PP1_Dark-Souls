using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

[CreateAssetMenu(menuName = "Items/Eqipment/Armor")]
public class EquipmentItem : Item
{
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
    public int m_fBleedResistance ;
    public int m_fPoisonResistance;
    public int m_fFrostResistance ;
    public int m_fCurseResistance ;
    public int m_fPoiseResistance ;

    [Header("Durability")]
    public float m_fDurability = 0;

    [Header("Weight")]
    public float m_fWeight = 0;
}
