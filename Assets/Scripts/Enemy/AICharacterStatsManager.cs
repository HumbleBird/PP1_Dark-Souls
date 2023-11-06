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
    public bool m_isFriend = false;

    protected override void Awake()
    {
        base.Awake();

        aiCharacter = GetComponent<AICharacterManager>();


        if(isBoss == false)
        {
            aiCharacterHealthBar = Managers.UI.MakeWorldSpaceUI<UIAICharacterHealthBar>(transform);
        }

        if(m_isFriend == false)
            teamIDNumber = (int)E_TeamId.Monster;
        else
            teamIDNumber = (int)E_TeamId.Player;



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
        if (aiCharacter.m_CharacterID != 0)
        {
            int id = 0;

            // Monster
            if (teamIDNumber == (int)E_TeamId.Monster)
            {
                Table_Monster.Info monsterData = Managers.Table.m_Monster.Get(aiCharacter.m_CharacterID);

                if (monsterData == null)
                    return;

                id = monsterData.m_iStatID;
            }

            // AI NPC
            else if (teamIDNumber == (int)E_TeamId.Player)
            {
                id = aiCharacter.m_CharacterID;
            }

            Table_Stat.Info statData = Managers.Table.m_Stat.Get(id);

            if (statData == null)
                return;

            m_sCharacterName = statData.m_sName;
            soulsAwardedOnDeath = statData.m_iRewardSouls;
            // Drop Reward Item
            maxHealth = statData.m_iHP;
            currentHealth = maxHealth;
            m_fPhysicalDamage = statData.m_PhysicalDamage;
            m_fMagicDamage = statData.m_MagicDamage;
            m_fFireDamage = statData.m_FireDamage;
            m_fLightningDamage = statData.m_LightningDamage;
            m_fDarkDamage = statData.m_DarkDamage;
        }

        // Poise
        m_fTotalPoiseDefence = m_fStatPoise;

        FullRecovery();
    }

    // 가져온 스텟을 이용해 능력치 정하기
    public override void InitAbility()
    {
    }

    // Current HP,Stamina FP 완전 회복
    public override void FullRecovery()
    {
        currentHealth = maxHealth;

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

    public override void AwardSoulsOnDeath()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
        player.playerStatsManager.AddSouls(aiCharacter.aiCharacterStatsManager.soulsAwardedOnDeath);
    }

    public override int CalculateMaxHP(int data)
    {
        throw new NotImplementedException();
    }
}
