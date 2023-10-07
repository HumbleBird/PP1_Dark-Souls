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

    // ������ ������ �޾ƿ´�.
    public void SetInfo(Item item)
    {
        m_Item = item;
    }

    // ������ �̹����� �������� �� ��� ���� �Ǵ�
    // �κ��丮 - ��뿩�� ���� (��� - ����, ������), (�ҿ� - ��� ��)
    // ��� 
    public abstract void ShowHowtoItem();

    // ������ �̹����� �������� �� ������ ������ �����ش�.
    public virtual void ShowItemInformation(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = true;
    }

    // ������ �̹����� �������� ���� �� ���� �̹��� ȿ���� �����Ѵ�.
    public virtual void PointerExitItem(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = false;
    }

    // �� Ÿ�Կ� ���� Refresh
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
