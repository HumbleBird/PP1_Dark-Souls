using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIManager
{
    public InteractablePopupUI m_InteractablePopupUI;
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
}
