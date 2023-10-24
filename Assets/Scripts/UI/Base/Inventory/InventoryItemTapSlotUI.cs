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

    public int m_iShowTapNum; // 보여지고 있는 탭의 5개 중에서 몇 번째 칸에 있는지 확인
    public int m_iNum = 0; // 총 15개의 탭의 고유 넘버

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

    // 해당 탭의 아이템들을 보여준다.
    public void SelectTap()
    {
        // 해당 타입의 아이템 갯수에 따라 slot를 생성한다.
        // UnSelected 아이템 템을 클릭하면 해당 탭의 타입의 아이템을 인벤토리에서 꺼내 보여준다.
        CreateInventorySlot((E_ItemType)m_iNum);

        // 탭 이미지를 활성화
        ActiveSelectedTap();

        // 인덱스 번호에 따라 해당 칸의 아이템 정보를 나타냄.
        ShowCurrentSelectItemSlotFromItemNum();

        // 사운드
        Managers.Sound.Play("UI/Inventory_Tap");

        // Inventory Main Tap Name Refresh
        m_InventoryItemMainUI.RefreshShowTapName((E_ItemType)m_iNum);
    }

    private void CreateInventorySlot(E_ItemType type)
    {
        // 초기화

        // 기존 슬롯을 모조리 제거한다.
        foreach (Transform child in m_InventoryItemMainUI.m_InventoryItemSlotsPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        m_listInventoryItemSlotUI.Clear();

        // 해당 탭의 아이템을 모조리 가져온다.
        items = m_Player.playerInventoryManager.FindItems(i => i.m_EItemType == type);

        int makeSlotCountMax = 0;

        if(items == null)
        {
            makeSlotCountMax = 5;
        }
        else
        {
            // 아이템 갯수를 5로 나눠 해당하는 5의 배수의 슬롯 갯수를 만든다.
            // if count == 6 나머지가 1 원래 만드려던 것 - 나머지의 값 = 더 만들려는 값.
            if ((items.Count % 5) != 0)
            {
                makeSlotCountMax = items.Count + 5 - (items.Count % 5);
            }
            else
                makeSlotCountMax = items.Count;
        }


        // 아이템 슬롯 UI를 생성하고 아이템 정보를 넣는다.
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
        // 전에 활성화 되었던 탭을 모조리 비활성화
        m_InventoryItemMainUI.TapsSelectClear();
        m_InventoryItemMainUI.m_goSelectedItemTaps[m_iShowTapNum].gameObject.SetActive(true);
        m_InventoryItemMainUI.m_iCurrentSelectTapNum = m_iShowTapNum;
        string num = m_iNum.ToString("00");
        m_InventoryItemMainUI.m_SelectedItemSlotImage[m_iShowTapNum].sprite = Managers.Resource.Load<Sprite>($"Art/Textures/UI/menu/Inventory/MENU_ItemTab/Middle/MENU_ItemTab_Middle_{num}");
    }

    // 현재 선택되어 있는 아이템 슬롯을 이용해 다른 탭으로 넘어가도 해당 슬롯을 가리키는 번호는 유지함.
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
            if (m_iShowTapNum == 0) // 현재 5개의 탭 중에서 가장 왼쪽에 있다면
            {
                posTap = "Left";
            }
            else if (m_iShowTapNum == 4) // 현재 5개의 탭 중에서 가장 오른쪽에 있다면
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
