
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;

public class GameSceneUI : UI_Scene
{
    [Header("Childes")]
    public HUD_SoulUI m_HUD_SoulUI;
    public QuickSlotsUI quickSlotsUI;
    public StatBarsUI m_StatBarsUI;
    public GameObject m_goCrosshair;
    public UIBossHealthBar m_BossHealthBar;
    public AreaUI m_AreaUI;

    Animator m_animator;

    PlayerManager m_Player;

    public FadeInOutScreenUI m_FadeInOutScreenUI;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        m_BossHealthBar = GetComponentInChildren<UIBossHealthBar>();
        m_StatBarsUI = GetComponentInChildren<StatBarsUI>();
        m_AreaUI = GetComponentInChildren<AreaUI>();
        m_HUD_SoulUI = GetComponentInChildren<HUD_SoulUI>();

        m_FadeInOutScreenUI = GetComponentInChildren<FadeInOutScreenUI>();

        m_animator = GetComponent<Animator>();

        Managers.GameUI.m_GameSceneUI = this;

        return true;
    }

    public void Start()
    {
        m_Player = Managers.Object.m_MyPlayer;
    }

    public void RefreshUI(E_StatUI stat = E_StatUI.All)
    {
        base.RefreshUI();

        m_StatBarsUI.RefreshUI(stat);
    }

    public void SoulsRefreshUI(bool isSousAdd, int addsous, bool isImmediately = false)
    {
        if(isSousAdd)
        {
            m_HUD_SoulUI.AddSouls(addsous, isImmediately);
        }
        else
        {
            m_HUD_SoulUI.LoseSouls(isImmediately);
        }
    }

    public void ShowYouDied()
    {
        m_animator.Play("YouDied");
    }

    public void ShowSoulsRetrieved()
    {
        m_animator.Play("SoulsRetrieved");
    }

    public void DeadSoundPlay()
    {
        Managers.Sound.Play("Character/Player/YOU DIED");
    }


    public void SoulsRetrievedSoundPlay()
    {
        Managers.Sound.Play("Object/Souls Retrieved");
    }
}
