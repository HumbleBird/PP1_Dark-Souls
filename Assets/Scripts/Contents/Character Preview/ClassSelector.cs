using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelector : MonoBehaviour
{
    PlayerManager player;

    [Header("Class Info UI")]
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
        // 전 아이템 전부 처분 후 바꿔뀌기
        player.playerInventoryManager.Clear();

        // 현재 장착
        player.playerEquipmentManager.m_HelmetEquipment = classGear[classChosen].headEquipment;
        player.playerEquipmentManager.m_TorsoEquipment = classGear[classChosen].chestEquipment;
        player.playerEquipmentManager.m_LegEquipment = classGear[classChosen].legEquipment;
        player.playerEquipmentManager.m_HandEquipment = classGear[classChosen].handEquipment;

        player.playerEquipmentManager.m_RightWeaponsSlots[0] = classGear[classChosen].primaryWeapon;
        player.playerEquipmentManager.m_LeftWeaponsSlots[0] = classGear[classChosen].offHandWeapon;
        player.playerEquipmentManager.m_CurrentHandRightWeapon = player.playerEquipmentManager.m_RightWeaponsSlots[0];
        player.playerEquipmentManager.m_CurrentHandLeftWeapon = player.playerEquipmentManager.m_LeftWeaponsSlots[0];

        // 인벤토리에 넣기
        player.playerInventoryManager.Add(classGear[classChosen].primaryWeapon);
        player.playerInventoryManager.Add(classGear[classChosen].offHandWeapon);

        player.playerInventoryManager.Add(classGear[classChosen].headEquipment);
        player.playerInventoryManager.Add(classGear[classChosen].chestEquipment);
        player.playerInventoryManager.Add(classGear[classChosen].legEquipment);
        player.playerInventoryManager.Add(classGear[classChosen].handEquipment);

        player.playerEquipmentManager.EquipAllEquipmentModel();
        player.playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }

    public void AssignClass(int num)
    {
        AssignClassStats(num);
        AssignClassEquipment(num);
    }
}
