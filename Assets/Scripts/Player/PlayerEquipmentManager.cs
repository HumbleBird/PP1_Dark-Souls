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
    public Dictionary<E_SingleGenderEquipmentArmorParts, GenderPartsModelChanger> m_FemaleGenderPartsModelChanger = new Dictionary<E_SingleGenderEquipmentArmorParts, GenderPartsModelChanger>();
    public Dictionary<E_SingleGenderEquipmentArmorParts, GenderPartsModelChanger> m_MaleGenderPartsModelChanger = new Dictionary<E_SingleGenderEquipmentArmorParts, GenderPartsModelChanger>();

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

        Naked_HelmetEquipment.m_HelmEquipmentItemName = "0";
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
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }


            player.playerStatsManager.physicalDamageAbsorptionHead = 0;

            foreach (GameObject go in facialFeatures)
            {
                go.SetActive(true);
            }

            // 얼굴 외형 특징들 장착

            // 헤어 스타일
            if(player.playerInventoryManager.currentHairStyle != null)
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hair].EquipEquipmentsModelByName(player.playerInventoryManager.currentHairStyle.name);
            }

            if(player.playerInventoryManager.currentHairItem != null)
            {

                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HelmetAttachment].EquipEquipmentsModelByName(player.playerInventoryManager.currentHairItem.name);
            }

            // 속눈썹
            //if(player.playerInventoryManager.currentEyelashesBtn != null)
            //{
            //    if (m_bIsFemale)
            //    {
            //        m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Head].EquipEquipmentsModelByName(player.playerInventoryManager.currentEyelashesBtn.name);
            //    }
            //    else
            //    {
            //        m_MaleGenderPartsModelChanger[EquipmentArmorParts.Head].EquipEquipmentsModelByName(player.playerInventoryManager.currentEyelashesBtn.name);
            //    }
            //}

            // 눈썹
            if(player.playerInventoryManager.currentEyebrows != null)
            {

                if (m_bIsFemale)
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Eyebrow].EquipEquipmentsModelByName(player.playerInventoryManager.currentEyebrows.name);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Eyebrow].EquipEquipmentsModelByName(player.playerInventoryManager.currentEyebrows.name);
                }
            }

            // 콧수염
            if(player.playerInventoryManager.currentFacialHair != null)
            {
                if (m_bIsFemale)
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.FacialHair].EquipEquipmentsModelByName(player.playerInventoryManager.currentFacialHair.name);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.FacialHair].EquipEquipmentsModelByName(player.playerInventoryManager.currentFacialHair.name);
                }
            }

            // 코
            //if(player.playerInventoryManager.currentNose != null)
            //{
            //    if (m_bIsFemale)
            //    {
            //        m_FemaleGenderPartsModelChanger[EquipmentArmorParts.Head].EquipEquipmentsModelByName(player.playerInventoryManager.currentNose.name);
            //    }
            //    else
            //    {
            //        m_MaleGenderPartsModelChanger[EquipmentArmorParts.Head].EquipEquipmentsModelByName(player.playerInventoryManager.currentNose.name);
            //    }
            //}
        }

    }

    private void HeadItemEquipModel()
    {
        // all gender 처리
        HelmEquipmentItem temp = player.playerInventoryManager.currentHelmetEquipment;

        if (temp.m_HeadCoverings_Base_Hair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_Base_Hair].EquipEquipmentsModelByName(temp.m_HeadCoverings_Base_Hair);
        }
        if (temp.m_HeadCoverings_No_FacialHair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_FacialHair].EquipEquipmentsModelByName(temp.m_HeadCoverings_No_FacialHair);
        }
        if (temp.m_HeadCoverings_No_Hair != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_Hair].EquipEquipmentsModelByName(temp.m_HeadCoverings_No_Hair);
        }
        if (temp.m_Head_Attachment_Helmet != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.HelmetAttachment].EquipEquipmentsModelByName(temp.m_Head_Attachment_Helmet);
        }

        if (temp.m_HelmEquipmentItemName != null || temp.m_HelmEquipmentItemName != null)
        {
            if (m_bIsFemale)
            {
                if(temp == player.playerEquipmentManager.Naked_HelmetEquipment)
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
                else
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head_No_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
            }
            else
            {
                if (temp == player.playerEquipmentManager.Naked_HelmetEquipment)
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head_No_Elements].EquipEquipmentsModelByName(temp.m_HelmEquipmentItemName);
                }
            }
        }

        if(temp.m_Extra_Elf_Ear != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Extra_Elf_Ear].EquipEquipmentsModelByName(temp.m_Extra_Elf_Ear);

        }

        if(temp.hideFacialFeatures == true)
        {
            foreach (GameObject go in facialFeatures)
            {
                go.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject go in facialFeatures)
            {
                go.SetActive(true);
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
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);

            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);
            }

            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }
    }

    private void ChestItemEquipModel()
    {
        // all gender 처리
        TorsoEquipmentItem temp = player.playerInventoryManager.currentTorsoEquipment;

        if (temp.m_Back_Attachment != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Back_Attachment].EquipEquipmentsModelByName(temp.m_Back_Attachment);
        }
        if (temp.m_Shoulder_Attachment_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Right].EquipEquipmentsModelByName(temp.m_Shoulder_Attachment_Right);
        }
        if (temp.m_Shoulder_Attachment_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Left].EquipEquipmentsModelByName(temp.m_Shoulder_Attachment_Left);
        }

        if (temp.m_TorsoEquipmentItemName != null)
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(temp.m_TorsoEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(temp.m_TorsoEquipmentItemName);
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
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_LeftName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_LeftName);
            }

            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    private void HandItemEquipModel()
    {
        // all gender 처리
        GantletsEquipmentItem temp = player.playerInventoryManager.currentHandEquipment;

        if (temp.m_Elbow_Attachment_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Right].EquipEquipmentsModelByName(temp.m_Elbow_Attachment_Right);
        }
        if (temp.m_Elbow_Attachment_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Left].EquipEquipmentsModelByName(temp.m_Elbow_Attachment_Left);
        }

        if (temp != null)
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);           
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }

            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHandEquipment.m_fWeight;
        }
        else
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
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
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_LeftLeggingName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_RightLeggingName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hip].EquipEquipmentsModelByName(Naked_LegEquipment.m_HipName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_LeftLeggingName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_RightLeggingName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hip].EquipEquipmentsModelByName(Naked_LegEquipment.m_HipName);
            }
            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }
    }

    private void LegItemEquipModel()
    {
        // all gender 처리
        LeggingsEquipmentItem temp = player.playerInventoryManager.currentLegEquipment;

        if (temp.m_Hips_Attachment != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hips_Attachment].EquipEquipmentsModelByName(temp.m_Hips_Attachment);
        }
        if (temp.m_Knee_Attachement_Right != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Right].EquipEquipmentsModelByName(temp.m_Knee_Attachement_Right);
        }
        if (temp.m_Knee_Attachement_Left != null)
        {
            m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Left].EquipEquipmentsModelByName(temp.m_Knee_Attachement_Left);
        }

        if (m_bIsFemale)
        {
            m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hip].EquipEquipmentsModelByName(temp.m_HipName);
            m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(temp.m_LeftLeggingName);
            m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(temp.m_RightLeggingName);
        }
        else
        {
            m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hip].EquipEquipmentsModelByName(temp.m_HipName);
            m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.LeftLegging].EquipEquipmentsModelByName(temp.m_LeftLeggingName);
            m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(temp.m_RightLeggingName);
        }
    }

    public void SelectGender(bool isFemale)
    {
        // 플레이어의 모듈러를 교체
        m_bIsFemale = isFemale;

        EquipAllEquipmentModel();
    }
}
