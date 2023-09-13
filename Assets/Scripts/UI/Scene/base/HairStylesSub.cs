using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class HairStylesSub : UI_Base
{
    public int count;
    CharacterCreationScreen characterCreationScreen;
    PlayerManager player;

    public void SetInfo()
    {
        characterCreationScreen = GetComponentInParent<CharacterCreationScreen>();
        player = characterCreationScreen.Player;

        // Text
        TextMeshProUGUI textCount = GetComponentInChildren<TextMeshProUGUI>();
        textCount.text = count.ToString();

        // Buton
        Button childBtn = GetComponent<Button>();

        // Select Color
        childBtn.targetGraphic = GetComponent<Image>();

        // Button Function
        childBtn.onClick.AddListener(() =>
        {
            characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.GetComponent<DisableAllChildrenOfSelectedGameObject>().DisableAllChildren();
            characterCreationScreen.m_CharacterCreationLeftPannelUI.AllPannelButonInteractable(true);

            // 두 번째 선택지부터
            if (count > 0)
            {
                player.playerEquipmentManager.m_AllGenderPartsModelChanger[Define.All_GenderItemPartsType.Hair].EquipEquipmentsModelByName("Chr_Hair_" + count.ToString("00"));
            }

            characterCreationScreen.m_CharacterCreationLeftPannelUI.m_HairBtn.Select();

            characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.SetActive(false);
        });

        // Event Trigger
        EventTrigger trigger = gameObject.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener((data) => { OnSelectAndPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { OnSelectAndPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void OnSelectAndPointerDown(PointerEventData data)
    {
        characterCreationScreen.m_CharacterCreationMiddlePannelUI.m_goHairStyles.GetComponent<DisableAllChildrenOfSelectedGameObject>().DisableAllChildren();

        if (count > 0)
        {
            player.playerEquipmentManager.m_AllGenderPartsModelChanger[All_GenderItemPartsType.Hair].EquipEquipmentsModelByName("Chr_Hair_" + count.ToString("00"));
        }
    }
}
