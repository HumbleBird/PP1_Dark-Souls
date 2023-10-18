using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroUI : UI_Base
{
    public VideoPlayer m_Vidioplayer;
    bool m_isPressKey = false;
    Animator m_Animator;

    public float m_fWaitForTimePostFadeOutMP4 = 3.5f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        m_Vidioplayer = GetComponentInChildren<VideoPlayer>();
        m_Vidioplayer.SetDirectAudioVolume(0, 1);
        m_Animator = GetComponent<Animator>();

        return true;
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            if (m_isPressKey == false)
            {
                m_isPressKey = true;
                if(Managers.Game.m_isNewGame)
                {
                    StartCoroutine(FadeOut(() => { Managers.Object.m_MyPlayer.gameObject.SetActive(true); }));

                }
                else
                {
                    StartCoroutine(FadeOut());
                }
            }
        }
    }

    public void PlayMP4(string name)
    {
        m_Vidioplayer.source = VideoSource.VideoClip;
        m_Vidioplayer.clip = Managers.Resource.Load<VideoClip>("Art/MP4/" + name);
        m_Vidioplayer.StepForward();
        m_Vidioplayer.Play();
    }

    IEnumerator FadeOut(Action fadeOutAction = null)
    {
        // MP4 Fade Out
        m_Animator.Play("IntroUI_FadeOut_MP4"); // Fade Out �ð��� 2��

        yield return new WaitForSeconds(m_fWaitForTimePostFadeOutMP4); // ���� ȭ�鿡�� �ణ�� ���


        m_Animator.Play("IntroUI_FadeOut_BackGround"); // Fade Out �ð��� 1��


        if (fadeOutAction != null)
        {
            fadeOutAction.Invoke();
        }

        Managers.Resource.Destroy(gameObject);
    }

}
