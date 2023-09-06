using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Gantlets Armor")]
public class GantletsEquipmentItem : EquipmentItem
{
    [Header("Item Parts Name")]
    public string m_Arm_Upper_RightName;
    public string m_Arm_Upper_LeftName;
    public string m_Arm_Lower_RightName;
    public string m_Arm_Lower_LeftName;
    public string m_Hand_RightName;
    public string m_Hand_LeftName;
}
