using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // ������ �ٲ㼭 �����ϱ�

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

    int poisonArmorResistance = 0;
    float totalEquipmentLoad = 0;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        player.playerStatsManager.CalculateAndSetMaxEquipload(player.playerStatsManager.m_iEnduranceLevel);

        Naked_HelmetEquipment.m_HelmEquipmentItemName = "0";

        EquipAllEquipmentModel();
    }

    public void EquipAllEquipmentModel()
    {
        // First All UnEquipment
        ModelChangerUnEquipAllItem();

        // ��� �ɷ�ġ ����
        EquipAllEquipmentAbilityValue();

        // ��� ���� ����
        EquipAllEquipmentLook();

        // TODO Weapon
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

    #region Equipment Ability
    private void EquipAllEquipmentAbilityValue()
    {
        player.playerStatsManager.m_fPhysicalDamageAbsorption = 0;

        poisonArmorResistance = 0;
        totalEquipmentLoad = 0;

        EquipHeadEquipmentAbilityValue();
        EquipChestEquipmentAbilityValue();
        EquipHandEquipmentAbilityValue();
        EquipLegEquipmentAbilityValue();

        player.playerStatsManager.m_iPoisonArmorResistance = poisonArmorResistance;
        player.playerStatsManager.CaculateAndSetCurrentEquipLoad(totalEquipmentLoad);
    }

    private void EquipHeadEquipmentAbilityValue()
    {
        if (m_HelmetEquipment != null)
        {
            // ������ ������ ���� �־�� ��.
            player.playerStatsManager.m_fPhysicalDamageAbsorption += m_HelmetEquipment.m_fPhysicalDamageAbsorption;
            poisonArmorResistance += m_HelmetEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_HelmetEquipment.m_fWeight;
        }
    }

    private void EquipChestEquipmentAbilityValue()
    {
        if (m_TorsoEquipment != null)
        {
            player.playerStatsManager.m_fPhysicalDamageAbsorption += m_TorsoEquipment.m_fPhysicalDamageAbsorption;
            poisonArmorResistance += m_TorsoEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_TorsoEquipment.m_fWeight;
        }
    }

    private void EquipHandEquipmentAbilityValue()
    {
        if (m_HandEquipment != null)
        {
            player.playerStatsManager.m_fPhysicalDamageAbsorption += m_HandEquipment.m_fPhysicalDamageAbsorption;
            poisonArmorResistance += m_HandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_HandEquipment.m_fWeight;
        }
    }

    private void EquipLegEquipmentAbilityValue()
    {
        if (m_LegEquipment != null)
        {
            player.playerStatsManager.m_fPhysicalDamageAbsorption += m_LegEquipment.m_fPhysicalDamageAbsorption;
            poisonArmorResistance += m_LegEquipment.m_fPoisonResistance;
            totalEquipmentLoad += m_LegEquipment.m_fWeight;
        }
    }
    #endregion

    #region Equipment Look
    public void EquipAllEquipmentLook()
    {
        EquipHeadEquipmentLook();
        EquipChestEquipmentLook();
        EquipHandArmorEquipmentLook();
        EquipLegArmorEquipmentLook();
    }

    private void EquipHeadEquipmentLook()
    {
        // �� Ư¡ ����� ����
        foreach (GameObject go in facialFeatures)
        {
            go.SetActive(true);
        }

        // �������� ��� �ִٸ�
        if (m_HelmetEquipment != null)
        {
            // all gender ó��
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

            // ���� �����
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

        // ��� ��Ÿ�� �� ���ǰ
        {
            // ��� ��Ÿ��
            if (currentHairStyle != null)
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hair].EquipEquipmentsModelByName(currentHairStyle.name);
            }

            if (currentHairItem != null)
            {

                m_AllGenderPartsModelChanger[All_GenderItemPartsType.HelmetAttachment].EquipEquipmentsModelByName(currentHairItem.name);
            }

            // �Ӵ���
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

            // ����
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

            // �����
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

            // ��
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

            // �߰�
            if (currentExtra != null)
            {
                m_AllGenderPartsModelChanger[All_GenderItemPartsType.Extra_Elf_Ear].EquipEquipmentsModelByName(currentExtra.name);
            }
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
                // all gender ó��
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

    private void EquipHandArmorEquipmentLook()
    {
        if (m_HandEquipment == null)
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
            // all gender ó��
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
            // all gender ó��
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
    #endregion

    // Quick Slot
    public void ChangeCurrentEquipmentToNextNumSlotEquipment(int num)
    {
        // Left Hand Slot
        if (num == 0)
            CheckEquipmentNextSlotItem(m_LeftWeaponsSlots, ref m_iCurrentLeftWeaponIndex);

        // Right Hand Slot
        if (num == 1)
            CheckEquipmentNextSlotItem(m_RightWeaponsSlots, ref m_iCurrentRightWeaponIndex);

        // Consumable Slot
        if (num == 2)
            CheckEquipmentNextSlotItem(m_ConsumableItemSlots, ref m_iCurrentConsumableItemndex);
    }

    void CheckEquipmentNextSlotItem(Item[] itemSlots, ref int index)
    {
        index++;

        // ������ ���Կ��� �Ѿ�� �ٽ� ó�� ��������
        if (index > itemSlots.Length - 1)
            index = 0;

        // ���� Ű Left Hand slot
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // ���� ���Կ� �������� �ִٸ� ������ �ε�
            if (index == i && itemSlots[i] != null)
            {
                Refresh();
                break;
            }
            // ���� ���Կ� �������� ���ٸ� �ǳʶٱ�
            else if (index == i && itemSlots[i] == null)
            {
                index += 1;

                // ������ ���Կ��� �Ѿ�� �ٽ� ó�� ��������
                if (index > itemSlots.Length - 1)
                    index = 0;

                for (int x = 0; x < itemSlots.Length; x++)
                {
                    if (itemSlots[i] == null)
                    {
                        index += 1;

                        // ������ ���Կ��� �Ѿ�� �ٽ� ó�� ��������
                        if (index > itemSlots.Length - 1)
                            index = 0;
                    }
                    else
                        break;
                }
            }
        }
    }

    public void Refresh()
    {
        // ���� �ε��� ��ȣ�� ������ ���� ������Ʈ.

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
        }
        else
        {
            m_CurrentHandConsumable = null;
        }

        // Spell Item

        // HUD - Quick Slot Update
        player.m_GameSceneUI.quickSlotsUI.RefreshUI();

        if(Managers.GameUI.m_EquipmentUI != null)
            Managers.GameUI.m_EquipmentUI.m_CurrentEquipmentsUI.RefreshUI();
    }

    // Equipment -> Inventory
    public void ChangeEquipment(E_EquipmentSlotsPartType type, Item item, int num = 0)
    {
        // 1. ���Ӱ� �����Ϸ��� �������� �̹� �������̶�� ������ ���Ը� ��ü
        // 2. ���Ӱ� �����Ϸ��� ������ ������ �̹� �ٸ� �������� �ִٸ� ���� ��ü

        // ������ ���� üũ

        switch (type)
        {
            case Define.E_EquipmentSlotsPartType.Right_Hand_Weapon:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if(item.m_isEquiping)
                {
                    if (((WeaponItem)item).m_isLeftHandEquiping)
                    {
                        m_LeftWeaponsSlots[item.m_iEquipSlotNum] = null;
                    }
                    else
                        m_RightWeaponsSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_RightWeaponsSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_RightWeaponsSlots[num].m_isEquiping = false;
                    }
                }

                break;
            case Define.E_EquipmentSlotsPartType.Left_Hand_Weapon:
                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    if (((WeaponItem)item).m_isLeftHandEquiping)
                    {
                        m_LeftWeaponsSlots[item.m_iEquipSlotNum] = null;
                    }
                    else
                        m_RightWeaponsSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_LeftWeaponsSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_LeftWeaponsSlots[num].m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Arrow:
                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_ArrowAmmoSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_ArrowAmmoSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_ArrowAmmoSlots[num].m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Bolt:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_BoltAmmoSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_BoltAmmoSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_BoltAmmoSlots[num].m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Helmt:
                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_HelmetEquipment = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_HelmetEquipment != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_HelmetEquipment.m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Chest_Armor:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_TorsoEquipment = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_TorsoEquipment != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_TorsoEquipment.m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Gantlets:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_HandEquipment = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_HandEquipment != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_HandEquipment.m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Leggings:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_LegEquipment = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_LegEquipment != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_LegEquipment.m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Ring:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_RingSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_RingSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_RingSlots[num].m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Consumable:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_ConsumableItemSlots[item.m_iEquipSlotNum] = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_ConsumableItemSlots[num] != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_ConsumableItemSlots[num].m_isEquiping = false;
                    }
                }
                break;
            case Define.E_EquipmentSlotsPartType.Pledge:

                // �����Ϸ��� �������� �̹� ������ �Ǿ� �ִٸ� ���� �ѹ��� �ű��.
                if (item.m_isEquiping)
                {
                    m_CurrentPledge = null;
                }
                // ���Ӱ� �����Ѵٸ�
                else
                {
                    // �̹� �ٸ� �������� ���� �Ǿ����� üũ
                    if (m_CurrentPledge != null)
                    {
                        // �̹� �������� �ִٸ� ���� �����Ϸ��� �����۰� �Ȱ��� ���������� üũ
                        m_CurrentPledge.m_isEquiping = false;
                    }
                }
                break;
            default:
                break;
        }

        item.m_isEquiping = true;
        item.m_EquipingType = type;
        item.m_iEquipSlotNum = num;

        // �������� ���Ӱ� ����
        switch (type)
        {
            case Define.E_EquipmentSlotsPartType.Right_Hand_Weapon:
                ((WeaponItem)item).m_isLeftHandEquiping = false;
                m_RightWeaponsSlots[num] = (WeaponItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Left_Hand_Weapon:
                ((WeaponItem)item).m_isLeftHandEquiping = true;
                m_LeftWeaponsSlots[num] = (WeaponItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Arrow:
                m_ArrowAmmoSlots[num] = (AmmoItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Bolt:
                m_BoltAmmoSlots[num] = (AmmoItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Helmt:
                m_HelmetEquipment = (HelmEquipmentItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Chest_Armor:
                m_TorsoEquipment = (TorsoEquipmentItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Gantlets:
                m_HandEquipment = (GantletsEquipmentItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Leggings:
                m_LegEquipment = (LeggingsEquipmentItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Ring:
                m_RingSlots[num] = (RingItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Consumable:
                m_ConsumableItemSlots[num] = (ToolItem)item;
                break;
            case Define.E_EquipmentSlotsPartType.Pledge:
                m_CurrentPledge = (PledgeItem)item;
                break;
            default:
                break;
        }


        Refresh();
    }
}
