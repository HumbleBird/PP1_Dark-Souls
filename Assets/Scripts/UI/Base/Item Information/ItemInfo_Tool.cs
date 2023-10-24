using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Tool : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Tool_ItemNameText,
        Tool_ItemTypeText,
        Tool_CurrentCountValueText, // 현재 소지수 / 최대 소지수
        Tool_SaveCountValueText, // 화토불에 현재 저장한 수 / 최대 저장할 수 있는 수

        ItemEffectDescriptionText,
        
        Tool_AttributeBonusStrengthValueText,
        Tool_AttributeBonusDexterityValueText,
        Tool_AttributeBonusIntelligenceValueText,
        Tool_AttributeBonusFaithValueText,
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


        ShowToolItem((ToolItem)item);
    }

    void ShowToolItem(ToolItem item)
    {
        GetText((int)Texts.Tool_ItemNameText).text = item.itemName;
        GetText((int)Texts.Tool_ItemTypeText).text = item.m_ToolType.ToString();
        GetText((int)Texts.Tool_CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.Tool_SaveCountValueText).text = item.m_iCurrentCount.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();

        GetText((int)Texts.Tool_AttributeBonusStrengthValueText).text = item.m_iAttributeBonusStrength.ToString();
        GetText((int)Texts.Tool_AttributeBonusDexterityValueText).text = item.m_iAttributeBonusDexterity.ToString();
        GetText((int)Texts.Tool_AttributeBonusIntelligenceValueText).text = item.m_iAttributeBonusIntelligence.ToString();
        GetText((int)Texts.Tool_AttributeBonusFaithValueText).text = item.m_iAttributeBonusFaith.ToString();
    }
}
