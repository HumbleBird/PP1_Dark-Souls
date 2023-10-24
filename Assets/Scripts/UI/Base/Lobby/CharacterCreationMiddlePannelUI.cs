using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CharacterCreationMiddlePannelUI : UI_Base
{

    enum GameObjects
    {
        Gender,

        // Classes
        ClassList,
        Classes,

        // Hair Styles
        HairStyles,

        // Hair Color
        HairColor,

        HairItem,

        Eyelashes,
        Eyebrows,
        FacialHair,
        FacialMask,
        FacialMaskColor,
        Nose,
        SkinColor,
    }

    enum Texts
    {

        PlayerLevelText,
        VigorLevelText,
        AttunementLevelText,
        EnduranceLevelText,
        VitalityLevelText,
        StrengthLevelText,
        DexterityLevelText,
        IntelligenceLevelText,
        FaithLevelText,
        LuckLevelText,

        ClassDescrtionText,
    }


    public GameObject m_goGender;
    public GameObject m_goClasses;
    public GameObject m_goHairStyles;
    public GameObject m_goHairColor;
    public GameObject m_goHairItem;
    public GameObject m_goEyelashes;
    public GameObject m_goEyebrows;
    public GameObject m_goFacialHair;
    public GameObject m_goFacialMask;
    public GameObject m_goFacialMaskColor;
    public GameObject m_goNose;
    public GameObject m_goSkinColor;

    HelmetHider hider;
    PlayerManager player;

    public int classChosen;

    CharacterCreationScreen m_CharacterCreationScreen;
    public List<ExternalFeaturesSubItem> m_listExternalFeaturesSubItem = new List<ExternalFeaturesSubItem>();


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        hider = FindObjectOfType<HelmetHider>();

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        m_goClasses = GetObject((int)GameObjects.Classes);
        m_goGender = GetObject((int)GameObjects.Gender);
        m_goHairStyles = GetObject((int)GameObjects.HairStyles);
        m_goHairColor = GetObject((int)GameObjects.HairColor);
        m_goHairItem = GetObject((int)GameObjects.HairItem);
        m_goEyelashes = GetObject((int)GameObjects.Eyelashes);
        m_goEyebrows = GetObject((int)GameObjects.Eyebrows);
        m_goFacialHair = GetObject((int)GameObjects.FacialHair);
        m_goFacialMask = GetObject((int)GameObjects.FacialMask);
        m_goFacialMaskColor = GetObject((int)GameObjects.FacialMaskColor);
        m_goNose = GetObject((int)GameObjects.Nose);
        m_goSkinColor = GetObject((int)GameObjects.SkinColor);

        return true;
    }

    public void SetInfo()
    {
        player = m_CharacterCreationScreen.Player;

        // 숫자는 후에 자동으로 처리할 것. 호출 순서 문제 때문에 걍 박음

        // All Gender
        CreateAllGenderPartsSubItem(GetObject((int)GameObjects.HairStyles), 39, All_GenderItemPartsType.Hair);
        CreateAllGenderPartsSubItem(GetObject((int)GameObjects.HairItem), 14, All_GenderItemPartsType.HelmetAttachment);

        // Gender
        {
            // TODO
            // CreateGenderPartsSubItem(GetObject((int)GameObjects.Eyelashes), 10, EquipmentArmorParts.);
            CreateGenderPartsSubItem(GetObject((int)GameObjects.Eyebrows), 11, E_SingleGenderEquipmentArmorParts.Eyebrow);
            CreateGenderPartsSubItem(GetObject((int)GameObjects.FacialHair), 19, E_SingleGenderEquipmentArmorParts.FacialHair);

            // TODO
            CreateGenderPartsSubItem(GetObject((int)GameObjects.FacialMask), 22, E_SingleGenderEquipmentArmorParts.Head);
            //CreateGenderPartsSubItem(GetObject((int)GameObjects.FacialMaskColor), 19, EquipmentArmorParts.);
        }
         
        // Uniqu
        CreateClassSubItem();
    }

    private void CreateClassSubItem()
    {
        GameObject gridPannel = GetObject((int)GameObjects.ClassList);
        foreach (Transform child in gridPannel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < (int)E_CharacterClass.MaxCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/ClassSubItem", gridPannel.transform);
            ClassSub item = go.GetOrAddComponent<ClassSub>();

            item.count = i;
            item.SetInfo();
        }
    }

    private void CreateAllGenderPartsSubItem(GameObject parent, int count, All_GenderItemPartsType type)
    {
        Transform[] gridPannels = parent.GetComponentsInChildren<Transform>();
        Transform gridPannel = gridPannels[3];
        foreach (Transform child in gridPannel)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/ExternalFeaturesSubItem", gridPannel.transform);
            ExternalFeaturesSubItem item = go.GetOrAddComponent<ExternalFeaturesSubItem>();

            item.count = i;
            item.e_AllGenderPartsType = type;
            item.m_bIsAllGender = true;
            item.SetInfo();
            m_listExternalFeaturesSubItem.Add(item);
        }
    }

    private void CreateGenderPartsSubItem(GameObject parent, int count, E_SingleGenderEquipmentArmorParts type)
    {
        Transform[] gridPannels = parent.GetComponentsInChildren<Transform>();
        Transform gridPannel = gridPannels[3];
        foreach (Transform child in gridPannel)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/ExternalFeaturesSubItem", gridPannel.transform);
            ExternalFeaturesSubItem item = go.GetOrAddComponent<ExternalFeaturesSubItem>();

            item.count = i;
            item.e_genderPartsType = type;
            item.m_bIsAllGender = false;
            item.SetInfo();
            m_listExternalFeaturesSubItem.Add(item);
        }
    }

    public void AllPannelButonInteractable(bool isTrue)
    {
    }

    public void PostSetInfo()
    {
        m_goGender.SetActive(false);
        m_goHairStyles.SetActive(false);
        m_goHairColor.SetActive(false);
        m_goClasses.SetActive(false);
        m_goClasses.SetActive(false);
        m_goHairStyles.SetActive(false);
        m_goHairColor.SetActive(false);
        m_goHairItem.SetActive(false);
        m_goEyelashes.SetActive(false);
        m_goEyebrows.SetActive(false);
        m_goFacialHair.SetActive(false);
        m_goFacialMask.SetActive(false);
        m_goFacialMaskColor.SetActive(false);
        m_goNose.SetActive(false);
        m_goSkinColor.SetActive(false);
    }

    public override void RefreshUI()
    {
        base.RefreshUI();

        GetText((int)Texts.PlayerLevelText).text = string.Format("{0, -1 } {1, 1}", "Player Level\n", player.playerStatsManager.playerLevel.ToString());
        GetText((int)Texts.VigorLevelText       ).text                 = string.Format("{0, -1 } {1, 1}", "VigorLevel\n", player.playerStatsManager.m_iVigorLevel.ToString());
        GetText((int)Texts.AttunementLevelText  ).text                 = string.Format("{0, -1 } {1, 1}", "AttunementLevel\n", player.playerStatsManager.m_iAttunementLevel.ToString());
        GetText((int)Texts.EnduranceLevelText   ).text                 = string.Format("{0, -1 } {1, 1}", "EnduranceLevel\n", player.playerStatsManager.m_iEnduranceLevel.ToString());
        GetText((int)Texts.VitalityLevelText    ).text                 = string.Format("{0, -1 } {1, 1}", "VitalityLevel\n", player.playerStatsManager.m_iVitalityLevel.ToString());
        GetText((int)Texts.StrengthLevelText    ).text                 = string.Format("{0, -1 } {1, 1}", "StrengthLevel\n", player.playerStatsManager.m_iStrengthLevel.ToString());
        GetText((int)Texts.DexterityLevelText   ).text                 = string.Format("{0, -1 } {1, 1}", "DexterityLevel\n", player.playerStatsManager.m_iDexterityLevel.ToString());
        GetText((int)Texts.IntelligenceLevelText).text                 = string.Format("{0, -1 } {1, 1}", "IntelligenceLevel\n", player.playerStatsManager.m_iIntelligenceLevel.ToString());
        GetText((int)Texts.FaithLevelText       ).text                 = string.Format("{0, -1 } {1, 1}", "FaithLevel\n", player.playerStatsManager.m_iFaithLevel.ToString());
        GetText((int)Texts.LuckLevelText).text                         = string.Format("{0, -1 } {1, 1}", "LuckLevel\n", player.playerStatsManager.m_iLuckLevel.ToString());

        GetText((int)Texts.ClassDescrtionText).text = FindObjectOfType< ClassSelector>().classStats[classChosen].m_sClassDecription;
    }
}