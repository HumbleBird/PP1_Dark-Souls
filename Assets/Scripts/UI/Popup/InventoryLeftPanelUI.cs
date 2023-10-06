using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLeftPanelUI : UI_Base
{
    enum Texts
    {
        ItemPartSubjectName, // ������ �ι� �� �̸�
        ItemNameText, // ������ �̸�
    }

    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    enum GameObjects
    {
        // Left Panel
        LeftArrow,
        RightArrow,
        InvnetoryContents, // ������ ������ ������ �θ� ������Ʈ
    }

    PlayerManager player;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));

        player = Managers.Object.m_MyPlayer;

        CreateInventorySlot(GetObject((int)GameObjects.InvnetoryContents), player.playerInventoryManager.m_Items.Count);


        return true;
    }

    private void CreateInventorySlot(GameObject parent, int count)
    {
        foreach (Transform child in parent.transform)
            Managers.Resource.Destroy(child.gameObject);

        int makeSlotCountMax = 0;

        // if count == 6 �������� 1 ���� ������� �� - �������� �� = �� ������� ��.
        if ((count % 5) != 0)
        {
            makeSlotCountMax = count + 5 - (count % 5);
        }
        else
            makeSlotCountMax = count;

        for (int i = 0; i < makeSlotCountMax; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/InventoryItemSlot", parent.transform);
            InventoryItemSlotUI item = go.GetOrAddComponent<InventoryItemSlotUI>();

            if (i < count)
            {
                item.SetInfo(player.playerInventoryManager.m_Items[i]);
            }
        }
    }

}
