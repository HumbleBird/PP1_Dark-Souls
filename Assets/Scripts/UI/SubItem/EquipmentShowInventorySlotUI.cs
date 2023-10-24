using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class EquipmentShowInventorySlotUI : ItemSlotUI
{
    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();



        RefreshUI();

        return true;
    }

    private void Start()
    {
        if (m_iSlotNum == 0)
            ShowItemInformation();
    }

    public override void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_ButtonClose");


        // 아이템 교체
        E_EquipmentSlotsPartType type = m_EquipmentUI.m_ShowItemInventoryUI.m_E_EquipmentSlotsPartType;
        int num = m_EquipmentUI.m_ShowItemInventoryUI.m_iEquipmentSlotNum;

        PlayerManager player = Managers.Object.m_MyPlayer;
        player.playerEquipmentManager.ChangeEquipment(type, m_Item, num);

        // Equipment UI Refresh
        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(true);
        m_EquipmentUI.m_CurrentEquipmentsUI.RefreshUI();
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(false);
    }

    // 아이템 이미지를 선택하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_ShowItemInventoryUI.ShowItemInformation(m_Item.name);
        m_EquipmentUI.m_ShowItemInventoryUI.PrivousSlotClear();
        m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;
        m_EquipmentUI.m_ShowItemInventoryUI.m_iCurrentSelectSlot = m_iSlotNum;
    }
}
