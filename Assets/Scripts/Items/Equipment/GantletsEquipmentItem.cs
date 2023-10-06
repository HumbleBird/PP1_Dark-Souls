using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Gantlets Armor")]
public class GantletsEquipmentItem : EquipmentItem
{
    GantletsEquipmentItem()
    {
        m_EItemType = Define.E_ItemType.Gauntlets;
    }

    [Header("Item All Gender Parts Name")]
    [SerializeField] int  Elbow_Attachment_Right;
    [SerializeField] int  Elbow_Attachment_Left;
    public string m_Elbow_Attachment_Right { get { return "Chr_ElbowAttachRight_" + Elbow_Attachment_Right.ToString("00"); } }
    public string m_Elbow_Attachment_Left { get { return "Chr_ElbowAttachLeft_" + Elbow_Attachment_Left.ToString("00"); } }

    [Header("Male_Parts Item Name")]
    [SerializeField] int Arm_Upper_RightName;
    [SerializeField] int Arm_Upper_LeftName;
    [SerializeField] int Arm_Lower_RightName;
    [SerializeField] int Arm_Lower_LeftName;
    [SerializeField] int Hand_RightName;
    [SerializeField] int Hand_LeftName;
    public string m_Arm_Upper_RightName { get { return "Chr_ArmUpperRight_" + playerGender + Arm_Upper_RightName.ToString("00"); } }
    public string m_Arm_Upper_LeftName { get { return "Chr_ArmUpperLeft_" + playerGender + Arm_Upper_LeftName.ToString("00"); } }
    public string m_Arm_Lower_RightName{ get { return "Chr_ArmLowerRight_" + playerGender + Arm_Lower_RightName.ToString("00"); } }
    public string m_Arm_Lower_LeftName { get { return "Chr_ArmLowerLeft_" + playerGender + Arm_Lower_LeftName.ToString("00"); } }
    public string m_Hand_RightName{ get { return "Chr_HandRight_" + playerGender + Hand_RightName.ToString("00"); } }
    public string m_Hand_LeftName { get { return "Chr_HandLeft_" + playerGender + Hand_LeftName.ToString("00"); } }
}
