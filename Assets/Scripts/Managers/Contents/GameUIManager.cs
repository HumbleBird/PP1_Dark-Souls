using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIManager
{
    public InteractableAnnouncementPopupUI m_InteractableAnnouncementPopupUI;
    public InteractableNPCPopupUI m_InteractableNPCPopupUI;
    public GameSceneUI m_GameSceneUI;
    public bool m_isShowingInteratablePopup = false;
    public EquipmentUI m_EquipmentUI;

    public void ShowIntro(string path)
    {
        IntroUI m_IntroUI =  Managers.Resource.Instantiate("UI/Base/IntroUI").GetComponent<IntroUI>();
        m_IntroUI.PlayMP4(path);
    }

    public void UIEventTrigger(GameObject go, EventTriggerType type, Action action)
    {
        EventTrigger trigger = go.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener((data) => { UIEventTriggerAction((PointerEventData)data, action) ; });
        trigger.triggers.Add(entry);
    }

    void UIEventTriggerAction(PointerEventData data, Action action)
    {
        action.Invoke();
    }

    public T ShowPopupUI<T>(bool isPlayerStop = true, string name = null) where T : UI_Popup
    {
        if(isPlayerStop)
        {
            // UI 갯수 체크해서 마우스 커서 On/OFF
            if (Managers.UI._popupStack.Count == 0)
            {
                Managers.Cursor.PowerOn();
                Managers.Game.PlayerisStop();
            }
        }

        T popup =  Managers.UI.ShowPopupUI<T>();

        return popup;
    }

    public void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI();

        // UI 갯수 체크해서 마우스 커서 On/OFF

        if (Managers.UI._popupStack.Count == 0)
        {
            Managers.Cursor.PowerOff();
            Managers.GameUI.m_EquipmentUI = null;
            Managers.Game.PlayerisStop(false);
            m_InteractableNPCPopupUI = null;

        }
    }

    public void CloseAllPopupUI()
    {
        Managers.UI.CloseAllPopupUI();

        Managers.Cursor.PowerOff();
        Managers.GameUI.m_EquipmentUI = null;
        Managers.Game.PlayerisStop(false);
        m_InteractableNPCPopupUI = null;
    }

}
