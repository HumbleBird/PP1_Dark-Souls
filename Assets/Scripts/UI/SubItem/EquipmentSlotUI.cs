using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : UI_Base
{
    public enum EquipmentSlotsPartsName
    {
        Right_Hand_Weapon_1,
        Right_Hand_Weapon_2,
        Right_Hand_Weapon_3,
        Left_Hand_Weapon_1,
        Left_Hand_Weapon_2,
        Left_Hand_Weapon_3,

        Arrow_1,
        Arrow_2,
        Bolt_1,
        Bolt_2,

        Helmt,
        Chest_Armor,
        Gantlets,
        Leggings,

        Ring_1,
        Ring_2,
        Ring_3,
        Ring_4,

        Consumable_Slot_1,
        Consumable_Slot_2,
        Consumable_Slot_3,
        Consumable_Slot_4,
        Consumable_Slot_5,
        Consumable_Slot_6,
        Consumable_Slot_7,
        Consumable_Slot_8,
        Consumable_Slot_9,
        Consumable_Slot_10,

        Pledge,
    }

    public EquipmentSlotsPartsName m_EquipmentSlotsPartsName;

    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    Item m_Item;
    EquipmentUI m_EquipmentUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        GetImage((int)Images.ItemSelectIcon).gameObject.BindEvent(() => { ShowItemInfomation(); });

        // Event Trigger
        EventTrigger trigger = gameObject.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { PointerExitItem((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { PointerEnterItem((PointerEventData)data); });
        trigger.triggers.Add(entry2);

        m_EquipmentUI = GetComponentInParent<EquipmentUI>();

        return true;
    }

    public void SetInfo(Item item)
    {
        m_Item = item;
        GetImage((int)Images.ItemIcon).sprite = m_Item.itemIcon;
        GetImage((int)Images.ItemIcon).enabled = true;
        GetImage((int)Images.ItemBasePlateIcon).enabled = true;

        // Item

        PlayerManager player = Managers.Object.m_MyPlayer;

    }

    // 아이템 이미지를 선택했을 때 이 아이템과 같은 종류의 아이템 창을 보여준다.
    // 종류는 다음과 같다.
    // 무기, 화살, 볼트, 각 값옷들, 링, 소모성 아이템, 계약
    public void ShowItemInfomation()
    {
        if (m_Item == null)
            return;

        Debug.Log("ShowItemInfomation" + m_Item.itemName);
    }

    // 아이템 이미지를 클릭하면 가운데 패널에 아이템 정보를 보여준다.
    public void PointerEnterItem(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = true;

        m_EquipmentUI.m_ItemInformationUI.ShowItemInformation(m_Item);
        m_EquipmentUI.m_CurrentEquipmentsUI.ShowItemInformation(m_Item.name, gameObject.name);
    }

    // 아이템 선택 효과를 삭제한다.
    public void PointerExitItem(PointerEventData data)
    {
        if (m_Item == null)
            return;

        GetImage((int)Images.ItemSelectIcon).enabled = false;

        Debug.Log("PointerExitItem" + m_Item.itemName);
    }
}
