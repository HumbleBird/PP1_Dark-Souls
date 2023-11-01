using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Catalyst Weapon Item")]
public class CatalystWeaponItem : WeaponItem
{
    CatalystWeaponItem()
    {
        m_eItemType = Define.E_ItemType.Catalyst;
    }

}
