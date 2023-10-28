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
        aiCharacterHealthBar = Managers.UI.MakeWorldSpaceUI<UIAICharacterHealthBar>(transform);

        teamIDNumber = (int)E_TeamId.Monster;
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
        maxHealth = CalculateMaxHP(0);

        FullRecovery();
    }

    // Current HP,Stamina FP 완전 회복
    public override void FullRecovery()
    {
        currentHealth = maxHealth;

    }

    public override int CalculateMaxHP(int MonsterId)
    {
        // 플레이어는 vigorlevel에 따라 결정

        // 그 외는 전부 테이블에서 가져오기
        return 100;
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
            aiCharacter.aiCharacterBossManager.RefreshUI();

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
            aiCharacter.aiCharacterBossManager.RefreshUI();

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
        aiCharacterHealthBar.RefreshUI(damage);

    }
}
