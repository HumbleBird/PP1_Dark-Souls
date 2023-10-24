using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class EquipmentSlotUI : ItemSlotUI
{
    public E_EquipmentSlotsPartType m_EquipmentSlotType;

    public string m_sSlotPartName; // 슬롯 파트 이름

    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        SetSlotPartName();

        return true;
    }

    void SetSlotPartName()
    {
        if (m_iSlotNum != -1)
        {
            int num = m_iSlotNum + 1;
            m_sSlotPartName = m_EquipmentSlotType.ToString() + " " + num;

        }
        else
        {
            m_sSlotPartName = m_EquipmentSlotType.ToString();

        }
        m_sSlotPartName = m_sSlotPartName.Replace("_", " ");
    }

    // 아이템 이미지를 클릭했을 때 나타나는 이벤트 함수.
    // Equipment UI에서 열었을 떄 (인벤토리를 통해서가 아니라)
    public override void ShowHowtoItem()
    {
        // 장착할 아이템을 인벤토리에서 가져온다.

        // 해당 슬롯 파트의 아이템과 슬롯 정보를 가져와서 넘겨준다.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotPartName, m_EquipmentSlotType, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);
    }

    public void ItemChangeFromInventoryBindEvent()
    {
        m_ItemSlotSubUI.m_ItemSelectIcon.gameObject.BindEvent(() => ChangeBindEvent()); ;
    }

    // 처음에 인벤토리에서 열었다면 교체하려는 아이템 파트의 아이템만 교체 가능하게
    void ChangeBindEvent()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
        bool isSuccess = false;

        switch (m_EquipmentSlotType)
        {
            case E_EquipmentSlotsPartType.Right_Hand_Weapon:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.MeleeWeapon ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.RangeWeapon ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Catalyst ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Shield)
                {
                    player.playerEquipmentManager.m_RightWeaponsSlots[m_iSlotNum] = (WeaponItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;

            case E_EquipmentSlotsPartType.Left_Hand_Weapon:
                if(m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.MeleeWeapon ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.RangeWeapon ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Catalyst ||
                    m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Shield)
                {
                    player.playerEquipmentManager.m_LeftWeaponsSlots[m_iSlotNum] = (WeaponItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Arrow:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Ammo &&
                    (((AmmoItem)m_EquipmentUI.m_TempPrivateItem).ammoType == AmmoType.Arrow))
                {
                    player.playerEquipmentManager.m_ArrowAmmoSlots[m_iSlotNum] = (AmmoItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Bolt:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Ammo &&
                    (((AmmoItem)m_EquipmentUI.m_TempPrivateItem).ammoType == AmmoType.Bolt))
                {
                    player.playerEquipmentManager.m_ArrowAmmoSlots[m_iSlotNum] = (AmmoItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Helmt:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Helmet)
                {
                    player.playerEquipmentManager.m_HelmetEquipment = (HelmEquipmentItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Chest_Armor:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.ChestArmor)
                {
                    player.playerEquipmentManager.m_TorsoEquipment = (TorsoEquipmentItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Gantlets:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Gauntlets)
                {
                    player.playerEquipmentManager.m_HandEquipment = (GantletsEquipmentItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Leggings:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Leggings)
                {
                    player.playerEquipmentManager.m_LegEquipment = (LeggingsEquipmentItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Ring:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Ring)
                {
                    player.playerEquipmentManager.m_RingSlots[m_iSlotNum] = (RingItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Consumable:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Tool)
                {
                    player.playerEquipmentManager.m_ConsumableItemSlots[m_iSlotNum] = (ToolItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Pledge:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Pledge)
                {
                    player.playerEquipmentManager.m_CurrentPledge = (PledgeItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            default:
                break;
        }

        if(isSuccess)
        {
            m_EquipmentUI.m_TempPrivateItem.m_isEquiping = true;
            player.playerEquipmentManager.Refresh();
            m_EquipmentUI.m_CurrentEquipmentsUI.RefreshUI();

            RefreshUI();

            Managers.Game.PlayAction(() =>
            {
                Managers.UI.ClosePopupUI();
                Managers.GameUI.m_EquipmentUI = null;
                Managers.UI.ShowPopupUI<InventoryUI>();
            });
        }
    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation()
    {
        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
        m_EquipmentUI.m_CurrentEquipmentsUI.PriviousSlotClear();
        m_EquipmentUI.m_CurrentEquipmentsUI.m_CurrentEquipmentSlot = this;
        m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;

        if (m_Item == null)
        {
            m_EquipmentUI.m_CurrentEquipmentsUI.ShowItemInformation(null, m_sSlotPartName);
        }
        else
        {
            m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
            m_EquipmentUI.m_CurrentEquipmentsUI.ShowItemInformation(m_Item.name, m_sSlotPartName);
        }

    }

    public override void RefreshUI()
    {
        base.RefreshUI();

        m_ItemSlotSubUI.m_ItemPlateIcon.enabled = false;
        m_ItemSlotSubUI.m_ItemUsingIcon.enabled = false;
    }
}
