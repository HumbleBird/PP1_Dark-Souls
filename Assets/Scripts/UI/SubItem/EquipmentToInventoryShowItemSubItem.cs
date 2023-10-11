using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentToInventoryShowItemSubItem : ItemSlotUI
{

    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        return true;
    }
    
    public override void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("Sounds/UI/UI_Button_Select_02");


        // 아이템 교체
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

        // Equipment UI Refresh
        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(true);
        m_EquipmentUI.m_CurrentEquipmentsUI.RefreshUI();
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(false);
    }

    // 아이템 이미지를 선택하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation(PointerEventData data)
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("Sounds/UI/UI_Button_PointerDown_04");
        GetImage((int)Images.ItemSelectIcon).enabled = true;
        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_ShowItemInventoryUI.ShowItemInformation(m_Item.name);
    }

    public override void RefreshUI()
    {
        if (m_Item != null)
        {
            GetImage((int)Images.ItemIcon).sprite = m_Item.itemIcon;
            GetImage((int)Images.ItemIcon).enabled = true;
            GetImage((int)Images.ItemBasePlateIcon).enabled = true;
        }
        else
        {
            GetImage((int)Images.ItemIcon).sprite = null;
            GetImage((int)Images.ItemIcon).enabled = false;
            GetImage((int)Images.ItemBasePlateIcon).enabled = false;
        }
    }
}
