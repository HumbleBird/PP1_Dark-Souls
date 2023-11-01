using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InventoryItemMainUI : UI_Base
{
    enum Texts
    {
        ItemTapNameText, // 아이템 부문 별 이름
        ItemNameText, // 아이템 이름
    }

    enum GameObjects
    {
        LBText, // 아이템 탭을 왼쪽으로 한 칸 이동
        RBText, // 아이템 탭을 오른쪽으로 한 칸 이동
        InventoryItemSlotsPanel, // 아이템 슬롯을 생성할 패널
    }

    PlayerManager m_Player;

    public InventoryItemTapSlotUI[] m_UnSelectedItemTaps = new InventoryItemTapSlotUI[15]; // 총 15개의 탭


    public InventoryItemTapSlotUI[] m_SelectUnSelectedItemTaps = new InventoryItemTapSlotUI[5]; // 보여주는 5개의 탭
    public GameObject[] m_goSelectedItemTaps = new GameObject[5];
    public Image[] m_SelectedItemSlotImage = new Image[5];

    public GameObject m_InventoryItemSlotsPanel;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        m_InventoryItemSlotsPanel = GetObject((int)GameObjects.InventoryItemSlotsPanel);

        m_Player = Managers.Object.m_MyPlayer;

        for (int i = 1; i <= m_UnSelectedItemTaps.Length; i++)
        {
            m_UnSelectedItemTaps[i - 1].SetInit(i);
        }

        Managers.Game.m_iInventoryCurrentSelectItemSlotNum = 0;

        GetObject((int)GameObjects.LBText).BindEvent(() => TapSlotMove(true)); ;
        GetObject((int)GameObjects.RBText).BindEvent(() => TapSlotMove(false)); ;

        SelectShowUnTaps();


        return true;
    }

    private void Start()
    {
        m_SelectUnSelectedItemTaps[Managers.Game.m_iInventoryCurrentSelectTapNum].SelectTap();
    }

    // 보여줄 5개의 탭을 설정
    public void SelectShowUnTaps()
    {
        // Un Selected Cleard
        for (int i = 0; i < m_UnSelectedItemTaps.Length; i++)
        {
            m_UnSelectedItemTaps[i].gameObject.SetActive(false);
            m_UnSelectedItemTaps[i].m_iShowTapNum = -1;

        }

        // 보여줄 5개 Tap 선정
        // 이미지 갱신
        int count = 0;
        for (int i = Managers.Game.m_iInventoryShowItemTapLeftNum; i <= Managers.Game.m_iInventoryShowItemTapRightNum; i++)
        {
            m_UnSelectedItemTaps[i].ShowTap(count);

            m_SelectedItemSlotImage[count].sprite = m_SelectUnSelectedItemTaps[count].m_TapSlotItemImage.sprite;
            // TODO Tap Num 에 따른 다양한 이미지 체인지
            count++;
        }
    }

    public void ShowItemName(string itemName)
    {
        GetText((int)Texts.ItemNameText).text = itemName;
    }

    public void RefreshShowTapName(E_ItemType type)
    {
        GetText((int)Texts.ItemTapNameText).text = type.ToString();

    }

    private void TapSlotMove(bool isLeft)
    {
        if(isLeft)
        {
            // 현재 활성화 탭이 어느 위치에 있는가
            if (Managers.Game.m_iInventoryCurrentSelectTapNum == 0) // 이미 왼쪽 걸 클릭한 상태였다면
            {
                if(Managers.Game.m_iInventoryShowItemTapLeftNum == 0) // 가장 왼쪽의 Tool Tap을 이미 선택한 상태였다면 가장 오른쪽으로 이동
                {
                    Managers.Game.m_iInventoryCurrentSelectTapNum = 4; // 활성화 탭을 가장 오른쪽으로 이동

                    Managers.Game.m_iInventoryShowItemTapLeftNum = 10;
                    Managers.Game.m_iInventoryShowItemTapRightNum = 14;
                }
                else // Tool Tap이 더 왼쪽으로 갈 수 있다면 한 칸씩 이동
                {
                    Managers.Game.m_iInventoryShowItemTapLeftNum--;
                    Managers.Game.m_iInventoryShowItemTapRightNum--;
                }
            }
            else // 2~5번째 탭을 선택한 상황이라면, 활성화 탭을 왼쪽으로 이동
            {
                Managers.Game.m_iInventoryCurrentSelectTapNum--;
            }
        }
        else
        {
            // 현재 활성화 탭이 어느 위치에 있는가
            if (Managers.Game.m_iInventoryCurrentSelectTapNum == 4) // 이미 왼쪽 걸 클릭한 상태였다면
            {
                if (Managers.Game.m_iInventoryShowItemTapRightNum == 14) // 가장 오른쪽의 서약 Tap을 이미 선택한 상태였다면 Tool 탭으로 이동
                {
                    Managers.Game.m_iInventoryCurrentSelectTapNum = 0; // 활성화 탭을 가장 왼쪽으로 이동
                    
                    Managers.Game.m_iInventoryShowItemTapLeftNum = 0;
                    Managers.Game.m_iInventoryShowItemTapRightNum = 4;
                }
                else // 보여주는 가장 오른쪽의 탭이 서약 탭이 아니라면
                {
                    Managers.Game.m_iInventoryShowItemTapLeftNum++;
                    Managers.Game.m_iInventoryShowItemTapRightNum++;
                }
            }
            else // 1~4번째 탭을 선택한 상황이라면, 활성화 탭을 오른쪽으로 이동
            {
                Managers.Game.m_iInventoryCurrentSelectTapNum++;
            }
        }

        // 보여줄 탭 선정
        SelectShowUnTaps();

        // 전에 활성화 했던 탭의 번호를 현재 갱신된 탭에 번호를 찾고 아이템 정보를 갱신한다.
        RefreshAfterMoveTap();
    }

    public void TapsSelectClear()
    {
        for (int i = 0; i < m_goSelectedItemTaps.Length; i++)
        {
            m_goSelectedItemTaps[i].gameObject.SetActive(false);
        }
    }

    // 가지고 온 넘버를 가지고 해당 탭의 정보를 갱신
    void RefreshAfterMoveTap()
    {
        m_SelectUnSelectedItemTaps[Managers.Game.m_iInventoryCurrentSelectTapNum].SelectTap();
    }
}
