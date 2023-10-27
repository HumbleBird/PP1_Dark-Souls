using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BriefPlayerStatInformationUI : UI_Base
{
    enum Texts
    {
        CurrentPlayerLevelText              ,
        
        CurrentVigorLevelText               ,
        CurrentAttunementLevelText              ,
        CurrentEnduranceLevelText                       ,
        CurrentVitalityLevelText                        ,
        CurrentStrengthLevelText                        ,
        CurrentDexterityLevelText                        ,
        CurrentIntelligenceLevelText                ,   
        CurrentFaithLevelText,
        CurrentLuckLevelText                    ,

        HPValueText             ,
        FPValueText             ,
        StaminaValueText            ,

        EquipLoadValueText              ,
        PoiseValueText                  ,     
        ItemDiscoveryValueText          ,
        AttunementSlotValueText            ,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        PlayerManager player = Managers.Object.m_MyPlayer;

        GetText((int)Texts.CurrentPlayerLevelText).text = player.playerStatsManager.playerLevel.ToString();
       
        GetText((int)Texts.CurrentVigorLevelText        ).text = player.playerStatsManager.m_iVigorLevel       .ToString();
        GetText((int)Texts.CurrentAttunementLevelText   ).text = player.playerStatsManager.m_iAttunementLevel  .ToString();
        GetText((int)Texts.CurrentEnduranceLevelText    ).text = player.playerStatsManager.m_iEnduranceLevel   .ToString();
        GetText((int)Texts.CurrentVitalityLevelText     ).text = player.playerStatsManager.m_iVitalityLevel    .ToString();
        GetText((int)Texts.CurrentStrengthLevelText     ).text = player.playerStatsManager.m_iStrengthLevel    .ToString();
        GetText((int)Texts.CurrentDexterityLevelText    ).text = player.playerStatsManager.m_iDexterityLevel   .ToString();
        GetText((int)Texts.CurrentIntelligenceLevelText ).text = player.playerStatsManager.m_iIntelligenceLevel.ToString();
        GetText((int)Texts.CurrentFaithLevelText).text = player.playerStatsManager.m_iFaithLevel       .ToString();
        GetText((int)Texts.CurrentLuckLevelText).text = player.playerStatsManager.m_iLuckLevel.ToString();
        
        GetText((int)Texts.HPValueText     ).text = string.Format("{0, -3} / {1, 3}", player.playerStatsManager.maxHealth.ToString(), player.playerStatsManager.currentHealth.ToString());
        GetText((int)Texts.FPValueText).text = string.Format("{0, -3} / {1, 3}", player.playerStatsManager.maxfocusPoint.ToString(), player.playerStatsManager.currentFocusPoints.ToString());
        GetText((int)Texts.StaminaValueText).text = player.playerStatsManager.maxStamina.ToString();


        GetText((int)Texts.EquipLoadValueText).text = string.Format("{0, -3} / {1, 3}", player.playerStatsManager.currentEquipLoad.ToString(), player.playerStatsManager.maxEquipLoad.ToString("0.00"));
        GetText((int)Texts.PoiseValueText           ).text = player.playerStatsManager.CurrentPoise.ToString("0.00");
        GetText((int)Texts.ItemDiscoveryValueText   ).text = player.playerStatsManager.m_iItemDiscovery.ToString();
        GetText((int)Texts.AttunementSlotValueText).text = player.playerStatsManager.m_iAttunementLevel.ToString();

        return true;
    }
}
