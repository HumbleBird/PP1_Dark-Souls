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

    // ������ �̹����� �������� �� �� �����۰� ���� ������ ������ â�� �����ش�.
    // ������ ������ ����.
    // ����, ȭ��, ��Ʈ, �� ���ʵ�, ��, �Ҹ� ������, ���
    public override void ShowHowtoItem()
    {
        base.ShowHowtoItem();

        // �ش� ���� ��Ʈ�� �����۰� ���� ������ �����ͼ� �Ѱ��ش�.
        m_EquipmentUI.m_ShowItemInventoryUI.SetInfo(m_sSlotName, m_EquipmentSlotsPartsName, m_iSlotNum);

        m_EquipmentUI.m_CurrentEquipmentsUI.gameObject.SetActive(false);
        m_EquipmentUI.m_ShowItemInventoryUI.gameObject.SetActive(true);
        // ���⿡ �������� �ִٸ� ��ü.
        // �������� ���ٸ� ���Ӱ� ����.
        // ������ ��Ʈ�� ������ ���� �ѹ��� �Ѱ��ش�.

    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
    public override void ShowItemInformation(PointerEventData data)
    {
        base.ShowItemInformation(data);

        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_CurrentEquipmentsUI.ShowItemInformation(m_Item.name, m_sSlotName);
    }
}
