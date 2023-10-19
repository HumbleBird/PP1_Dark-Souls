using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Valuables")]
public class ValuablesItem : Item
{
    public ValuablesItem()
    {
        m_EItemType = Define.E_ItemType.Valuables;
    }

    [Header("Current Count")]
    public int m_iCurrentCount; // ���� ���� ������ ��
    public int m_iMaxCount; // �ִ� ���� ������ ��

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // ���� ������ ��
    public int m_iMaxSaveCount; // �ִ� ������ ������ ��

    public string m_sItemDescription;
}
