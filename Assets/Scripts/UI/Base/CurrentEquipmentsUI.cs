using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEquipmentsUI : UI_Base
{
    enum Texts
    {
        EquipmentSlotsNameText,
        ItemNameText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        PlayerManager player = Managers.Object.m_MyPlayer;

        return true;
    }

    public void ShowItemInformation(string itemName, string slotPartName)
    {
        GetText((int)Texts.EquipmentSlotsNameText).text = slotPartName;
        if(itemName != null)
        {
            GetText((int)Texts.ItemNameText).text = itemName;
        }
        else
        {
            GetText((int)Texts.ItemNameText).text = "";
        }
    }
}
