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

    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    public int m_iSlotNum = -1;
    public string m_sSlotName;
    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        if (m_iSlotNum != -1)
        {
            m_iSlotNum += 1;
            m_sSlotName = m_EquipmentSlotsPartsName.ToString() + " " + m_iSlotNum;

        }
        else
        {
            m_sSlotName = m_EquipmentSlotsPartsName.ToString();

        }
        m_sSlotName = m_sSlotName.Replace("_", " ");

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        return true;
    }

    // 아이템 이미지를 선택했을 때 이 아이템과 같은 종류의 아이템 창을 보여준다.
    // 종류는 다음과 같다.
    // 무기, 화살, 볼트, 각 값옷들, 링, 소모성 아이템, 계약
    public override void ShowHowtoItem()
    {
        base.ShowHowtoItem();

        // 해당 슬롯 파트의 아이템과 슬롯 정보를 가져와서 넘겨준다.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotName, m_EquipmentSlotsPartsName, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);
        // 여기에 아이템이 있다면 교체.
        // 아이템이 없다면 새롭게 장착.
        // 아이템 파트와 아이템 슬롯 넘버를 넘겨준다.

    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_CurrentEquipmentsUI.ShowItemInformation(m_Item.name, m_sSlotName);
    }
}
