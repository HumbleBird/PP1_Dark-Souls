using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Define;

public class AICharacterStatsManager : CharacterStatsManager
{
    AICharacterManager aiCharacter;

    public int soulsAwardedOnDeath = 50;
    public UIAICharacterHealthBar aiCharacterHealthBar;

    public bool isBoss;

    protected override void Awake()
    {
        base.Awake();

        aiCharacter = GetComponent<AICharacterManager>();


        if(isBoss)
        {

        }
        else
        {
            aiCharacterHealthBar = Managers.UI.MakeWorldSpaceUI<UIAICharacterHealthBar>(transform);
        }

        teamIDNumber = (int)E_TeamId.Monster;

        if(aiCharacter.m_CharacterID != 0 )
        {
            Table_Monster.Info data = Managers.Table.m_Monster.Get(aiCharacter.m_CharacterID);

            if (data == null)
                return;

            m_sCharacterName = data.m_sName;

            Table_Stat.Info statData = Managers.Table.m_Stat.Get(data.m_iStatID);

            if (statData == null)
                return;

            soulsAwardedOnDeath = statData.m_iRewardSouls;
            // Drop Reward Item
            maxHealth = statData.m_iHP;
            currentHealth = maxHealth;
            m_fPhysicalDamage = statData.m_PhysicalDamage;
            m_fMagicDamage= statData.m_MagicDamage;
            m_fFireDamage= statData.m_FireDamage;
            m_fLightningDamage= statData.m_LightningDamage;
            m_fDarkDamage= statData.m_DarkDamage;
        }
    }

    protected override void Start()
    {
        base.Start();

        LoadStat();
    }

    // 능력치를 CSV 테이블에서 로드
    public override void LoadStat()
    {
        // CSV 에서 정보 로드

        InitAbility();
    }

    // 가져온 스텟을 이용해 능력치 정하기
    public override void InitAbility()
    {
        maxHealth = CalculateMaxHP(aiCharacter.m_CharacterID);

        // Poise
        m_fTotalPoiseDefence = m_fStatPoise;

        FullRecovery();
    }

    // Current HP,Stamina FP 완전 회복
    public override void FullRecovery()
    {
        currentHealth = maxHealth;

    }

    public override int CalculateMaxHP(int MonsterId)
    {
        Table_Monster.Info data = Managers.Table.m_Monster.Get(aiCharacter.m_CharacterID);

        if (data == null)
            return 1 ;

        m_sCharacterName = data.m_sName;

        Table_Stat.Info statData = Managers.Table.m_Stat.Get(data.m_iStatID);

        if (statData == null)
            return 1 ;

        return statData.m_iHP;
    }

    public override void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        base.TakeDamageNoAnimation(damage, fireDamage);

        if(!isBoss)
        {
            aiCharacterHealthBar.RefreshUI(damage);

        }
        else if (isBoss && aiCharacter.aiCharacterBossManager != null)
        {
            aiCharacter.aiCharacterBossManager.HealthRefresh();

        }
    }

    public override void TakePoisonDamage(int damage)
    {
        if (aiCharacter.isDead)
            return;

        base.TakePoisonDamage(damage);

        if (!isBoss)
        {
            aiCharacterHealthBar.RefreshUI(damage);
        }
        else if (isBoss && aiCharacter.aiCharacterBossManager != null)
        {
            aiCharacter.aiCharacterBossManager.HealthRefresh();

        }

        if (currentHealth <= 0)
        {
            aiCharacter.Dead();

            aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Dead_01", true);
        }
    }

    public void BreakGuard()
    {
        aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Break Guard", true);
    }

    public override void HealthBarUIUpdate(int damage)
    {
        if (!isBoss)
        {
            aiCharacterHealthBar.RefreshUI(damage);
        }
        else if (isBoss && aiCharacter.aiCharacterBossManager != null)
        {
            aiCharacter.aiCharacterBossManager.HealthRefresh();

        }
    }
}
