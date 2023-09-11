using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eqipment/Helm Armor")]
public class HelmEquipmentItem : EquipmentItem
{
    [Header("Item All Gender Parts Name")]
    public string HeadCoverings_Base_Hair;
    public string HeadCoverings_No_FacialHair;
    public string HeadCoverings_No_Hair;
    public string Head_Attachment_Helmet;


    [Header("Item Gender Parts Name")]
    public string m_HelmEquipmentItemName;

    [Header("Extra")]
    public string Extra_Elf_Ear;
}
