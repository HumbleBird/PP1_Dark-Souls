using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Ammo : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,
        WeaponTypeText,
        AttackTypeText,
        CurrentCountValueText, // ���� ������ / �ִ� ������
        SaveCountValueText, // ȭ��ҿ� ���� ������ �� / �ִ� ������ �� �ִ� ��

        // Attack Power
        AttackPhysicalValueText,
        AttackMagicValueText,
        AttackFireValueText,
        AttackLightningValueText,
        AttackDarkValueText,
        AttackCriticalValueText,
        AttackRangeValueText,
        MagicAdjustmentValueText,

        //Additional Effects
        AdditionalEffectBleedValueText,
        AdditionalEffectPoisonValueText,
        AdditionalEffectFrostValueText,
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);

        ShowItem((RangedAmmoItem)item);
    }

    void ShowItem(RangedAmmoItem item)
    {
        // Property
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.WeaponTypeText).text = item.ammoType.ToString();
        GetText((int)Texts.AttackTypeText).text = item.AttackType.ToString();
        GetText((int)Texts.CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.SaveCountValueText).text = item.m_iCurrentCount.ToString();
        
        // Attack Power
        GetText((int)Texts.AttackPhysicalValueText).text = item.m_iPhysicalDamage.ToString();
        GetText((int)Texts.AttackMagicValueText).text = item.m_iMagicDamage.ToString();
        GetText((int)Texts.AttackFireValueText).text = item.m_iFireDamage.ToString();
        GetText((int)Texts.AttackLightningValueText).text = item.m_iLightningDamage.ToString();
        GetText((int)Texts.AttackDarkValueText).text = item.m_iDarkDamage.ToString();
        GetText((int)Texts.AttackCriticalValueText).text = item.m_iCriticalDamage.ToString();

        //Additional Effects
        GetText((int)Texts.AdditionalEffectBleedValueText).text = item.m_iBleeding.ToString();
        GetText((int)Texts.AdditionalEffectPoisonValueText).text = item.m_iPoison.ToString();
        GetText((int)Texts.AdditionalEffectFrostValueText).text = item.m_iFrost.ToString();

    }
}