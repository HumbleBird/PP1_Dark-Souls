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

    // 아이템 정보를 받아온다.
    public void SetInfo(Item item)
    {
        m_Item = item;
        //RefreshUI();
    }

    // 아이템 이미지를 선택했을 때 사용 여부 판단
    // 인벤토리 - 사용여부 묻기 (장비 - 장착, 버리기), (소울 - 사용 등)
    // 장비 - 장착 여부
    public abstract void ShowHowtoItem();

    // 아이템 이미지를 선택했을 때 아이템 정보를 보여준다.
    public virtual void ShowItemInformation()
    {
        if (m_Item == null)
            return;

        m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;
    }

    // 각 타입에 따른 Refresh
    public override void RefreshUI()
    {
        if (m_Item != null)
        {
            m_ItemSlotSubUI.m_ItemIcon.sprite = m_Item.m_ItemIcon;
            m_ItemSlotSubUI.m_ItemIcon.enabled = true;
            m_ItemSlotSubUI.m_ItemPlateIcon.enabled = true;

            if (m_Item.m_isEquiping) // 장착중이라면
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
