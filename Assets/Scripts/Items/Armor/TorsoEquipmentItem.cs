using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Eqipment/Torso Armor")]
public class TorsoEquipmentItem : EquipmentItem
{
    public TorsoEquipmentItem(int id) : base(id)
    {
        SetInfo(id);

        if (id == (int)E_ArmorItemID.KnightTorso)
        {
            Back_Attachment = 0;
            Shoulder_Attachment_Right = 5;
            Shoulder_Attachment_Left = 5;
            TorsoEquipmentItemName = 22;
        }
        if (id == (int)E_ArmorItemID.AssassinTorso)
        {
            Back_Attachment = 15;
            Shoulder_Attachment_Right = 14;
            Shoulder_Attachment_Left = 14;
            TorsoEquipmentItemName = 13;
        }
        if (id == (int)E_ArmorItemID.HeraldTorso)
        {
            Back_Attachment = 0;
            Shoulder_Attachment_Right = 6;
            Shoulder_Attachment_Left = 6;
            TorsoEquipmentItemName = 2;
        }
        if (id == (int)E_ArmorItemID.PyromancerTorso)
        {
            Back_Attachment = 0;
            Shoulder_Attachment_Right = 15;
            Shoulder_Attachment_Left = 15;
            TorsoEquipmentItemName = 3;
        }
        if (id == (int)E_ArmorItemID.WarriorTorso)
        {
            Back_Attachment = 0;
            Shoulder_Attachment_Right = 17;
            Shoulder_Attachment_Left = 17;
            TorsoEquipmentItemName = 15;
        }
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
