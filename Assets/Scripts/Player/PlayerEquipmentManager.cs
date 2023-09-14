using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    public bool m_bIsFemale = false;

    [Header("Equipment Model Changers")]
    public Dictionary<All_GenderItemPartsType, AllGenderPartModelChanger> m_AllGenderPartsModelChanger = new Dictionary<All_GenderItemPartsType, AllGenderPartModelChanger>();
    public Dictionary<EquipmentArmorParts, GenderPartsModelChanger> m_FemaleGenderPartsModelChanger = new Dictionary<EquipmentArmorParts, GenderPartsModelChanger>();
    public Dictionary<EquipmentArmorParts, GenderPartsModelChanger> m_MaleGenderPartsModelChanger = new Dictionary<EquipmentArmorParts, GenderPartsModelChanger>();

    [Header("Naked Armor Equipment")]
    public HelmEquipmentItem Naked_HelmetEquipment;
    public TorsoEquipmentItem Naked_TorsoEquipment;
    public LeggingsEquipmentItem Naked_LegEquipment;
    public GantletsEquipmentItem Naked_HandEquipment;

    [Header("Facial Features")]
    public GameObject[] facialFeatures;

    float poisonResistance = 0;
    float totalEquipmentLoad = 0;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        player.characterStatsManager.CalculateAndSetMaxEquipload();
    }

    public void EquipAllEquipmentModel()
    {
        // First All UnEquipment
        ModelChangerUnEquipAllItem();

        poisonResistance = 0;
        totalEquipmentLoad = 0;

        // Helm
        HeadItemEquip();

        // Torso
        ChestItemEquip();

        // Hands
        HandItemEquip();

        // Legs
        LegItemEquip();

        player.playerStatsManager.poisonResistance = poisonResistance;
        player.playerStatsManager.CaculateAndSetCurrentEquipLoad(totalEquipmentLoad);
    }

    private void ModelChangerUnEquipAllItem()
    {
        foreach (ModelChangerManager modelchanger in m_AllGenderPartsModelChanger.Values)
        {
            modelchanger.UnEquipAllEquipmentsModels();
        }
        foreach (ModelChangerManager modelchanger in m_FemaleGenderPartsModelChanger.Values)
        {
            modelchanger.UnEquipAllEquipmentsModels();
        }
        foreach (ModelChangerManager modelchanger in m_MaleGenderPartsModelChanger.Values)
        {
            modelchanger.UnEquipAllEquipmentsModels();
        }
    }

    private void HeadItemEquip()
    {
        if (player.playerInventoryManager.currentHelmetEquipment != null)
        {
            // 아이템 종류를 보고 넣어야 지.

            HeadItemEquipModel();
            player.playerStatsManager.physicalDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHelmetEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHelmetEquipment.m_fWeight;

        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Head_All_Elements].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Head_All_Elements].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;

            foreach (GameObject go in facialFeatures)
            {
                go.SetActive(true);
            }
        }

    }

    private void HeadItemEquipModel()
    {
        // all gender 처리
        HelmEquipmentItem temp = player.playerInventoryManager.currentHelmetEquipment;

        if (temp.HeadCoverings_Base_Hair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_Base_Hair].EquipEquipmentsModelByName(temp.HeadCoverings_Base_Hair);
        }
        if (temp.HeadCoverings_No_FacialHair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_FacialHair].EquipEquipmentsModelByName(temp.HeadCoverings_No_FacialHair);
        }
        if (temp.HeadCoverings_No_Hair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_Hair].EquipEquipmentsModelByName(temp.HeadCoverings_No_Hair);
        }
        if (temp.Head_Attachment_Helmet != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Head_Attachment_Helmet].EquipEquipmentsModelByName(temp.Head_Attachment_Helmet);
        }

        if (temp.m_HelmEquipmentItemName != null)
        {
            if (m_bIsFemale)
            {
                if(temp == player.playerEquipmentManager.Naked_HelmetEquipment)
                {
                    m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Head_All_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
                else
                {
                    m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Head_No_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
            }
            else
            {
                if (temp == player.playerEquipmentManager.Naked_HelmetEquipment)
                {
                    m_MaleGenderPartsModelChanger[EquipmentArmorParts.Head_All_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[EquipmentArmorParts.Head_No_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
            }
        }

        if(temp.Extra_Elf_Ear != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Extra_Elf_Ear].EquipEquipmentsModelByName(temp.Extra_Elf_Ear);

        }

        if(temp.hideFacialFeatures == true)
        {
            foreach (GameObject go in facialFeatures)
            {
                go.SetActive(false);
            }
        }
    }

    private void ChestItemEquip()
    {
        if (player.playerInventoryManager.currentTorsoEquipment != null)
        {
            // 아이템 종류를 보고 넣어야 지.

            ChestItemEquipModel();
            player.playerStatsManager.physicalDamageAbsorptionBody = player.playerInventoryManager.currentTorsoEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentTorsoEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentTorsoEquipment.m_fWeight;
        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);

            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);
            }

            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }
    }

    private void ChestItemEquipModel()
    {
        // all gender 처리
        TorsoEquipmentItem temp = player.playerInventoryManager.currentTorsoEquipment;

        if (temp.Back_Attachment != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Back_Attachment].EquipEquipmentsModelByName(temp.Back_Attachment);
        }
        if (temp.Shoulder_Attachment_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Right].EquipEquipmentsModelByName(temp.Shoulder_Attachment_Right);
        }
        if (temp.Shoulder_Attachment_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Left].EquipEquipmentsModelByName(temp.Shoulder_Attachment_Left);
        }

        if (temp.m_TorsoEquipmentItemName != null)
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(temp.m_TorsoEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(temp.m_TorsoEquipmentItemName);
            }
        }
    }

    private void HandItemEquip()
    {
        if (player.playerInventoryManager.currentHandEquipment != null)
        {
            // 아이템 종류를 보고 넣어야 지.

            HandItemEquipModel();
            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHandEquipment.m_fWeight;
        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }

            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    private void HandItemEquipModel()
    {
        // all gender 처리
        GantletsEquipmentItem temp = player.playerInventoryManager.currentHandEquipment;

        if (temp.Elbow_Attachment_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Right].EquipEquipmentsModelByName(temp.Elbow_Attachment_Right);
        }
        if (temp.Elbow_Attachment_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Left].EquipEquipmentsModelByName(temp.Elbow_Attachment_Left);
        }

        if (temp != null)
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);           
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }

            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHandEquipment.m_fWeight;
        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }
        }
    }

    private void LegItemEquip()
    {
        if (player.playerInventoryManager.currentLegEquipment != null)
        {
            // 아이템 종류를 보고 넣어야 지.

            LegItemEquipModel();
            player.playerStatsManager.physicalDamageAbsorptionLegs = player.playerInventoryManager.currentLegEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentLegEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentLegEquipment.m_fWeight;
        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_LeftLeggingName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_RightLeggingName);
                m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(Naked_LegEquipment.m_HipName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_LeftLeggingName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_RightLeggingName);
                m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(Naked_LegEquipment.m_HipName);
            }
            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }
    }

    private void LegItemEquipModel()
    {
        // all gender 처리
        LeggingsEquipmentItem temp = player.playerInventoryManager.currentLegEquipment;

        if (temp.Hips_Attachment != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hips_Attachment].EquipEquipmentsModelByName(temp.Hips_Attachment);
        }
        if (temp.Knee_Attachement_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Right].EquipEquipmentsModelByName(temp.Knee_Attachement_Right);
        }
        if (temp.Knee_Attachement_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Left].EquipEquipmentsModelByName(temp.Knee_Attachement_Left);
        }

        if (m_bIsFemale)
        {
            m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(temp.m_HipName);
            m_FemaleGenderPartsModelChanger[EquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(temp.m_LeftLeggingName);
            m_FemaleGenderPartsModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(temp.m_RightLeggingName);
        }
        else
        {
            m_MaleGenderPartsModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(temp.m_HipName);
            m_MaleGenderPartsModelChanger[EquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(temp.m_LeftLeggingName);
            m_MaleGenderPartsModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(temp.m_RightLeggingName);
        }
    }

}
