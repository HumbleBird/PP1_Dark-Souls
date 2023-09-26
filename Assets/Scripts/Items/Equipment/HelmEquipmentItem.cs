using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Helm Armor")]
public class HelmEquipmentItem : EquipmentItem
{
    [Header("Item All Gender Parts Name")]
    [SerializeField] int HeadCoverings_Base_Hair;
    [SerializeField] int HeadCoverings_No_FacialHair;
    [SerializeField] int HeadCoverings_No_Hair;
    [SerializeField] int Head_Attachment_Helmet;

    public string m_HeadCoverings_Base_Hair { get { return "Chr_HeadCoverings_Base_"  + HeadCoverings_Base_Hair.ToString("00"); } }
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
            if (player.playerInventoryManager.currentHelmetEquipment == null || player.playerInventoryManager.currentHelmetEquipment == player.playerEquipmentManager.Naked_HelmetEquipment)
            {
                return "Chr_Head_" + playerGender + HelmEquipmentItemName.ToString("00");
            }
            else
            {
                return "Chr_Head_No_Elements_" + playerGender + HelmEquipmentItemName.ToString("00");
            }
        } 
    }

    public bool hideFacialFeatures = false;
    //public bool hideBeardFeatures;
    //public bool hideEyebrowsFeatures;
    // ect



}
