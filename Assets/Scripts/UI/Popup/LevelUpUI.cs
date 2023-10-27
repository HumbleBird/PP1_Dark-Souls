using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Define;

public class LevelUpUI : InteractableNPCPopupUI
{
    enum Texts
    {
        // Left Property

        // Player Level
        CurrentPlayerLevelText,
        ProjectedPlayerLevelText,

        // Souls
        CurrentSoulsText,
        CaluateToSoulsText,
        RequiredSoulsText,

        // Left Attributes

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

        ProjectedVigorLevelText,
        ProjectedAttunementLevelText,
        ProjectedEnduranceLevelText,
        ProjectedVitalityLevelText,
        ProjectedStrengthLevelText,
        ProjectedDexterityLevelText,
        ProjectedIntelligenceLevelText,
        ProjectedFaithLevelText,
        ProjectedLuckLevelText,

        // Middle - Base Power
        CurrentHPText,
        CaluateHPText,
        CurrentFPText,
        CaluateFPText,
        CurrentStaminaText,
        CaluateStaminaText,
        CurrentEquipLoadText,
        CaluateEquipLoadText,
        CurrentPoiseText,
        CaluatePoiseText,
        CurrentItemDiscoveryText,
        CaluateItemDiscoveryText,


        // Middle - Attack Power
        CurrentRWeapon1Text,
        CaluateRWeapon1Text,
        CurrentRWeapon2Text,
        CaluateRWeapon2Text,
        CurrentRWeapon3Text,
        CaluateRWeapon3Text,
        CurrentLWeapon1Text,
        CaluateLWeapon1Text,
        CurrentLWeapon2Text,
        CaluateLWeapon2Text,
        CurrentLWeapon3Text,
        CaluateLWeapon3Text,

        // Right - Defense
        CurrentPhysicalDefenseText,
        CaluatePhysicalDefenseText,
        CurrentVSStrikeDefenseText,
        CaluateVSStrikeDefenseText,
        CurrentVSSlashDefenseText,
        CaluateVSSlashDefenseText,
        CurrentVSThrustDefenseText,
        CaluateVSThrustDefenseText,
        CurrentMagicDefenseText,
        CaluateMagiclDefenseText,
        CurrentFireDefenseText,
        CaluateFireDefenseText,
        CurrentLightningDefenseText,
        CaluateLightningDefenseText,
        CurrentDarkDefenseText,
        CaluateDarkDefenseText,


        // Right - Resistances
        CurrentBleedResistanceText,
        CaluateBleedResistanceText,
        CurrentPoisonResistanceText,
        CaluatePoisonResistanceText,
        CurrentFrostResistanceText,
        CaluateFrostResistanceText,
        CurrentCurseResistanceText,
        CaluateCurseResistanceText,
    }

    enum GameObjects
    {
        VigorArrow              ,
        AttunementArrow         ,
        EnduranceArrow          ,
        VitalityArrow           ,
        StrengthArrow           ,
        DexterityArrow              ,
        IntelligenceArrow           ,
        FaithArrow,
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

    // Base Power
    int m_iCalculateHP;
    float m_iCalculateFP;
    float m_iCalculateStamina;
    float m_iCalculateEquipLoad;
    int m_iCalculatePoise;
    int m_iCalculateItemDiscovery;

    // Attack Power
    int m_iCalculateRWeapon1;
    int m_iCalculateRWeapon2;
    int m_iCalculateRWeapon3;
    int m_iCalculateLWeapon1;
    int m_iCalculateLWeapon2;
    int m_iCalculateLWeapon3;

    // Defense
    int m_iCalculatePhysicalDefense;
    int m_iCalculateVSStrikeDefense;
    int m_iCalculateVSSlashDefense;
    int m_iCalculateVSThrustDefense;
    int m_iCalculateMagickDefense;
    int m_iCalculateFireDefense;
    int m_iCalculateLightning;
    int m_iCalculateDarkDefense;

    int m_iCaluateBleedResistance;
    int m_iCaluatePoisonResistanc;
    int m_iCaluateFrostResistance;
    int m_iCaluateCurseResistance;


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

        playerManager = Managers.Object.m_MyPlayer;
        InfoReset();

        return true;
    }

