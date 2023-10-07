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

    public int m_iSlotNum = -1; // ���� ��ȣ
    public string m_sSlotPartName; // ���� ��Ʈ �̸�

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
        // ������ �������� �κ��丮���� �����´�.

        // �ش� ���� ��Ʈ�� �����۰� ���� ������ �����ͼ� �Ѱ��ش�.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotPartName, m_EquipmentSlotsPartsName, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);

    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
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

    // ������ �̹����� �������� ���� �� ���� �̹��� ȿ���� �����Ѵ�.
    public override void PointerExitItem(PointerEventData data)
    {

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }
}
