using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SelectHairColor : MonoBehaviour
{
    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    [Header("Color Sliders")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Color currentHairColor;

    // skin mesh renderer로부터의 material을 잡는다. 메테리얼 속성값을 변경한다.
    public List<SkinnedMeshRenderer> rendererList = new List<SkinnedMeshRenderer>();

    public void UpdateSliders()
    {
        redAmount = redSlider.value;
        greenAmount = greenSlider.value;
        blueAmount = blueSlider.value;
        SetHairColor();
    }

    public void SetHairColor()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount);

        for (int i = 0; i < rendererList.Count; i++)
        {
            rendererList[i].material.SetColor("_Color_Hair", currentHairColor);
            rendererList[i].material.SetColor("_Color", currentHairColor);
        }
    }
}