    void InfoReset()
    {
        #region Base Property

        // �÷��̾� ����
        m_iCurrentPlayerLevel = playerManager.playerStatsManager.playerLevel;
        InitStat(m_iCurrentPlayerLevel, GetText((int)Texts.CurrentPlayerLevelText), GetText((int)Texts.ProjectedPlayerLevelText));

        // �ҿ�
        m_iCurrentSouls = playerManager.playerStatsManager.currentSoulCount;
        GetText((int)Texts.CurrentSoulsText).text = m_iCurrentSouls.ToString();
        GetText((int)Texts.CaluateToSoulsText).text = m_iCurrentSouls.ToString();
        GetText((int)Texts.RequiredSoulsText).text = "0";
        GetText((int)Texts.RequiredSoulsText).color = new Color(1f, 1f, 1f);

        // ����
        m_iProjectedVigorLevel = playerManager.playerStatsManager.m_iVigorLevel;
        m_iProjectedAttunementLevel = playerManager.playerStatsManager.m_iAttunementLevel;
        m_iProjectedEnduranceLevel = playerManager.playerStatsManager.m_iEnduranceLevel;
        m_iProjectedVitalityLevel = playerManager.playerStatsManager.m_iVitalityLevel;
        m_iProjectedStrengthLevel = playerManager.playerStatsManager.m_iStrengthLevel;
        m_iProjectedDexterityLevel = playerManager.playerStatsManager.m_iDexterityLevel;
        m_iProjectedIntelligenceLevel = playerManager.playerStatsManager.m_iIntelligenceLevel;
        m_iProjectedFaithLevel = playerManager.playerStatsManager.m_iFaithLevel;
        m_iProjectedLuckLevel = playerManager.playerStatsManager.m_iLuckLevel;

        InitStat(m_iProjectedLuckLevel, GetText((int)Texts.CurrentVigorLevelText       ), GetText((int)Texts.ProjectedVigorLevelText       ));
        InitStat(m_iProjectedLuckLevel, GetText((int)Texts.CurrentAttunementLevelText  ), GetText((int)Texts.ProjectedAttunementLevelText  ));
        InitStat(m_iProjectedLuckLevel, GetText((int)Texts.CurrentEnduranceLevelText   ), GetText((int)Texts.ProjectedEnduranceLevelText   ));
        InitStat(m_iProjectedVitalityLevel, GetText((int)Texts.CurrentVitalityLevelText    ), GetText((int)Texts.ProjectedVitalityLevelText    ));
        InitStat(m_iProjectedStrengthLevel, GetText((int)Texts.CurrentStrengthLevelText    ), GetText((int)Texts.ProjectedStrengthLevelText    ));
        InitStat(m_iProjectedDexterityLevel, GetText((int)Texts.CurrentDexterityLevelText   ), GetText((int)Texts.ProjectedDexterityLevelText   ));
        InitStat(m_iProjectedIntelligenceLevel, GetText((int)Texts.CurrentFaithLevelText       ), GetText((int)Texts.ProjectedFaithLevelText       ));
        InitStat(m_iProjectedFaithLevel, GetText((int)Texts.CurrentIntelligenceLevelText), GetText((int)Texts.ProjectedIntelligenceLevelText));
        InitStat(m_iProjectedLuckLevel, GetText((int)Texts.CurrentLuckLevelText), GetText((int)Texts.ProjectedLuckLevelText         ));
        #endregion

        #region Base, Attack Power

        m_iCalculateHP = playerManager.playerStatsManager.maxHealth;
        m_iCalculateFP = playerManager.playerStatsManager.maxfocusPoint;
        m_iCalculateStamina = playerManager.playerStatsManager.maxStamina;
        m_iCalculateEquipLoad = playerManager.playerStatsManager.maxEquipLoad;
        m_iCalculatePoise = playerManager.playerStatsManager.CurrentPoise;
        m_iCalculateItemDiscovery = playerManager.playerStatsManager.m_iItemDiscovery;

        InitStat(m_iCalculateHP, GetText((int)Texts.CurrentHPText), GetText((int)Texts.CaluateHPText));
        InitStat(Mathf.RoundToInt(m_iCalculateFP), GetText((int)Texts.CurrentFPText), GetText((int)Texts.CaluateFPText));
        InitStat(Mathf.RoundToInt(m_iCalculateStamina), GetText((int)Texts.CurrentStaminaText), GetText((int)Texts.CaluateStaminaText));
        InitStat(Mathf.RoundToInt(m_iCalculateEquipLoad), GetText((int)Texts.CurrentEquipLoadText), GetText((int)Texts.CaluateEquipLoadText));
        InitStat(m_iCalculatePoise, GetText((int)Texts.CurrentPoiseText), GetText((int)Texts.CaluatePoiseText));
        InitStat(m_iCalculateItemDiscovery, GetText((int)Texts.CurrentItemDiscoveryText), GetText((int)Texts.CaluateItemDiscoveryText));


        m_iCalculateRWeapon1 = playerManager.playerStatsManager.m_iRWeapon1;
        m_iCalculateRWeapon2 = playerManager.playerStatsManager.m_iRWeapon2;
        m_iCalculateRWeapon3 = playerManager.playerStatsManager.m_iRWeapon3;
        m_iCalculateLWeapon1 = playerManager.playerStatsManager.m_iLWeapon1;
        m_iCalculateLWeapon2 = playerManager.playerStatsManager.m_iLWeapon2;
        m_iCalculateLWeapon3 = playerManager.playerStatsManager.m_iLWeapon3;

        InitStat(m_iCalculateRWeapon1, GetText((int)Texts.CurrentRWeapon1Text), GetText((int)Texts.CaluateRWeapon1Text));
        InitStat(m_iCalculateRWeapon2, GetText((int)Texts.CurrentRWeapon2Text), GetText((int)Texts.CaluateRWeapon2Text));
        InitStat(m_iCalculateRWeapon3, GetText((int)Texts.CurrentRWeapon3Text), GetText((int)Texts.CaluateRWeapon3Text));
        InitStat(m_iCalculateLWeapon1, GetText((int)Texts.CurrentLWeapon1Text), GetText((int)Texts.CaluateLWeapon1Text));
        InitStat(m_iCalculateLWeapon2, GetText((int)Texts.CurrentLWeapon2Text), GetText((int)Texts.CaluateLWeapon2Text));
        InitStat(m_iCalculateLWeapon3, GetText((int)Texts.CurrentLWeapon3Text), GetText((int)Texts.CaluateLWeapon3Text));
        #endregion

        #region Defense, Resistances
                                                                                                  ;
        m_iCalculatePhysicalDefense = playerManager.playerStatsManager.m_fPhysicalDefense          ;
        m_iCalculateVSStrikeDefense = playerManager.playerStatsManager.m_fVSStrikeDefense;
        m_iCalculateVSSlashDefense  = playerManager.playerStatsManager.m_fVSSlashDefense  ;
        m_iCalculateVSThrustDefense = playerManager.playerStatsManager.m_fVSThrustDefense;
        m_iCalculateMagickDefense   = playerManager.playerStatsManager.m_fMagicDefense     ;
        m_iCalculateFireDefense     = playerManager.playerStatsManager.m_fFireDefense        ;
        m_iCalculateLightning       = playerManager.playerStatsManager.m_fLightningDefense     ;
        m_iCalculateDarkDefense     = playerManager.playerStatsManager.m_fDarkDefense        ;

        InitStat(m_iCalculatePhysicalDefense, GetText((int)Texts.CurrentPhysicalDefenseText ), GetText((int)Texts.CaluatePhysicalDefenseText  ));
        InitStat(m_iCalculateVSStrikeDefense, GetText((int)Texts.CurrentVSStrikeDefenseText ), GetText((int)Texts.CaluateVSStrikeDefenseText  ));
        InitStat(m_iCalculateVSSlashDefense , GetText((int)Texts.CurrentVSSlashDefenseText  ), GetText((int)Texts.CaluateVSSlashDefenseText   ));
        InitStat(m_iCalculateVSThrustDefense, GetText((int)Texts.CurrentVSThrustDefenseText ), GetText((int)Texts.CaluateVSThrustDefenseText  ));
        InitStat(m_iCalculateMagickDefense  , GetText((int)Texts.CurrentFireDefenseText     ), GetText((int)Texts.CaluateFireDefenseText      ));
        InitStat(m_iCalculateFireDefense    , GetText((int)Texts.CurrentMagicDefenseText    ), GetText((int)Texts.CaluateMagiclDefenseText        ));
        InitStat(m_iCalculateLightning      , GetText((int)Texts.CurrentLightningDefenseText), GetText((int)Texts.CaluateLightningDefenseText ));
        InitStat(m_iCalculateDarkDefense    , GetText((int)Texts.CurrentDarkDefenseText), GetText((int)Texts.CaluateDarkDefenseText));


        // �Ӽ�
        m_iCaluateBleedResistance = playerManager.playerStatsManager.m_fBleedResistance;
        m_iCaluatePoisonResistanc = playerManager.playerStatsManager.m_fBleedResistance           ;
        m_iCaluateFrostResistance = playerManager.playerStatsManager.m_fFrostResistance           ;
        m_iCaluateCurseResistance = playerManager.playerStatsManager.m_fCurseResistance; ;

        InitStat( m_iCaluateBleedResistance, GetText((int)Texts.CurrentBleedResistanceText ), GetText((int)Texts.CaluateBleedResistanceText ));
        InitStat( m_iCaluatePoisonResistanc, GetText((int)Texts.CurrentPoisonResistanceText), GetText((int)Texts.CaluatePoisonResistanceText));
        InitStat( m_iCaluateFrostResistance, GetText((int)Texts.CurrentFrostResistanceText ), GetText((int)Texts. CaluateFrostResistanceText ));
        InitStat(m_iCaluateCurseResistance,  GetText((int)Texts.CurrentCurseResistanceText), GetText((int)Texts.CaluateCurseResistanceText));

        #endregion

        UpdateProjectedPlayerLevel();
    }

