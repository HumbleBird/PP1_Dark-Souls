using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ReinforcedMaterialItem : Item
{
    public ReinforcedMaterialItem()
    {
        m_EItemType = E_ItemType.ReinforcedMaterial;
    }

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    public string m_sItemDescription;
}
