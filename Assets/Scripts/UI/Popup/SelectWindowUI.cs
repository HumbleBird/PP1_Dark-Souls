using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWindowUI : UI_Popup
{
    GameUIManager m_GameUIManager;

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
        GetObject((int)GameObjects.SelectEquipment).BindEvent(() => ShowEquipemnt());
        GetObject((int)GameObjects.SelectGameOptions).BindEvent(() => ShowGameOption());

        m_GameUIManager = GetComponentInParent<GameUIManager>();

        return true;
    }

    public void ShowInventory()
    {
        m_GameUIManager.m_PlayerPrivateUI.m_goInventory.SetActive(true);
        Managers.UI.ClosePopupUI();
    }

    public void ShowEquipemnt()
    {
        m_GameUIManager.m_PlayerPrivateUI.m_Equipemnt.gameObject.SetActive(true);
        Managers.UI.ClosePopupUI();
    }

    public void ShowGameOption()
    {

    }
}
