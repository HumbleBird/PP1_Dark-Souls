using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Range Weapon Item")]
public class RangeWeaponItem : WeaponItem
{
    RangeWeaponItem()
    {
        m_eItemType = Define.E_ItemType.RangeWeapon;
    }

}
