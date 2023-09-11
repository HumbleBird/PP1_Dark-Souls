using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Leggings Armor")]
public class LeggingsEquipmentItem : EquipmentItem
{
    [Header("Item All Gender Parts Name")]
    public string Hips_Attachment;
    public string Knee_Attachement_Right;
    public string Knee_Attachement_Left;

    [Header("Item Gender Parts Name")]
    public string m_HipName;
    public string m_LeftLeggingName;
    public string m_RightLeggingName;
}
