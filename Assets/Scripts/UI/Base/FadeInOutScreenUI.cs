using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutScreenUI : MonoBehaviour
{
    public Animator animator;
    public bool m_bisAnimationEnd = false;

    public Image m_goFadeInOutImage;

    public float m_fFadeTime = 3.5f; // 검은 화면을 보여주는 시간.

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (Managers.Scene.CurrentScene.SceneType != Define.Scene.Game)
        {
            m_goFadeInOutImage.gameObject.SetActive(true);
        }

        if (Managers.Scene.CurrentScene.SceneType == Define.Scene.Game && Managers.Game.m_isNewGame)
        {
            StartCoroutine(Temp());
        }

        if (Managers.Scene.CurrentScene.SceneType == Define.Scene.Lobby)
        {
            FadeIn();
        }
    }

    IEnumerator Temp()
    {
        Managers.GameUI.m_GameSceneUI.m_FadeInOutScreenUI.m_goFadeInOutImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Managers.GameUI.m_GameSceneUI.m_FadeInOutScreenUI.m_goFadeInOutImage.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }

    public void FadeOut(Action toPlayAction = null)
    {
        StartCoroutine(IFadeOut(toPlayAction));
    }

    // 검은 화면을 걷음
    public IEnumerator IFadeIn()
    {
        animator.Play("FadeInScreen");

        while (true)
        {
            // 2. 페이드 인 컨디션이 없다면 바로 페이드 아웃 후 페이드 아웃 함수 실행.
            if (m_bisAnimationEnd == true)
            {
                m_bisAnimationEnd = false;
                m_goFadeInOutImage.gameObject.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    // 검은 화면을 보여줌
    public IEnumerator IFadeOut(Action action = null)
    {
        animator.Play("FadeOutScreen");
        m_goFadeInOutImage.gameObject.SetActive(true);

        while (true)
        {
            // 2. 페이드 인 컨디션이 없다면 바로 페이드 아웃 후 페이드 아웃 함수 실행.
            if (m_bisAnimationEnd == true)
            {
                m_bisAnimationEnd = false;

                if (action != null)
                {
                    Managers.Game.PlayAction(() => action());
                }
                break;
            }

            yield return null;
        }
    }
}
