using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Torso Armor")]
public class TorsoEquipmentItem : EquipmentItem
{
    TorsoEquipmentItem()
    {
        m_EItemType = Define.E_ItemType.ChestArmor;
    }

    [Header("Item All Gender Parts Name")]
    [SerializeField] int Back_Attachment;
    [SerializeField] int Shoulder_Attachment_Right;
    [SerializeField] int Shoulder_Attachment_Left;
    public string m_Back_Attachment { get { return "Chr_BackAttachment_" + Back_Attachment.ToString("00"); } }
    public string m_Shoulder_Attachment_Right { get { return "Chr_ShoulderAttachRight_" + Shoulder_Attachment_Right.ToString("00"); } }
    public string m_Shoulder_Attachment_Left { get { return "Chr_ShoulderAttachLeft_" + Shoulder_Attachment_Left.ToString("00"); } }

    [Header("Item Gender Parts Name")]
    [SerializeField] int TorsoEquipmentItemName;
    public string m_TorsoEquipmentItemName { get { return "Chr_Torso_" + playerGender + TorsoEquipmentItemName.ToString("00"); } }
}
