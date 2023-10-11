using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class CharacterCreationScreen : UI_Scene
{
    public CharacterCreationLeftPannelUI m_CharacterCreationLeftPannelUI;
    public CharacterCreationMiddlePannelUI m_CharacterCreationMiddlePannelUI;
    public CharacterCreationRightPannelUI m_CharacterCreationRightPannelUI;
    public ConfirmationUI m_ConfirmationUI;

    public PlayerManager Player;

    public FadeInOutScreenUI m_FadeInOutScreenUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationLeftPannelUI = GetComponentInChildren<CharacterCreationLeftPannelUI>();
        m_CharacterCreationMiddlePannelUI = GetComponentInChildren<CharacterCreationMiddlePannelUI>();
        m_CharacterCreationRightPannelUI = GetComponentInChildren<CharacterCreationRightPannelUI>();
        m_ConfirmationUI = GetComponentInChildren<ConfirmationUI>();

        m_FadeInOutScreenUI = FindObjectOfType<FadeInOutScreenUI>();

        return true;
    }

    private void Start()
    {
        Player = Managers.Object.m_MyPlayer;
        m_CharacterCreationRightPannelUI.SetInfo();
        m_CharacterCreationMiddlePannelUI.SetInfo();
        m_CharacterCreationLeftPannelUI.SetInfo();

        m_CharacterCreationRightPannelUI.PostSetInfo();
        m_CharacterCreationMiddlePannelUI.PostSetInfo();
        m_CharacterCreationLeftPannelUI.PostSetInfo();

        m_ConfirmationUI.gameObject.SetActive(false);
    }
}
