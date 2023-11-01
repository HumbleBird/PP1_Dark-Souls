using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;

public class ShowItemInventoryUI : UI_Base
{
    // Equpment â���� �������� Ŭ���ؼ� �ش� ��� ���� ��Ʈ�� �ش��ϴ� ��� �κ��丮���� �����ؼ� �����´�.
    // Right Hand Slot�� Ŭ���ϸ� ����, ���忡 �ش��ϴ� �����۵��� ���� �����´�.
    // ������ ó���� ����. �������� �κ��丮�� �׺��� �� ������ ���� ����, Scroll�� Ȱ��ȭ ��Ų��.

    public E_EquipmentSlotsPartType m_E_EquipmentSlotsPartType;
    List<EquipmentShowInventorySlotUI> m_EquipmentShowInventorySlot = new List<EquipmentShowInventorySlotUI>();

    public int m_iEquipmentSlotNum;
    public int m_iCurrentSelectSlot = 0;

    public TextMeshProUGUI m_sEquipmentSlotsNameText;
    public TextMeshProUGUI m_ItemNameText;

    public GameObject m_goPanel;

    // Equipment UI�� ���ؼ� ��� ���� ��
    public void SetInfo(string slotPartName, E_EquipmentSlotsPartType equipmentSlotsPartsName, int SlotNum)
    {
        // ���� ��Ʈ �̸�
        m_sEquipmentSlotsNameText.text = slotPartName;

        // ���� Num
        m_iEquipmentSlotNum = SlotNum;

        // ��� Type
        m_E_EquipmentSlotsPartType = equipmentSlotsPartsName;

        // ���� �����
        CreateInventorySlot(m_goPanel, m_E_EquipmentSlotsPartType);
    }

    // EquipmentToInventoryShowItemSubItem���� Pointer Down Event�� �߻������� ��
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

    // ������ ����� Equipment Slot ��Ʈ�� �ش��ϴ� ������ Ÿ���� �÷��̾� �κ��丮���� ���� �ܾ�´�.
    private void CreateInventorySlot(GameObject parent, E_EquipmentSlotsPartType equipmentSlotsPartsName)
    {
        // ���� �ʱ�ȭ
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        m_EquipmentShowInventorySlot.Clear();

        // ���� ��Ʈ�� �ش��ϴ� �������� �÷��̾��� �κ��丮���� ��������
        PlayerManager player = Managers.Object.m_MyPlayer;
        List<Item> items = new List<Item>();

        switch (equipmentSlotsPartsName)
        {
            case E_EquipmentSlotsPartType.Right_Hand_Weapon:
            case E_EquipmentSlotsPartType.Left_Hand_Weapon:
                items = player.playerInventoryManager.FindItems(
                    i => i.m_eItemType == E_ItemType.MeleeWeapon ||
                    i.m_eItemType == E_ItemType.RangeWeapon ||
                    i.m_eItemType == E_ItemType.Catalyst ||
                    i.m_eItemType == E_ItemType.Shield);

                break;
            case E_EquipmentSlotsPartType.Arrow:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Ammo && ((AmmoItem)i).ammoType == AmmoType.Arrow);
                break;
            case E_EquipmentSlotsPartType.Bolt:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Ammo && ((AmmoItem)i).ammoType == AmmoType.Bolt);
                break;
            case E_EquipmentSlotsPartType.Helmt:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Helmet);
                break;
            case E_EquipmentSlotsPartType.Chest_Armor:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.ChestArmor);
                break;
            case E_EquipmentSlotsPartType.Gantlets:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Gauntlets);
                break;
            case E_EquipmentSlotsPartType.Leggings:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Leggings);
                break;
            case E_EquipmentSlotsPartType.Ring:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Ring);
                break;
            case E_EquipmentSlotsPartType.Consumable:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Tool);
                break;
            case E_EquipmentSlotsPartType.Pledge:
                items = player.playerInventoryManager.FindItems(i => i.m_eItemType == E_ItemType.Pledge);
                break;
            default:
                break;
        }

        // ���� �����
        int standCount = 30; // ���߿� UI ũ�⸦ �����ϸ� ���ڸ� ������ ��.
        int ColCount = 5;
        //int scrollMinCount = 41; // ��ũ�� �ּ� ����

        if (items != null)
        {
            if (items.Count > standCount)
            {
                // ex) item.count = 52��� 52 + 7 - 52 / 7 = 59 - 3 = 56
                standCount = (items.Count + ColCount) - (items.Count % ColCount);
            }
        }

        for (int i = 0; i < standCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/EquipmentShowInventorySlotUI", parent.transform);
            EquipmentShowInventorySlotUI slot = go.GetOrAddComponent<EquipmentShowInventorySlotUI>();
            m_EquipmentShowInventorySlot.Add(slot);
            slot.m_iSlotNum = i;

            // ���� �ȿ� ������ ���� �ֱ�
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
