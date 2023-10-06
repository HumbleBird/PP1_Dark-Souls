using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentToInventoryShowItemSubItem : ItemSlotUI
{
    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon,
        ItemProtectIcon,
    }

    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        return true;
    }

    // Ŭ���� ����
    // ��� �������� �÷��̾��� �� ��° ���Կ� ������ų�� �˾ƾ� ��.

    public void ChangeItem()
    {
        if (m_Item == null)
            return;

        // �� ��° ĭ�� �������� �÷��̾��� equipment�� ����
        // UI �ݰ� ����
    }
    
    public override void ShowHowtoItem()
    {
        base.ShowHowtoItem();

        // ������ ��ü
        PlayerManager player = Managers.Object.m_MyPlayer;
        int num = m_EquipmentUI.m_ShowItemInventoryUI.m_iEquipmentSlotNum;

        switch (m_EquipmentUI.m_ShowItemInventoryUI.m_E_EquipmentSlotsPartType)
        {
            case Define.E_EquipmentSlotsPartType.Right_Hand_Weapon:
                player.playerEquipmentManager.m_RightWeaponsSlots[num] = (WeaponItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Left_Hand_Weapon:
                player.playerEquipmentManager.m_LeftWeaponsSlots[num] = (WeaponItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Arrow:
                player.playerEquipmentManager.m_ArrowAmmoSlots[num] = (RangedAmmoItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Bolt:
                player.playerEquipmentManager.m_BoltAmmoSlots[num] = (RangedAmmoItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Helmt:
                player.playerEquipmentManager.m_HelmetEquipment = (HelmEquipmentItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Chest_Armor:
                player.playerEquipmentManager.m_TorsoEquipment = (TorsoEquipmentItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Gantlets:
                player.playerEquipmentManager.m_HandEquipment = (GantletsEquipmentItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Leggings:
                player.playerEquipmentManager.m_LegEquipment = (LeggingsEquipmentItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Ring:
                player.playerEquipmentManager.m_RingSlots[num] = (RingItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Consumable:
                player.playerEquipmentManager.m_ConsumableItemSlots[num] = (ToolItem)m_Item;
                break;
            case Define.E_EquipmentSlotsPartType.Pledge:
                player.playerEquipmentManager.m_CurrentPledge = m_Item;
                break;
            default:
                break;
        }

        player.playerEquipmentManager.Refresh();

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(true);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(false);
    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_ShowItemInventoryUI.ShowItemInformation(m_Item.name);
    }
}
