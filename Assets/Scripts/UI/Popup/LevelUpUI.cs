using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Define;

public class LevelUpUI : UI_Popup
{
    enum Texts
    {
        // Player Level
        CurrentPlayerLevelText,
        ProjectedPlayerLevelText,

        // Souls
        CurrentSoulsText,
        CaluateToSoulsText,
        SoulsRequiredToLevelUpText,

        // Attributes
        CurrentVigorLevelText,
        CurrentAttunementLevelText,
        CurrentEnduranceLevelText,
        CurrentVitalityLevelText,
        CurrentStrengthLevelText,
        CurrentDexterityLevelText,
        CurrentIntelligenceLevelText,
        CurrentFaithLevelText,
        CurrentLuckLevelText,

        ProjectedVigorLevelText         ,
        ProjectedAttunementLevelText            ,
        ProjectedEnduranceLevelText     ,
        ProjectedVitalityLevelText      ,
        ProjectedStrengthLevelText      ,
        ProjectedDexterityLevelText     ,
        ProjectedIntelligenceLevelText ,
        ProjectedFaithLevelText,
        ProjectedLuckLevelText
    }

    enum GameObjects
    {
        VigorArrow              ,
        AttunementArrow         ,
        EnduranceArrow          ,
        VitalityArrow           ,
        StrengthArrow           ,
        DexterityArrow              ,
        FaithArrow              ,
        IntelligenceArrow           ,
        LuckArrow,

        ConfirmBtn,
    }

    public PlayerManager playerManager;

    int m_iCurrentPlayerLevel               ;
    int m_iProjectedPlayerLevel             ;
    int m_iCurrentSouls                     ;
    int m_iRequiredSouls                    ;
    int m_iProjectedVigorLevel              ;
    int m_iProjectedAttunementLevel         ;
    int m_iProjectedEnduranceLevel                  ;
    int m_iProjectedVitalityLevel                   ;
    int m_iProjectedStrengthLevel                   ;
    int m_iProjectedDexterityLevel                  ;
    int m_iProjectedFaithLevel              ;
    int m_iProjectedIntelligenceLevel           ;
    int m_iProjectedLuckLevel               ;

