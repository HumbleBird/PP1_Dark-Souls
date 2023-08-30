using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class AICharacterStatsManager : CharacterStatsManager
{
    AICharacterManager aiCharacter;

    public UIAICharacterHealthBar aiCharacterHealthBar;

    public bool isBoss;

    protected override void Awake()
    {
        base.Awake();
        aiCharacter = GetComponent<AICharacterManager>();
        aiCharacterHealthBar = Managers.UI.MakeWorldSpaceUI<UIAICharacterHealthBar>(transform);

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

    }

    protected override void Start()
    {
        base.Start();
        if(!isBoss)
        {
             aiCharacterHealthBar.SetMaxHealth(maxHealth);
        }
    }

    public override void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        base.TakeDamageNoAnimation(damage, fireDamage);

        if(!isBoss)
        {
            aiCharacterHealthBar.SetHealth(currentHealth);

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
            aiCharacterHealthBar.SetHealth(currentHealth);

        }
        else if (isBoss && aiCharacter.aiCharacterBossManager != null)
        {
            aiCharacter.aiCharacterBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            aiCharacter.isDead = true;
            aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Dead_01", true);
        }
    }

    public void BreakGuard()
    {
        aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Break Guard", true);
    }


    private void HandleDeath()
    {
        currentHealth = 0;
        aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Dead_01", true);
        aiCharacter.isDead = true;
    }

    public override void UpdateUI()
    {
        base.UpdateUI();

        aiCharacterHealthBar.SetHealth(currentHealth);
    }
}
