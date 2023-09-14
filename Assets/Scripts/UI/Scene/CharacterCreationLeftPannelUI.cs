using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class CharacterCreationLeftPannelUI : UI_Base
{
    CharacterCreationScreen m_CharacterCreationScreen;

    public enum Buttons
    {
        // Name
        NameBtn,

        // Class
        ClassBtn,

        // Hair
        HairBtn,
        HairColorBtn,
        HairloomBtn,

        // Face
        EyelashesBtn,
        EyebrowsBtn,
        FacialHairBtn,
        FacialMaskBtn,
        FacialMaskColorBtn,
        NoseBtn,

        // Skin
        SkinColorBtn,

        // Game Start Button
        StartGameBtn,
    }

    public Button m_NameBtn;
    public Button m_ClassBtn;
    public Button m_HairBtn;
    public Button m_HairColorBtn;
    public Button m_SkinBtn;
    public Button m_StartGameBtn;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();

        BindButton(typeof(Buttons));

        m_NameBtn = GetButton((int)Buttons.NameBtn);
        m_ClassBtn = GetButton((int)Buttons.ClassBtn);
        m_HairBtn = GetButton((int)Buttons.HairBtn);
        m_HairColorBtn = GetButton((int)Buttons.HairColorBtn);
        m_StartGameBtn = GetButton((int)Buttons.StartGameBtn);

        return true;
    }

    HelmetHider hider;

    public void SetInfo()
    {
         hider = FindObjectOfType<HelmetHider>();
        // Name

        // Class
        m_ClassBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goClasses.SetActive(true);
            AllPannelButonInteractable(false);
            //m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFirstClass.GetComponent<Button>().Select();
        });

        // Hair
        m_HairBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideHelmet();
        });

        // HairColor
        m_HairColorBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairColor.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideHelmet();
        });


        // Skin

    }

    public void AllPannelButonInteractable(bool isTrue)
    {
        m_NameBtn.interactable = isTrue;
        m_ClassBtn.interactable = isTrue;
        m_HairBtn.interactable = isTrue;
        m_SkinBtn.interactable = isTrue;
        m_HairColorBtn.interactable = isTrue;
        m_StartGameBtn.interactable = isTrue;
    }


    public void PostSetInfo()
    {
    }
}
