using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : UI_Base
{
    enum Images
    {
        // Left Panel
        ItemBasePlateIcon,
        ItemIcon,
        ItemSelectIcon
    }

    Item m_Item;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        GetImage((int)Images.ItemBasePlateIcon);
        GetImage((int)Images.ItemIcon);
        GetImage((int)Images.ItemSelectIcon);

        return true;
    }

    public void SetInfo(Item item)
    {
        m_Item = item;
    }

    public void ShowItemInfomation()
    {
        if (m_Item == null)
            return;

        // 가운데에 정보 띄윅
        // left에 아이템 이름 뛰우기
        // 아이템 선택 이미지 
        Debug.Log(m_Item.name);
    }
}
