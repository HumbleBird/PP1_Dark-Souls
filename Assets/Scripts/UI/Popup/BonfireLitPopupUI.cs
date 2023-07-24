 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireLitPopupUI : MonoBehaviour
{
    CanvasGroup canvas;

    // Start is called before the first frame update
    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public void DisplayBonfireLitPopup()
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
                gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
