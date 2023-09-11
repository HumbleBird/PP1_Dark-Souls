using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Torso Armor")]
public class TorsoEquipmentItem : EquipmentItem
{
    [Header("Item All Gender Parts Name")]
    public string Back_Attachment;
    public string Shoulder_Attachment_Right;
    public string Shoulder_Attachment_Left;

    [Header("Item Gender Parts Name")]
    public string m_TorsoEquipmentItemName;
}
