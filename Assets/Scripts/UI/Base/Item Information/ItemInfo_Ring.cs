using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Ring : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Ring_ItemNameText,
        Ring_EquipLoadValueText,

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


        ShowToolItem((RingItem)item);
    }

    void ShowToolItem(RingItem item)
    {
        GetText((int)Texts.Ring_ItemNameText).text = item.m_ItemName;
        GetText((int)Texts.Ring_EquipLoadValueText).text = item.m_iWeight.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();
    }
}