using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class ExternalFeaturesSubItem : UI_Base
{
    public All_GenderItemPartsType e_AllGenderPartsType;
    public EquipmentArmorParts e_genderPartsType;
    
    public int count;
    public bool m_bIsAllGender = false;
    public string m_sModulerName = "Chr_";

    CharacterCreationScreen characterCreationScreen;
    PlayerManager player;
    HelmetHider hider;

    private void SetModularName()
    {
        if(m_bIsAllGender)
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
        GameObject toEquipmFeature;

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
            characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.GetComponent<DisableAllChildrenOfSelectedGameObject>().DisableAllChildren();
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
                toEquipmFeature = null;

            TypeUniquFunction(toEquipmFeature);
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
        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.GetComponent<DisableAllChildrenOfSelectedGameObject>().DisableAllChildren();

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

                        hider.UnHiderHelmet();
                    }
                    break;
                case All_GenderItemPartsType.Head_Attachment_Helmet:
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

        }
        
    }
}
