using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_SoulUI : MonoBehaviour
{
    Animator m_Animator;
    public TextMeshProUGUI m_CurrentSoulsText;
    public TextMeshProUGUI m_AddSouls;
    int m_iCurrentSouls;
    PlayerManager m_Player;

    public void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Start()
    {
        m_Player = Managers.Object.m_MyPlayer;
        m_iCurrentSouls = m_Player.playerStatsManager.currentSoulCount;
        m_CurrentSoulsText.text = m_iCurrentSouls.ToString();
    }

    public void AddSouls(int AddSouls, bool isImmediately = false)
    {
        if(isImmediately)
        {
            m_iCurrentSouls = m_Player.playerStatsManager.currentSoulCount;
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

        }
        else
        {
            m_AddSouls.text = "+" + AddSouls.ToString();
            StartCoroutine(IAddSouls(AddSouls));
        }
    }

    IEnumerator IAddSouls(int AddSouls)
    {
        yield return new WaitForSeconds(1f);

        // Animator Play
        // 1. add souls animator
        m_Animator.Play("AddSouls");

        yield return new WaitForSeconds(1.5f);

        // 2. 1���� ������ current souls animator play
        // 3. 2���� ���ÿ� �ڷ�ƾ���� 1�� ����.
        while (true)
        {
            if (m_Player.playerStatsManager.currentSoulCount <= m_iCurrentSouls)
            {
                m_iCurrentSouls = m_Player.playerStatsManager.currentSoulCount;
                m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

                yield break;
            }

            m_iCurrentSouls += Mathf.RoundToInt(AddSouls * Time.deltaTime / 2);
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

            yield return null;
        }

    }


    public void LoseSouls(bool isImmediately = false)
    {
        if (isImmediately)
        {
            m_iCurrentSouls = m_Player.playerStatsManager.currentSoulCount;
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();
        }
        else
        {
            StartCoroutine(ILoseSouls());

        }

    }

    IEnumerator ILoseSouls()
    {
        int oriSoul = m_iCurrentSouls;
        float downSpeed = 0;
        if (oriSoul > 100000)
            downSpeed = 1;
        else if (oriSoul >= 50000)
            downSpeed = 3;
        else if (oriSoul >= 20000)
            downSpeed = 5;
        else 
            downSpeed = 7;


        while (true)
        {
            if (m_Player.playerStatsManager.currentSoulCount <= m_iCurrentSouls)
            {
                m_iCurrentSouls = 0;
                m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

                yield break;
            }

            m_iCurrentSouls -= Mathf.RoundToInt(oriSoul * Time.deltaTime / downSpeed);
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

            yield return null;
        }
    }
}
