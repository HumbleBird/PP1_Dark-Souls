using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public PlayerManager player;
    public EquipmentWindowUI equipmentWindowUI;
    public QuickSlotsUI quickSlotsUI;

    [Header("HUD")]
    public GameObject crossHair;
    public TextMeshProUGUI soulCount;

    [Header("UI Windows")]
    public GameObject hudWindow;
    public GameObject selectWindow;
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow;
    public GameObject levelUpWindow;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool headEquipmentSlotSelected;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    WeaponInventorySlot[] weaponInventorySlots;

    [Header("Head Equipment Inventory")]
    public GameObject headEquipmentInventorySlotPrefab;
    public Transform headEquipmentInventorySlotsParent;
    HeadEquipmentInventorySlot[] headEquipmentInventorySlots;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();

        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        headEquipmentInventorySlots = headEquipmentInventorySlotsParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
    }

    private void Start()
    {
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(player.playerInventoryManager);

        if (player.playerInventoryManager.currentSpell != null)
        {
            quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);

        }

        if (player.playerInventoryManager.currentConsumable != null)
        {
            quickSlotsUI.UpdateCurrentConsumableIcon(player.playerInventoryManager.currentConsumable);

        }

        soulCount.text = player.playerStatsManager.currentSoulCount.ToString();
    }

    public void UpdateUI()
    {
        // Weapon Inventory Slots
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if(i < player.playerInventoryManager.weaponsInventory.Count)
            {
                if(weaponInventorySlots.Length < player.playerInventoryManager.weaponsInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(player.playerInventoryManager.weaponsInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }

        // Head Equipment Inventory Slot
        for (int i = 0; i < headEquipmentInventorySlots.Length; i++)
        {
            if (i < player.playerInventoryManager.headEquipmentInventory.Count)
            {
                if (headEquipmentInventorySlots.Length < player.playerInventoryManager.headEquipmentInventory.Count)
                {
                    Instantiate(headEquipmentInventorySlotPrefab, headEquipmentInventorySlotsParent);
                    headEquipmentInventorySlots = headEquipmentInventorySlotsParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
                }
                headEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.headEquipmentInventory[i]);
            }
            else
            {
                headEquipmentInventorySlots[i].ClearInventorySlot();
            }
        }
    }

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        ResetAllSelectedSlots();
        weaponInventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Selected = false;
        rightHandSlot02Selected = false;
        leftHandSlot01Selected  = false;
        leftHandSlot02Selected = false;
        headEquipmentSlotSelected = false;
    }
}
