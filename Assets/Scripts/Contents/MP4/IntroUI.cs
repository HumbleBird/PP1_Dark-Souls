using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroUI : UI_Popup
{
    enum GameObjects
    {
        VideoImg,
    }

    public VideoPlayer m_Vidioplayer;
    float DownSpeed = 1f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        m_Vidioplayer = GetComponentInChildren<VideoPlayer>();
        m_Vidioplayer.SetDirectAudioVolume(0, 1);

        return true;
    }

    bool m_isPressKey = false;

    private void Update()
    {
        if(Input.anyKey)
        {
            if (m_isPressKey == false)
            {
                m_isPressKey = true;
                StartCoroutine(FadeOut());
            }
        }
    }

    public void PlayMP4(string name)
    {
        m_Vidioplayer.source = VideoSource.VideoClip;
        m_Vidioplayer.clip = Managers.Resource.Load<VideoClip>("Art/MP4/" +name);
        m_Vidioplayer.StepForward();

        // 코루틴으로 클릭 감지하면 사라짐. 이미지의 알파값을 이용한다.
        // 검은 화면 좀 보여주다가 페이드 인으로 인게임 화면으로 넘어감
    }

    IEnumerator FadeOut()
    {
        Color color1 = GetObject((int)GameObjects.VideoImg).GetComponent<RawImage>().color;
        float vol = m_Vidioplayer.GetDirectAudioVolume(0);
        while (true)
        {
            // 오디오
            vol -= (DownSpeed * Time.deltaTime * 0.8f);
            m_Vidioplayer.SetDirectAudioVolume(0, vol);

            // 이미지
            color1.a = color1.a - (DownSpeed * Time.deltaTime);

            GetObject((int)GameObjects.VideoImg).GetComponent<RawImage>().color = color1;

            if (color1.a <= 0)
            {
                yield return new WaitForSeconds(5f);

                Managers.UI.ClosePopupUI();
            }

            yield return null;
        }
    }
}
