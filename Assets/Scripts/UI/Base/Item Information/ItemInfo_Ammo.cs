using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Ammo : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Ammo_ItemNameText,
        Ammo_WeaponTypeText,
        Ammo_AttackTypeText,
        Ammo_CurrentCountValueText, // 현재 소지수 / 최대 소지수
        Ammo_SaveCountValueText, // 화토불에 현재 저장한 수 / 최대 저장할 수 있는 수

        // Attack Power
        Ammo_AttackPhysicalValueText,
        Ammo_AttackMagicValueText,
        Ammo_AttackFireValueText,
        Ammo_AttackLightningValueText,
        Ammo_AttackDarkValueText,
        Ammo_AttackCriticalValueText,

        //Additional Effects
        Ammo_AdditionalEffectBleedValueText,
        Ammo_AdditionalEffectPoisonValueText,
        Ammo_AdditionalEffectFrostValueText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        return true;
    }


    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);

        ShowItem((AmmoItem)item);
    }

    void ShowItem(AmmoItem item)
    {
        // Property
        GetText((int)Texts.Ammo_ItemNameText).text = item.itemName;
        GetText((int)Texts.Ammo_WeaponTypeText).text = item.ammoType.ToString();
        GetText((int)Texts.Ammo_AttackTypeText).text = item.AttackType.ToString();
        GetText((int)Texts.Ammo_CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.Ammo_SaveCountValueText).text = item.m_iCurrentCount.ToString();
        
        // Attack Power
        GetText((int)Texts.Ammo_AttackPhysicalValueText).text = item.m_iPhysicalDamage.ToString();
        GetText((int)Texts.Ammo_AttackMagicValueText).text = item.m_iMagicDamage.ToString();
        GetText((int)Texts.Ammo_AttackFireValueText).text = item.m_iFireDamage.ToString();
        GetText((int)Texts.Ammo_AttackLightningValueText).text = item.m_iLightningDamage.ToString();
        GetText((int)Texts.Ammo_AttackDarkValueText).text = item.m_iDarkDamage.ToString();
        GetText((int)Texts.Ammo_AttackCriticalValueText).text = item.m_iCriticalDamage.ToString();

        //Additional Effects
        GetText((int)Texts.Ammo_AdditionalEffectBleedValueText).text = item.m_iBleeding.ToString();
        GetText((int)Texts.Ammo_AdditionalEffectPoisonValueText).text = item.m_iPoison.ToString();
        GetText((int)Texts.Ammo_AdditionalEffectFrostValueText).text = item.m_iFrost.ToString();

    }
}