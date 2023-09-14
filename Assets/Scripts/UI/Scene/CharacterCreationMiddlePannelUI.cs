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
        // Hair Styles
        ContentPannel,
        HairStyles,

        // Hair Color
        HairColor,
        RedColor,
        BlondeColor,
        BrownColor,

        // Classes
        ClassList,
        Classes,
    }

    enum Buttons
    {
        HairColorBtn,
        ConfirmHairColorBtn
    }

    enum Texts
    {
        ClassDescrtionText,

        PlayerLevelText,
        VigorLevelText,
        AttunementLevelText,
        EnduranceLevelText,
        VitalityLevelText,
        StrengthLevelText,
        DexterityLevelText,
        IntelligenceLevelText,
        FaithLevelText,
        LuckLevelText
    }


    public GameObject m_goHairStyles;
    public GameObject m_goHairColor;
    public GameObject m_goClasses;

    public GameObject m_goFirstHairStyle;
    public GameObject m_goFirstClass;

    HelmetHider hider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        hider = FindObjectOfType<HelmetHider>();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        m_goHairStyles = GetObject((int)GameObjects.HairStyles);
        m_goHairColor = GetObject((int)GameObjects.HairColor);
        m_goClasses = GetObject((int)GameObjects.Classes);

        GetButton((int)Buttons.ConfirmHairColorBtn).onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            hider.UnHiderHelmet();
        });

        GetObject((int)GameObjects.RedColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            hider.UnHiderHelmet();
        });

        GetObject((int)GameObjects.BlondeColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            hider.UnHiderHelmet();
        });

        GetObject((int)GameObjects.BrownColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            hider.UnHiderHelmet();
        });

        return true;
    }

    public void SetInfo()
    {
        {
            // Hair Sytles Sub 芒 积己
            GameObject gridPannel = GetObject((int)GameObjects.ContentPannel);
            foreach (Transform child in gridPannel.transform)
                Managers.Resource.Destroy(child.gameObject);

            for (int i = 0; i < 39; i++)
            {
                GameObject go = Managers.Resource.Instantiate("UI/SubItem/HairStyleSubItem", gridPannel.transform);
                HairStylesSub item = go.GetOrAddComponent<HairStylesSub>();

                item.count = i;
                item.SetInfo();

                if (i == 0)
                    m_goFirstHairStyle = go;
            }
        }

        {
            // Class Btn Sub 芒 积己
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

    }

    public void AllPannelButonInteractable(bool isTrue)
    {
    }

    public void PostSetInfo()
    {
        m_goHairStyles.SetActive(false);
        m_goHairColor.SetActive(false);
        m_goClasses.SetActive(false);
    }
}