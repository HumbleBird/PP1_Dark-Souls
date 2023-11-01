using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartSceneUI : UI_Scene
{
    public FadeInOutScreenUI m_FadeInOutScreenUI;

    enum Texts
    {
        PressAnyButtonText  ,
        ContinueText        ,
        //LoadGameText        ,
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

    GameObject m_Intro; // ���� ȸ�� Ÿ��Ʋ�� ������
    GameObject m_Start; // �ƹ��ų� Ű�� Ŭ���ϸ� ���� �޴���
    GameObject m_MainMenu; // �� ���� ����, �α� ����, ������ ��

    public GameObject m_goContinue;

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
        GetText((int)Texts.PressAnyButtonText).gameObject.BindEvent(() => m_bIsButtonPress = true);
        GetText((int)Texts.ContinueText      ).gameObject.BindEvent(() => { Continue(); });
        //GetText((int)Texts.LoadGameText      ).gameObject.BindEvent(() => { LoadGame(); });
        GetText((int)Texts.NewGameText       ).gameObject.BindEvent(() => { NewGame(); });
        GetText((int)Texts.QuitText).gameObject.BindEvent(() => { QuitGame(); });

        // Button Sound
        PointerDown(GetText((int)Texts.PressAnyButtonText).gameObject);
        PointerDown(GetText((int)Texts.ContinueText      ).gameObject);
        //PointerDown(GetText((int)Texts.LoadGameText      ).gameObject);
        PointerDown(GetText((int)Texts.NewGameText       ).gameObject);
        PointerDown(GetText((int)Texts.QuitText).gameObject);

        // �ε� �����Ͱ� ���ٸ�
        if(Managers.Game.m_isNewGame == true)
        {
            m_goContinue.SetActive(true);
        }
        else
        {
            m_goContinue.SetActive(false);
        }

        m_Animator = GetComponentInChildren<Animator>();
        m_FadeInOutScreenUI = GetComponentInChildren<FadeInOutScreenUI>();

        m_Start.SetActive(false);
        m_MainMenu.SetActive(false);
        StartCoroutine( IntroScreen());

        return true;
	}

    // ó�� ȭ���� �Ȱ� ���� Ÿ��Ʋ�� ������
    IEnumerator IntroScreen()
    {
        // 3�ʰ� ���
        yield return new WaitForSeconds(1f); 

        // ���̵� �� (=ȭ�� ������)
        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeIn());

        // 3�ʰ� ���� Ÿ��Ʋ�� ������
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeOut());

        m_Intro.SetActive(false);


        StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        m_Start.SetActive(true);

        // BGM Ʋ��
        Managers.Sound.Play("Dark Souls III", 1, Define.Sound.Bgm);

        // �ؽ�Ʈ ��׶��� �̹��� ȿ�� �ֱ�.
        m_Animator.Play("UntilSelectButtonTextEffect");

        // (=ȭ�� ������)
        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeIn());

        // �ƹ� ��ư�̳� �Է� ������ ���� ���

        while (true)
        {
            if (m_bIsButtonPress)
            {
                Managers.Sound.Play("UI/Popup_ButtonClose");
                m_Animator.Play("SelectButton_PressAnyButton");
                m_bIsButtonPress = false;
                break;
            }

            yield return null;
        }

        //  (=ȭ�� ������)
        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeOut());

        m_Start.SetActive(false);

        StartCoroutine(MainmenuScreen());
    }

    IEnumerator MainmenuScreen()
    {
        m_MainMenu.SetActive(true);

        // ���̵� �ƿ� (=ȭ�� ������)
        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeIn());

        // �ؽ�Ʈ ��׶��� �̹��� ȿ�� �ֱ�.
        m_Animator.Play("UntilSelectButtonTextEffect");

        while (true)
        {
            if (m_bIsButtonPress)
            {
                Managers.Sound.Play("UI/Popup_ButtonClose");
                m_bIsButtonPress = false;
                break;
            }

            yield return null;
        }

        // ȭ�� ������
        yield return StartCoroutine(m_FadeInOutScreenUI.IFadeOut());

        m_MainMenu.SetActive(false);

        m_FadeOutAction();
    }


	void Continue()
    {
        // ĳ���� ���� �ε�
        m_bIsButtonPress = true;
        m_Animator.Play("SelectButton_Continue");
        m_FadeOutAction = () => { Managers.Scene.LoadScene(Define.Scene.Game); };
    }

    void LoadGame()
    {

    }

    void NewGame()
    {
        Managers.Game.m_isNewGame = true;
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
