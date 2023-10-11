using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ExternalFeaturesColorSubItem : UI_Base
{
    enum Buttons
    {
        ConfirmHairColorBtn
    }

    enum Sliders
    {
        RedSlider,
        GreenSlider,
        BlueSlider
    }

    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    private Color currentHairColor;
    PlayerManager player;

    public ExternalFeaturesColorParts m_eExternalFeaturesColorParts;

    // skin mesh renderer로부터의 material을 잡는다. 메테리얼 속성값을 변경한다.
    public List<SkinnedMeshRenderer> m_ColorRendererList = new List<SkinnedMeshRenderer>();

    public string m_sPartsName;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // 바인드
        BindSlider(typeof(Sliders));
        BindButton(typeof(Buttons));

        return true;
    }

    private void Start()
    {

        ColorButtonAddFunction();

        // Set Color Parts Potion
        SetNameColorPartion();
    }

    private void SetNameColorPartion()
    {
        switch (m_eExternalFeaturesColorParts)
        {
            case ExternalFeaturesColorParts.Hair:
                m_sPartsName = "_Color_Hair";
                break;
            case ExternalFeaturesColorParts.FacialMask:
                m_sPartsName = null;
                break;
            case ExternalFeaturesColorParts.Skin:
                m_sPartsName = "_Color_Skin";
                break;
            default:
                m_sPartsName = null;
                break;
        }
    }

    private void ColorButtonAddFunction()
    {
        CharacterCreationScreen m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        GameObject go = null;
        HelmetHider hider = FindObjectOfType<HelmetHider>();

        switch (m_eExternalFeaturesColorParts)
        {
            case ExternalFeaturesColorParts.Hair:
                go = m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairColor;
                break;
            case ExternalFeaturesColorParts.FacialMask:
                go = m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goFacialMaskColor;
                break;
            case ExternalFeaturesColorParts.Skin:
                go = m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goSkinColor;
                break;
            default:
                break;
        }


        // 컬러 버튼에 기능 추가
        ColorButton[] colors = GetComponentsInChildren<ColorButton>();
        foreach (ColorButton color in colors)
        {
            color.GetComponent<Button>().onClick.AddListener(() =>
            {
                m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
                m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
                go.SetActive(false);

                Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

                switch (m_eExternalFeaturesColorParts)
                {
                    case ExternalFeaturesColorParts.Hair:
                        hider.UnHideEquipment(E_CameraShowPartType.Head);
                        break;
                    case ExternalFeaturesColorParts.FacialMask:
                        hider.UnHideEquipment(E_CameraShowPartType.Head);
                        break;
                    case ExternalFeaturesColorParts.Skin:
                        hider.UnHideEquipment(E_CameraShowPartType.All);
                        break;
                    default:
                        break;
                }
            });
        }

        // 컬러 확인 버튼에 기능 추가
        GetButton((int)Buttons.ConfirmHairColorBtn).onClick.AddListener(() =>
        {
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairColorBtn.Select();
            go.SetActive(false);

            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);

            switch (m_eExternalFeaturesColorParts)
            {
                case ExternalFeaturesColorParts.Hair:
                    hider.UnHideEquipment(E_CameraShowPartType.Head);
                    break;
                case ExternalFeaturesColorParts.FacialMask:
                    hider.UnHideEquipment(E_CameraShowPartType.Head);
                    break;
                case ExternalFeaturesColorParts.Skin:
                    hider.UnHideEquipment(E_CameraShowPartType.All);
                    break;
                default:
                    break;
            }
            

        });

        // 슬라이더 값을 변경시 호출되는 함수 추가
        Slider[] sliers = go.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliers)
        {
            slider.onValueChanged.AddListener(delegate { UpdateSliders(); });
        }
    }

    public void UpdateSliders()
    {
        redAmount = GetSlider((int)Sliders.RedSlider).value;
        greenAmount = GetSlider((int)Sliders.GreenSlider).value;
        blueAmount = GetSlider((int)Sliders.BlueSlider).value;
        SetPartsColor();
    }

    public void SetPartsColor()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount);


        for (int i = 0; i < m_ColorRendererList.Count; i++)
        {
            m_ColorRendererList[i].material.SetColor(m_sPartsName, currentHairColor);
        }
    }
}

