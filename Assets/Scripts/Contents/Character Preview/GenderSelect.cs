using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class GenderSelect : UI_Base
{
    enum Buttons
    {
        MaleSelectBtn,
        FemaleSelectBtn
    }

    CharacterCreationScreen characterCreationScreen;
    PlayerManager player;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));


        return true;
    }

    public void Start()
    {
        characterCreationScreen = GetComponentInParent<CharacterCreationScreen>();

        player = Managers.Object.m_MyPlayer;

        // Button Select
        Button Malebutton = GetButton((int)Buttons.MaleSelectBtn);
        Malebutton.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            characterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);

            characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goGender.SetActive(false);
            characterCreationScreen.m_CharacterCreationLeftPannelUI.m_GenderBtn.Select();

            // ���� ������ ���Ҵ��� �޶����� Ȯ��
            // ���ڷ� ����
            player.playerEquipmentManager.m_bIsFemale = false;

            // ���Ͽ� �ִ� ���� ������ �̸� ����
            ChangeSubItemGender();

            // ��ⷯ �ٽ� ����
            player.playerEquipmentManager.EquipAllEquipmentModel();

            // ����
            Managers.Sound.Play("UI/Popup_ButtonShow");
        });

        Button Femalebutton = GetButton((int)Buttons.FemaleSelectBtn);
        Femalebutton.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
            characterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);

            characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goGender.SetActive(false);
            characterCreationScreen.m_CharacterCreationLeftPannelUI.m_GenderBtn.Select();

            // ���� ������ ���Ҵ��� �޶����� Ȯ��
            player.playerEquipmentManager.m_bIsFemale = true;

            // ���Ͽ� �ִ� ���� ������ �̸� ����
            ChangeSubItemGender();

            // ��ⷯ �ٽ� ����
            player.playerEquipmentManager.EquipAllEquipmentModel();

            // ����
            Managers.Sound.Play("UI/Popup_ButtonShow");

        });

        // Event Trigger
        {
            EventTrigger trigger = Malebutton.gameObject.GetOrAddComponent<EventTrigger>();

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((data) => { SelectMale((PointerEventData)data); });
            trigger.triggers.Add(entry2);
        }
        {
            EventTrigger trigger = Femalebutton.gameObject.GetOrAddComponent<EventTrigger>();

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((data) => { SelectFemale((PointerEventData)data); });
            trigger.triggers.Add(entry2);
        }

    }

    void SelectMale(PointerEventData data)
    {
        player.playerEquipmentManager.m_bIsFemale = false;

        // ���Ͽ� �ִ� ���� ������ �̸� ����
        ChangeSubItemGender();

        // ��ⷯ �ٽ� ����
        player.playerEquipmentManager.EquipAllEquipmentModel();

        // ����
        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
    }

    void SelectFemale(PointerEventData data)
    {
        player.playerEquipmentManager.m_bIsFemale = true;

        // ���Ͽ� �ִ� ���� ������ �̸� ����
        ChangeSubItemGender();

        // ��ⷯ �ٽ� ����
        player.playerEquipmentManager.EquipAllEquipmentModel();

        // ����
        Managers.Sound.Play("UI/Popup_OrderButtonSelect");
    }

    private void ChangeSubItemGender()
    {
        foreach (var item in characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_listExternalFeaturesSubItem)
        {
            item.SetModularName();
        }
    }
}
