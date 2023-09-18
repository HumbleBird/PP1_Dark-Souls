using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelector : MonoBehaviour
{
    PlayerManager player;

    [Header("Class Info UI")]
    public TextMeshProUGUI m_iPlayerLevelText            ;
    public TextMeshProUGUI m_iVigorLevelText            ;
    public TextMeshProUGUI m_iAttunementLevelText       ;
    public TextMeshProUGUI m_iEnduranceLevelText        ;
    public TextMeshProUGUI m_iVitalityLevelText     ;
    public TextMeshProUGUI m_iStrengthLevelText         ;
    public TextMeshProUGUI m_iDexterityLevelText        ;
    public TextMeshProUGUI m_iIntelligenceLevelText         ;
    public TextMeshProUGUI m_iFaithLevelText            ;
    public TextMeshProUGUI m_iLuckLevelText             ;
    public TextMeshProUGUI classDescription;

    [Header("Class Starting Stats")]
    public ClassStats[] classStats;

    [Header("Class Starting Gear")]
    public ClassGear[] classGear;

    private void Start()
    {
        player = Managers.Object.m_MyPlayer;
    }

    private void AssignClassStats(int classChosen)
    {
        player.playerStatsManager.playerLevel          = classStats[classChosen].m_sClassLevel;
        player.playerStatsManager.m_iVigorLevel        = classStats[classChosen].m_iVigorLevel       ;
        player.playerStatsManager.m_iAttunementLevel   = classStats[classChosen].m_iAttunementLevel  ;
        player.playerStatsManager.m_iEnduranceLevel    = classStats[classChosen].m_iEnduranceLevel   ;
        player.playerStatsManager.m_iVitalityLevel     = classStats[classChosen].m_iVitalityLevel    ;
        player.playerStatsManager.m_iStrengthLevel     = classStats[classChosen].m_iStrengthLevel    ;
        player.playerStatsManager.m_iDexterityLevel    = classStats[classChosen].m_iDexterityLevel   ;
        player.playerStatsManager.m_iIntelligenceLevel = classStats[classChosen].m_iIntelligenceLevel;
        player.playerStatsManager.m_iFaithLevel        = classStats[classChosen].m_iFaithLevel       ; 
        player.playerStatsManager.m_iLuckLevel         = classStats[classChosen].m_iLuckLevel           ;

        CharacterCreationScreen characterCreationScreen = FindObjectOfType<CharacterCreationScreen>();
        characterCreationScreen.m_CharacterCreationMiddlePannelUI.classChosen = classChosen;
        characterCreationScreen.m_CharacterCreationMiddlePannelUI.RefreshUI();

        classDescription.text = classStats[classChosen].m_sClassDecription;
    }

    private void AssignClassEquipment(int classChosen)
    {
        player.playerInventoryManager.currentHelmetEquipment = classGear[classChosen].headEquipment;
        player.playerInventoryManager.currentTorsoEquipment = classGear[classChosen].chestEquipment;
        player.playerInventoryManager.currentLegEquipment = classGear[classChosen].legEquipment;
        player.playerInventoryManager.currentHandEquipment = classGear[classChosen].handEquipment;

        player.playerInventoryManager.weaponsInRightHandSlots[0] = classGear[classChosen].primaryWeapon;
        player.playerInventoryManager.weaponsInLeftHandSlots[0] = classGear[classChosen].offHandWeapon;

        player.playerEquipmentManager.EquipAllEquipmentModel();
        player.playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }

    public void AssignClass(int num)
    {
        AssignClassStats(num);
        AssignClassEquipment(num);
    }
}
