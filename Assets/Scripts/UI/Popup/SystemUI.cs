using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemUI : UI_Popup
{
    enum Texts
    {
        NameText,
        LevelValueText
    }

    public Image m_UnSelectedImages;
    public Image m_SelectedImages;

    public TextMeshProUGUI m_QuitGameText;
    public Image m_SelectQuitGameImage;

    public GameObject m_panel;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        PlayerManager player = Managers.Object.m_MyPlayer;
        GetText((int)Texts.NameText).text = player.playerStatsManager.m_sCharacterName;
        GetText((int)Texts.LevelValueText).text = player.playerStatsManager.playerLevel.ToString();

        m_UnSelectedImages.gameObject.UIEventTrigger(EventTriggerType.PointerEnter, () => {  
            Managers.Sound.Play("UI/Popup_ButtonClose");
            m_SelectedImages.gameObject.SetActive(true); m_panel.SetActive(true); });

        m_QuitGameText.gameObject.UIEventTrigger(EventTriggerType.PointerEnter, () => { 
            Managers.Sound.Play("UI/Popup_OrderButtonSelect");
            m_SelectQuitGameImage.enabled = true; });

        m_SelectQuitGameImage.gameObject.UIEventTrigger(EventTriggerType.PointerExit, () => { m_SelectQuitGameImage.enabled = false; });
        
        m_SelectQuitGameImage.gameObject.BindEvent(() =>
        {
            // Sound
            Managers.Sound.Play("UI/Popup_ButtonClose");

            // 게임 데이터 저장

            // 게임 저장 여부 확인
            Managers.Game.m_isNewGame = false;

            // 메인 메뉴로 돌아가기
            Managers.Scene.LoadScene(Define.Scene.Start);
        }); 
        

        return true;
    }
}
