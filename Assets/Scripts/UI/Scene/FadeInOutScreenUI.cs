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

        // ���̵� ��
        m_bisFadeIn = true;
        animator.Play("FadeInScreen");
        // ���̵� �ƿ�

        // ���̵� ���϶� ������ �ְų�, ���̵� �ƿ��� �� ������ �̺�Ʈ�� �ִٸ�
        if(FadeInCondition != null || FadeOutFunc != null)
        {
            while (true)
            {
                // 2. ���̵� �� ������� ���ٸ� �ٷ� ���̵� �ƿ� �� ���̵� �ƿ� �Լ� ����.
                if (m_bisAnimationEnd == true && FadeInCondition == null)
                {
                    m_bisAnimationEnd = false;
                    FadeOut(FadeOutFunc);
                    break;
                }

                // 1. ���̵� �� ������� �ִٸ� ���� ���. ������ �����ϸ� ���̵� �ƿ� �Լ��� ����
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

        // ���̵� �ƿ�
        m_bisFadeIn = false;
        animator.Play("FadeOutScreen");

        // ���̵� ���϶� ������ �ְų�, ���̵� �ƿ��� �� ������ �̺�Ʈ�� �ִٸ�
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