    public int baseLevelUpCost = 5;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.VigorArrow        ).BindEvent(() => UpdateAttributesStat(0)); ;
        GetObject((int)GameObjects.AttunementArrow   ).BindEvent(() => UpdateAttributesStat(1)); ;
        GetObject((int)GameObjects.EnduranceArrow    ).BindEvent(() => UpdateAttributesStat(2)); ;
        GetObject((int)GameObjects.VitalityArrow     ).BindEvent(() => UpdateAttributesStat(3)); ;
        GetObject((int)GameObjects.StrengthArrow     ).BindEvent(() => UpdateAttributesStat(4)); ;
        GetObject((int)GameObjects.DexterityArrow    ).BindEvent(() => UpdateAttributesStat(5)); ;
        GetObject((int)GameObjects.FaithArrow        ).BindEvent(() => UpdateAttributesStat(6)); ;
        GetObject((int)GameObjects.IntelligenceArrow ).BindEvent(() => UpdateAttributesStat(7)); ;
        GetObject((int)GameObjects.LuckArrow ).BindEvent(() => UpdateAttributesStat(8)); ;

        GetObject((int)GameObjects.ConfirmBtn ).BindEvent(() => ConfirmPlayerLevelUpStates()); ;

        return true;
    }

    public void Start()
    {
        playerManager = Managers.Object.m_MyPlayer;

        InfoReset();

        gameObject.SetActive(false);
    }

    // 초기 셋팅
    private void OnEnable()
    {
        if (playerManager == null)
            return;

        InfoReset();
    }

    void InfoReset()
    {
        // 플레이어 레벨
        m_iCurrentPlayerLevel = playerManager.playerStatsManager.playerLevel;
        GetText((int)Texts.CurrentPlayerLevelText).text = m_iCurrentPlayerLevel.ToString();
        GetText((int)Texts.ProjectedPlayerLevelText).text = m_iCurrentPlayerLevel.ToString();

        // 소울
        m_iCurrentSouls = playerManager.playerStatsManager.currentSoulCount;
        GetText((int)Texts.CurrentSoulsText).text = m_iCurrentSouls.ToString();
        GetText((int)Texts.SoulsRequiredToLevelUpText).text = "0";
        GetText((int)Texts.SoulsRequiredToLevelUpText).color = new Color(1f, 1f, 1f);

        // 스텟
        m_iProjectedVigorLevel = playerManager.playerStatsManager.m_iVigorLevel;
        GetText((int)Texts.CurrentVigorLevelText).text = m_iProjectedVigorLevel.ToString();
        GetText((int)Texts.ProjectedVigorLevelText).text = m_iProjectedVigorLevel.ToString();
        GetText((int)Texts.ProjectedVigorLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedAttunementLevel = playerManager.playerStatsManager.m_iAttunementLevel;
        GetText((int)Texts.CurrentAttunementLevelText).text = m_iProjectedAttunementLevel.ToString();
        GetText((int)Texts.ProjectedAttunementLevelText).text = m_iProjectedAttunementLevel.ToString();
        GetText((int)Texts.ProjectedAttunementLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedEnduranceLevel = playerManager.playerStatsManager.m_iEnduranceLevel;
        GetText((int)Texts.CurrentEnduranceLevelText).text = m_iProjectedEnduranceLevel.ToString();
        GetText((int)Texts.ProjectedEnduranceLevelText).text = m_iProjectedEnduranceLevel.ToString();
        GetText((int)Texts.ProjectedEnduranceLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedVitalityLevel = playerManager.playerStatsManager.m_iVitalityLevel;
        GetText((int)Texts.CurrentVitalityLevelText).text = m_iProjectedVitalityLevel.ToString();
        GetText((int)Texts.ProjectedVitalityLevelText).text = m_iProjectedVitalityLevel.ToString();
        GetText((int)Texts.ProjectedVitalityLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedStrengthLevel = playerManager.playerStatsManager.m_iStrengthLevel;
        GetText((int)Texts.CurrentStrengthLevelText).text = m_iProjectedStrengthLevel.ToString();
        GetText((int)Texts.ProjectedStrengthLevelText).text = m_iProjectedStrengthLevel.ToString();
        GetText((int)Texts.ProjectedStrengthLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedDexterityLevel = playerManager.playerStatsManager.m_iDexterityLevel;
        GetText((int)Texts.CurrentDexterityLevelText).text = m_iProjectedDexterityLevel.ToString();
        GetText((int)Texts.ProjectedDexterityLevelText).text = m_iProjectedDexterityLevel.ToString();
        GetText((int)Texts.ProjectedDexterityLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedIntelligenceLevel = playerManager.playerStatsManager.m_iIntelligenceLevel;
        GetText((int)Texts.CurrentFaithLevelText).text = m_iProjectedIntelligenceLevel.ToString();
        GetText((int)Texts.ProjectedFaithLevelText).text = m_iProjectedIntelligenceLevel.ToString();
        GetText((int)Texts.ProjectedFaithLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedFaithLevel = playerManager.playerStatsManager.m_iFaithLevel;
        GetText((int)Texts.CurrentIntelligenceLevelText).text = m_iProjectedFaithLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedFaithLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).color = new Color(1f, 1f, 1f);

        m_iProjectedLuckLevel = playerManager.playerStatsManager.m_iLuckLevel;
        GetText((int)Texts.CurrentIntelligenceLevelText).text = m_iProjectedLuckLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedLuckLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).color = new Color(1f, 1f, 1f);

        UpdateProjectedPlayerLevel();
    }

    // 화살표에 따른 해당 스텟 다음 레벨 올림
    public void UpdateAttributesStat(int num)
    {
        if (num == 0)
            m_iProjectedVigorLevel++;
        else if (num == 1)
            m_iProjectedAttunementLevel++;
        else if (num == 2)
            m_iProjectedEnduranceLevel++;
        else if (num == 3)
            m_iProjectedVitalityLevel++;
        else if (num == 4)
            m_iProjectedStrengthLevel++;
        else if (num == 5)
            m_iProjectedDexterityLevel++;
        else if (num == 6)
            m_iProjectedIntelligenceLevel++;
        else if (num == 7)
            m_iProjectedFaithLevel++;
        else if (num == 8)
            m_iProjectedLuckLevel++;

        // 해당 스텟을 올리기 전에 돈이 충분한지. 그러니까 다음 업그레이드 비용이 있는지를 체크
        if (playerManager.playerStatsManager.currentSoulCount <= CalculateSoulCostToLevelUp())
        {
            if (num == 0)
                m_iProjectedVigorLevel--;
            else if (num == 1)
                m_iProjectedAttunementLevel--;
            else if (num == 2)
                m_iProjectedEnduranceLevel--;
            else if (num == 3)
                m_iProjectedVitalityLevel--;
            else if (num == 4)
                m_iProjectedStrengthLevel--;
            else if (num == 5)
                m_iProjectedDexterityLevel--;
            else if (num == 6)
                m_iProjectedIntelligenceLevel--;
            else if (num == 7)
                m_iProjectedFaithLevel--;
            else if (num == 8)
                m_iProjectedLuckLevel--;
            return;
        }

        // UI Stat 표시
        GetText((int)Texts.ProjectedVigorLevelText).text = m_iProjectedVigorLevel.ToString();
        GetText((int)Texts.ProjectedAttunementLevelText).text = m_iProjectedAttunementLevel.ToString();
        GetText((int)Texts.ProjectedEnduranceLevelText).text = m_iProjectedEnduranceLevel.ToString();
        GetText((int)Texts.ProjectedVitalityLevelText).text = m_iProjectedVitalityLevel.ToString();
        GetText((int)Texts.ProjectedStrengthLevelText).text = m_iProjectedStrengthLevel.ToString();
        GetText((int)Texts.ProjectedDexterityLevelText).text = m_iProjectedDexterityLevel.ToString();
        GetText((int)Texts.ProjectedFaithLevelText).text = m_iProjectedIntelligenceLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedFaithLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedLuckLevel.ToString();

        m_iRequiredSouls = 0;
        m_iRequiredSouls = CalculateSoulCostToLevelUp();
        UpdateProjectedPlayerLevel();
    }

    // 플레이어 레벨 변화 수치 계산
    private void UpdateProjectedPlayerLevel()
    {
        // 플레이어 레벨 계산
        int sum = 0;

        sum = sum + (m_iProjectedVigorLevel - playerManager.playerStatsManager.m_iVigorLevel);
        sum = sum + (m_iProjectedAttunementLevel - playerManager.playerStatsManager.m_iAttunementLevel);
        sum = sum + (m_iProjectedEnduranceLevel - playerManager.playerStatsManager.m_iEnduranceLevel);
        sum = sum + (m_iProjectedVitalityLevel - playerManager.playerStatsManager.m_iVitalityLevel);
        sum = sum + (m_iProjectedStrengthLevel - playerManager.playerStatsManager.m_iStrengthLevel);
        sum = sum + (m_iProjectedDexterityLevel - playerManager.playerStatsManager.m_iDexterityLevel);
        sum = sum + (m_iProjectedIntelligenceLevel - playerManager.playerStatsManager.m_iIntelligenceLevel);
        sum = sum + (m_iProjectedFaithLevel - playerManager.playerStatsManager.m_iFaithLevel);
        sum = sum + (m_iProjectedLuckLevel - playerManager.playerStatsManager.m_iLuckLevel);

        m_iProjectedPlayerLevel = m_iCurrentPlayerLevel + sum;
        GetText((int)Texts.ProjectedPlayerLevelText).text = m_iProjectedPlayerLevel.ToString();

        GetText((int)Texts.SoulsRequiredToLevelUpText).text = m_iRequiredSouls.ToString();
        GetText((int)Texts.CaluateToSoulsText).text = (playerManager.playerStatsManager.currentSoulCount - m_iRequiredSouls).ToString();

        SetLevelTextColor();
    }

    // 소울 코스트 계산
    private int CalculateSoulCostToLevelUp()
    {
        int sum = 0;
        for (int i = 0; i < m_iProjectedPlayerLevel; i++)
        {
            sum += Mathf.RoundToInt((m_iProjectedPlayerLevel * baseLevelUpCost) * 1.5f);
        }

        return sum;
    }

    // 변동 수치 값 텍스트 컬러 변화
    void SetLevelTextColor()
    {
        // 플레이어 레벨
        if (m_iCurrentPlayerLevel == m_iProjectedPlayerLevel)
            GetText((int)Texts.CaluateToSoulsText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.CaluateToSoulsText).color = new Color(0f, 0f, 1f);

        // 코스트 
        if (m_iRequiredSouls == 0)
            GetText((int)Texts.CaluateToSoulsText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.CaluateToSoulsText).color = new Color(1f, 0f, 0f);

        // 스텟

        // Vigor
        if (playerManager.playerStatsManager.m_iVigorLevel == m_iProjectedVigorLevel)
            GetText((int)Texts.ProjectedVigorLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedVigorLevelText).color = new Color(0f, 0f, 1f);

        // Attunement
        if (playerManager.playerStatsManager.m_iAttunementLevel == m_iProjectedAttunementLevel)
            GetText((int)Texts.ProjectedAttunementLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedAttunementLevelText).color = new Color(0f, 0f, 1f);

        // Endurance
        if (playerManager.playerStatsManager.m_iEnduranceLevel == m_iProjectedEnduranceLevel)
            GetText((int)Texts.ProjectedEnduranceLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedEnduranceLevelText).color = new Color(0f, 0f, 1f);

        // Vitality
        if (playerManager.playerStatsManager.m_iVitalityLevel == m_iProjectedVitalityLevel)
            GetText((int)Texts.ProjectedVitalityLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedVitalityLevelText).color = new Color(0f, 0f, 1f);

        // Strength
        if (playerManager.playerStatsManager.m_iStrengthLevel == m_iProjectedStrengthLevel)
            GetText((int)Texts.ProjectedStrengthLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedStrengthLevelText).color = new Color(0f, 0f, 1f);

        // Dexterity
        if (playerManager.playerStatsManager.m_iDexterityLevel == m_iProjectedDexterityLevel)
            GetText((int)Texts.ProjectedDexterityLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedDexterityLevelText).color = new Color(0f, 0f, 1f);

        // Intelligen
        if (playerManager.playerStatsManager.m_iIntelligenceLevel == m_iProjectedIntelligenceLevel)
            GetText((int)Texts.ProjectedIntelligenceLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedIntelligenceLevelText).color = new Color(0f, 0f, 1f);

        // Faith
        if (playerManager.playerStatsManager.m_iFaithLevel == m_iProjectedFaithLevel)
            GetText((int)Texts.ProjectedFaithLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedFaithLevelText).color = new Color(0f, 0f, 1f);

        // Luck
        if (playerManager.playerStatsManager.m_iLuckLevel == m_iProjectedLuckLevel)
            GetText((int)Texts.ProjectedLuckLevelText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.ProjectedLuckLevelText).color = new Color(0f, 0f, 1f);
    }



    // 변화 수치를 확정 업데이트
    public void ConfirmPlayerLevelUpStates()
    {
        // 플레이어 레벨 변경
        playerManager.playerStatsManager.playerLevel = m_iProjectedPlayerLevel;

        // 업데이트 된 특성 레벨 변경
        playerManager.playerStatsManager.m_iVigorLevel = m_iProjectedVigorLevel;
        playerManager.playerStatsManager.m_iAttunementLevel = m_iProjectedAttunementLevel;
        playerManager.playerStatsManager.m_iEnduranceLevel = m_iProjectedEnduranceLevel;
        playerManager.playerStatsManager.m_iVitalityLevel = m_iProjectedVitalityLevel;
        playerManager.playerStatsManager.m_iStrengthLevel = m_iProjectedStrengthLevel;
        playerManager.playerStatsManager.m_iDexterityLevel = m_iProjectedDexterityLevel;
        playerManager.playerStatsManager.m_iIntelligenceLevel = m_iProjectedIntelligenceLevel;
        playerManager.playerStatsManager.m_iFaithLevel = m_iProjectedFaithLevel;
        playerManager.playerStatsManager.m_iLuckLevel = m_iProjectedLuckLevel;

        // 소울 비용 계산
        playerManager.playerStatsManager.currentSoulCount -= m_iRequiredSouls;
        Managers.GameUI.m_GameSceneUI.SoulsRefreshUI(false, 0, false);
        m_iRequiredSouls = 0;

        // Stat Init 
        playerManager.playerStatsManager.InitAbility();

        // Stat Bar Ui RefreshUI
        Managers.GameUI.m_GameSceneUI.m_StatBarsUI.SetBGWidthUI(E_StatUI.All);

        gameObject.SetActive(false);
    }
}
