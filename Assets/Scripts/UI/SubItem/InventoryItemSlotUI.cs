using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlotUI : ItemSlotUI
{
    InventoryUI m_InventoryUI;

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
        Managers.Game.PlayAction(() =>
        {
            Managers.UI.ClosePopupUI();
            EquipmentUI eui = Managers.UI.ShowPopupUI<EquipmentUI>();
            eui.m_TempPrivateItem = m_Item;
            eui.m_CurrentEquipmentsUI.ChangeSlotsBindEvent();
        });
    }

    // ������ �̹����� �����ϸ� ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation(PointerEventData data)
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
        GetImage((int)Images.ItemSelectIcon).enabled = true;
        m_InventoryUI.m_InventoryMiddleUI.ShowItemInformation(m_Item);
        m_InventoryUI.m_InventoryLeftPanelUI.SetInfo(m_Item.itemName);
    }

    // �� Ÿ�Կ� ���� Refresh
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
