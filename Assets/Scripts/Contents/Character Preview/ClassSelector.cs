using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

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
        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Helmt, classGear[classChosen].headEquipment);
        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Chest_Armor, classGear[classChosen].chestEquipment);
        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Gantlets, classGear[classChosen].handEquipment);
        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Leggings, classGear[classChosen].legEquipment);

        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Right_Hand_Weapon, classGear[classChosen].primaryWeapon);
        player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Left_Hand_Weapon, classGear[classChosen].offHandWeapon);

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
