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

    // 아이템 이미지를 선택했을 때 사용 여부를 묻는다.
    public override void ShowHowtoItem()
    {
        base.ShowHowtoItem();

        // 장비창 열기
    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_InventoryUI.m_InventoryMiddleUI.ShowItemInformation(m_Item);
    }
}
