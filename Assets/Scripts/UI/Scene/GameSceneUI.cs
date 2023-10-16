
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;

public class GameSceneUI : UI_Scene
{
    enum Texts
    {
        SoulCountText,
    }

    [Header("Childes")]
    public TextMeshProUGUI m_textSoulCount;
    public QuickSlotsUI quickSlotsUI;
    public StatBarsUI m_StatBarsUI;
    public GameObject m_goCrosshair;
    public UIBossHealthBar m_BossHealthBar;
    public AreaUI m_AreaUI;
    public InteractablePopupUI m_InteractablePopupUI;

    Animator m_animator;

    PlayerManager m_Player;

    public FadeInOutScreenUI m_FadeInOutScreenUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        m_textSoulCount = GetText((int)Texts.SoulCountText);

        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        m_BossHealthBar = GetComponentInChildren<UIBossHealthBar>();
        m_StatBarsUI = GetComponentInChildren<StatBarsUI>();
        m_AreaUI = GetComponentInChildren<AreaUI>();
        m_InteractablePopupUI = GetComponentInChildren<InteractablePopupUI>();

        m_FadeInOutScreenUI = GetComponentInChildren<FadeInOutScreenUI>();

        m_animator = GetComponent<Animator>();

        Managers.GameUI.m_GameSceneUI = this;

        return true;
    }

    public void Start()
    {
        m_Player = Managers.Object.m_MyPlayer;

        RefreshUI();
    }

    public void RefreshUI(E_StatUI stat = E_StatUI.All)
    {
        base.RefreshUI();

        m_textSoulCount.text = m_Player.playerStatsManager.currentSoulCount.ToString();
        m_StatBarsUI.RefreshUI(stat);
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
