using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Eqipment/Gantlets Armor")]
public class GantletsEquipmentItem : EquipmentItem
{
    public GantletsEquipmentItem(int id) : base(id)
    {
        SetInfo(id);


        if (id == (int)E_ArmorItemID.KnightGauntlets)
        {
            Elbow_Attachment_Right = 0;
            Elbow_Attachment_Left = 0;

            Arm_Upper_RightName=6;
            Arm_Upper_LeftName=6;
            Arm_Lower_RightName=16;
            Arm_Lower_LeftName=16;
            Hand_RightName=12;
            Hand_LeftName=12;
        }
        else if (id == (int)E_ArmorItemID.AssassinGauntlets)
        {
            Elbow_Attachment_Right = 0;
            Elbow_Attachment_Left = 0;

            Arm_Upper_RightName = 7;
            Arm_Upper_LeftName = 7;
            Arm_Lower_RightName = 4;
            Arm_Lower_LeftName = 4;
            Hand_RightName = 2;
            Hand_LeftName = 2;
        }
        else if (id == (int)E_ArmorItemID.HeraldGauntlets)
        {
            Elbow_Attachment_Right = 0;
            Elbow_Attachment_Left = 0;

            Arm_Upper_RightName = 3;
            Arm_Upper_LeftName = 3;
            Arm_Lower_RightName = 15;
            Arm_Lower_LeftName = 15;
            Hand_RightName = 2;
            Hand_LeftName = 11;
        }
        else if (id == (int)E_ArmorItemID.PyromancerGauntlets)
        {
            Elbow_Attachment_Right = 0;
            Elbow_Attachment_Left = 0;

            Arm_Upper_RightName = 1;
            Arm_Upper_LeftName = 1;
            Arm_Lower_RightName = 3;
            Arm_Lower_LeftName = 3;
            Hand_RightName = 4;
            Hand_LeftName = 4;
        }
        else if (id == (int)E_ArmorItemID.WarriorGauntlets)
        {
            Elbow_Attachment_Right = 0;
            Elbow_Attachment_Left = 0;

            Arm_Upper_RightName = 7;
            Arm_Upper_LeftName = 7;
            Arm_Lower_RightName = 12;
            Arm_Lower_LeftName = 12;
            Hand_RightName = 1;
            Hand_LeftName = 1;
        }
    }

    [Header("Item All Gender Parts Name")]
    [SerializeField] int  Elbow_Attachment_Right;
    [SerializeField] int  Elbow_Attachment_Left;
    public string m_Elbow_Attachment_Right { get { return "Chr_ElbowAttachRight_" + Elbow_Attachment_Right.ToString("00"); } }
    public string m_Elbow_Attachment_Left { get { return "Chr_ElbowAttachLeft_" + Elbow_Attachment_Left.ToString("00"); } }

    [Header("Gender_Parts Item Name")]
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