    void InitStat(int level, TextMeshProUGUI currnetText, TextMeshProUGUI projectedText)
    {
        GetText((int)Texts.CurrentIntelligenceLevelText).text = level.ToString();
        currnetText.text = level.ToString();
        projectedText.text = level.ToString();
        projectedText.color = new Color(1f, 1f, 1f);
    }

    // ȭ��ǥ�� ���� �ش� ���� ���� ���� �ø�
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

        // �ش� ������ �ø��� ���� ���� �������. �׷��ϱ� ���� ���׷��̵� ����� �ִ����� üũ
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

        // Attributes
        GetText((int)Texts.ProjectedVigorLevelText).text = m_iProjectedVigorLevel.ToString();
        GetText((int)Texts.ProjectedAttunementLevelText).text = m_iProjectedAttunementLevel.ToString();
        GetText((int)Texts.ProjectedEnduranceLevelText).text = m_iProjectedEnduranceLevel.ToString();
        GetText((int)Texts.ProjectedVitalityLevelText).text = m_iProjectedVitalityLevel.ToString();
        GetText((int)Texts.ProjectedStrengthLevelText).text = m_iProjectedStrengthLevel.ToString();
        GetText((int)Texts.ProjectedDexterityLevelText).text = m_iProjectedDexterityLevel.ToString();
        GetText((int)Texts.ProjectedFaithLevelText).text = m_iProjectedIntelligenceLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedFaithLevel.ToString();
        GetText((int)Texts.ProjectedIntelligenceLevelText).text = m_iProjectedLuckLevel.ToString();

