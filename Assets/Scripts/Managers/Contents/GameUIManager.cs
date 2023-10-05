
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : UI_Base
{
    public PlayerManager player;

    [Header("HUD Window UI")]
    public HUDUI m_HUDUI;

    [Header("Interact Window UI")]
    public InteractablePopupUI m_InteractablePopupUI;

    public bool m_bIsShowingPopup = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_HUDUI = GetComponentInChildren<HUDUI>();

        return true;
    }
}
