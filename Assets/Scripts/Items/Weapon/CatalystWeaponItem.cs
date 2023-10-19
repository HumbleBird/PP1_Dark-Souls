using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Catalyst Weapon Item")]
public class CatalystWeaponItem : WeaponItem
{
    CatalystWeaponItem()
    {
        m_EItemType = Define.E_ItemType.Catalyst;
    }

    public int m_iMagicAdjustment;
}
