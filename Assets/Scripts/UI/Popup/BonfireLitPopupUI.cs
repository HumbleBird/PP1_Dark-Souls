 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireLitPopupUI : UI_Popup
{
    CanvasGroup canvas;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        canvas = GetComponent<CanvasGroup>();

        return true;
    }

    public void Start()
    {
        StartCoroutine(FadeInPopup());
    }

    IEnumerator FadeInPopup()
    {
        gameObject.SetActive(true);

        for (float fade = 0.05f; fade < 1; fade = fade + 0.05f)
        {
            canvas.alpha = fade;

            if(fade > 0.9f)
            {
                StartCoroutine(FadeOutPopup());
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOutPopup()
    {
        for (float fade = 1f; fade > 0; fade = fade - 0.05f)
        {
            canvas.alpha = fade;

            if (fade <= 0.05f)
            {
                Managers.UI.ClosePopupUI();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
