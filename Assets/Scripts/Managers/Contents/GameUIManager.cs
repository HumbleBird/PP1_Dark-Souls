using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager
{
    public GameSceneUI m_GameSceneUI;
    public bool m_isShowingInteratablePopup = false;

    public void ShowIntro(string path)
    {
        IntroUI m_IntroUI = Managers.UI.ShowPopupUI<IntroUI>();
        m_IntroUI.PlayMP4(path);
    }
}
