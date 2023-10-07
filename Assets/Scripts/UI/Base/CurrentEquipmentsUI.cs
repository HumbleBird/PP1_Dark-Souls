using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CurrentEquipmentsUI : UI_Base
{
    enum Texts
    {
        EquipmentSlotsNameText,
        ItemNameText,
    }

    PlayerManager player;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        player = Managers.Object.m_MyPlayer;

        SetSlotInit();

        return true;
    }

    public void ShowItemInformation(string itemName, string slotPartName)
    {
        GetText((int)Texts.EquipmentSlotsNameText).text = slotPartName;
        if(itemName != null)
        {
            GetText((int)Texts.ItemNameText).text = itemName;
        }
        else
        {
            GetText((int)Texts.ItemNameText).text = "";
        }
    }

    void SetSlotInit()
    {

        int rightWeaponNum = 0;
        int leftWeaponNum = 0;
        int ArrowNum = 0;
        int BoltNum = 0;
        int RingNum = 0;
        int ConsumableNum = 0;

        EquipmentSlotUI[] slots = GetComponentsInChildren<EquipmentSlotUI>();
        foreach (EquipmentSlotUI slot in slots)
        {
            switch (slot.m_EquipmentSlotsPartsName)
            {
                case E_EquipmentSlotsPartType.Right_Hand_Weapon:
                    slot.m_iSlotNum = rightWeaponNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_RightWeaponsSlots);
                    rightWeaponNum++;
                    break;
                case E_EquipmentSlotsPartType.Left_Hand_Weapon:
                    slot.m_iSlotNum = leftWeaponNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_LeftWeaponsSlots);
                    leftWeaponNum++;
                    break;
                case E_EquipmentSlotsPartType.Arrow:
                    slot.m_iSlotNum = ArrowNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_ArrowAmmoSlots);
                    ArrowNum++;
                    break;
                case E_EquipmentSlotsPartType.Bolt:
                    slot.m_iSlotNum = BoltNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_BoltAmmoSlots);
                    BoltNum++;
                    break;
                case E_EquipmentSlotsPartType.Helmt:
                    slot.SetInfo(player.playerEquipmentManager.m_HelmetEquipment);
                    break;
                case E_EquipmentSlotsPartType.Chest_Armor:
                    slot.SetInfo(player.playerEquipmentManager.m_TorsoEquipment);
                    break;
                case E_EquipmentSlotsPartType.Gantlets:
                    slot.SetInfo(player.playerEquipmentManager.m_HandEquipment);
                    break;
                case E_EquipmentSlotsPartType.Leggings:
                    slot.SetInfo(player.playerEquipmentManager.m_LegEquipment);
                    break;
                case E_EquipmentSlotsPartType.Ring:
                    slot.m_iSlotNum = RingNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_RingSlots);
                    RingNum++;
                    break;
                case E_EquipmentSlotsPartType.Consumable:
                    slot.m_iSlotNum = ConsumableNum;
                    DetailPushItems(slot, player.playerEquipmentManager.m_ConsumableItemSlots);
                    ConsumableNum++;
                    break;
                case E_EquipmentSlotsPartType.Pledge:
                    slot.SetInfo(player.playerEquipmentManager.m_CurrentPledge);
                    break;
            }
        }
    }

    public override void RefreshUI()
    {
        EquipmentSlotUI[] slots = GetComponentsInChildren<EquipmentSlotUI>();
        foreach (EquipmentSlotUI slot in slots)
        {
            switch (slot.m_EquipmentSlotsPartsName)
            {
                case E_EquipmentSlotsPartType.Right_Hand_Weapon:
                    DetailPushItems(slot, player.playerEquipmentManager.m_RightWeaponsSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Left_Hand_Weapon:
                    DetailPushItems(slot, player.playerEquipmentManager.m_LeftWeaponsSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Arrow:
                    DetailPushItems(slot, player.playerEquipmentManager.m_ArrowAmmoSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Bolt:
                    DetailPushItems(slot, player.playerEquipmentManager.m_BoltAmmoSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Helmt:
                    DetailPushItem(slot, player.playerEquipmentManager.m_HelmetEquipment, true);
                    break;
                case E_EquipmentSlotsPartType.Chest_Armor:
                    DetailPushItem(slot, player.playerEquipmentManager.m_TorsoEquipment, true);
                    break;
                case E_EquipmentSlotsPartType.Gantlets:
                    DetailPushItem(slot, player.playerEquipmentManager.m_HandEquipment, true);
                    break;
                case E_EquipmentSlotsPartType.Leggings:
                    DetailPushItem(slot, player.playerEquipmentManager.m_LegEquipment, true);
                    break;
                case E_EquipmentSlotsPartType.Ring:
                    DetailPushItems(slot, player.playerEquipmentManager.m_RingSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Consumable:
                    DetailPushItems(slot, player.playerEquipmentManager.m_ConsumableItemSlots, true);
                    break;
                case E_EquipmentSlotsPartType.Pledge:
                    DetailPushItem(slot, player.playerEquipmentManager.m_CurrentPledge, true);
                    break;
            }
        }
    }

    void DetailPushItems(EquipmentSlotUI slotUI, Item[] playerEquipmentItems, bool isRefresh = false)
    {
        for (int i = 0; i < playerEquipmentItems.Length; i++)
        {
            if(playerEquipmentItems[slotUI.m_iSlotNum] != null)
            {
                slotUI.SetInfo(playerEquipmentItems[slotUI.m_iSlotNum]);

                if (isRefresh == true)
                {
                    slotUI.RefreshUI();

                }
                return;
            }
        }
    }

    void DetailPushItem(EquipmentSlotUI slotUI, Item playerEquipmentItems, bool isRefresh = false)
    {
        if (playerEquipmentItems != null)
        {
            slotUI.SetInfo(playerEquipmentItems);

            if (isRefresh == true)
            {
                slotUI.RefreshUI();

            }
            return;
        }
    }

    public void ChangeSlotsBindEvent()
    {
        EquipmentSlotUI[] slots = GetComponentsInChildren<EquipmentSlotUI>();
        foreach (EquipmentSlotUI slot in slots)
        {
            slot.ItemChangeFromInventoryBindEvent();
        }
    }
}
