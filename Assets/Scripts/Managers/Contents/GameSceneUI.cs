
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneUI : UI_Scene
{
    enum GameObjects
    {
        StatBars,
        Crosshair,
    }

    enum Texts
    {
        SoulCountText,
    }

    public QuickSlotsUI quickSlotsUI;
    public TextMeshProUGUI m_textSoulCount;
    public GameObject m_goStatBars;
    public GameObject m_goCrosshair;
    public UIBossHealthBar m_BossHealthBar;

    PlayerManager m_Player;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        m_textSoulCount = GetText((int)Texts.SoulCountText);
        m_goStatBars = GetObject((int)GameObjects.StatBars);
        m_goCrosshair = GetObject((int)GameObjects.Crosshair);

        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        m_BossHealthBar = GetComponentInChildren<UIBossHealthBar>();

        m_goCrosshair.SetActive(false);
        return true;
    }

    public void Start()
    {
        m_Player = Managers.Object.m_MyPlayer;

        m_textSoulCount.text = m_Player.playerStatsManager.currentSoulCount.ToString();
    }

    public override void RefreshUI()
    {
        base.RefreshUI();

        m_textSoulCount.text = m_Player.playerStatsManager.currentSoulCount.ToString();
    }
}
