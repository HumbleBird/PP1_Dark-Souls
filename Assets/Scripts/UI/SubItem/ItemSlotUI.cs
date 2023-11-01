using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemSlotUI : UI_Base
{
    public ItemSlotSubUI m_ItemSlotSubUI;

    public Item m_Item;

    public int m_iSlotNum = -1;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_ItemSlotSubUI = GetComponentInChildren<ItemSlotSubUI>();
        m_ItemSlotSubUI.m_ItemSelectIcon.gameObject.BindEvent(() => { ShowHowtoItem(); });

        gameObject.UIEventTrigger(EventTriggerType.PointerEnter, ShowItemInformation);

        RefreshUI();

        return true;
    }

    // ������ ������ �޾ƿ´�.
    public void SetInfo(Item item)
    {
        m_Item = item;
        //RefreshUI();
    }

    // ������ �̹����� �������� �� ��� ���� �Ǵ�
    // �κ��丮 - ��뿩�� ���� (��� - ����, ������), (�ҿ� - ��� ��)
    // ��� - ���� ����
    public abstract void ShowHowtoItem();

    // ������ �̹����� �������� �� ������ ������ �����ش�.
    public virtual void ShowItemInformation()
    {
        if (m_Item == null)
            return;

        m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;
    }

    // �� Ÿ�Կ� ���� Refresh
    public override void RefreshUI()
    {
        if (m_Item != null)
        {
            m_ItemSlotSubUI.m_ItemIcon.sprite = m_Item.m_ItemIcon;
            m_ItemSlotSubUI.m_ItemIcon.enabled = true;
            m_ItemSlotSubUI.m_ItemPlateIcon.enabled = true;

            if (m_Item.m_isEquiping) // �������̶��
                m_ItemSlotSubUI.m_ItemUsingIcon.enabled = true;
            else
                m_ItemSlotSubUI.m_ItemUsingIcon.enabled = false;
        }
        else
        {
            m_ItemSlotSubUI.m_ItemIcon.sprite = null;
            m_ItemSlotSubUI.m_ItemIcon.enabled = false;
            m_ItemSlotSubUI.m_ItemPlateIcon.enabled = false;
        }
    }
}
