 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonfireLitPopupUI : MonoBehaviour
{
    Image image;

    public void Start()
    {
        image = GetComponentInChildren<Image>();

        StartCoroutine(FadeInPopup());
    }

    IEnumerator FadeInPopup()
    {
        Color c = image.color;

        while (true)
        {
            c.a += Time.deltaTime;
            image.color = c;

            if (c.a >= 1)
            {
                c.a = 1;
                image.color = c;
                yield return StartCoroutine(FadeOutPopup());
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOutPopup()
    {
        Color c = image.color;

        while (true)
        {
            c.a -= Time.deltaTime;
            image.color = c;

            if (c.a <= 0.01f)
            {
                Managers.Resource.Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
