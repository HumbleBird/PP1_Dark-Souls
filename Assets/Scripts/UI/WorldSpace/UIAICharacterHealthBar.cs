using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAICharacterHealthBar : MonoBehaviour
{
    public Slider slider;
    private float timeUntilBarIsHidden = 0;
    [SerializeField] UIEnemyYellowBar yelloBar;
    [SerializeField] float yelloBarTimer = 3;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] int currentDamageTaken;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        yelloBar = GetComponentInChildren<UIEnemyYellowBar>();
    }

    private void OnEnable()
    {
        currentDamageTaken = 0;
    }

    public void SetHealth(int health)
    {
        if (yelloBar != null)
        {
            yelloBar.gameObject.SetActive(true);

            yelloBar.timer = yelloBarTimer;

            if (health > slider.value)
            {
                yelloBar.slider.value = health;
            }
        }

        currentDamageTaken += Mathf.RoundToInt(slider.value - health);
        damageText.text = currentDamageTaken.ToString();

        slider.value = health;
        timeUntilBarIsHidden = 5;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;


        if (yelloBar != null)
        {
            yelloBar.SetMaxStat(maxHealth);
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);

        timeUntilBarIsHidden -= Time.deltaTime;

        if(slider != null)
        {
            if (timeUntilBarIsHidden <= 0)
            {
                timeUntilBarIsHidden = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }

            if (slider.value <= 0)
            {
                Destroy(slider.gameObject);
            }
        }
    }
}
