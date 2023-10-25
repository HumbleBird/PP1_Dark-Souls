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

    public void AddSouls(int AddSous, bool isImmediately = false)
    {
        if(isImmediately)
        {
            m_iCurrentSouls = m_Player.playerStatsManager.currentSoulCount;
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

        }
        else
        {
            m_AddSouls.text = "+" + AddSous.ToString();
            StartCoroutine(IAddSouls());
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

    IEnumerator IAddSouls()
    {
        yield return new WaitForSeconds(1f);

        // Animator Play
        // 1. add souls animator
        m_Animator.Play("AddSouls");

        yield return new WaitForSeconds(1.5f);

        // 2. 1번이 끝나면 current souls animator play
        // 3. 2번과 동시에 코루틴으로 1씩 증가.
        while (true)
        {
            if (m_Player.playerStatsManager.currentSoulCount == m_iCurrentSouls)
            {

                yield break;
            }

            m_iCurrentSouls++;
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

            yield return null;
        }

    }

    IEnumerator ILoseSouls()
    {
        while (true)
        {
            if (m_Player.playerStatsManager.currentSoulCount == m_iCurrentSouls)
            {

                yield break;
            }

            m_iCurrentSouls--;
            m_CurrentSoulsText.text = m_iCurrentSouls.ToString();

            yield return null;
        }
    }
}
