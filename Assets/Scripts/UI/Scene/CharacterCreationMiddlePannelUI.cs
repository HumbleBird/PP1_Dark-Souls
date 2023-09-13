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
        ContentPannel,
        HairColor,
        HairStyles,

        // Color
        RedColor,
        BlondeColor,
        BrownColor
    }

    enum Buttons
    {
        HairColorBtn,
        ConfirmHairColorBtn
    }


    public GameObject m_goHairStyles;
    public GameObject m_goHairColor;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        m_goHairStyles = GetObject((int)GameObjects.HairStyles);
        m_goHairColor = GetObject((int)GameObjects.HairColor);

        GetButton((int)Buttons.ConfirmHairColorBtn).onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
        });

        GetObject((int)GameObjects.RedColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
        });

        GetObject((int)GameObjects.BlondeColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
        });

        GetObject((int)GameObjects.BrownColor).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            m_goHairColor.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

        });

        return true;
    }

    public void SetInfo()
    {
        // Hair Ã¢ »ý¼º
        GameObject gridPannel = GetObject((int)GameObjects.ContentPannel);
        foreach (Transform child in gridPannel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 39; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/HairStyleSubItem", gridPannel.transform);
            HairStylesSub item = go.GetOrAddComponent<HairStylesSub>();

            item.count = i;
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
    }
}