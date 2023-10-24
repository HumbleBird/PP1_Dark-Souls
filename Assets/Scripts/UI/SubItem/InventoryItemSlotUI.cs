using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlotUI : ItemSlotUI
{
    InventoryUI m_InventoryUI;
    public InventoryItemTapSlotUI m_InventoryItemTapSlotUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_InventoryUI = GetComponentInParent<InventoryUI>();

        return true;
    }

    // ������ �̹����� �������� �� �ش� ������ �������� �����ش�.
    // ����� ����
    // �ҿ��̶�� ��� ��
    // ������ �ϴ� ����
    public override void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_ButtonClose");

        EquipmentItemChange();
    }

    // ���â���� �������� ��ü Ȥ�� �����Ϸ��� ��.
    void EquipmentItemChange()
    {
        if (m_Item.m_EItemType == Define.E_ItemType.Magic)
            return;

        Managers.Game.PlayAction(() =>
        {
            Managers.UI.ClosePopupUI();
            EquipmentUI eui = Managers.UI.ShowPopupUI<EquipmentUI>();
            Managers.GameUI.m_EquipmentUI = eui;
            eui.m_TempPrivateItem = m_Item;
            eui.m_CurrentEquipmentsUI.ChangeSlotsBindEvent();
        });
    }

    // ������ �̹����� �����ϸ�(Pointer Down) ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
        m_InventoryItemTapSlotUI.PriviousSlotClear();

        m_InventoryUI.ShowItemInfo(m_Item);

        m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;
        m_InventoryUI.m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum = m_iSlotNum;
    }
}
