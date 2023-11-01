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
        ItemTapNameText, // ������ �ι� �� �̸�
        ItemNameText, // ������ �̸�
    }

    enum GameObjects
    {
        LBText, // ������ ���� �������� �� ĭ �̵�
        RBText, // ������ ���� ���������� �� ĭ �̵�
        InventoryItemSlotsPanel, // ������ ������ ������ �г�
    }

    PlayerManager m_Player;

    public InventoryItemTapSlotUI[] m_UnSelectedItemTaps = new InventoryItemTapSlotUI[15]; // �� 15���� ��


    public InventoryItemTapSlotUI[] m_SelectUnSelectedItemTaps = new InventoryItemTapSlotUI[5]; // �����ִ� 5���� ��
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

    // ������ 5���� ���� ����
    public void SelectShowUnTaps()
    {
        // Un Selected Cleard
        for (int i = 0; i < m_UnSelectedItemTaps.Length; i++)
        {
            m_UnSelectedItemTaps[i].gameObject.SetActive(false);
            m_UnSelectedItemTaps[i].m_iShowTapNum = -1;

        }

        // ������ 5�� Tap ����
        // �̹��� ����
        int count = 0;
        for (int i = Managers.Game.m_iInventoryShowItemTapLeftNum; i <= Managers.Game.m_iInventoryShowItemTapRightNum; i++)
        {
            m_UnSelectedItemTaps[i].ShowTap(count);

            m_SelectedItemSlotImage[count].sprite = m_SelectUnSelectedItemTaps[count].m_TapSlotItemImage.sprite;
            // TODO Tap Num �� ���� �پ��� �̹��� ü����
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
            // ���� Ȱ��ȭ ���� ��� ��ġ�� �ִ°�
            if (Managers.Game.m_iInventoryCurrentSelectTapNum == 0) // �̹� ���� �� Ŭ���� ���¿��ٸ�
            {
                if(Managers.Game.m_iInventoryShowItemTapLeftNum == 0) // ���� ������ Tool Tap�� �̹� ������ ���¿��ٸ� ���� ���������� �̵�
                {
                    Managers.Game.m_iInventoryCurrentSelectTapNum = 4; // Ȱ��ȭ ���� ���� ���������� �̵�

                    Managers.Game.m_iInventoryShowItemTapLeftNum = 10;
                    Managers.Game.m_iInventoryShowItemTapRightNum = 14;
                }
                else // Tool Tap�� �� �������� �� �� �ִٸ� �� ĭ�� �̵�
                {
                    Managers.Game.m_iInventoryShowItemTapLeftNum--;
                    Managers.Game.m_iInventoryShowItemTapRightNum--;
                }
            }
            else // 2~5��° ���� ������ ��Ȳ�̶��, Ȱ��ȭ ���� �������� �̵�
            {
                Managers.Game.m_iInventoryCurrentSelectTapNum--;
            }
        }
        else
        {
            // ���� Ȱ��ȭ ���� ��� ��ġ�� �ִ°�
            if (Managers.Game.m_iInventoryCurrentSelectTapNum == 4) // �̹� ���� �� Ŭ���� ���¿��ٸ�
            {
                if (Managers.Game.m_iInventoryShowItemTapRightNum == 14) // ���� �������� ���� Tap�� �̹� ������ ���¿��ٸ� Tool ������ �̵�
                {
                    Managers.Game.m_iInventoryCurrentSelectTapNum = 0; // Ȱ��ȭ ���� ���� �������� �̵�
                    
                    Managers.Game.m_iInventoryShowItemTapLeftNum = 0;
                    Managers.Game.m_iInventoryShowItemTapRightNum = 4;
                }
                else // �����ִ� ���� �������� ���� ���� ���� �ƴ϶��
                {
                    Managers.Game.m_iInventoryShowItemTapLeftNum++;
                    Managers.Game.m_iInventoryShowItemTapRightNum++;
                }
            }
            else // 1~4��° ���� ������ ��Ȳ�̶��, Ȱ��ȭ ���� ���������� �̵�
            {
                Managers.Game.m_iInventoryCurrentSelectTapNum++;
            }
        }

        // ������ �� ����
        SelectShowUnTaps();

        // ���� Ȱ��ȭ �ߴ� ���� ��ȣ�� ���� ���ŵ� �ǿ� ��ȣ�� ã�� ������ ������ �����Ѵ�.
        RefreshAfterMoveTap();
    }

    public void TapsSelectClear()
    {
        for (int i = 0; i < m_goSelectedItemTaps.Length; i++)
        {
            m_goSelectedItemTaps[i].gameObject.SetActive(false);
        }
    }

    // ������ �� �ѹ��� ������ �ش� ���� ������ ����
    void RefreshAfterMoveTap()
    {
        m_SelectUnSelectedItemTaps[Managers.Game.m_iInventoryCurrentSelectTapNum].SelectTap();
    }
}
