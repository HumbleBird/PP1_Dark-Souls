using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CharacterCreationMiddlePannelUI : UI_Base
{
    CharacterCreationScreen m_CharacterCreationScreen;

    enum GameObjects
    {
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

    enum Buttons
    {
        HairColorBtn,
        ConfirmHairColorBtn
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

    public GameObject m_goFirstHairStyle;
    public GameObject m_goFirstClass;

    HelmetHider hider;
    PlayerManager player;

    public int classChosen;

    // 1개의 색에는 r, g, b 값에 알파 값이 1 으로 적용됨
    // 리스트에 색들을 보관해 뒀다가 생성.
    // a(155, 132, 513), b(194, 94, 09) ... n개까지
    // 리스트에 담겨 있음

    int[,] m_Colors = 
    {
        { 255, 216, 216 }, // 아주 밝은 빨강
        { 250, 224, 212 }, // 아주 밝은 주황
    };

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        hider = FindObjectOfType<HelmetHider>();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        m_goClasses = GetObject((int)GameObjects.Classes);
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

    private void CreateHairColorSetInfo()
    {
        CreateColor();

        // Buton
        ColorButton[] colors = m_goHairColor.GetComponentsInChildren<ColorButton>();
        foreach (ColorButton color in colors)
        {
            color.GetComponent<Button>().onClick.AddListener(() =>
            {
                m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
                m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
                m_goHairColor.SetActive(false);

                Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
                hider.UnHiderHelmet();
            });
        }

        GetButton((int)Buttons.ConfirmHairColorBtn).onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            hider.UnHiderHelmet();
        });

        // Slider
        Slider[] sliers = m_goHairColor.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliers)
        {
            slider.onValueChanged.AddListener(delegate { m_goHairColor.GetComponent<SelectExternalFeaturesColor>().UpdateSliders(); });
        }
    }

    private void CreateColor()
    {


    }

    public void SetInfo()
    {
        player = m_CharacterCreationScreen.Player;

        CreateAllGenderPartsSubItem(GetObject((int)GameObjects.HairStyles), 39, All_GenderItemPartsType.Hair);

        CreateClassSubItem();

        CreateHairColorSetInfo();
    }

    private void CreateClassSubItem()
    {
        GameObject gridPannel = GetObject((int)GameObjects.ClassList);
        foreach (Transform child in gridPannel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 3; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/ClassSubItem", gridPannel.transform);
            ClassSub item = go.GetOrAddComponent<ClassSub>();

            item.count = i;
            item.SetInfo();

            if (i == 0)
                m_goFirstClass = go;
        }
    }

    private void CreateAllGenderPartsSubItem(GameObject parent, int count, All_GenderItemPartsType type)
    {
        Transform[] gridPannels = parent.GetComponentsInChildren<Transform>();
        Transform gridPannel = gridPannels[2];
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
        }
    }

    private void CreateGenderPartsSubItem(GameObject parent, int count, EquipmentArmorParts type)
    {
        GameObject gridPannel = parent;
        foreach (Transform child in gridPannel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/ExternalFeaturesSubItem", gridPannel.transform);
            ExternalFeaturesSubItem item = go.GetOrAddComponent<ExternalFeaturesSubItem>();

            item.count = i;
            item.e_genderPartsType = type;
            item.m_bIsAllGender = false;
            item.SetInfo();
        }
    }

    public void AllPannelButonInteractable(bool isTrue)
    {
    }

    public void PostSetInfo()
    {
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