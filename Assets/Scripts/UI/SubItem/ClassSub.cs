using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class ClassSub : MonoBehaviour
{
    public int count;
    CharacterCreationScreen m_CharacterCreationScreen;
    PlayerManager player;

    ClassSelector classSelector;

    public void SetInfo()
    {
        classSelector = FindObjectOfType<ClassSelector>();

        m_CharacterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        player = m_CharacterCreationScreen.Player;

        // Text
        TextMeshProUGUI classText = GetComponentInChildren<TextMeshProUGUI>();
        classText.text = ((E_CharacterClass)count).ToString();

        // Buton
        Button childBtn = GetComponent<Button>();

        // Button Function
        childBtn.onClick.AddListener(() =>
        {
            Managers.Sound.Play("UI/Popup_ButtonShow");

            classSelector.AssignClass(count);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);
            m_CharacterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goClasses.SetActive(false);
            m_CharacterCreationScreen.m_CharacterCreationLeftPannelUI.m_ClassBtn.Select();
            Camera.main.GetComponent<CharacterPreviewCamera>().ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
        });

        gameObject.UIEventTrigger(EventTriggerType.PointerEnter, () => {
            Managers.Sound.Play("UI/Popup_OrderButtonSelect");
            classSelector.AssignClass(count);
        });
    }
}
