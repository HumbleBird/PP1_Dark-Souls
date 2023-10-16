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
        if(Managers.Scene.CurrentScene.SceneType == Define.Scene.Game)
        {
            
        }
        else
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }

    public void FadeOut(Action toPlayAction = null)
    {
        StartCoroutine(IFadeOut(toPlayAction));
    }

    //public void FadeInOut(Func<bool> FadeInCondition = null, Action FadeOutFunc = null)
    //{
    //    StartCoroutine(IFadeInOut(FadeInCondition, FadeOutFunc));
    //}

    // 검은 화면을 걷음
    IEnumerator IFadeIn()
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
    IEnumerator IFadeOut(Action action = null)
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



    //// 검은 화면을 걷음
    //IEnumerator IFadeInOut(Func<bool> FadeInCondition, Action FadeOutFunc)
    //{
    //    animator.Play("FadeInScreen");

    //    // 페이드 인일때 조건이 있거나, 페이드 아웃일 때 실행할 이벤트가 있다면
    //    if (FadeInCondition != null || FadeOutFunc != null)
    //    {
    //        while (true)
    //        {
    //            // 2. 페이드 인 컨디션이 없다면 바로 페이드 아웃 후 페이드 아웃 함수 실행.
    //            if (m_bisAnimationEnd == true && FadeInCondition == null)
    //            {
    //                m_bisAnimationEnd = false;
    //                FadeOut(FadeOutFunc);
    //                break;
    //            }

    //            // 1. 페이드 인 컨디션이 있다면 무한 대기. 조건을 만족하면 페이드 아웃 함수를 실행
    //            else if (m_bisAnimationEnd == true && FadeInCondition.Invoke())
    //            {
    //                m_bisAnimationEnd = false;
    //                FadeOut(FadeOutFunc);
    //                break;
    //            }



    //            yield return null;
    //        }
    //    }
    //}

    //// 검은 화면을 걷음
    //IEnumerator IFadeIn()
    //{
    //    animator.Play("FadeInScreen");

    //    while (true)
    //    {

    //        // 2. 페이드 인 컨디션이 없다면 바로 페이드 아웃 후 페이드 아웃 함수 실행.
    //        if (m_bisAnimationEnd == true)
    //        {
    //            m_bisAnimationEnd = false;
    //            gameObject.SetActive(false);
    //            break;
    //        }

    //        yield return null;
    //    }
    //}

    //// 검은 화면을 드러냄
    //IEnumerator IFadeOut(Action FadeOutFunc = null)
    //{
    //    // 페이드 아웃
    //    animator.Play("FadeOutScreen");
    //    m_bisAnimationEnd = false;

    //    yield return new WaitForSeconds(m_fFadeTime);

    //    // 페이드 인일때 조건이 있거나, 페이드 아웃일 때 실행할 이벤트가 있다면
    //    if (FadeOutFunc != null)
    //    {
    //        while (true)
    //        {
    //            if (m_bisAnimationEnd == true )
    //            {
    //                m_bisAnimationEnd = false;
    //                Managers.Game.PlayAction(() => { FadeOutFunc.Invoke(); } );

    //                break;
    //            }

    //            yield return null;
    //        }
    //    }
    //}



}
