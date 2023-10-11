using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager
{
    public InteractablePopupUI m_InteractablePopupUI;

    public void ShowIntro(string path)
    {
        IntroUI m_IntroUI = Managers.UI.ShowPopupUI<IntroUI>();
        m_IntroUI.PlayMP4(path);
    }
}
