using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_ReinforcedMaterial_Valuables : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,

        CurrentCountValueText, // ���� ������ / �ִ� ������
        SaveCountValueText, // ȭ��ҿ� ���� ������ �� / �ִ� ������ �� �ִ� ��

        ItemEffectDescriptionText
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        if(item.m_EItemType == Define.E_ItemType.ReinforcedMaterial)
            ShowReinforcedMaterialItemItem((ReinforcedMaterialItem)item);
        else if (item.m_EItemType == Define.E_ItemType.Valuables)
            ShowValuablesItemMaterialItemItem((ValuablesItem)item);
    }

    void ShowReinforcedMaterialItemItem(ReinforcedMaterialItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.SaveCountValueText).text = item.m_iCurrentCount.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }

    void ShowValuablesItemMaterialItemItem(ValuablesItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.CurrentCountValueText).text = item.m_iCurrentCount.ToString() + "/ " + item.m_iMaxCount.ToString();
        GetText((int)Texts.SaveCountValueText).text = item.m_iCurrentCount.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }
}