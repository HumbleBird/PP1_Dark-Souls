using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager
{
    public InteractablePopupUI m_InteractablePopupUI;
    public GameSceneUI m_GameSceneUI;
    public bool m_isShowingInteratablePopup = false;

    public void ShowIntro(string path)
    {
        IntroUI m_IntroUI =  Managers.Resource.Instantiate("UI/Base/IntroUI").GetComponent<IntroUI>();
        m_IntroUI.PlayMP4(path);
    }
}
