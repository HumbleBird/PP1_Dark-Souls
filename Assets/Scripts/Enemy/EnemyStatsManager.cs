using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatsManager : CharacterStatsManager
{
    EnemyManager enemyManager;
    EnemyAnimationManager enemyAnimationManager;
    EnemyBossManager enemyBossManager;
    public UIEnemyHealthBar enemyHealthBar;

    public bool isBoss;

    protected override void Awake()
    {
        base.Awake();
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponent<EnemyAnimationManager>();
        enemyBossManager = GetComponent<EnemyBossManager>();

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    void Start()
    {
        if(!isBoss)
        {
             enemyHealthBar.SetMaxHealth(maxHealth);
        }
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public override void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        base.TakeDamageNoAnimation(damage, fireDamage);

        if(!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);

        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

        }
    }

    public override void TakePoisonDamage(int damage)
    {
        if (isDead)
            return;

        base.TakePoisonDamage(damage);

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);

        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            enemyAnimationManager.PlayerTargetAnimation("Dead_01", true);
        }
    }

    public void BreakGuard()
    {
        enemyAnimationManager.PlayerTargetAnimation("Break Guard", true);
    }

    public override void TakeDamage(int damage, int fireDamage, string damageAnimation)
    {
        base.TakeDamage(damage, fireDamage, damageAnimation);


        if (!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);
        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
        }


        enemyAnimationManager.PlayerTargetAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        currentHealth = 0;
        enemyAnimationManager.PlayerTargetAnimation("Dead_01", true);
        isDead = true;
    }
}
