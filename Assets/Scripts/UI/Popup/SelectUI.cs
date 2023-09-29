using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : UI_Popup
{

    enum GameObjects
    {
        SelectInventory,
        SelectEquipment,
        SelectGameOptions
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.SelectInventory).BindEvent(() => ShowInventory());
        GetObject((int)GameObjects.SelectEquipment).BindEvent(() => ShowEquipment());
        GetObject((int)GameObjects.SelectGameOptions).BindEvent(() => ShowGameOption());

        return true;
    }

    public void ShowInventory()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<InventoryUI>();
    }

    public void ShowEquipment()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<EquipmentUI>();
    }

    public void ShowGameOption()
    {
        Managers.UI.ClosePopupUI();

    }
}
