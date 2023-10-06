using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : UI_Base
{
    enum Images
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

        return true;
    }

    public  void SetInfo(Item item)
    {
        m_Item = item;
    }

    // ������ �̹����� �������� �� ��� ���θ� ���´�.
    public virtual void ShowHowtoItem()
    {
        if (m_Item == null)
            return;

        Debug.Log("ShowItemInfomation" + m_Item.itemName);
    }

    // ������ �̹����� Ŭ���ϸ� ��� �гο� ������ ������ �����ش�.
    public virtual void ShowItemInformation(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = true;
    }

    // ������ ���� ȿ���� �����Ѵ�.
    public void PointerExitItem(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }
}
