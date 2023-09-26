using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPrivateUI : UI_Base
{
    enum GameObjects
    {
        InventoryUI,
        EquipementUI,
    }

    public GameObject m_goInventory;
    public EquipmentWindowUI m_Equipemnt;
    public SelectWindowUI m_SelectWindowUI;
    public ItemStatWindowUI m_ItemStat;
    public GameObject m_goLevelUp;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        m_goInventory = GetObject((int)GameObjects.InventoryUI);

        m_SelectWindowUI = GetComponentInChildren<SelectWindowUI>();

        return true;
    }
}
