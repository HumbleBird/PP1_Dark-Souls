using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class CharacterCreationScreen : UI_Scene
{
    public enum Buttons
    {
        // Left Pannel
        NameBtn,
        ClassBtn    ,
        HairBtn     ,
        SkinBtn     ,

        // Midel Pannel

        // Right Pannel
    }

    enum GameObjects
    {
        // Left Pannel

        // Midel Pannel
        ContentPannel,
        HairStyles,

        // Right Pannel
    }

    public Button m_NameBtn ;
    public Button m_ClassBtn;
    public Button m_HairBtn ;
    public Button m_SkinBtn ;
    
    public GameObject HairStyles;
    public PlayerManager Player;

    HairStylesSub firstHairStylesSub;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        m_NameBtn = GetButton((int)Buttons.NameBtn);
        m_ClassBtn = GetButton((int)Buttons.ClassBtn);
        m_HairBtn  = GetButton((int)Buttons.HairBtn);
        m_SkinBtn = GetButton((int)Buttons.SkinBtn);

        HairStyles = GetObject((int)GameObjects.HairStyles);

        return true;
    }

    private void Start()
    {
        Player = Managers.Object.m_MyPlayer;
        MiddlePannelAwake();

        LeftPannelSetInfo();
        MiddlePannelSetInfo();

        StartInit();
    }

    private void LeftPannelSetInfo()
    {
        // Name

        // Class

        // Hair
        m_HairBtn.onClick.AddListener(() => 
        {
            HairStyles.SetActive(true);
            //firstHairStylesSub.GetComponent<Button>().Select();

            m_NameBtn .interactable = false;
            m_ClassBtn.interactable = false;
            m_HairBtn .interactable = false;
            m_SkinBtn.interactable = false;
        });

        // Skin

    }

    private void MiddlePannelAwake()
    {
        // Hair
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
                firstHairStylesSub = item;
        }
    }

    private void MiddlePannelSetInfo()
    {

    }

    private void RightPannelSetInfo()
    {

    }

    private void StartInit()
    {
        HairStyles.SetActive(false);
    }
}
