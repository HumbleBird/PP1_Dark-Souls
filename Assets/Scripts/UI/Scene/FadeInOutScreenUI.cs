using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScreenUI : MonoBehaviour
{
    Canvas m_Canvase;

    public Animator animator;
    public bool m_bisFadeIn = false;
    public bool m_bisAnimationEnd = false;

    public float m_fWaitForFadeScreenTime = 3.5f;
    public float fadeTime = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        m_Canvase = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        m_Canvase.sortingOrder = 10;

        FadeInOut(null, null);
    }

    public void FadeInOut(Func<bool> FadeInCondition, Action FadeOutFunc)
    {

        StartCoroutine(IFadeIn(FadeInCondition, FadeOutFunc));
    }

    void FadeOut(Action toPlayAction)
    {
        StartCoroutine(IFadeOut(toPlayAction));
    }

    IEnumerator IFadeIn(Func<bool> FadeInCondition, Action FadeOutFunc)
    {


        yield return new WaitForSeconds(m_fWaitForFadeScreenTime);

        // 페이드 인
        m_bisFadeIn = true;
        animator.Play("FadeInScreen");
        // 페이드 아웃

        // 페이드 인일때 조건이 있거나, 페이드 아웃일 때 실행할 이벤트가 있다면
        if(FadeInCondition != null || FadeOutFunc != null)
        {
            while (true)
            {
                // 2. 페이드 인 컨디션이 없다면 바로 페이드 아웃 후 페이드 아웃 함수 실행.
                if (m_bisAnimationEnd == true && FadeInCondition == null)
                {
                    m_bisAnimationEnd = false;
                    FadeOut(FadeOutFunc);
                    break;
                }

                // 1. 페이드 인 컨디션이 있다면 무한 대기. 조건을 만족하면 페이드 아웃 함수를 실행
                else if (m_bisAnimationEnd == true && FadeInCondition.Invoke())
                {
                    m_bisAnimationEnd = false;
                    FadeOut(FadeOutFunc);
                    break;
                }



                yield return null;
            }
        }
    }

    IEnumerator IFadeOut(Action FadeOutFunc)
    {
        m_Canvase.sortingOrder = 10;

        // 페이드 아웃
        m_bisFadeIn = false;
        animator.Play("FadeOutScreen");

        // 페이드 인일때 조건이 있거나, 페이드 아웃일 때 실행할 이벤트가 있다면
        if (FadeOutFunc != null)
        {
            while (true)
            {
                if (m_bisAnimationEnd == true )
                {
                    m_bisAnimationEnd = false;
                    FadeOutFunc.Invoke();
                    break;
                }

                yield return null;
            }
        }
    }

}
