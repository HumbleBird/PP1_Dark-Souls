using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // 아이템 바꿔서 장착하기

    PlayerManager player;

    public bool m_bIsFemale = false;

    #region Character Creation

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

    [Header("Current External Features")]
    public GameObject currentHairStyle;
    public GameObject currentHairItem;
    public GameObject currentEyelashesBtn;
    public GameObject currentEyebrows;
    public GameObject currentFacialHair;
    public GameObject currentFacialMask;
    public GameObject currentNose;
    public GameObject currentExtra;

    #endregion

    float poisonResistance = 0;
    float totalEquipmentLoad = 0;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        player.playerStatsManager.CalculateAndSetMaxEquipload();

        Naked_HelmetEquipment.m_HelmEquipmentItemName = "0";

        EquipAllEquipmentModel();
    }

    public void EquipAllEquipmentModel()
    {
        // First All UnEquipment
        ModelChangerUnEquipAllItem();

        // 장비 능력치 적용
        EquipAllEquipmentAbilityValue();

        // 장비 외형 적용
        EquipAllEquipmentLook();

        // TODO Weapon
    }

    public void EquipAllEquipmentAbilityValue()
    {
        poisonResistance = 0;
        totalEquipmentLoad = 0;

        EquipHeadEquipmentAbilityValue();
        EquipChestEquipmentAbilityValue();
        EquipHandEquipmentAbilityValue();
        EquipLegEquipmentAbilityValue();

        player.playerStatsManager.poisonResistance = poisonResistance;
        player.playerStatsManager.CaculateAndSetCurrentEquipLoad(totalEquipmentLoad);
    }

    public void EquipAllEquipmentLook()
    {
        EquipHeadEquipmentLook();
        EquipChestEquipmentLook();
        EquipHandArmorEquipmentLook();
        EquipLegArmorEquipmentLook();
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

    private void EquipHeadEquipmentAbilityValue()
    {
        if (m_HelmetEquipment != null)
        {
            // 아이템 종류를 보고 넣어야 지.
            player.playerStatsManager.physicalDamageAbsorptionHead = m_HelmetEquipment.m_fPhysicalDefense;
            poisonResistance += m_HelmetEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_HelmetEquipment.m_fWeight;
        }
        else
        {
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;
        }
    }

    private void EquipHeadEquipmentLook()
    {
        // 얼굴 특징 숨기기 관련
        foreach (GameObject go in facialFeatures)
        {
            go.SetActive(true);
        }

        // 장착중인 장비가 있다면
        if (m_HelmetEquipment != null)
        {
            // all gender 처리
            HelmEquipmentItem temp = m_HelmetEquipment;

            if (temp.m_HeadCoverings_Base_Hair != "Chr_HeadCoverings_Base_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_Base_Hair].EquipEquipmentsModelByName(temp.m_HeadCoverings_Base_Hair);
            }
            if (temp.m_HeadCoverings_No_FacialHair != "Chr_HeadCoverings_No_FacialHair_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_FacialHair].EquipEquipmentsModelByName(temp.m_HeadCoverings_No_FacialHair);
            }
            if (temp.m_HeadCoverings_No_Hair != "Chr_HeadCoverings_No_Hair_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HeadCoverings_No_Hair].EquipEquipmentsModelByName(temp.m_HeadCoverings_No_Hair);
            }
            if (temp.m_Head_Attachment_Helmet != "Chr_HelmetAttachment_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HelmetAttachment].EquipEquipmentsModelByName(temp.m_Head_Attachment_Helmet);
            }

            if (m_bIsFemale)
            {
                if (temp == player.playerEquipmentManager.Naked_HelmetEquipment)
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

            // 외형 숨기기
            if (temp.m_bisHideFacialFeatures == true)
            {
                foreach (GameObject go in facialFeatures)
                {
                    go.SetActive(false);
                }
            }
        }
        else
        {
            // Naked
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Head].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            }

            
        }

        // 헤어 스타일 및 장식품
        {
            // 헤어 스타일
            if (currentHairStyle != null)
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hair].EquipEquipmentsModelByName(currentHairStyle.name);
            }

            if (currentHairItem != null)
            {

                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HelmetAttachment].EquipEquipmentsModelByName(currentHairItem.name);
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
            if (currentEyebrows != null)
            {

                if (m_bIsFemale)
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Eyebrow].EquipEquipmentsModelByName(currentEyebrows.name);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Eyebrow].EquipEquipmentsModelByName(currentEyebrows.name);
                }
            }

            // 콧수염
            if (currentFacialHair != null)
            {
                if (m_bIsFemale)
                {
                    m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.FacialHair].EquipEquipmentsModelByName(currentFacialHair.name);
                }
                else
                {
                    m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.FacialHair].EquipEquipmentsModelByName(currentFacialHair.name);
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

            // 추가
            if (currentExtra != null)
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Extra_Elf_Ear].EquipEquipmentsModelByName(currentExtra.name);
            }
        }
        
    }

    private void EquipChestEquipmentAbilityValue()
    {
        if (m_TorsoEquipment != null)
        {
            player.playerStatsManager.physicalDamageAbsorptionBody = m_TorsoEquipment.m_fPhysicalDefense;
            poisonResistance += m_TorsoEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_TorsoEquipment.m_fWeight;
        }
        else
        {
            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }
    }

    private void EquipChestEquipmentLook()
    {
        if (m_TorsoEquipment == null)
        {
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);
            }
        }
        else
        {
            // all gender 처리
            TorsoEquipmentItem temp = m_TorsoEquipment;

            if (temp.m_Back_Attachment != "Chr_BackAttachment_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Back_Attachment].EquipEquipmentsModelByName(temp.m_Back_Attachment);
            }
            if (temp.m_Shoulder_Attachment_Right != "Chr_ShoulderAttachRight_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Right].EquipEquipmentsModelByName(temp.m_Shoulder_Attachment_Right);
            }
            if (temp.m_Shoulder_Attachment_Left != "Chr_ShoulderAttachLeft_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Shoulder_Attachment_Left].EquipEquipmentsModelByName(temp.m_Shoulder_Attachment_Left);
            }

            if (temp.m_TorsoEquipmentItemName != "Chr_Torso_00")
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
    }

    private void EquipHandEquipmentAbilityValue()
    {
        if (m_HandEquipment != null)
        {
            player.playerStatsManager.physicalDamageAbsorptionHands = m_HandEquipment.m_fPhysicalDefense;
            poisonResistance += m_HandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_HandEquipment.m_fWeight;
        }
        else
        {
            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    private void EquipHandArmorEquipmentLook()
    {
        if(m_HandEquipment == null)
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
        }
        else
        {
            // all gender 처리
            GantletsEquipmentItem temp = m_HandEquipment;

            if (temp.m_Elbow_Attachment_Right != "Chr_ElbowAttachRight_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Right].EquipEquipmentsModelByName(temp.m_Elbow_Attachment_Right);
            }
            if (temp.m_Elbow_Attachment_Left != "Chr_ElbowAttachLeft_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Elbow_Attachment_Left].EquipEquipmentsModelByName(temp.m_Elbow_Attachment_Left);
            }

            // Gender
            if (m_bIsFemale)
            {
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(temp.m_Arm_Upper_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(temp.m_Arm_Upper_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(temp.m_Arm_Lower_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(temp.m_Arm_Lower_LeftName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(temp.m_Hand_RightName);
                m_FemaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(temp.m_Hand_LeftName);
            }
            else
            {
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(temp.m_Arm_Upper_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Upper_Left].EquipEquipmentsModelByName(temp.m_Arm_Upper_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(temp.m_Arm_Lower_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Arm_Lower_Left].EquipEquipmentsModelByName(temp.m_Arm_Lower_LeftName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Right].EquipEquipmentsModelByName(temp.m_Hand_RightName);
                m_MaleGenderPartsModelChanger[E_SingleGenderEquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(temp.m_Hand_LeftName);
            }
        }
    }

    private void EquipLegEquipmentAbilityValue()
    {
        if (m_LegEquipment != null)
        {
            player.playerStatsManager.physicalDamageAbsorptionLegs = m_LegEquipment.m_fPhysicalDefense;
            poisonResistance += m_LegEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_LegEquipment.m_fWeight;
        }
        else
        {

            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }
    }

    private void EquipLegArmorEquipmentLook()
    {
        if(m_LegEquipment == null)
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

        }
        else
        {
            // all gender 처리
            LeggingsEquipmentItem temp = m_LegEquipment;

            if (temp.m_Hips_Attachment != "Chr_HipsAttachment_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hips_Attachment].EquipEquipmentsModelByName(temp.m_Hips_Attachment);
            }
            if (temp.m_Knee_Attachement_Right != "Chr_KneeAttachRight_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Right].EquipEquipmentsModelByName(temp.m_Knee_Attachement_Right);
            }
            if (temp.m_Knee_Attachement_Left != "Chr_KneeAttachLeft_00")
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Knee_Attachement_Left].EquipEquipmentsModelByName(temp.m_Knee_Attachement_Left);
            }

            // Gender
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
    }

    public void ChangeCurrentEquipmentToNextNumSlotEquipment(int num)
    {
        // Left Hand Slot
        if(num == 0)
        {
            m_iCurrentLeftWeaponIndex++;

            // 마지막 슬롯에서 넘어가서 다시 처음 슬롯으로
            if(m_iCurrentLeftWeaponIndex > m_LeftWeaponsSlots.Length - 1)
                m_iCurrentLeftWeaponIndex = 0;

            // 왼쪽 키 Left Hand slot
            for (int i = 0; i < m_LeftWeaponsSlots.Length; i++)
            {
                // 다음 슬롯에 아이템이 있다면 아이템 로드
                if (m_iCurrentLeftWeaponIndex == i && m_LeftWeaponsSlots[i] != null)
                {
                    m_CurrentHandRightWeapon = m_LeftWeaponsSlots[m_iCurrentLeftWeaponIndex];
                    character.characterWeaponSlotManager.LoadWeaponOnSlot(m_LeftWeaponsSlots[m_iCurrentLeftWeaponIndex], true);
                    break;
                }
                // 다음 슬롯에 아이템이 없다면 건너뛰기
                else if (m_iCurrentLeftWeaponIndex == i && m_LeftWeaponsSlots[i] == null)
                {
                    m_iCurrentLeftWeaponIndex += 1;
                }
            }
        }
        
        // Right Hand Slot
        else if (num == 1)
        {
            m_iCurrentRightWeaponIndex++;

            // 마지막 슬롯에서 넘어가서 다시 처음 슬롯으로
            if (m_iCurrentRightWeaponIndex > m_RightWeaponsSlots.Length - 1)
                m_iCurrentRightWeaponIndex = 0;

            // 왼쪽 키 Left Hand slot
            for (int i = 0; i < m_RightWeaponsSlots.Length; i++)
            {
                // 다음 슬롯에 아이템이 있다면 아이템 로드
                if (m_iCurrentRightWeaponIndex == i && m_RightWeaponsSlots[i] != null)
                {
                    m_CurrentHandRightWeapon = m_RightWeaponsSlots[m_iCurrentRightWeaponIndex];
                    character.characterWeaponSlotManager.LoadWeaponOnSlot(m_RightWeaponsSlots[m_iCurrentRightWeaponIndex], false);
                    break;
                }
                // 다음 슬롯에 아이템이 없다면 건너뛰기
                else if (m_iCurrentRightWeaponIndex == i && m_RightWeaponsSlots[i] == null)
                {
                    m_iCurrentRightWeaponIndex += 1;
                }
            }

        }
        
        // Consumable Slot
        else if (num == 2)
        {
            m_iCurrentConsumableItemndex++;

            // 마지막 슬롯에서 넘어가서 다시 처음 슬롯으로
            if (m_iCurrentConsumableItemndex > m_ConsumableItemSlots.Length - 1)
            {
                m_iCurrentConsumableItemndex = 0;
            }

            // 왼쪽 키 Left Hand slot
            for (int i = 0; i < m_ConsumableItemSlots.Length; i++)
            {
                // 다음 슬롯에 아이템이 있다면 아이템 로드
                if (m_iCurrentConsumableItemndex == i && m_ConsumableItemSlots[i] != null)
                {
                    m_CurrentHandConsumable = m_ConsumableItemSlots[m_iCurrentConsumableItemndex];
                    break;
                }
                // 다음 슬롯에 아이템이 없다면 건너뛰기
                else if (m_iCurrentConsumableItemndex == i && m_ConsumableItemSlots[i] == null)
                {
                    m_iCurrentConsumableItemndex += 1;
                }
            }

            player.m_GameSceneUI.quickSlotsUI.RefreshUI();

        }

    }

    public void Refresh()
    {
        // 현재 인덱스 번호를 가지고 정보 업데이트.

        // Right Weapon
        if (m_RightWeaponsSlots[m_iCurrentRightWeaponIndex] != null)
        {
            m_CurrentHandRightWeapon = m_RightWeaponsSlots[m_iCurrentRightWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(m_RightWeaponsSlots[m_iCurrentRightWeaponIndex], false);
        }
        else
        {
            m_CurrentHandRightWeapon = player.playerWeaponSlotManager.unarmWeapon;
            player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerWeaponSlotManager.unarmWeapon, false);
        }

        // Left Weapon
        if (m_LeftWeaponsSlots[m_iCurrentLeftWeaponIndex] != null)
        {
            m_CurrentHandLeftWeapon = m_LeftWeaponsSlots[m_iCurrentLeftWeaponIndex];
            player.playerWeaponSlotManager.LoadWeaponOnSlot(m_LeftWeaponsSlots[m_iCurrentLeftWeaponIndex], true);
        }
        else
        {
            m_CurrentHandLeftWeapon = player.playerWeaponSlotManager.unarmWeapon;
            player.playerWeaponSlotManager.LoadWeaponOnSlot(player.playerWeaponSlotManager.unarmWeapon, true);
        }

        // Current Consumable Item

        if (m_ConsumableItemSlots[m_iCurrentConsumableItemndex] != null)
        {
            m_CurrentHandConsumable = m_ConsumableItemSlots[m_iCurrentConsumableItemndex];
            player.m_GameSceneUI.quickSlotsUI.RefreshUI();
        }
        else
        {
            m_CurrentHandConsumable = null;
            player.m_GameSceneUI.quickSlotsUI.RefreshUI();
        }
        // Spell Item
    }
}
