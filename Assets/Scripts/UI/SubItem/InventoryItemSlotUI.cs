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

    // 아이템 이미지를 선택했을 때 해당 아이템 선택지를 보여준다.
    // 장비라면 장착
    // 소울이라면 사용 등
    // 지금은 일단 장착
    public override void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        Managers.Sound.Play("UI/Popup_ButtonClose");

        EquipmentItemChange();
    }

    // 장비창에서 아이템을 교체 혹은 장착하려고 함.
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

    // 아이템 이미지를 선택하면(Pointer Down) 가운데 패널에 아이템 정보를 보여준다.
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
