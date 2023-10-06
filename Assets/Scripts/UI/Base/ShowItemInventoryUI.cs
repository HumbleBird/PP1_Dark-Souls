using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ShowItemInventoryUI : UI_Base
{
    // Equpment â���� �������� Ŭ���ؼ� �ش� ��� ���� ��Ʈ�� �ش��ϴ� ��� �κ��丮���� �����ؼ� �����´�.
    // Right Hand Slot�� Ŭ���ϸ� ����, ���忡 �ش��ϴ� �����۵��� ���� �����´�.
    // ������ ó���� ����. �������� �κ��丮�� �׺��� �� ������ ���� ����, Scroll�� Ȱ��ȭ ��Ų��.

    enum Texts
    {
        EquipmentSlotsNameText,
        ItemNameText,
    }

    enum GameObjects
    {
        Panel
    }

    public string m_sItemPartSlotName;
    public int m_iEquipmentSlotNum;
    public E_EquipmentSlotsPartType m_E_EquipmentSlotsPartType;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        return true;
    }

    // Equipment UI�� ���ؼ� ��� ���� ��
    public void SetInfo(string slotPartName, E_EquipmentSlotsPartType equipmentSlotsPartsName, int SlotNum)
    {
        GetText((int)Texts.EquipmentSlotsNameText).text = slotPartName;

        m_iEquipmentSlotNum = SlotNum;

        m_E_EquipmentSlotsPartType = equipmentSlotsPartsName;
        CreateInventorySlot(GetObject((int)GameObjects.Panel), equipmentSlotsPartsName);
    }

    // EquipmentToInventoryShowItemSubItem���� Pointer Down Event�� �߻������� ��
    public void ShowItemInformation(string itemName)
    {
        if (itemName != null)
        {
            GetText((int)Texts.ItemNameText).text = itemName;
        }
        else
        {
            GetText((int)Texts.ItemNameText).text = "";
        }
    }

    // ������ ����� Equipment Slot ��Ʈ�� �ش��ϴ� ������ Ÿ���� �÷��̾� �κ��丮���� ���� �ܾ�´�.
    private void CreateInventorySlot(GameObject parent, E_EquipmentSlotsPartType equipmentSlotsPartsName)
    {
        // ���� �ʱ�ȭ
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        // ���� ��Ʈ�� �ش��ϴ� �������� �÷��̾��� �κ��丮���� ��������
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
                items = player.playerInventoryManager.FindItems(i => ((RangedAmmoItem)i).ammoType == AmmoType.Arrow);
                break;
            case E_EquipmentSlotsPartType.Bolt:
                items = player.playerInventoryManager.FindItems(i => ((RangedAmmoItem)i).ammoType == AmmoType.Bolt);
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

        if (items == null)
            return;

        // ���� �����
        int standCount = 28; // ���߿� UI ũ�⸦ �����ϸ� ���ڸ� ������ ��.
        int ColCount = 7;
        //int scrollMinCount = 100; // ��ũ�� �ּ� ����

        if(items.Count > standCount)
        {
            // ex) item.count = 52��� 52 + 7 - 52 / 7 = 59 - 3 = 56
            standCount = (items.Count + ColCount) - (items.Count % ColCount);
        }

        for (int i = 0; i < standCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/EquipmentToInventoryShowItemSubItem", parent.transform);
            EquipmentToInventoryShowItemSubItem item = go.GetOrAddComponent<EquipmentToInventoryShowItemSubItem>();

            // ���� �ȿ� ������ ���� �ֱ�
            if (i < items.Count)
            {
                item.SetInfo(items[i]);
            }
        }
    }
}
