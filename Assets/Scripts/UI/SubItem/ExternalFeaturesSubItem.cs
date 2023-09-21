using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class ExternalFeaturesSubItem : UI_Base
{
    public All_GenderItemPartsType e_AllGenderPartsType;
    public E_SingleGenderEquipmentArmorParts e_genderPartsType;
    
    public int count;
    public bool m_bIsAllGender = false;
    public string m_sModulerName = "Chr_";

    CharacterCreationScreen characterCreationScreen;
    PlayerManager player;
    HelmetHider hider;


    public void SetModularName()
    {
        m_sModulerName = "Chr_";

        if (m_bIsAllGender)
        {
            m_sModulerName = m_sModulerName + e_AllGenderPartsType.ToString() + "_"+ count.ToString("00");
        }
        else
        {
            if(player.playerEquipmentManager.m_bIsFemale)
            {
                m_sModulerName = m_sModulerName + e_genderPartsType.ToString() + "_Female_" + count.ToString("00");
            }
            else
            {
                m_sModulerName = m_sModulerName + e_genderPartsType.ToString() + "_Male_" + count.ToString("00");
            }

        }
    }

    public void SetInfo()
    {
        GameObject toEquipmFeature = null;

        characterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        player = characterCreationScreen.Player;
        hider = FindObjectOfType<HelmetHider>();

        SetModularName();

        // Text
        TextMeshProUGUI textCount = GetComponentInChildren<TextMeshProUGUI>();
        textCount.text = count.ToString();

        // Buton
        Button childBtn = GetComponent<Button>();

        // Select Color
        childBtn.targetGraphic = GetComponent<Image>();

        // Button Function
        childBtn.onClick.AddListener(() =>
        {
            //characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.GetComponent<DisableAllChildrenOfSelectedGameObject>().DisableAllChildren();
            DisableAllChildrenOfSelectedGameObject();
            characterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);

            // 두 번째 선택지부터
            if (count > 0)
            {
                if (m_bIsAllGender)
                {
                    toEquipmFeature = player.playerEquipmentManager.m_AllGenderPartsModelChanger[e_AllGenderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                }
                else
                {
                    if (player.playerEquipmentManager.m_bIsFemale)
                    {
                        toEquipmFeature = player.playerEquipmentManager.m_FemaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                    }
                    else
                    {
                        toEquipmFeature = player.playerEquipmentManager.m_MaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                    }
                }
            }
            else
            {
                // 얼굴 외형에 대해서만 예외 적으로 처리
                if (m_bIsAllGender == false)
                {
                    if (e_genderPartsType == E_SingleGenderEquipmentArmorParts.Head)
                    {
                        if (m_bIsAllGender)
                        {
                            toEquipmFeature = player.playerEquipmentManager.m_AllGenderPartsModelChanger[e_AllGenderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                        }
                        else
                        {
                            if (player.playerEquipmentManager.m_bIsFemale)
                            {
                                toEquipmFeature = player.playerEquipmentManager.m_FemaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                            }
                            else
                            {
                                toEquipmFeature = player.playerEquipmentManager.m_MaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                            }
                        }
                    }
                }
            }


            TypeUniquFunction(toEquipmFeature);

            ExceptionHandling();
        });

        // Event Trigger
        EventTrigger trigger = gameObject.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener((data) => { OnSelectAndPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { OnSelectAndPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void OnSelectAndPointerDown(PointerEventData data)
    {
        DisableAllChildrenOfSelectedGameObject();

        if (count > 0)
        {
            if(m_bIsAllGender)
            {
                player.playerEquipmentManager.m_AllGenderPartsModelChanger[e_AllGenderPartsType].EquipEquipmentsModelByName(m_sModulerName);
            }
            else
            {
                if(player.playerEquipmentManager.m_bIsFemale)
                {
                    player.playerEquipmentManager.m_FemaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                }
                else
                {
                    player.playerEquipmentManager.m_MaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                }
            }
        }

         ExceptionHandling();
    }

    private void TypeUniquFunction(GameObject go)
    {
        if(m_bIsAllGender)
        {
            switch (e_AllGenderPartsType)
            {
                case All_GenderItemPartsType.HeadCoverings_Base_Hair:
                    break;
                case All_GenderItemPartsType.HeadCoverings_No_FacialHair:
                    break;
                case All_GenderItemPartsType.HeadCoverings_No_Hair:

                    break;
                case All_GenderItemPartsType.Hair:
                    {
                        player.playerInventoryManager.currentHairStyle = go;

                        characterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairBtn.Select();
                        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.SetActive(false);
                        
                        Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                        hider.UnHideEquipment(E_ArmorEquipmentType.Helmet);

                    }
                    break;
                case All_GenderItemPartsType.HelmetAttachment:
                    {
                        player.playerInventoryManager.currentHairItem = go;

                        characterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairItemBtn.Select();
                        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairItem.SetActive(false);

                        Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                        hider.UnHideEquipment(E_ArmorEquipmentType.Helmet);

                    }
                    break;
                case All_GenderItemPartsType.Chest_Attachment:
                    break;
                case All_GenderItemPartsType.Back_Attachment:
                    break;
                case All_GenderItemPartsType.Shoulder_Attachment_Right:
                    break;
                case All_GenderItemPartsType.Shoulder_Attachment_Left:
                    break;
                case All_GenderItemPartsType.Elbow_Attachment_Right:
                    break;
                case All_GenderItemPartsType.Elbow_Attachment_Left:
                    break;
                case All_GenderItemPartsType.Hips_Attachment:
                    break;
                case All_GenderItemPartsType.Knee_Attachement_Right:
                    break;
                case All_GenderItemPartsType.Knee_Attachement_Left:
                    break;
                case All_GenderItemPartsType.Extra_Elf_Ear:
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (e_genderPartsType)
            {
                case E_SingleGenderEquipmentArmorParts.Head:
                    {
                        player.playerInventoryManager.currentFacialMask = go;

                        if(go != null)
                        {
                            string result = Regex.Replace(go.name, @"[^0-9]", "");

                            player.playerEquipmentManager.Naked_HelmetEquipment.m_HelmEquipmentItemName = result;
                        }

                        characterCreationScreen.m_CharacterCreationLeftPannelUI.FacialMaskBtn.Select();
                        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFacialMask.SetActive(false);

                        Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                        hider.UnHideEquipment(E_ArmorEquipmentType.Helmet);

                    }
                    break;
                case E_SingleGenderEquipmentArmorParts.Head_No_Elements:
                    break;
                case E_SingleGenderEquipmentArmorParts.Eyebrow:
                    {
                        player.playerInventoryManager.currentEyebrows = go;

                        characterCreationScreen.m_CharacterCreationLeftPannelUI.EyebrowsBtn.Select();
                        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goEyebrows.SetActive(false);

                        Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                        hider.UnHideEquipment(E_ArmorEquipmentType.Helmet);

                    }
                    break;
                case E_SingleGenderEquipmentArmorParts.FacialHair:
                    {
                        player.playerInventoryManager.currentFacialHair = go;

                        characterCreationScreen.m_CharacterCreationLeftPannelUI.FacialHairBtn.Select();
                        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFacialHair.SetActive(false);

                        Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                        hider.UnHideEquipment(E_ArmorEquipmentType.Helmet);

                    }
                    break;
                case E_SingleGenderEquipmentArmorParts.Torso:
                    break;
                case E_SingleGenderEquipmentArmorParts.Arm_Upper_Right:
                    break;
                case E_SingleGenderEquipmentArmorParts.Arm_Upper_Left:
                    break;
                case E_SingleGenderEquipmentArmorParts.Arm_Lower_Right:
                    break;
                case E_SingleGenderEquipmentArmorParts.Arm_Lower_Left:
                    break;
                case E_SingleGenderEquipmentArmorParts.Hand_Right:
                    break;
                case E_SingleGenderEquipmentArmorParts.Hand_Left:
                    break;
                case E_SingleGenderEquipmentArmorParts.Hip:
                    break;
                case E_SingleGenderEquipmentArmorParts.LeftLegging:
                    break;
                case E_SingleGenderEquipmentArmorParts.RightLegging:
                    break;
                default:
                    break;
            }
        }
        
    }

    private void DisableAllChildrenOfSelectedGameObject()
    {
        if (m_bIsAllGender)
        {
            player.playerEquipmentManager.m_AllGenderPartsModelChanger[e_AllGenderPartsType].UnEquipAllEquipmentsModels();
        }
        else
        {
            if (player.playerEquipmentManager.m_bIsFemale)
            {
                player.playerEquipmentManager.m_FemaleGenderPartsModelChanger[e_genderPartsType].UnEquipAllEquipmentsModels();
            }
            else
            {
                player.playerEquipmentManager.m_MaleGenderPartsModelChanger[e_genderPartsType].UnEquipAllEquipmentsModels();
            }
        }
    }

    private void ExceptionHandling()
    {
        // 얼굴 외형에 대해서만 예외 적으로 처리
        if (m_bIsAllGender == false)
        {
            if (e_genderPartsType == E_SingleGenderEquipmentArmorParts.Head)
            {
                if (m_bIsAllGender)
                {
                    player.playerEquipmentManager.m_AllGenderPartsModelChanger[e_AllGenderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                }
                else
                {
                    if (player.playerEquipmentManager.m_bIsFemale)
                    {
                        player.playerEquipmentManager.m_FemaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                    }
                    else
                    {
                         player.playerEquipmentManager.m_MaleGenderPartsModelChanger[e_genderPartsType].EquipEquipmentsModelByName(m_sModulerName);
                    }
                }
            }
        }
    }
}
