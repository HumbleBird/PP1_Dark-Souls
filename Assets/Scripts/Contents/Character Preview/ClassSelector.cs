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

    CharacterCreationScreen characterCreationScreen;

    private void Start()
    {
        player = Managers.Object.m_MyPlayer;
        characterCreationScreen = FindObjectOfType<CharacterCreationScreen>();
    }

    private void AssignClassStats(Table_StartClassStat.Info data)
    {
        player.playerStatsManager.playerLevel          = data.m_iPlayerLevel;
        player.playerStatsManager.m_iVigorLevel        = data.m_iVigorLevel       ;
        player.playerStatsManager.m_iAttunementLevel   = data.m_iAttunementLevel  ;
        player.playerStatsManager.m_iEnduranceLevel    = data.m_iEnduranceLevel   ;
        player.playerStatsManager.m_iVitalityLevel     = data.m_iVitalityLevel    ;
        player.playerStatsManager.m_iStrengthLevel     = data.m_iStrengthLevel    ;
        player.playerStatsManager.m_iDexterityLevel    = data.m_iDexterityLevel   ;
        player.playerStatsManager.m_iIntelligenceLevel = data.m_iIntelligenceLevel;
        player.playerStatsManager.m_iFaithLevel        = data.m_iFaithLevel       ; 
        player.playerStatsManager.m_iLuckLevel         = data.m_iLuckLevel           ;

        characterCreationScreen.m_CharacterCreationMiddlePannelUI.classChosen = data;
        characterCreationScreen.m_CharacterCreationMiddlePannelUI.RefreshUI();

        classDescription.text = data.m_sClassDescrition;
    }

    private void AssignClassEquipment(Table_StartClassStat.Info data)
    {
        // 전 아이템 전부 처분 후 바꿔뀌기
        player.playerInventoryManager.Clear();

        // 현재 장착

        // Left Hand1
        {
            Table_Item_Weapon.Info weapon = Managers.Table.m_Item_Weapon.Get(data.m_iLeftHand1Id);
            if (weapon != null)
            {
                WeaponItem item = (WeaponItem)Managers.Game.MakeItem(E_ItemType.MeleeWeapon, data.m_iLeftHand1Id);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Left_Hand_Weapon, item);
                player.playerInventoryManager.Add(item);
            }


        }

        // Right Hand1
        {
            Table_Item_Weapon.Info weapon = Managers.Table.m_Item_Weapon.Get(data.m_iRightHand1Id);
            if (weapon != null)
            {
                WeaponItem item = (WeaponItem)Managers.Game.MakeItem(E_ItemType.MeleeWeapon, data.m_iRightHand1Id);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Right_Hand_Weapon, item);
                player.playerInventoryManager.Add(item);
            }
        }

        // Head
        {
            Table_Item_Armor.Info armor = Managers.Table.m_Item_Armor.Get(data.m_iHeadArmorId);
            if (armor != null)
            {
                HelmEquipmentItem item = (HelmEquipmentItem)Managers.Game.MakeItem(E_ItemType.Helmet, data.m_iHeadArmorId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Helmt, item);
                player.playerInventoryManager.Add(item);
            }
        }

        // Chest Armor
        {
            Table_Item_Armor.Info armor = Managers.Table.m_Item_Armor.Get(data.m_iChestArmorId);
            if (armor != null)
            {
                TorsoEquipmentItem item = (TorsoEquipmentItem)Managers.Game.MakeItem(E_ItemType.ChestArmor, data.m_iChestArmorId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Chest_Armor, item);
                player.playerInventoryManager.Add(item);
            }

        }

        // Hand
        {
            Table_Item_Armor.Info armor = Managers.Table.m_Item_Armor.Get(data.m_iHandArmorId);
            if (armor != null)
            {
                GantletsEquipmentItem item = (GantletsEquipmentItem)Managers.Game.MakeItem(E_ItemType.Gauntlets, data.m_iHandArmorId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Gantlets, item);
                player.playerInventoryManager.Add(item);
            }
        }

        // Leg
        {
            Table_Item_Armor.Info armor = Managers.Table.m_Item_Armor.Get(data.m_iLegArmorId);
            if (armor != null)
            {
                LeggingsEquipmentItem item = (LeggingsEquipmentItem)Managers.Game.MakeItem(E_ItemType.Leggings, data.m_iLegArmorId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Leggings, item);
                player.playerInventoryManager.Add(item);
            }
        }

        // Spell
        {
            // 1
            Table_Item_Spell.Info spll = Managers.Table.m_Item_Spell.Get(data.m_iSpell1Id);
            if (spll != null)
            {
                SpellItem item = (SpellItem)Managers.Game.MakeItem(E_ItemType.Magic, data.m_iSpell1Id);
                item.m_bEquipped = true;
                player.playerEquipmentManager.m_CurrentHandSpell = item;
                player.playerInventoryManager.Add(item);
            }
        }

        // Ring
        {
            Table_Item_Ring.Info ring = Managers.Table.m_Item_Ring.Get(data.m_iRindId);
            if (ring != null)
            {
                RingItem item = (RingItem)Managers.Game.MakeItem(E_ItemType.Magic, data.m_iRindId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Ring, item);
                player.playerInventoryManager.Add(item);
            }
        }

        // Toll
        {
            // 에스트
            ToolItem item = (ToolItem)Managers.Game.MakeItem(E_ItemType.Tool, 1);
            player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Consumable, item);
            player.playerInventoryManager.Add(item);
        }
        {
            // 추가 아이템
            Table_Item_Tool.Info tool = Managers.Table.m_Item_Tool.Get(data.m_iToolItemId);
            if (tool != null)
            {
                ToolItem item = (ToolItem)Managers.Game.MakeItem(E_ItemType.Tool, data.m_iToolItemId);
                player.playerEquipmentManager.ChangeEquipment(E_EquipmentSlotsPartType.Consumable, item);
                player.playerInventoryManager.Add(item);
            }
        }

        player.playerEquipmentManager.EquipAllEquipmentModel();
        player.playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }

    public void AssignClass(int id)
    {
        Table_StartClassStat.Info data = Managers.Table.m_StartClassStat.Get(id);
        if (data == null)
            return;

        AssignClassStats(data);
        AssignClassEquipment(data);
    }
}
