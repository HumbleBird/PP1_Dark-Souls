using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        PointerDown(GetObject((int)GameObjects.SelectInventory));
        PointerDown(GetObject((int)GameObjects.SelectEquipment));
        PointerDown(GetObject((int)GameObjects.SelectGameOptions));

        return true;
    }

    public void ShowInventory()
    {
        Managers.Sound.Play("UI/Popup_ButtonShow");
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<InventoryUI>();
    }

    public void ShowEquipment()
    {
        Managers.Sound.Play("UI/Popup_ButtonShow");
        Managers.UI.ClosePopupUI();
        Managers.GameUI.m_EquipmentUI = Managers.UI.ShowPopupUI<EquipmentUI>();
        
    }

    public void ShowGameOption()
    {
        Managers.Sound.Play("UI/Popup_ButtonShow");
        Managers.UI.ClosePopupUI();

    }

    void PointerDown(GameObject go)
    {
        EventTrigger trigger = go.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { PointerDownSoundPlay((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void PointerDownSoundPlay(PointerEventData data)
    {
        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
    }
}
