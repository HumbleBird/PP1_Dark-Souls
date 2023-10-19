using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Ring : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,
        EquipLoadValueText,

        ItemEffectDescriptionText,
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        ShowToolItem((RingItem)item);
    }

    void ShowToolItem(RingItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.EquipLoadValueText).text = item.m_iWeight.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }
}