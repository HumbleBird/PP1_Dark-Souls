using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Eqipment/Helm Armor")]
public class HelmEquipmentItem : EquipmentItem
{
    public HelmEquipmentItem(int id) : base(id)
    {
        SetInfo(id);

        if(id == (int)E_ArmorItemID.KnightHelm)
        {
            HeadCoverings_Base_Hair = 0 ;
            HeadCoverings_No_FacialHair = 0 ;
            HeadCoverings_No_Hair = 0 ;
            Head_Attachment_Helmet = 0 ;
            HelmEquipmentItemName = 6;
            m_bisHideFacialFeatures = true;
        }
        if (id == (int)E_ArmorItemID.AssassinHelm)
        {
            HeadCoverings_Base_Hair = 0;
            HeadCoverings_No_FacialHair = 3;
            HeadCoverings_No_Hair = 8;
            Head_Attachment_Helmet = 0;
            HelmEquipmentItemName = 0;
            m_bisHideFacialFeatures = false;
        }
        if (id == (int)E_ArmorItemID.HeraldHelm)
        {
            HeadCoverings_Base_Hair = 0;
            HeadCoverings_No_FacialHair = 0;
            HeadCoverings_No_Hair = 9;
            Head_Attachment_Helmet = 0;
            HelmEquipmentItemName = 0;
            m_bisHideFacialFeatures = false;
        }
        if (id == (int)E_ArmorItemID.PyromancerHelm)
        {
            HeadCoverings_Base_Hair = 3;
            HeadCoverings_No_FacialHair = 0;
            HeadCoverings_No_Hair = 0;
            Head_Attachment_Helmet = 0;
            HelmEquipmentItemName = 0;
            m_bisHideFacialFeatures = false;

            //Chr_Hair_02
            //Chr_Hair_06
            //Chr_Head_Male_01
            //Chr_Eyebrow_Male_01
               
        }
        if (id == (int)E_ArmorItemID.WarriorHelm)
        {
            HeadCoverings_Base_Hair = 0;
            HeadCoverings_No_FacialHair = 0;
            HeadCoverings_No_Hair = 0;
            Head_Attachment_Helmet = 0;
            HelmEquipmentItemName = 0;
            m_bisHideFacialFeatures = false;

            //Chr_Ear_Ear_02
            //Chr_Eyebrow_Female_01

        }
    }

    [Header("Item All Gender Parts Name")]
    [SerializeField] int HeadCoverings_Base_Hair;
    [SerializeField] int HeadCoverings_No_FacialHair;
    [SerializeField] int HeadCoverings_No_Hair;
    [SerializeField] int Head_Attachment_Helmet;

    public string m_HeadCoverings_Base_Hair  { get { return "Chr_HeadCoverings_Base_Hair_" + HeadCoverings_Base_Hair.ToString("00"); } }
    public string m_HeadCoverings_No_FacialHair { get { return "Chr_HeadCoverings_No_FacialHair_" + HeadCoverings_No_FacialHair.ToString("00"); } }
    public string m_HeadCoverings_No_Hair { get { return "Chr_HeadCoverings_No_Hair_" + HeadCoverings_No_Hair.ToString("00"); } }
    public string m_Head_Attachment_Helmet { get { return "Chr_HelmetAttachment_" + Head_Attachment_Helmet.ToString("00"); } }

    [Header("Item Parts Name")]
    [SerializeField] int HelmEquipmentItemName;
    public string m_HelmEquipmentItemName
    {
        set { HelmEquipmentItemName = int.Parse(value); }

        get 
        {
            PlayerManager player = Managers.Object.m_MyPlayer;
            if (player.playerEquipmentManager.m_HelmetEquipment == null || player.playerEquipmentManager.m_HelmetEquipment == player.playerEquipmentManager.Naked_HelmetEquipment || HelmEquipmentItemName == 0)
            {
                return "Chr_Head_" + playerGender + HelmEquipmentItemName.ToString("00");
            }
            else
            {
                return "Chr_Head_No_Elements_" + playerGender + HelmEquipmentItemName.ToString("00");
            }
        } 
    }

    public bool m_bisHideFacialFeatures = false;
    //public bool hideBeardFeatures;
    //public bool hideEyebrowsFeatures;
    // ect



}
