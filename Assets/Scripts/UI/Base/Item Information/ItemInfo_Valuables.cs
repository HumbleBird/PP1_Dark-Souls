using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Valuables : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,
        ItemTypeText,
        CountValueText, // 현재 소지수 / 최대 소지수
        SaveCountValueText, // 화토불에 현재 저장한 수 / 최대 저장할 수 있는 수

        ItemEffectDescriptionText,

        ParameterBonusStrengthValueText,
        ParameterBonusDexterityValueText,
        ParameterBonusIntelligenceValueText,
        ParameterBonusFaithValueText,
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        ShowToolItem((ToolItem)item);
    }

    void ShowToolItem(ToolItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.ItemTypeText).text = item.m_ToolType.ToString();
        GetText((int)Texts.CountValueText).text = item.m_iCurrentCount.ToString() + " / " + item.m_iMaxCount.ToString();
        GetText((int)Texts.SaveCountValueText).text = item.m_iCurrentCount.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();

        GetText((int)Texts.ParameterBonusStrengthValueText).text = item.m_iAttributeBonusStrength.ToString();
        GetText((int)Texts.ParameterBonusDexterityValueText).text = item.m_iAttributeBonusDexterity.ToString();
        GetText((int)Texts.ParameterBonusIntelligenceValueText).text = item.m_iAttributeBonusIntelligence.ToString();
        GetText((int)Texts.ParameterBonusFaithValueText).text = item.m_iAttributeBonusFaith.ToString();
    }
}