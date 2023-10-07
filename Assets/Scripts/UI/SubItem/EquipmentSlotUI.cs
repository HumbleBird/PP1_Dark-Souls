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

    public int m_iSlotNum = -1; // 슬롯 번호
    public string m_sSlotPartName; // 슬롯 파트 이름

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

    public override void ShowHowtoItem()
    {
        // 장착할 아이템을 인벤토리에서 가져온다.

        // 해당 슬롯 파트의 아이템과 슬롯 정보를 가져와서 넘겨준다.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotPartName, m_EquipmentSlotsPartsName, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);

    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation(PointerEventData data)
    {
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

    // 아이템 이미지를 선택하지 않을 때 선택 이미지 효과를 삭제한다.
    public override void PointerExitItem(PointerEventData data)
    {

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }
}
