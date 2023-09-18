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
        HairItemBtn,

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

        // Name
    public Button m_NameBtn;

        // Class
    public Button m_ClassBtn;

        // Hair
    public Button m_HairBtn;
    public Button m_HairColorBtn;
    public Button m_HairItemBtn;

        // Face
    public Button EyelashesBtn;
    public Button EyebrowsBtn;
    public Button FacialHairBtn;
    public Button FacialMaskBtn;
    public Button FacialMaskColorBtn;
    public Button NoseBtn;

        // Skin
    public Button SkinColorBtn;

        // Game Start Button
    public Button m_StartGameBtn;

    HelmetHider hider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        hider = FindObjectOfType<HelmetHider>();

        BindButton(typeof(Buttons));

        // Name
        m_NameBtn               = GetButton((int)Buttons.NameBtn);

        // Class
        m_ClassBtn              = GetButton((int)Buttons.ClassBtn);

        // Hair
        m_HairBtn               = GetButton((int)Buttons.HairBtn);
        m_HairColorBtn              = GetButton((int)Buttons.HairColorBtn);
        m_HairItemBtn = GetButton((int)Buttons.HairItemBtn);

        // Face
        EyelashesBtn              = GetButton((int)Buttons.EyelashesBtn);
        EyebrowsBtn              = GetButton((int)Buttons.EyebrowsBtn);
        FacialHairBtn            = GetButton((int)Buttons.FacialHairBtn);
        FacialMaskBtn           = GetButton((int)Buttons.FacialMaskBtn);
        FacialMaskColorBtn          = GetButton((int)Buttons.FacialMaskColorBtn);
        NoseBtn                  = GetButton((int)Buttons.NoseBtn);

        // Skin
        SkinColorBtn              = GetButton((int)Buttons.SkinColorBtn);

        // Game Start Button
        m_StartGameBtn          = GetButton((int)Buttons.StartGameBtn);

        return true;
    }


    public void SetInfo()
    {
        NameButtonFunctionAdd();
        ClassButtonFunctionAdd();
        HairButtonFunctionAdd();
        FaceButtonFunctionAdd();
        SkinButtonFunctionAdd();
        GameStartButtonFunctionAdd();
    }

    #region ButtonFuncitonAdd
    private void NameButtonFunctionAdd()
    {

    }

    private void ClassButtonFunctionAdd()
    {
        // Class
        m_ClassBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goClasses.SetActive(true);
            AllPannelButonInteractable(false);
            //m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFirstClass.GetComponent<Button>().Select();
        });
    }

    private void HairButtonFunctionAdd()
    {
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
    }

    private void FaceButtonFunctionAdd()
    {
        EyelashesBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });

        EyebrowsBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });

        FacialHairBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });

        FacialMaskBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });

        FacialMaskColorBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });

        NoseBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });
    }

    private void SkinButtonFunctionAdd()
    {
        SkinColorBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });
    }

    private void GameStartButtonFunctionAdd()
    {
        m_StartGameBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);
        });
    }

    #endregion

    public void AllPannelButonInteractable(bool isTrue)
    {
        // Name
        m_NameBtn.interactable = isTrue;

        // Class
        m_ClassBtn.interactable = isTrue;

        // Hair
        m_HairBtn.interactable = isTrue;
        m_HairColorBtn.interactable = isTrue;

        // Face
        EyelashesBtn.interactable = isTrue;
        EyebrowsBtn.interactable = isTrue;
        FacialHairBtn.interactable = isTrue;
        FacialMaskBtn.interactable = isTrue;
        FacialMaskColorBtn.interactable = isTrue;
        NoseBtn.interactable = isTrue;

        // Skin
        SkinColorBtn.interactable = isTrue;

        // Game Start Button
        m_StartGameBtn.interactable = isTrue;
    }


    public void PostSetInfo()
    {
    }
}
