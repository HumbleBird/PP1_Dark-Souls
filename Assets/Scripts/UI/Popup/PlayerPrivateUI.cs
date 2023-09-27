using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPrivateUI : UI_Base
{
    public SelectUI m_SelectUI;
    public EquipmentUI m_EquipmentUI;
    public InventoryUI m_InventoryUI;
    public LevelUpUI m_LevelUpUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