        // Base Power
        GetText((int)Texts.CaluateHPText).text = (m_iProjectedVigorLevel * 10).ToString();
        GetText((int)Texts.CaluateFPText).text = (m_iProjectedAttunementLevel * 10).ToString();
        GetText((int)Texts.CaluateStaminaText).text = (m_iProjectedEnduranceLevel * 10).ToString();
        GetText((int)Texts.CaluateEquipLoadText).text = Mathf.RoundToInt(playerManager.playerStatsManager.CalculateAndSetMaxEquipload(m_iProjectedEnduranceLevel)).ToString();
        //GetText((int)Texts.CaluatePoiseText).text = "00"; playerManager.playerStatsManager.CurrentPoise.ToString();
        //GetText((int)Texts.CurrentItemDiscoveryText).text = playerManager.playerStatsManager.m_iItemDiscovery.ToString();


        // Attack Power
        // TODO


        // Defense
        // TODO


        // Resistances
        // TODO

        m_iRequiredSouls = 0;
        m_iRequiredSouls = CalculateSoulCostToLevelUp();
        UpdateProjectedPlayerLevel();
    }

    // �÷��̾� ���� ��ȭ ��ġ ���
    private void UpdateProjectedPlayerLevel()
    {
        // �÷��̾� ���� ���
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

        GetText((int)Texts.RequiredSoulsText).text = m_iRequiredSouls.ToString();
        GetText((int)Texts.CaluateToSoulsText).text = (playerManager.playerStatsManager.currentSoulCount - m_iRequiredSouls).ToString();

        SetLevelTextColor();
    }

    // �ҿ� �ڽ�Ʈ ���
    private int CalculateSoulCostToLevelUp()
    {
        int sum = 0;
        for (int i = 0; i < m_iProjectedPlayerLevel; i++)
        {
            sum += Mathf.RoundToInt((m_iProjectedPlayerLevel * baseLevelUpCost) * 1.5f);
        }

        return sum;
    }

    // ���� ��ġ �� �ؽ�Ʈ �÷� ��ȭ
    void SetLevelTextColor()
    {
        // �÷��̾� ����
        SetColor(m_iCurrentPlayerLevel, m_iProjectedPlayerLevel, GetText((int)Texts.CaluateToSoulsText));

        // �ڽ�Ʈ 
        SetColor(m_iRequiredSouls, 0       , GetText((int)Texts.CaluateToSoulsText));

        // ����

        SetColor(playerManager.playerStatsManager.m_iVigorLevel       , m_iProjectedVigorLevel       , GetText((int)Texts.ProjectedVigorLevelText       ));
        SetColor(playerManager.playerStatsManager.m_iAttunementLevel  , m_iProjectedAttunementLevel  , GetText((int)Texts.ProjectedAttunementLevelText  ));
        SetColor(playerManager.playerStatsManager.m_iEnduranceLevel   , m_iProjectedEnduranceLevel   , GetText((int)Texts.ProjectedEnduranceLevelText   ));
        SetColor(playerManager.playerStatsManager.m_iVitalityLevel    , m_iProjectedVitalityLevel    , GetText((int)Texts.ProjectedVitalityLevelText    ));
        SetColor(playerManager.playerStatsManager.m_iStrengthLevel    , m_iProjectedStrengthLevel    , GetText((int)Texts.ProjectedStrengthLevelText    ));
        SetColor(playerManager.playerStatsManager.m_iDexterityLevel   , m_iProjectedDexterityLevel   , GetText((int)Texts.ProjectedDexterityLevelText   ));
        SetColor(playerManager.playerStatsManager.m_iIntelligenceLevel, m_iProjectedFaithLevel       , GetText((int)Texts.ProjectedFaithLevelText       ));
        SetColor(playerManager.playerStatsManager.m_iFaithLevel       , m_iProjectedIntelligenceLevel, GetText((int)Texts.ProjectedIntelligenceLevelText));
        SetColor(playerManager.playerStatsManager.m_iLuckLevel, m_iProjectedLuckLevel,                 GetText((int)Texts.ProjectedLuckLevelText        ));
    }

    void SetColor(int beforeValue, int AfterValue, TextMeshProUGUI text)
    {
        // Luck
        if (beforeValue == AfterValue)
            text.color = new Color(1f, 1f, 1f);
        else
            text.color = new Color(0f, 0f, 1f);
    }


    // ��ȭ ��ġ�� Ȯ�� ������Ʈ
    public void ConfirmPlayerLevelUpStates()
    {
        // �÷��̾� ���� ����
        playerManager.playerStatsManager.playerLevel = m_iProjectedPlayerLevel;

        playerManager.playerStatsManager.m_iVigorLevel = m_iProjectedVigorLevel;
        playerManager.playerStatsManager.m_iAttunementLevel = m_iProjectedAttunementLevel;
        playerManager.playerStatsManager.m_iEnduranceLevel = m_iProjectedEnduranceLevel;
        playerManager.playerStatsManager.m_iVitalityLevel = m_iProjectedVitalityLevel;
        playerManager.playerStatsManager.m_iStrengthLevel = m_iProjectedStrengthLevel;
        playerManager.playerStatsManager.m_iDexterityLevel = m_iProjectedDexterityLevel;
        playerManager.playerStatsManager.m_iIntelligenceLevel = m_iProjectedIntelligenceLevel;
        playerManager.playerStatsManager.m_iFaithLevel = m_iProjectedFaithLevel;
        playerManager.playerStatsManager.m_iLuckLevel = m_iProjectedLuckLevel;

        // �ҿ� ��� ���
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
