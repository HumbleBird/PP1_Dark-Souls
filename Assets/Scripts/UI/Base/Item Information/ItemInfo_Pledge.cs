using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Pledge : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,

        ItemEffectDescriptionText,
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        ShowToolItem((PledgeItem)item);
    }

    void ShowToolItem(PledgeItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }
}
