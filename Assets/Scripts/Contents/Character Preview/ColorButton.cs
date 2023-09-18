using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public SelectExternalFeaturesColor m_SelectExternalFeaturesColor;

    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    [Header("Color Sliders")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Image colorImage;

    private void Awake()
    {
        m_SelectExternalFeaturesColor = GetComponentInParent<SelectExternalFeaturesColor>();

        colorImage = GetComponent<Image>();
        redAmount = colorImage.color.r;
        greenAmount = colorImage.color.g;
        blueAmount  = colorImage.color.b;

        // Buton
        Button childBtn = GetComponent<Button>();
        // Button Function
        childBtn.onClick.AddListener(() =>
        {
            SetSliderValuesToImageColor();
        });

         // Event Trigger
         EventTrigger trigger = gameObject.GetOrAddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener((data) => { SetSliderValuesToImageColor((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { SetSliderValuesToImageColor((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    public void SetSliderValuesToImageColor(PointerEventData data)
    {
        redSlider.value   = redAmount    ;
        greenSlider.value = greenAmount  ;
        blueSlider.value =    blueAmount;

        m_SelectExternalFeaturesColor.redAmount = redAmount;
        m_SelectExternalFeaturesColor.greenAmount = greenAmount;
        m_SelectExternalFeaturesColor.blueAmount = blueAmount;
        m_SelectExternalFeaturesColor.SetPartsColor();
    }

    public void SetSliderValuesToImageColor()
    {
        redSlider.value   = redAmount    ;
        greenSlider.value = greenAmount  ;
        blueSlider.value =    blueAmount;

        m_SelectExternalFeaturesColor.redAmount = redAmount;
        m_SelectExternalFeaturesColor.greenAmount = greenAmount;
        m_SelectExternalFeaturesColor.blueAmount = blueAmount;
        m_SelectExternalFeaturesColor.SetPartsColor();
    }
}
