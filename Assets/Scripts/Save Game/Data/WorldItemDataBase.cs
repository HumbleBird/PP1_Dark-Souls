using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDataBase
{

    // ���߿� dic���� ����
    public List<WeaponItem> weaponItems = new List<WeaponItem>();
    public List<EquipmentItem> equipmentItems = new List<EquipmentItem>();


    public WeaponItem GetWeaponItemByID(int weaponID)
    {
        return weaponItems.FirstOrDefault(weapon => weapon.m_iItemID == weaponID);
    }

    public EquipmentItem GetEquipmentItemByID(int equipmentID)
    {
        return equipmentItems.FirstOrDefault(equipment => equipment.m_iItemID == equipmentID);
    }
}
