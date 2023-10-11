using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class EquipmentSlotUI : ItemSlotUI
{
    public E_EquipmentSlotsPartType m_EquipmentSlotsPartsName;

    public int m_iSlotNum = -1; // ���� ��ȣ
    public string m_sSlotPartName; // ���� ��Ʈ �̸�

    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SetSlotPartName();

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        return true;
    }

    void SetSlotPartName()
    {
        if (m_iSlotNum != -1)
        {
            int num = m_iSlotNum + 1;
            m_sSlotPartName = m_EquipmentSlotsPartsName.ToString() + " " + num;

        }
        else
        {
            m_sSlotPartName = m_EquipmentSlotsPartsName.ToString();

        }
        m_sSlotPartName = m_sSlotPartName.Replace("_", " ");
    }

    // ������ �̹����� Ŭ������ �� ��Ÿ���� �̺�Ʈ �Լ�.
    public override void ShowHowtoItem()
    {
        // ������ �������� �κ��丮���� �����´�.

        // �ش� ���� ��Ʈ�� �����۰� ���� ������ �����ͼ� �Ѱ��ش�.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotPartName, m_EquipmentSlotsPartsName, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);
    }

    public void ItemChangeFromInventoryBindEvent()
    {
        GetImage((int)Images.ItemSelectIcon).gameObject.BindEvent(() => ChangeBindEvent()); ;
    }

    void ChangeBindEvent()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
        bool isSuccess = false;

        // ó���� �κ��丮���� �����ٸ� ��ü�Ϸ��� ������ ��Ʈ�� �����۸� ��ü �����ϰ�
        switch (m_EquipmentSlotsPartsName)
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
                    (((RangedAmmoItem)m_EquipmentUI.m_TempPrivateItem).ammoType == AmmoType.Arrow))
                {
                    player.playerEquipmentManager.m_ArrowAmmoSlots[m_iSlotNum] = (RangedAmmoItem)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            case E_EquipmentSlotsPartType.Bolt:
                if (m_EquipmentUI.m_TempPrivateItem.m_EItemType == E_ItemType.Ammo &&
                    (((RangedAmmoItem)m_EquipmentUI.m_TempPrivateItem).ammoType == AmmoType.Bolt))
                {
                    player.playerEquipmentManager.m_ArrowAmmoSlots[m_iSlotNum] = (RangedAmmoItem)m_EquipmentUI.m_TempPrivateItem;
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
                    player.playerEquipmentManager.m_CurrentPledge = (Item)m_EquipmentUI.m_TempPrivateItem;
                    isSuccess = true;
                }
                break;
            default:
                break;
        }

        if(isSuccess)
        {
            player.playerEquipmentManager.Refresh();
            m_EquipmentUI.m_CurrentEquipmentsUI.RefreshUI();

            RefreshUI();

            Managers.Game.PlayAction(() =>
            {
                Managers.UI.ClosePopupUI();
                Managers.UI.ShowPopupUI<InventoryUI>();
            });
        }
    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation(PointerEventData data)
    {
        Managers.Sound.Play("Sounds/UI/UI_Button_PointerDown_04");
        GetImage((int)Images.ItemSelectIcon).enabled = true;

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

    // ������ �̹����� �������� ���� �� ���� �̹��� ȿ���� �����Ѵ�.
    public override void PointerExitItem(PointerEventData data)
    {

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }
}
