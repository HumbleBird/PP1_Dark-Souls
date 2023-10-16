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

    public float m_fFadeTime = 3.5f; // ���� ȭ���� �����ִ� �ð�.

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

    // ���� ȭ���� ����
    IEnumerator IFadeIn()
    {
        animator.Play("FadeInScreen");

        while (true)
        {
            // 2. ���̵� �� ������� ���ٸ� �ٷ� ���̵� �ƿ� �� ���̵� �ƿ� �Լ� ����.
            if (m_bisAnimationEnd == true)
            {
                m_bisAnimationEnd = false;
                m_goFadeInOutImage.gameObject.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    // ���� ȭ���� ������
    IEnumerator IFadeOut(Action action = null)
    {
        animator.Play("FadeOutScreen");
        m_goFadeInOutImage.gameObject.SetActive(true);

        while (true)
        {
            // 2. ���̵� �� ������� ���ٸ� �ٷ� ���̵� �ƿ� �� ���̵� �ƿ� �Լ� ����.
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



    //// ���� ȭ���� ����
    //IEnumerator IFadeInOut(Func<bool> FadeInCondition, Action FadeOutFunc)
    //{
    //    animator.Play("FadeInScreen");

    //    // ���̵� ���϶� ������ �ְų�, ���̵� �ƿ��� �� ������ �̺�Ʈ�� �ִٸ�
    //    if (FadeInCondition != null || FadeOutFunc != null)
    //    {
    //        while (true)
    //        {
    //            // 2. ���̵� �� ������� ���ٸ� �ٷ� ���̵� �ƿ� �� ���̵� �ƿ� �Լ� ����.
    //            if (m_bisAnimationEnd == true && FadeInCondition == null)
    //            {
    //                m_bisAnimationEnd = false;
    //                FadeOut(FadeOutFunc);
    //                break;
    //            }

    //            // 1. ���̵� �� ������� �ִٸ� ���� ���. ������ �����ϸ� ���̵� �ƿ� �Լ��� ����
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

    //// ���� ȭ���� ����
    //IEnumerator IFadeIn()
    //{
    //    animator.Play("FadeInScreen");

    //    while (true)
    //    {

    //        // 2. ���̵� �� ������� ���ٸ� �ٷ� ���̵� �ƿ� �� ���̵� �ƿ� �Լ� ����.
    //        if (m_bisAnimationEnd == true)
    //        {
    //            m_bisAnimationEnd = false;
    //            gameObject.SetActive(false);
    //            break;
    //        }

    //        yield return null;
    //    }
    //}

    //// ���� ȭ���� �巯��
    //IEnumerator IFadeOut(Action FadeOutFunc = null)
    //{
    //    // ���̵� �ƿ�
    //    animator.Play("FadeOutScreen");
    //    m_bisAnimationEnd = false;

    //    yield return new WaitForSeconds(m_fFadeTime);

    //    // ���̵� ���϶� ������ �ְų�, ���̵� �ƿ��� �� ������ �̺�Ʈ�� �ִٸ�
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
