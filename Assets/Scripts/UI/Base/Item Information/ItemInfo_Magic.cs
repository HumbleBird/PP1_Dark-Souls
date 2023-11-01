using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Magic : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Magic_ItemNameText,

        Magic_CurrentCountValueText, // 현재 소지수 / 최대 소지수
        Magic_SaveCountValueText, // 화토불에 현재 저장한 수 / 최대 저장할 수 있는 수
        Magic_FocusCostValueText,
        Magic_MagicUseSlotValueText,

        ItemEffectDescriptionText,

        Magic_AttributeRequirementIntelligenceValueText,
        Magic_AttributeRequirementFaithValueText
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

        ShowReinforcedMaterialItemItem((SpellItem)item);
    }

    void ShowReinforcedMaterialItemItem(SpellItem item)
    {
        GetText((int)Texts.Magic_ItemNameText).text = item.m_ItemName;

        GetText((int)Texts.Magic_CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.Magic_SaveCountValueText).text = item.m_iCurrentCount.ToString();
        GetText((int)Texts.Magic_FocusCostValueText).text = item.m_iCostFP.ToString();
        GetText((int)Texts.Magic_MagicUseSlotValueText).text = item.m_iMagicUseSlot.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();

        GetText((int)Texts.Magic_AttributeRequirementIntelligenceValueText).text = item.m_iAttributeRequirementIntelligence.ToString();
        GetText((int)Texts.Magic_AttributeRequirementFaithValueText).text = item.m_iAttributeRequirementFaith.ToString();

    }
}
