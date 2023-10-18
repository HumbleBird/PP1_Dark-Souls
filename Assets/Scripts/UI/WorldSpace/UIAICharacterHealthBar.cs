using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIAICharacterHealthBar : UI_Base
{
    enum Images
    {
        YellowBarFill,
        HealthBarFill
    }

    enum Texts
    {
        DamageValue
    }

    enum GameObjects
    {
        HealthBar
    }

    Image m_HealthBarFill;
    Image m_YellowBarFill;
    GameObject m_HealthBar;
    TextMeshProUGUI m_DamageValueText;

    private float timeUntilBarIsHidden = 0;
    int DamageSum = 0;

    AICharacterManager m_AICharacterManager;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));

        m_YellowBarFill = GetImage((int)Images.YellowBarFill);
        m_HealthBarFill = GetImage((int)Images.HealthBarFill);
        m_DamageValueText = GetText((int)Texts.DamageValue);

        m_HealthBar = GetObject((int)GameObjects.HealthBar);

        m_HealthBar.SetActive(false);

        return true;
    }

    private void Start()
    {
        m_AICharacterManager = GetComponentInParent<AICharacterManager>();
        transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (m_AICharacterManager.isDead)
            return;

        if (m_HealthBar.activeSelf == false)
            return;

        transform.LookAt(transform.position + Camera.main.transform.forward);

        timeUntilBarIsHidden -= Time.deltaTime;

        if (timeUntilBarIsHidden <= 0)
        {
            timeUntilBarIsHidden = 0;
            DamageSum = 0;
            m_HealthBar.SetActive(false);
        }
    }

    public void RefreshUI(int Damage)
    {
        m_HealthBar.SetActive(true);
        m_HealthBarFill.fillAmount = m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth;
        StartCoroutine(DownHP());

        timeUntilBarIsHidden = 5;
        DamageSum += Damage;
        m_DamageValueText.text = DamageSum.ToString();
    }

    public IEnumerator DownHP()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            m_YellowBarFill.fillAmount -= Time.deltaTime;
            if (m_YellowBarFill.fillAmount <= m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth)
            {
                m_YellowBarFill.fillAmount = m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth;
                yield break;
            }

            yield return null;
        }
    }


}
