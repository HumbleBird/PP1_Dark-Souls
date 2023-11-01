using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Leggings Armor")]
public class LeggingsEquipmentItem : EquipmentItem
{
    public LeggingsEquipmentItem(int id) : base(id)
    {
        SetInfo(id);


        if (id == 1004)
        {
            Hips_Attachment=0;
            Knee_Attachement_Right=0;
            Knee_Attachement_Left=0;
            HipName=2;
            LeftLeggingName=16;
            RightLeggingName=16;
        }
    }

    [Header("Item All Gender Parts Name")]
    [SerializeField] int Hips_Attachment;
    [SerializeField] int Knee_Attachement_Right;
    [SerializeField] int Knee_Attachement_Left;
    public string m_Hips_Attachment { get { return "Chr_HipsAttachment_" + Hips_Attachment.ToString("00"); } }
    public string m_Knee_Attachement_Right { get { return "Chr_KneeAttachRight_" + Knee_Attachement_Right.ToString("00"); } }
    public string m_Knee_Attachement_Left { get { return "Chr_KneeAttachLeft_" + Knee_Attachement_Left.ToString("00"); } }

    [Header("Item Gender Parts Name")]
    [SerializeField] int HipName;
    [SerializeField] int LeftLeggingName;
    [SerializeField] int RightLeggingName;
    public string m_HipName { get { return "Chr_Hips_" + playerGender + HipName.ToString("00"); } }
    public string m_LeftLeggingName { get { return "Chr_LegLeft_" + playerGender + LeftLeggingName.ToString("00"); } }
    public string m_RightLeggingName { get { return "Chr_LegRight_" + playerGender + RightLeggingName.ToString("00"); } }
}
