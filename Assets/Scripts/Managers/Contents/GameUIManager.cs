using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : UI_Base
{
    public PlayerManager player;
    public ItemStatWindowUI itemStatWindowUI;

    public EquipmentWindowUI equipmentWindowUI;
    public QuickSlotsUI quickSlotsUI;

    [Header("HUD")]
    public GameObject crossHair;
    public TextMeshProUGUI soulCount;
    public HealthBar              m_HealthBar;
    public StaminaBar             m_StaminaBar;
    public FocusPointBar          m_FocusPointBar;
    public PoisonBuildUpBar       m_PoisonBuildUpBar;
    public PoisonAmountBar        m_PoisonAmountBar;

    [Header("UI Windows")]
    public GameObject hudWindow;
    public GameObject selectWindow;
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow;
    public GameObject itemStatsWindow;
    public GameObject levelUpWindow;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool headEquipmentSlotSelected;
    public bool bodyEquipmentSlotSelected;
    public bool legEquipmentSlotSelected;
    public bool handEquipmentSlotSelected;

    [Header("Pop Ups")]
    BonfireLitPopupUI bonfireLitPopupUI;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    WeaponInventorySlot[] weaponInventorySlots;

    [Header("Head Equipment Inventory")]
    public GameObject headEquipmentInventorySlotPrefab;
    public Transform headEquipmentInventorySlotsParent;
    HeadEquipmentInventorySlot[] headEquipmentInventorySlots;

    [Header("Body Equipment Inventory")]
    public GameObject bodyEquipmentInventorySlotPrefab;
    public Transform bodyEquipmentInventorySlotsParent;
    BodyEquipmentInventorySlot[] bodyEquipmentInventorySlots;

    [Header("Leg Equipment Inventory")]
    public GameObject legEquipmentInventorySlotPrefab;
    public Transform legEquipmentInventorySlotsParent;
    LegEquipmentInventorySlot[] legEquipmentInventorySlots;

    [Header("Hand Equipment Inventory")]
    public GameObject handEquipmentInventorySlotPrefab;
    public Transform handEquipmentInventorySlotsParent;
    HandEquipmentInventorySlot[] handEquipmentInventorySlots;

    private void Awake()
    {
        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
        bonfireLitPopupUI = GetComponentInChildren<BonfireLitPopupUI>();

        m_HealthBar = GetComponentInChildren<HealthBar       >();
        m_StaminaBar = GetComponentInChildren<StaminaBar      >();
        m_FocusPointBar = GetComponentInChildren<FocusPointBar   >();
        m_PoisonBuildUpBar = GetComponentInChildren<PoisonBuildUpBar>();
        m_PoisonAmountBar = GetComponentInChildren<PoisonAmountBar>();

        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        headEquipmentInventorySlots = headEquipmentInventorySlotsParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
        bodyEquipmentInventorySlots = bodyEquipmentInventorySlotsParent.GetComponentsInChildren<BodyEquipmentInventorySlot>();
        legEquipmentInventorySlots = legEquipmentInventorySlotsParent.GetComponentsInChildren< LegEquipmentInventorySlot>();
        handEquipmentInventorySlots = handEquipmentInventorySlotsParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
    }

    private void Start()
    {
        player = Managers.Object.m_MyPlayer;


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
            if (i < player.playerInventoryManager.weaponsInventory.Count)
            {
                if (weaponInventorySlots.Length < player.playerInventoryManager.weaponsInventory.Count)
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

        // Body Equipment Inventory Slot
        for (int i = 0; i < bodyEquipmentInventorySlots.Length; i++)
        {
            if (i < player.playerInventoryManager.bodyEquipmentInventory.Count)
            {
                if (bodyEquipmentInventorySlots.Length < player.playerInventoryManager.bodyEquipmentInventory.Count)
                {
                    Instantiate(bodyEquipmentInventorySlotPrefab, bodyEquipmentInventorySlotsParent);
                    bodyEquipmentInventorySlots = bodyEquipmentInventorySlotsParent.GetComponentsInChildren<BodyEquipmentInventorySlot>();
                }
                bodyEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.bodyEquipmentInventory[i]);
            }
            else
            {
                bodyEquipmentInventorySlots[i].ClearInventorySlot();
            }
        }

        // Leg Equipment Inventory Slot
        for (int i = 0; i < legEquipmentInventorySlots.Length; i++)
        {
            if (i < player.playerInventoryManager.legEquipmentInventory.Count)
            {
                if (legEquipmentInventorySlots.Length < player.playerInventoryManager.legEquipmentInventory.Count)
                {
                    Instantiate(legEquipmentInventorySlotPrefab, legEquipmentInventorySlotsParent);
                    legEquipmentInventorySlots = legEquipmentInventorySlotsParent.GetComponentsInChildren<LegEquipmentInventorySlot>();
                }
                legEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.legEquipmentInventory[i]);
            }
            else
            {
                legEquipmentInventorySlots[i].ClearInventorySlot();
            }
        }

        // Hand Equipment Inventory Slot
        for (int i = 0; i < handEquipmentInventorySlots.Length; i++)
        {
            if (i < player.playerInventoryManager.handEquipmentInventory.Count)
            {
                if (handEquipmentInventorySlots.Length < player.playerInventoryManager.handEquipmentInventory.Count)
                {
                    Instantiate(handEquipmentInventorySlotPrefab, handEquipmentInventorySlotsParent);
                    handEquipmentInventorySlots = handEquipmentInventorySlotsParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
                }
                handEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.handEquipmentInventory[i]);
            }
            else
            {
                handEquipmentInventorySlots[i].ClearInventorySlot();
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
        itemStatsWindow.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Selected = false;
        rightHandSlot02Selected = false;
        leftHandSlot01Selected  = false;
        leftHandSlot02Selected = false;

        headEquipmentSlotSelected = false;
        bodyEquipmentSlotSelected = false;
        legEquipmentSlotSelected = false;
        handEquipmentSlotSelected = false;
    }

    public void ActivateBonfirePopup()
    {
        bonfireLitPopupUI.DisplayBonfireLitPopup();
    }
}
