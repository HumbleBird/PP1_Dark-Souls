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

    // 아이템 이미지를 선택했을 때 해당 아이템 선택지를 보여준다.
    // 장비라면 장착
    // 소울이라면 사용 등
    public override void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        switch (m_Item.m_EItemType)
        {
            case Define.E_ItemType.Tool:
                break;
            case Define.E_ItemType.ReinforcedMaterial:
                break;
            case Define.E_ItemType.Valuables:
                break;
            case Define.E_ItemType.Magic:
                break;
            case Define.E_ItemType.MeleeWeapon:
                break;
            case Define.E_ItemType.RangeWeapon:
                break;
            case Define.E_ItemType.Catalyst:
                break;
            case Define.E_ItemType.Shield:
                break;
            case Define.E_ItemType.Helmet:
                break;
            case Define.E_ItemType.ChestArmor:
                break;
            case Define.E_ItemType.Gauntlets:
                break;
            case Define.E_ItemType.Leggings:
                break;
            case Define.E_ItemType.Ammo:
                break;
            case Define.E_ItemType.Ring:
                break;
            case Define.E_ItemType.Pledge:
                break;
            default:
                break;
        }
    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_InventoryUI.m_InventoryMiddleUI.ShowItemInformation(m_Item);
    }

    // 각 타입에 따른 Refresh
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
