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

        CurrentAttunementSlotsText,
        CaluateAttunementSlotsText
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
    int m_iCalculateFP;
    int m_iCalculateStamina;
    float m_iCalculateEquipLoad;
    float m_iCalculatePoise;
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

    int m_iCaluateAttunementSlot;

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

        // 플레이어 레벨
        m_iCurrentPlayerLevel = playerManager.playerStatsManager.playerLevel;
        InitStat(m_iCurrentPlayerLevel, GetText((int)Texts.CurrentPlayerLevelText), GetText((int)Texts.ProjectedPlayerLevelText));

        // 소울
        m_iCurrentSouls = playerManager.playerStatsManager.currentSoulCount;
        GetText((int)Texts.CurrentSoulsText).text = m_iCurrentSouls.ToString();
        GetText((int)Texts.CaluateToSoulsText).text = m_iCurrentSouls.ToString();
        GetText((int)Texts.RequiredSoulsText).text = "0";
        GetText((int)Texts.RequiredSoulsText).color = new Color(1f, 1f, 1f);

        // 스텟
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
        m_iCalculateEquipLoad = playerManager.playerStatsManager.m_MaxEquipLoad;
        m_iCalculatePoise = playerManager.playerStatsManager.m_fStatPoise;
        m_iCalculateItemDiscovery = playerManager.playerStatsManager.m_iItemDiscovery;

        InitStat(m_iCalculateHP, GetText((int)Texts.CurrentHPText), GetText((int)Texts.CaluateHPText));
        InitStat(m_iCalculateFP, GetText((int)Texts.CurrentFPText), GetText((int)Texts.CaluateFPText));
        InitStat(m_iCalculateStamina, GetText((int)Texts.CurrentStaminaText), GetText((int)Texts.CaluateStaminaText));
        InitStat(m_iCalculateEquipLoad, GetText((int)Texts.CurrentEquipLoadText), GetText((int)Texts.CaluateEquipLoadText));
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
        m_iCalculatePhysicalDefense = playerManager.playerStatsManager.m_iPhysicalDefense          ;
        m_iCalculateVSStrikeDefense = playerManager.playerStatsManager.m_iVSStrikeDefense;
        m_iCalculateVSSlashDefense  = playerManager.playerStatsManager.m_iVSSlashDefense  ;
        m_iCalculateVSThrustDefense = playerManager.playerStatsManager.m_iVSThrustDefense;
        m_iCalculateMagickDefense   = playerManager.playerStatsManager.m_iMagicDefense     ;
        m_iCalculateFireDefense     = playerManager.playerStatsManager.m_iFireDefense        ;
        m_iCalculateLightning       = playerManager.playerStatsManager.m_iLightningDefense     ;
        m_iCalculateDarkDefense     = playerManager.playerStatsManager.m_iDarkDefense        ;

        InitStat(m_iCalculatePhysicalDefense, GetText((int)Texts.CurrentPhysicalDefenseText ), GetText((int)Texts.CaluatePhysicalDefenseText  ));
        InitStat(m_iCalculateVSStrikeDefense, GetText((int)Texts.CurrentVSStrikeDefenseText ), GetText((int)Texts.CaluateVSStrikeDefenseText  ));
        InitStat(m_iCalculateVSSlashDefense , GetText((int)Texts.CurrentVSSlashDefenseText  ), GetText((int)Texts.CaluateVSSlashDefenseText   ));
        InitStat(m_iCalculateVSThrustDefense, GetText((int)Texts.CurrentVSThrustDefenseText ), GetText((int)Texts.CaluateVSThrustDefenseText  ));
        InitStat(m_iCalculateMagickDefense  , GetText((int)Texts.CurrentFireDefenseText     ), GetText((int)Texts.CaluateFireDefenseText      ));
        InitStat(m_iCalculateFireDefense    , GetText((int)Texts.CurrentMagicDefenseText    ), GetText((int)Texts.CaluateMagiclDefenseText        ));
        InitStat(m_iCalculateLightning      , GetText((int)Texts.CurrentLightningDefenseText), GetText((int)Texts.CaluateLightningDefenseText ));
        InitStat(m_iCalculateDarkDefense    , GetText((int)Texts.CurrentDarkDefenseText), GetText((int)Texts.CaluateDarkDefenseText));


        // 속성
        m_iCaluateBleedResistance = playerManager.playerStatsManager.m_iBleedResistance;
        m_iCaluatePoisonResistanc = playerManager.playerStatsManager.m_iBleedResistance           ;
        m_iCaluateFrostResistance = playerManager.playerStatsManager.m_iFrostResistance           ;
        m_iCaluateCurseResistance = playerManager.playerStatsManager.m_iCurseResistance; ;

        InitStat( m_iCaluateBleedResistance, GetText((int)Texts.CurrentBleedResistanceText ), GetText((int)Texts.CaluateBleedResistanceText ));
        InitStat( m_iCaluatePoisonResistanc, GetText((int)Texts.CurrentPoisonResistanceText), GetText((int)Texts.CaluatePoisonResistanceText));
        InitStat( m_iCaluateFrostResistance, GetText((int)Texts.CurrentFrostResistanceText ), GetText((int)Texts. CaluateFrostResistanceText ));
        InitStat(m_iCaluateCurseResistance,  GetText((int)Texts.CurrentCurseResistanceText), GetText((int)Texts.CaluateCurseResistanceText));

        // Attunement Slot
        m_iCaluateAttunementSlot = playerManager.playerStatsManager.m_iAttunementSlots;
        InitStat(m_iCaluateAttunementSlot, GetText((int)Texts.CurrentAttunementSlotsText), GetText((int)Texts.CaluateAttunementSlotsText));


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

    void InitStat(float level, TextMeshProUGUI currnetText, TextMeshProUGUI projectedText)
    {
        GetText((int)Texts.CurrentIntelligenceLevelText).text = level.ToString();
        currnetText.text = level.ToString();
        projectedText.text = level.ToString();
        projectedText.color = new Color(1f, 1f, 1f);
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
        GetText((int)Texts.CaluateHPText).text = playerManager.playerStatsManager.CalculateMaxHP(m_iProjectedVigorLevel).ToString();
        GetText((int)Texts.CaluateFPText).text = playerManager.playerStatsManager.CalculateMaxfocusPoint(m_iProjectedAttunementLevel).ToString();
        GetText((int)Texts.CaluateStaminaText).text = playerManager.playerStatsManager.CalculateMaxStamina(m_iProjectedEnduranceLevel).ToString();
        GetText((int)Texts.CaluateEquipLoadText).text = Mathf.RoundToInt(playerManager.playerStatsManager.CalculateAndSetMaxEquipload(m_iProjectedVitalityLevel)).ToString();
        GetText((int)Texts.CaluatePoiseText).text = playerManager.playerStatsManager.m_fStatPoise.ToString(); // only 장비로 인해서만 수치에 영향을 받음
        GetText((int)Texts.CaluateItemDiscoveryText).text = playerManager.playerStatsManager.CalculateItemDiscovery(m_iProjectedLuckLevel).ToString();


        // Attack Power
        // TODO
        // 어디서?


        // Defense
        GetText((int)Texts.CaluatePhysicalDefenseText).text = (playerManager.playerStatsManager.CalculatePhysicalDefenseFromVitalityLevel(m_iProjectedVitalityLevel) + playerManager.playerStatsManager.CalculatePhysicalDefenseFromStrengthLevel(m_iProjectedStrengthLevel)).ToString();
        // TODO VS
        GetText((int)Texts.CaluateMagiclDefenseText).text = playerManager.playerStatsManager.CalculateMagicDefense(m_iProjectedIntelligenceLevel).ToString();
        GetText((int)Texts.CaluateFireDefenseText).text = playerManager.playerStatsManager.CalculateFireDefense(m_iProjectedIntelligenceLevel).ToString();
        GetText((int)Texts.CaluateLightningDefenseText).text = playerManager.playerStatsManager.CalculateLightningDefense(m_iProjectedIntelligenceLevel).ToString();
        GetText((int)Texts.CaluateDarkDefenseText).text = playerManager.playerStatsManager.CalculateDarkDefense(m_iProjectedFaithLevel).ToString();

        // Resistances
        GetText((int)Texts.CaluateBleedResistanceText).text = playerManager.playerStatsManager.CalculateDarkDefense(m_iProjectedEnduranceLevel).ToString();
        GetText((int)Texts.CaluatePoisonResistanceText).text = playerManager.playerStatsManager.CalculatePosionResistance(m_iProjectedVitalityLevel).ToString();
        GetText((int)Texts.CaluateFrostResistanceText).text = playerManager.playerStatsManager.CalculateFrostResistance(m_iProjectedVigorLevel).ToString();
        GetText((int)Texts.CaluateCurseResistanceText).text = playerManager.playerStatsManager.CalculateCurseResistance(m_iProjectedLuckLevel).ToString();

        // Attunement Slot
        GetText((int)Texts.CaluateAttunementSlotsText).text = playerManager.playerStatsManager.CalculateAttunementSlot(m_iProjectedAttunementLevel).ToString();

        m_iRequiredSouls = 0;
        m_iRequiredSouls = CalculateSoulCostToLevelUp();
        UpdateProjectedPlayerLevel();

        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
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

        GetText((int)Texts.RequiredSoulsText).text = m_iRequiredSouls.ToString();
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
        SetColor(m_iCurrentPlayerLevel, m_iProjectedPlayerLevel, GetText((int)Texts.CaluateToSoulsText));

        // 코스트 
        if (m_iRequiredSouls == 0)
            GetText((int)Texts.CaluateToSoulsText).color = new Color(1f, 1f, 1f);
        else
            GetText((int)Texts.CaluateToSoulsText).color = new Color(1f, 0f, 0f);

        // 스텟
        SetColor(playerManager.playerStatsManager.m_iVigorLevel       , m_iProjectedVigorLevel       , GetText((int)Texts.ProjectedVigorLevelText       ));
        SetColor(playerManager.playerStatsManager.m_iAttunementLevel  , m_iProjectedAttunementLevel  , GetText((int)Texts.ProjectedAttunementLevelText  ));
        SetColor(playerManager.playerStatsManager.m_iEnduranceLevel   , m_iProjectedEnduranceLevel   , GetText((int)Texts.ProjectedEnduranceLevelText   ));
        SetColor(playerManager.playerStatsManager.m_iVitalityLevel    , m_iProjectedVitalityLevel    , GetText((int)Texts.ProjectedVitalityLevelText    ));
        SetColor(playerManager.playerStatsManager.m_iStrengthLevel    , m_iProjectedStrengthLevel    , GetText((int)Texts.ProjectedStrengthLevelText    ));
        SetColor(playerManager.playerStatsManager.m_iDexterityLevel   , m_iProjectedDexterityLevel   , GetText((int)Texts.ProjectedDexterityLevelText   ));
        SetColor(playerManager.playerStatsManager.m_iIntelligenceLevel, m_iProjectedFaithLevel       , GetText((int)Texts.ProjectedFaithLevelText       ));
        SetColor(playerManager.playerStatsManager.m_iFaithLevel       , m_iProjectedIntelligenceLevel, GetText((int)Texts.ProjectedIntelligenceLevelText));
        SetColor(playerManager.playerStatsManager.m_iLuckLevel, m_iProjectedLuckLevel,                 GetText((int)Texts.ProjectedLuckLevelText        ));

        // Base Power
        SetColor(playerManager.playerStatsManager.maxHealth       , playerManager.playerStatsManager.CalculateMaxHP(m_iProjectedVigorLevel), GetText((int)Texts.CaluateHPText));
        SetColor(playerManager.playerStatsManager.maxfocusPoint, playerManager.playerStatsManager.CalculateMaxfocusPoint(m_iProjectedAttunementLevel), GetText((int)Texts.CaluateFPText));
        SetColor(playerManager.playerStatsManager.maxStamina, playerManager.playerStatsManager.CalculateMaxStamina(m_iProjectedEnduranceLevel), GetText((int)Texts.CaluateStaminaText));
        SetColor(playerManager.playerStatsManager.m_MaxEquipLoad, playerManager.playerStatsManager.CalculateAndSetMaxEquipload(m_iProjectedVitalityLevel), GetText((int)Texts.CaluateEquipLoadText));
        //SetColor(playerManager.playerStatsManager.CurrentPoise, playerManager.playerStatsManager.CalculateMaxHP(CaluatePoiseT)       , GetText((int)Texts.CaluatePoiseText));
        SetColor(playerManager.playerStatsManager.m_iItemDiscovery, playerManager.playerStatsManager.CalculateItemDiscovery(m_iProjectedLuckLevel), GetText((int)Texts.CaluateItemDiscoveryText));

        // Attack Power
        // TODO

        // Defefnse
        SetColor(playerManager.playerStatsManager.m_iPhysicalDefense, playerManager.playerStatsManager.CalculatePhysicalDefenseFromVitalityLevel(m_iProjectedVitalityLevel) + playerManager.playerStatsManager.CalculatePhysicalDefenseFromStrengthLevel(m_iProjectedStrengthLevel), GetText((int)Texts.CaluatePhysicalDefenseText));
        // VS TODO
        SetColor(playerManager.playerStatsManager.m_iMagicDefense, playerManager.playerStatsManager.CalculateMagicDefense(m_iProjectedIntelligenceLevel), GetText((int)Texts.CaluateMagiclDefenseText));
        SetColor(playerManager.playerStatsManager.m_iFireDefense, playerManager.playerStatsManager.CalculateFireDefense(m_iProjectedStrengthLevel), GetText((int)Texts.CaluateFireDefenseText));
        SetColor(playerManager.playerStatsManager.m_iLightningDefense, playerManager.playerStatsManager.CalculateLightningDefense(m_iProjectedEnduranceLevel), GetText((int)Texts.CaluateLightningDefenseText));
        SetColor(playerManager.playerStatsManager.m_iDarkDefense, playerManager.playerStatsManager.CalculateDarkDefense(m_iProjectedFaithLevel), GetText((int)Texts.CaluateDarkDefenseText));

        // Resistance
        SetColor(playerManager.playerStatsManager.m_iBleedResistance, playerManager.playerStatsManager.CalculateBleedResistance(m_iProjectedEnduranceLevel), GetText((int)Texts.CaluateMagiclDefenseText));
        SetColor(playerManager.playerStatsManager.m_iPoisonResistance, playerManager.playerStatsManager.CalculatePosionResistance(m_iProjectedVitalityLevel), GetText((int)Texts.CaluateMagiclDefenseText));
        SetColor(playerManager.playerStatsManager.m_iFrostResistance, playerManager.playerStatsManager.CalculateFrostResistance(m_iProjectedVigorLevel), GetText((int)Texts.CaluateMagiclDefenseText));
        SetColor(playerManager.playerStatsManager.m_iCurseResistance, playerManager.playerStatsManager.CalculateCurseResistance(m_iProjectedLuckLevel), GetText((int)Texts.CaluateMagiclDefenseText));

        SetColor(playerManager.playerStatsManager.m_iAttunementSlots, playerManager.playerStatsManager.CalculateAttunementSlot(m_iProjectedAttunementLevel), GetText((int)Texts.CaluateAttunementSlotsText));

    }

    void SetColor(int beforeValue, int AfterValue, TextMeshProUGUI text)
    {
        // Luck
        if (beforeValue == AfterValue)
            text.color = new Color(1f, 1f, 1f);
        else
            text.color = new Color(0f, 0f, 1f);
    }

    void SetColor(float beforeValue, float AfterValue, TextMeshProUGUI text)
    {
        // Luck
        if (beforeValue == AfterValue)
            text.color = new Color(1f, 1f, 1f);
        else
            text.color = new Color(0f, 0f, 1f);
    }


    // 변화 수치를 확정 업데이트
    public void ConfirmPlayerLevelUpStates()
    {
        // 플레이어 레벨 변경
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

        // 소울 비용 계산
        playerManager.playerStatsManager.currentSoulCount -= m_iRequiredSouls;
        Managers.GameUI.m_GameSceneUI.SoulsRefreshUI(false, 0, true);
        m_iRequiredSouls = 0;

        // Stat Init 
        playerManager.playerStatsManager.InitAbility();

        // Stat Bar Ui RefreshUI
        Managers.GameUI.m_GameSceneUI.m_StatBarsUI.SetBGWidthUI(E_StatUI.All);

        Managers.Sound.Play("UI/Popup_ButtonClose");
    }
}
