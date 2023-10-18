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

        SetAbilityValueFromLevel();
    }

    public override int SetMaxHealth()
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
            aiCharacter.aiCharacterBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

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
            aiCharacter.aiCharacterBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

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

    public override void SetAbilityValueFromLevel()
    {
        maxHealth = SetMaxHealth();
    }

    public override void InitStats()
    {
        currentHealth = maxHealth;
    }
}
