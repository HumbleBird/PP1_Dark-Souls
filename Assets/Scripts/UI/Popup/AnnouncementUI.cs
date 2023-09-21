using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnouncementUI : UI_Popup
{
    enum Texts
    {
        ContentsText
    }

    enum Images
    {
        Background,
        BackPanel,
    }

    public void SetInfo(string content)
    {
        GetText((int)Texts.ContentsText).text = content.ToString();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        return true;
    }

    public void OnEnable()
    {
        StartCoroutine(FadeOutPopup());
    }

    IEnumerator FadeOutPopup()
    {
        Color color1 =    GetImage((int)Images.Background).color;
        Color color2 =    GetImage((int)Images.BackPanel).color ;
        Color textColor = GetText((int)Texts.ContentsText).color;

        while (true)
        {
            color1.a = color1.a - (1 * Time.deltaTime);
            color2.a = color2.a - (1 * Time.deltaTime);
            textColor.a = textColor.a - (1 * Time.deltaTime);

            GetImage((int)Images.Background).color = color1;
            GetImage((int)Images.BackPanel).color  = color2;
            GetText((int)Texts.ContentsText).color = textColor;

            if (color2.a <= 0.05f)
            {
                Managers.UI.ClosePopupUI();
            }

            yield return null;
        }
    }
}
