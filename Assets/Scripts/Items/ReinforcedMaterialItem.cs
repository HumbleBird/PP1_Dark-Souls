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
    public int m_iCurrentCount; // ���� ���� ������ ��
    public int m_iMaxCount; // �ִ� ���� ������ ��

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // ���� ������ ��
    public int m_iMaxSaveCount; // �ִ� ������ ������ ��

    public string m_sItemDescription;
}
