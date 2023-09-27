
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : UI_Base
{
    public PlayerManager player;

    [Header("Player Private Window UI")]
    public PlayerPrivateUI m_PlayerPrivateUI;

    [Header("HUD Window UI")]
    public HUDUI m_HUDUI;

    [Header("Interact Window UI")]
    public InteractablePopupUI m_InteractablePopupUI;

    public bool m_bIsShowingPopup = false;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool headEquipmentSlotSelected;
    public bool bodyEquipmentSlotSelected;
    public bool legEquipmentSlotSelected;
    public bool handEquipmentSlotSelected;

    //[Header("Weapon Inventory")]
    //public GameObject weaponInventorySlotPrefab;
    //public Transform weaponInventorySlotsParent;
    //WeaponInventorySlot[] weaponInventorySlots;

    //[Header("Head Equipment Inventory")]
    //public GameObject headEquipmentInventorySlotPrefab;
    //public Transform headEquipmentInventorySlotsParent;
    //HeadEquipmentInventorySlot[] headEquipmentInventorySlots;

    //[Header("Body Equipment Inventory")]
    //public GameObject bodyEquipmentInventorySlotPrefab;
    //public Transform bodyEquipmentInventorySlotsParent;
    //BodyEquipmentInventorySlot[] bodyEquipmentInventorySlots;

    //[Header("Leg Equipment Inventory")]
    //public GameObject legEquipmentInventorySlotPrefab;
    //public Transform legEquipmentInventorySlotsParent;
    //LegEquipmentInventorySlot[] legEquipmentInventorySlots;

    //[Header("Hand Equipment Inventory")]
    //public GameObject handEquipmentInventorySlotPrefab;
    //public Transform handEquipmentInventorySlotsParent;
    //HandEquipmentInventorySlot[] handEquipmentInventorySlots;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_PlayerPrivateUI = GetComponentInChildren<PlayerPrivateUI>();
        m_HUDUI = GetComponentInChildren<HUDUI>();

        return true;
    }



    //public void UpdateUI()
    //{
    //    // Weapon Inventory Slots
    //    for (int i = 0; i < weaponInventorySlots.Length; i++)
    //    {
    //        if (i < player.playerInventoryManager.weaponsInventory.Count)
    //        {
    //            if (weaponInventorySlots.Length < player.playerInventoryManager.weaponsInventory.Count)
    //            {
    //                Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
    //                weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
    //            }
    //            weaponInventorySlots[i].AddItem(player.playerInventoryManager.weaponsInventory[i]);
    //        }
    //        else
    //        {
    //            weaponInventorySlots[i].ClearInventorySlot();
    //        }
    //    }

    //    // Head Equipment Inventory Slot
    //    for (int i = 0; i < headEquipmentInventorySlots.Length; i++)
    //    {
    //        if (i < player.playerInventoryManager.headEquipmentInventory.Count)
    //        {
    //            if (headEquipmentInventorySlots.Length < player.playerInventoryManager.headEquipmentInventory.Count)
    //            {
    //                Instantiate(headEquipmentInventorySlotPrefab, headEquipmentInventorySlotsParent);
    //                headEquipmentInventorySlots = headEquipmentInventorySlotsParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
    //            }
    //            headEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.headEquipmentInventory[i]);
    //        }
    //        else
    //        {
    //            headEquipmentInventorySlots[i].ClearInventorySlot();
    //        }
    //    }

    //    // Body Equipment Inventory Slot
    //    for (int i = 0; i < bodyEquipmentInventorySlots.Length; i++)
    //    {
    //        if (i < player.playerInventoryManager.bodyEquipmentInventory.Count)
    //        {
    //            if (bodyEquipmentInventorySlots.Length < player.playerInventoryManager.bodyEquipmentInventory.Count)
    //            {
    //                Instantiate(bodyEquipmentInventorySlotPrefab, bodyEquipmentInventorySlotsParent);
    //                bodyEquipmentInventorySlots = bodyEquipmentInventorySlotsParent.GetComponentsInChildren<BodyEquipmentInventorySlot>();
    //            }
    //            bodyEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.bodyEquipmentInventory[i]);
    //        }
    //        else
    //        {
    //            bodyEquipmentInventorySlots[i].ClearInventorySlot();
    //        }
    //    }

    //    // Leg Equipment Inventory Slot
    //    for (int i = 0; i < legEquipmentInventorySlots.Length; i++)
    //    {
    //        if (i < player.playerInventoryManager.legEquipmentInventory.Count)
    //        {
    //            if (legEquipmentInventorySlots.Length < player.playerInventoryManager.legEquipmentInventory.Count)
    //            {
    //                Instantiate(legEquipmentInventorySlotPrefab, legEquipmentInventorySlotsParent);
    //                legEquipmentInventorySlots = legEquipmentInventorySlotsParent.GetComponentsInChildren<LegEquipmentInventorySlot>();
    //            }
    //            legEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.legEquipmentInventory[i]);
    //        }
    //        else
    //        {
    //            legEquipmentInventorySlots[i].ClearInventorySlot();
    //        }
    //    }

    //    // Hand Equipment Inventory Slot
    //    for (int i = 0; i < handEquipmentInventorySlots.Length; i++)
    //    {
    //        if (i < player.playerInventoryManager.handEquipmentInventory.Count)
    //        {
    //            if (handEquipmentInventorySlots.Length < player.playerInventoryManager.handEquipmentInventory.Count)
    //            {
    //                Instantiate(handEquipmentInventorySlotPrefab, handEquipmentInventorySlotsParent);
    //                handEquipmentInventorySlots = handEquipmentInventorySlotsParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
    //            }
    //            handEquipmentInventorySlots[i].AddItem(player.playerInventoryManager.handEquipmentInventory[i]);
    //        }
    //        else
    //        {
    //            handEquipmentInventorySlots[i].ClearInventorySlot();
    //        }
    //    }


    //}
}
