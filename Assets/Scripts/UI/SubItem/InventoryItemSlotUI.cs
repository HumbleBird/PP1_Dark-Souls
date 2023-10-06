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

    // ������ �̹����� �������� �� ��� ���θ� ���´�.
    public override void ShowHowtoItem()
    {
        base.ShowHowtoItem();

        // ���â ����
    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_InventoryUI.m_InventoryMiddleUI.ShowItemInformation(m_Item);
    }
}
