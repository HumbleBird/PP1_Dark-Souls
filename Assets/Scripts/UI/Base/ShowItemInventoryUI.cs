using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;

public class ShowItemInventoryUI : UI_Base
{
    // Equpment 창에서 아이템을 클릭해서 해당 장비 슬롯 파트에 해당하는 장비를 인벤토리에서 구분해서 가져온다.
    // Right Hand Slot을 클릭하면 무기, 쉴드에 해당하는 아이템들을 전부 가져온다.
    // 슬롯은 처음에 많게. 아이템의 인벤토리가 그보다 더 많으면 새로 생성, Scroll을 활성화 시킨다.

    public E_EquipmentSlotsPartType m_E_EquipmentSlotsPartType;
    List<EquipmentShowInventorySlotUI> m_EquipmentShowInventorySlot = new List<EquipmentShowInventorySlotUI>();

    public int m_iEquipmentSlotNum;
    public int m_iCurrentSelectSlot = 0;

    public TextMeshProUGUI m_sEquipmentSlotsNameText;
    public TextMeshProUGUI m_ItemNameText;

    public GameObject m_goPanel;

    // Equipment UI를 통해서 들어 왔을 떄
    public void SetInfo(string slotPartName, E_EquipmentSlotsPartType equipmentSlotsPartsName, int SlotNum)
    {
        // 슬롯 파트 이름
        m_sEquipmentSlotsNameText.text = slotPartName;

        // 슬롯 Num
        m_iEquipmentSlotNum = SlotNum;

        // 장비 Type
        m_E_EquipmentSlotsPartType = equipmentSlotsPartsName;

        // 슬롯 만들기
        CreateInventorySlot(m_goPanel, m_E_EquipmentSlotsPartType);
    }

    // EquipmentToInventoryShowItemSubItem에서 Pointer Down Event를 발생시켰을 때
    public void ShowItemInformation(string itemName)
    {
        if (itemName != null)
        {
            m_ItemNameText.text = itemName;
        }
        else
        {
            m_ItemNameText.text = "";
        }
    }

    // 슬롯을 만들고 Equipment Slot 파트에 해당하는 아이템 타입을 플레이어 인벤토리에서 전부 긁어온다.
    private void CreateInventorySlot(GameObject parent, E_EquipmentSlotsPartType equipmentSlotsPartsName)
    {
        // 슬롯 초기화
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        m_EquipmentShowInventorySlot.Clear();

        // 슬롯 파트에 해당하는 아이템을 플레이어의 인벤토리에서 가져오기
        PlayerManager player = Managers.Object.m_MyPlayer;
        List<Item> items = new List<Item>();

        switch (equipmentSlotsPartsName)
        {
            case E_EquipmentSlotsPartType.Right_Hand_Weapon:
            case E_EquipmentSlotsPartType.Left_Hand_Weapon:
                items = player.playerInventoryManager.FindItems(
                    i => i.m_EItemType == E_ItemType.MeleeWeapon ||
                    i.m_EItemType == E_ItemType.RangeWeapon ||
                    i.m_EItemType == E_ItemType.Catalyst ||
                    i.m_EItemType == E_ItemType.Shield);

                break;
            case E_EquipmentSlotsPartType.Arrow:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Ammo && ((AmmoItem)i).ammoType == AmmoType.Arrow);
                break;
            case E_EquipmentSlotsPartType.Bolt:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Ammo && ((AmmoItem)i).ammoType == AmmoType.Bolt);
                break;
            case E_EquipmentSlotsPartType.Helmt:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Helmet);
                break;
            case E_EquipmentSlotsPartType.Chest_Armor:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.ChestArmor);
                break;
            case E_EquipmentSlotsPartType.Gantlets:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Gauntlets);
                break;
            case E_EquipmentSlotsPartType.Leggings:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Leggings);
                break;
            case E_EquipmentSlotsPartType.Ring:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Ring);
                break;
            case E_EquipmentSlotsPartType.Consumable:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Tool);
                break;
            case E_EquipmentSlotsPartType.Pledge:
                items = player.playerInventoryManager.FindItems(i => i.m_EItemType == E_ItemType.Pledge);
                break;
            default:
                break;
        }

        // 슬롯 만들기
        int standCount = 30; // 나중에 UI 크기를 조정하면 숫자를 조정할 것.
        int ColCount = 5;
        //int scrollMinCount = 41; // 스크롤 최소 갯수

        if (items != null)
        {
            if (items.Count > standCount)
            {
                // ex) item.count = 52라면 52 + 7 - 52 / 7 = 59 - 3 = 56
                standCount = (items.Count + ColCount) - (items.Count % ColCount);
            }
        }

        for (int i = 0; i < standCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/EquipmentShowInventorySlotUI", parent.transform);
            EquipmentShowInventorySlotUI slot = go.GetOrAddComponent<EquipmentShowInventorySlotUI>();
            m_EquipmentShowInventorySlot.Add(slot);
            slot.m_iSlotNum = i;

            // 슬롯 안에 아이템 집어 넣기
            if (items != null)
            {
                if (i < items.Count)
                {
                    slot.SetInfo(items[i]);
                }
            }
        }
    }

    public void PrivousSlotClear()
    {
        if (m_EquipmentShowInventorySlot[m_iCurrentSelectSlot] != null)
            m_EquipmentShowInventorySlot[m_iCurrentSelectSlot].m_ItemSlotSubUI.m_ItemSelectIcon.enabled = false;
    }
}
