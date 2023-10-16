using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class StatBarsUI : UI_Base
{
    enum Images
    {
        HealthBarFill               ,
        DownHealthBarFill           ,
        StaminaBarFill              ,
        FocusPointBarFill           ,
        PoisonBuildUpFill           ,
        //PoisonBuildDownFill,

    }

    enum GameObjects
    {
        PoisonBar,
        // TODO Blood, Frost, etc...
            
    }

    PlayerManager m_Player;

    public GameObject m_PoisonBar;

    Image m_HealthBarFill           ;
    Image m_DownHealthBarFill;
    Image m_StaminaBarFill              ;
    Image m_FocusPointBarFill           ;
    Image m_PoisonBuildUpFill           ;
    public Image m_PoisonBuildDownFill         ; // 가끔 오류나서 Bind가 안 

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));

        m_HealthBarFill       = GetImage((int)Images.HealthBarFill      );
        m_DownHealthBarFill = GetImage((int)Images.DownHealthBarFill);
        m_StaminaBarFill      = GetImage((int)Images.StaminaBarFill     );
        m_FocusPointBarFill   = GetImage((int)Images.FocusPointBarFill  );
        m_PoisonBuildUpFill   = GetImage((int)Images.PoisonBuildUpFill  );
        //m_PoisonBuildDownFill = GetImage((int)Images.PoisonBuildDownFill);

        //m_PoisonBar = GetObject((int)GameObjects.PoisonBar);

        m_PoisonBar.SetActive(false);

        return true;
    }

    public void Start()
    {
        m_Player = Managers.Object.m_MyPlayer;
    }

    public void RefreshUI(E_StatUI stat)
    {
        switch (stat)
        {
            case E_StatUI.Hp:
                RefreshHPBar();
                break;
            case E_StatUI.Stamina:
                RefreshStaminaBar();
                break;
            case E_StatUI.FocusPoint:
                RefreshFocusPointBar();
                break;
            case E_StatUI.Posion:
                RefreshPoisonBar();
                break;
            case E_StatUI.All:
                RefreshHPBar();
                RefreshStaminaBar();
                RefreshFocusPointBar();
                RefreshPoisonBar();
                break;
            default:
                break;
        }
    }

    public IEnumerator DownHP()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            m_DownHealthBarFill.fillAmount -= Time.deltaTime;
            if (m_DownHealthBarFill.fillAmount <= m_Player.playerStatsManager.currentHealth / (float)m_Player.playerStatsManager.maxHealth)
            {
                m_DownHealthBarFill.fillAmount = m_Player.playerStatsManager.currentHealth / (float)m_Player.playerStatsManager.maxHealth;
                yield break;
            }

            yield return null;
        }
    }

    void RefreshHPBar()
    {
        m_HealthBarFill.fillAmount = m_Player.playerStatsManager.currentHealth / (float)m_Player.playerStatsManager.maxHealth;
        StartCoroutine(DownHP());
    }

    void RefreshStaminaBar()
    {
        m_StaminaBarFill.fillAmount = m_Player.playerStatsManager.currentStamina / (float)m_Player.playerStatsManager.maxStamina;

    }

    void RefreshFocusPointBar()
    {
        m_FocusPointBarFill.fillAmount = m_Player.playerStatsManager.currentFocusPoints / (float)m_Player.playerStatsManager.maxfocusPoint;

    }

    void RefreshPoisonBar()
    {
        m_PoisonBar.SetActive(true);

        // 중독 전
        if (m_Player.playerStatsManager.isPoisoned == false)
        {
            m_PoisonBuildDownFill.gameObject.SetActive(false);

            m_PoisonBuildUpFill.gameObject.SetActive(true);
            m_PoisonBuildUpFill.fillAmount = m_Player.playerStatsManager.poisonBuildup / (float)m_Player.playerStatsManager.poisonAmount;

            if (m_Player.playerStatsManager.poisonBuildup <= m_Player.playerStatsManager.poisonAmount)
                m_PoisonBar.SetActive(false);
        }
        // 중독 후
        else
        {
            m_PoisonBuildUpFill.gameObject.SetActive(false);

            m_PoisonBuildDownFill.gameObject.SetActive(true);
            m_PoisonBuildDownFill.fillAmount = m_Player.playerStatsManager.poisonBuildup / (float)m_Player.playerStatsManager.poisonAmount;

        }
    }
}
