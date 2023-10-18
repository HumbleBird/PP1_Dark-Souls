using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractablePopupUI : UI_Popup
{
    enum Texts
    {
        InteractionText,
        ItemText
    }

    enum Images
    {
        ItemImage
    }

    public TextMeshProUGUI m_InteractionText;
    public TextMeshProUGUI m_ItemText;
    public Image m_ItemImage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        m_InteractionText = GetText((int)Texts.InteractionText);
        m_ItemText = GetText((int)Texts.ItemText);
        m_ItemImage = GetImage((int)Images.ItemImage);

        m_ItemText.gameObject.SetActive(false);
        m_ItemImage.gameObject.SetActive(false);
        return true;
    }
}
