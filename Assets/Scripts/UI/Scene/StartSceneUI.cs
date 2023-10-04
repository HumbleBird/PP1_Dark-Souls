using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 아 애니메이터로 처리하면 되는 걸ㅋㅋㅋ 노가다 씹 

public class StartSceneUI : UI_Scene
{
    public FadeInOutScreenUI m_FadeInOutScreenUI;

    enum Texts
    {
        PressAnyButtonText  ,
        ContinueText        ,
        LoadGameText        ,
        NewGameText         ,
        QuitText            ,
    }

	enum GameObjects
    {
		Intro,
		Start,
		MainMenu
	}


    bool m_bIsButtonPress = false;

    Action m_FadeOutAction = null;

    GameObject m_Intro; // 게임 회사 타이틀을 보여줌
    GameObject m_Start; // 아무거나 키를 클릭하면 메인 메뉴로
    GameObject m_MainMenu; // 새 게임 시작, 로그 게임, 나가기 등

    Animator m_Animator;

    public override bool Init()
	{
		if (base.Init() == false)
			return false;

		BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        m_Intro    = GetObject((int)GameObjects.Intro);
        m_Start    = GetObject((int)GameObjects.Start);
        m_MainMenu = GetObject((int)GameObjects.MainMenu);

        // Button Event
        GetText((int)Texts.ContinueText      ).gameObject.BindEvent(() => { Continue(); });
        GetText((int)Texts.LoadGameText      ).gameObject.BindEvent(() => { LoadGame(); });
        GetText((int)Texts.NewGameText       ).gameObject.BindEvent(() => { NewGame(); });
        GetText((int)Texts.QuitText).gameObject.BindEvent(() => { QuitGame(); });

        // Button Sound
        PointerDown(GetText((int)Texts.PressAnyButtonText).gameObject);
        PointerDown(GetText((int)Texts.ContinueText      ).gameObject);
        PointerDown(GetText((int)Texts.LoadGameText      ).gameObject);
        PointerDown(GetText((int)Texts.NewGameText       ).gameObject);
        PointerDown(GetText((int)Texts.QuitText).gameObject);

        m_Animator = GetComponentInChildren<Animator>();
        m_FadeInOutScreenUI = FindObjectOfType<FadeInOutScreenUI>();

        m_Start.SetActive(false);
        m_MainMenu.SetActive(false);

        IntroScreen();

        return true;
	}

    void IntroScreen()
    {
        m_FadeInOutScreenUI.FadeIn(
            null, // 페이드 인에서 조건 없이 실행.
            () => { m_Intro.SetActive(false); m_Start.SetActive(true); StartScreen(); });

    }

    void StartScreen()
    {
        // BGM 틀기
        Managers.Sound.Play("Bgm/Dark Souls III", 1, Define.Sound.Bgm);

        // 텍스트 백그라운드 이미지 효과 주기.
        m_Animator.Play("UntilSelectButtonTextEffect");

        m_FadeInOutScreenUI.FadeIn(
                // 아무 키나 입력하면 다음으로 이동.
                () =>
                {
                    if (Input.anyKeyDown)
                    {
                        Managers.Sound.Play("Sounds/UI/UI_Button_Select_02");
                        m_Animator.Play("SelectButton_PressAnyButton");
                        return true;
                    }
                    else
                        return false;
                },
                () => 
                {
                    m_Start.SetActive(false);
                    m_MainMenu.SetActive(true);
                    MainmenuScreen();
                }
            );

    }

    void MainmenuScreen()
    {
        // 텍스트 백그라운드 이미지 효과 주기.
        m_Animator.Play("UntilSelectButtonTextEffect");

        m_FadeInOutScreenUI.FadeIn(
            // 아무 키나 입력하면 다음으로 이동.
            () =>
            {
                if (m_bIsButtonPress)
                {
                    Managers.Sound.Play("Sounds/UI/UI_Button_Select_02");
                    m_bIsButtonPress = false;
                    return true;
                }
                else
                    return false;
            },
            () => 
            {
                m_MainMenu.SetActive(false);
                m_FadeOutAction();
                MainmenuScreen();
            }
        );
    }


	void Continue()
    {
    }

    void LoadGame()
    {

    }

    void NewGame()
    {
        m_bIsButtonPress = true;
        m_Animator.Play("SelectButton_New Game");
        m_FadeOutAction = () => { Managers.Scene.LoadScene(Define.Scene.Lobby); };
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void PointerDown(GameObject go)
    {
        EventTrigger trigger = go.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener((data) => { PointerDownSoundPlay((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { PointerDownSoundPlay((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void PointerDownSoundPlay(PointerEventData data)
    {
        Managers.Sound.Play("Sounds/UI/UI_Button_PointerDown_04");
    }
}
