using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSliderOnEnable : MonoBehaviour
{
    public Slider statSlider;

    private void Awake()
    {
        statSlider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        statSlider.Select();
        //statSlider.OnSelect(null);
    }
}
