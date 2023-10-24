using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InventoryItemTapSlotUI : UI_Base
{
    InventoryUI m_InventoryUI;
    InventoryItemMainUI m_InventoryItemMainUI;

    PlayerManager m_Player;

    List<Item> items;
    public List<InventoryItemSlotUI> m_listInventoryItemSlotUI = new List<InventoryItemSlotUI>();

    public int m_iShowTapNum; // �������� �ִ� ���� 5�� �߿��� �� ��° ĭ�� �ִ��� Ȯ��
    public int m_iNum = 0; // �� 15���� ���� ���� �ѹ�

    public Image m_TapSlotBGImage;
    public Image m_TapSlotItemImage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        m_Player = Managers.Object.m_MyPlayer;

        m_InventoryUI = GetComponentInParent<InventoryUI>();
        m_InventoryItemMainUI = GetComponentInParent<InventoryItemMainUI>();
        m_TapSlotBGImage = GetComponent<Image>();
        m_TapSlotItemImage = GetComponentsInChildren<Image>()[1];
        return true;
    }

    public void SetInit(int num)
    {
        m_iNum = num;
        gameObject.UIEventTrigger(UnityEngine.EventSystems.EventTriggerType.PointerEnter, () => SelectTap());
    }

    // �ش� ���� �����۵��� �����ش�.
    public void SelectTap()
    {
        // �ش� Ÿ���� ������ ������ ���� slot�� �����Ѵ�.
        // UnSelected ������ ���� Ŭ���ϸ� �ش� ���� Ÿ���� �������� �κ��丮���� ���� �����ش�.
        CreateInventorySlot((E_ItemType)m_iNum);

        // �� �̹����� Ȱ��ȭ
        ActiveSelectedTap();

        // �ε��� ��ȣ�� ���� �ش� ĭ�� ������ ������ ��Ÿ��.
        ShowCurrentSelectItemSlotFromItemNum();

        // ����
        Managers.Sound.Play("UI/Inventory_Tap");

        // Inventory Main Tap Name Refresh
        m_InventoryItemMainUI.RefreshShowTapName((E_ItemType)m_iNum);
    }

    private void CreateInventorySlot(E_ItemType type)
    {
        // �ʱ�ȭ

        // ���� ������ ������ �����Ѵ�.
        foreach (Transform child in m_InventoryItemMainUI.m_InventoryItemSlotsPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        m_listInventoryItemSlotUI.Clear();

        // �ش� ���� �������� ������ �����´�.
        items = m_Player.playerInventoryManager.FindItems(i => i.m_EItemType == type);

        int makeSlotCountMax = 0;

        if(items == null)
        {
            makeSlotCountMax = 5;
        }
        else
        {
            // ������ ������ 5�� ���� �ش��ϴ� 5�� ����� ���� ������ �����.
            // if count == 6 �������� 1 ���� ������� �� - �������� �� = �� ������� ��.
            if ((items.Count % 5) != 0)
            {
                makeSlotCountMax = items.Count + 5 - (items.Count % 5);
            }
            else
                makeSlotCountMax = items.Count;
        }


        // ������ ���� UI�� �����ϰ� ������ ������ �ִ´�.
        for (int i = 0; i < makeSlotCountMax; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/InventoryItemSlotUI", m_InventoryItemMainUI.m_InventoryItemSlotsPanel.transform);
            InventoryItemSlotUI slot = go.GetOrAddComponent<InventoryItemSlotUI>();
            m_listInventoryItemSlotUI.Add(slot);
            slot.m_iSlotNum = i;
            slot.m_InventoryItemTapSlotUI = this;

            if (items != null)
            {
                if (i < items.Count)
                {
                    slot.SetInfo(items[i]);
                    slot.RefreshUI();
                }
            }
        }
    }

    private void ActiveSelectedTap()
    {
        // ���� Ȱ��ȭ �Ǿ��� ���� ������ ��Ȱ��ȭ
        m_InventoryItemMainUI.TapsSelectClear();
        m_InventoryItemMainUI.m_goSelectedItemTaps[m_iShowTapNum].gameObject.SetActive(true);
        m_InventoryItemMainUI.m_iCurrentSelectTapNum = m_iShowTapNum;
        string num = m_iNum.ToString("00");
        m_InventoryItemMainUI.m_SelectedItemSlotImage[m_iShowTapNum].sprite = Managers.Resource.Load<Sprite>($"Art/Textures/UI/menu/Inventory/MENU_ItemTab/Middle/MENU_ItemTab_Middle_{num}");
    }

    // ���� ���õǾ� �ִ� ������ ������ �̿��� �ٸ� ������ �Ѿ�� �ش� ������ ����Ű�� ��ȣ�� ������.
    public void ShowCurrentSelectItemSlotFromItemNum()
    {
        if (items == null || m_listInventoryItemSlotUI[m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum].m_Item == null)
        {
            //m_listInventoryItemSlotUI[inventoryItemMainUI.m_iCurrentSelectItemSlotNum].m_ItemSelectIcon.enabled = false;
            m_InventoryUI.CloseItemInfo();
            return;
        }

        m_listInventoryItemSlotUI[m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum].m_ItemSlotSubUI.m_ItemSelectIcon.enabled = true;
        m_InventoryUI.ShowItemInfo(items[m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum]);
    }

    public void ShowTap(int ShowTapNum)
    {
        m_iShowTapNum = ShowTapNum;
        gameObject.SetActive(true);
        m_InventoryItemMainUI.m_SelectUnSelectedItemTaps[m_iShowTapNum] = this;
        RefreshTapImage();
    }

    private void RefreshTapImage()
    {
        string posTap = "Middle";

        if (m_iNum != (int)E_ItemType.Tool && m_iNum != (int)E_ItemType.Pledge)
        {
            if (m_iShowTapNum == 0) // ���� 5���� �� �߿��� ���� ���ʿ� �ִٸ�
            {
                posTap = "Left";
            }
            else if (m_iShowTapNum == 4) // ���� 5���� �� �߿��� ���� �����ʿ� �ִٸ�
            {
                posTap = "Right";
            }
        }

        m_TapSlotBGImage.sprite = Managers.Resource.Load<Sprite>($"Art/Textures/UI/menu/Inventory/MENU_Inventory_ItemTap_{posTap}");

        string num = m_iNum.ToString("00");
        m_TapSlotItemImage.sprite = Managers.Resource.Load<Sprite>($"Art/Textures/UI/menu/Inventory/MENU_ItemTab/{posTap}/MENU_ItemTab_{posTap}_{num}");

    }

    public void PriviousSlotClear()
    {
        if (m_listInventoryItemSlotUI[m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum] != null)
        {
            m_listInventoryItemSlotUI[m_InventoryItemMainUI.m_iCurrentSelectItemSlotNum].m_ItemSlotSubUI.m_ItemSelectIcon.enabled = false;
        }
    }
}
