using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : UI_Base
{
    public TextMeshProUGUI m_BossNameText;
    public TextMeshProUGUI m_DamageValueText;

    AICharacterManager m_AICharacterManager;

    public Image m_HealthBarFill;
    public Image m_DownHealthBarFill;

    public int m_iDamageValueSum;
    float m_DamageValueAddTime = 0;
    float m_ShowDamageTime = 3f;

    public GameObject m_goPanel;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        m_DamageValueText.enabled = false;
        m_goPanel.gameObject.SetActive(false);

        return true;
    }

    public void Update()
    {
        if(m_DamageValueAddTime != 0)
        {
            m_DamageValueAddTime -= Time.deltaTime;

            if(m_DamageValueAddTime <= 0)
            {
                m_DamageValueAddTime = m_ShowDamageTime;
                m_DamageValueText.enabled = false;
            }
        }
    }

    public void SetInfo(AICharacterManager aiCharacter)
    {
        m_AICharacterManager = aiCharacter;
        m_BossNameText.text = m_AICharacterManager.name;
    }

    public void  SetUIHealthBarToActive()
    {
        m_goPanel.gameObject.SetActive(true);

    }

    public override void RefreshUI()
    {
        m_HealthBarFill.fillAmount = m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth;
        StartCoroutine(DownHP());
        ShowDamageValue();
    }

    public IEnumerator DownHP()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            m_DownHealthBarFill.fillAmount -= Time.deltaTime;
            if (m_DownHealthBarFill.fillAmount <= m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth)
            {
                m_DownHealthBarFill.fillAmount = m_AICharacterManager.aiCharacterStatsManager.currentHealth / (float)m_AICharacterManager.aiCharacterStatsManager.maxHealth;
                yield break;
            }

            yield return null;
        }
    }

    public void ShowDamageValue()
    {
        m_DamageValueText.enabled = true;

        m_DamageValueAddTime = m_ShowDamageTime;

        int damage = m_AICharacterManager.aiCharacterStatsManager.maxHealth - m_AICharacterManager.aiCharacterStatsManager.currentHealth;
        m_iDamageValueSum += damage;

        m_DamageValueText.text = m_iDamageValueSum.ToString();
    }
}
