using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDataBase
{

    // 나중에 dic으로 관리
    public List<WeaponItem> weaponItems = new List<WeaponItem>();
    public List<EquipmentItem> equipmentItems = new List<EquipmentItem>();


    public WeaponItem GetWeaponItemByID(int weaponID)
    {
        return weaponItems.FirstOrDefault(weapon => weapon.itemID == weaponID);
    }

    public EquipmentItem GetEquipmentItemByID(int equipmentID)
    {
        return equipmentItems.FirstOrDefault(equipment => equipment.itemID == equipmentID);
    }
}
