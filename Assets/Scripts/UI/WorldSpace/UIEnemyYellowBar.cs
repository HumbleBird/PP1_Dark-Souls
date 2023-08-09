using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyYellowBar : MonoBehaviour
{
    public Slider slider;
    UIAICharacterHealthBar parentHealthBar;

    public float timer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        parentHealthBar = GetComponentInParent<UIAICharacterHealthBar>();
    }

    public void SetMaxStat(int maxStat)
    {
        slider.maxValue = maxStat;
        slider.value = maxStat;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            if(slider.value > parentHealthBar.slider.value)
            {
                slider.value -= 1f;
            }
            else if (slider.value <= parentHealthBar.slider.value)
            {
                gameObject.SetActive(false);
            }
            
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
