using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Magic : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,

        CurrentCountValueText, // ���� ������ / �ִ� ������
        SaveCountValueText, // ȭ��ҿ� ���� ������ �� / �ִ� ������ �� �ִ� ��
        FocusCostValueText,
        MagicUseSlotValueText,

        ItemEffectDescriptionText,

        AttributeRequirementIntelligenceValueText,
        AttributeRequirementFaithValueText
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);

        ShowReinforcedMaterialItemItem((SpellItem)item);
    }

    void ShowReinforcedMaterialItemItem(SpellItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;

        GetText((int)Texts.CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.SaveCountValueText).text = item.m_iCurrentCount.ToString();
        GetText((int)Texts.FocusCostValueText).text = item.focusPointCost.ToString();
        GetText((int)Texts.MagicUseSlotValueText).text = item.m_iMagicUseSlot.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();

        GetText((int)Texts.AttributeRequirementIntelligenceValueText).text = item.m_iAttributeRequirementIntelligence.ToString();
        GetText((int)Texts.AttributeRequirementFaithValueText).text = item.m_iAttributeRequirementFaith.ToString();

    }
}
