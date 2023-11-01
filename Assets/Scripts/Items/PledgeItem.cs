using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pledge")]
public class PledgeItem : Item
{
    PledgeItem()
    {
        m_eItemType = Define.E_ItemType.Pledge;
    }

    [Header("Item Description")]
    public string m_sItemDescription;
}
