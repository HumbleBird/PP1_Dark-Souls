using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemSlotUI : UI_Base
{
    protected enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    protected Item m_Item;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        GetImage((int)Images.ItemSelectIcon).gameObject.BindEvent(() => { ShowHowtoItem(); });

        // Event Trigger
        EventTrigger trigger = gameObject.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { PointerExitItem((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { ShowItemInformation((PointerEventData)data); });
        trigger.triggers.Add(entry2);

        RefreshUI();

        return true;
    }

    // 아이템 정보를 받아온다.
    public void SetInfo(Item item)
    {
        m_Item = item;
    }

    // 아이템 이미지를 선택했을 때 사용 여부 판단
    // 인벤토리 - 사용여부 묻기 (장비 - 장착, 버리기), (소울 - 사용 등)
    // 장비 
    public abstract void ShowHowtoItem();

    // 아이템 이미지를 선택했을 때 아이템 정보를 보여준다.
    public virtual void ShowItemInformation(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = true;
    }

    // 아이템 이미지를 선택하지 않을 때 선택 이미지 효과를 삭제한다.
    public virtual void PointerExitItem(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }

    // 각 타입에 따른 Refresh
    public override void RefreshUI()
    {
        if (m_Item != null)
        {
            GetImage((int)Images.ItemIcon).sprite = m_Item.itemIcon;
            GetImage((int)Images.ItemIcon).enabled = true;
            //GetImage((int)Images.ItemBasePlateIcon).enabled = true;
        }
        else
        {
            GetImage((int)Images.ItemIcon).sprite = null;
            GetImage((int)Images.ItemIcon).enabled = false;
            //GetImage((int)Images.ItemBasePlateIcon).enabled = false;
        }
    }
}
