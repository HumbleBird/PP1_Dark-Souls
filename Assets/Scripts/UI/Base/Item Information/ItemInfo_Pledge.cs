using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Pledge : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Pledge_ItemNameText,

        ItemEffectDescriptionText,
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


        ShowToolItem((PledgeItem)item);
    }

    void ShowToolItem(PledgeItem item)
    {
        GetText((int)Texts.Pledge_ItemNameText).text = item.itemName;

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }
}
