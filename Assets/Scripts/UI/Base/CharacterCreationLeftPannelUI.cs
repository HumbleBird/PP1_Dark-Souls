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

        GenderBtn,

        // Class
        ClassBtn,

        // Hair
        HairBtn,
        HairColorBtn,
        HairItemBtn,

        // Face
        //EyelashesBtn,
        EyebrowsBtn,
        FacialHairBtn,
        FacialMaskBtn,
        FacialMaskColorBtn,
        //NoseBtn,

        // Skin
        SkinColorBtn,

        // Game Start Button
        StartGameBtn,
    }

        // Name
    public Button m_NameBtn;

    public Button m_GenderBtn;

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

        m_GenderBtn             = GetButton((int)Buttons.GenderBtn);

        // Class
        m_ClassBtn              = GetButton((int)Buttons.ClassBtn);

        // Hair
        m_HairBtn               = GetButton((int)Buttons.HairBtn);
        m_HairColorBtn              = GetButton((int)Buttons.HairColorBtn);
        m_HairItemBtn = GetButton((int)Buttons.HairItemBtn);

        // Face
        //EyelashesBtn              = GetButton((int)Buttons.EyelashesBtn);
        EyebrowsBtn              = GetButton((int)Buttons.EyebrowsBtn);
        FacialHairBtn            = GetButton((int)Buttons.FacialHairBtn);
        FacialMaskBtn           = GetButton((int)Buttons.FacialMaskBtn);
        FacialMaskColorBtn          = GetButton((int)Buttons.FacialMaskColorBtn);
        //NoseBtn                  = GetButton((int)Buttons.NoseBtn);

        // Skin
        SkinColorBtn              = GetButton((int)Buttons.SkinColorBtn);

        // Game Start Button
        m_StartGameBtn          = GetButton((int)Buttons.StartGameBtn);

        PointerDown(m_NameBtn.gameObject);
        PointerDown(m_GenderBtn.gameObject);
        PointerDown(m_ClassBtn.gameObject);
        PointerDown(m_HairBtn     .gameObject);
        PointerDown(m_HairColorBtn.gameObject);
        PointerDown(m_HairItemBtn.gameObject);
        PointerDown(EyebrowsBtn       .gameObject);
        PointerDown(FacialHairBtn     .gameObject);
        PointerDown(FacialMaskBtn     .gameObject);
        PointerDown(FacialMaskColorBtn.gameObject);
        PointerDown(SkinColorBtn.gameObject);
        PointerDown(m_StartGameBtn.gameObject);

        return true;
    }


    public void SetInfo()
    {
        NameButtonFunctionAdd();
        GenderButtonFunctionAdd();
        ClassButtonFunctionAdd();
        HairButtonFunctionAdd();
        FaceButtonFunctionAdd();
        SkinButtonFunctionAdd();
        GameStartButtonFunctionAdd();
    }

    #region ButtonFuncitonAdd
    private void NameButtonFunctionAdd()
    {
        // Name
        m_NameBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");
        });
    }

    private void GenderButtonFunctionAdd()
    {
        // Class
        m_GenderBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goGender.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.All);
            AllPannelButonInteractable(false);
        });
    }

    private void ClassButtonFunctionAdd()
    {
        // Class
        m_ClassBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goClasses.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.All);
            AllPannelButonInteractable(false);
        });
    }

    private void HairButtonFunctionAdd()
    {
        // Hair
        m_HairBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);
        });

        // HairColor
        m_HairColorBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairColor.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);

        });

        m_HairItemBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairItem.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);

        });


    }

    private void FaceButtonFunctionAdd()
    {
        //EyelashesBtn.onClick.AddListener(() =>
        //{
        //    Managers.UI.ShowPopupUI<AnnouncementUI>();
        //    // 업데이트 팝업 뛰우기


        //    //AllPannelButonInteractable(false);
        //    // TODO
        //});

        EyebrowsBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goEyebrows.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);

        });

        FacialHairBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFacialHair.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);

        });

        FacialMaskBtn.onClick.AddListener(() =>
        {
                        Managers.Sound.Play("UI/Popup_ButtonShow");
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFacialMask.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.Head);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.Head);

            //AllPannelButonInteractable(false);
            // TODO
        });

        FacialMaskColorBtn.onClick.AddListener(() =>
        {
                        Managers.Sound.Play("UI/Popup_ButtonShow");
            Managers.UI.ShowPopupUI<AnnouncementUI>();
            //AllPannelButonInteractable(false);
            // TODO
        });

        //NoseBtn.onClick.AddListener(() =>
        //{
        //    Managers.UI.ShowPopupUI<AnnouncementUI>();
        //    //AllPannelButonInteractable(false);
        //    // TODO 
        //});
    }

    private void SkinButtonFunctionAdd()
    {
        SkinColorBtn.onClick.AddListener(() =>
        {
            AllPannelButonInteractable(false);

                        Managers.Sound.Play("UI/Popup_ButtonShow");
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goSkinColor.SetActive(true);
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.All);
            AllPannelButonInteractable(false);
            hider.HideEquipment(E_CameraShowPartType.All);

        });
    }

    private void GameStartButtonFunctionAdd()
    {
        m_StartGameBtn.onClick.AddListener(() =>
        {
            // TODO 정말 완료했는지 팝업 창 뛰우기
            // 1. 인트로 신으로 넘어가기
                        Managers.Sound.Play("UI/Popup_ButtonShow");
            m_CharacterCreationScreen.m_ConfirmationUI.gameObject.SetActive(true);
        });
    }

    #endregion

    public void AllPannelButonInteractable(bool isTrue)
    {
        m_GenderBtn.interactable = isTrue;

        // Name
        m_NameBtn.interactable = isTrue;

        // Class
        m_ClassBtn.interactable = isTrue;

        // Hair
        m_HairBtn.interactable = isTrue;
        m_HairColorBtn.interactable = isTrue;

        // Face
        //EyelashesBtn.interactable = isTrue;
        EyebrowsBtn.interactable = isTrue;
        FacialHairBtn.interactable = isTrue;
        FacialMaskBtn.interactable = isTrue;
        FacialMaskColorBtn.interactable = isTrue;
        //NoseBtn.interactable = isTrue;

        // Skin
        SkinColorBtn.interactable = isTrue;

        // Game Start Button
        m_StartGameBtn.interactable = isTrue;
    }


    public void PostSetInfo()
    {
    }

    void PointerDown(GameObject go)
    {
        EventTrigger trigger = go.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { PointerDownSoundPlay((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void PointerDownSoundPlay(PointerEventData data)
    {
        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
    }
}
