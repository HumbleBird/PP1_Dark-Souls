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
        // Left Pannel
        NameBtn,
        ClassBtn,
        HairBtn,
        HairColorBtn,
        SkinBtn,
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
        m_SkinBtn = GetButton((int)Buttons.SkinBtn);
        m_HairColorBtn = GetButton((int)Buttons.HairColorBtn);
        m_StartGameBtn = GetButton((int)Buttons.StartGameBtn);

        return true;
    }

    public void SetInfo()
    {
        // Name

        // Class

        // Hair
        m_HairBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
        });

        // HairColor
        m_HairColorBtn.onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairColor.SetActive(true);
            //m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_HairColorBtn.Select();
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
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
