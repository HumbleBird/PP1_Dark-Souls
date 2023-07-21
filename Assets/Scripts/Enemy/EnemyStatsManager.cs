using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatsManager : CharacterStatsManager
{
    EnemyManager enemy;


    public UIEnemyHealthBar enemyHealthBar;

    public bool isBoss;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyManager>();

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
        else if (isBoss && enemy.enemyBossManager != null)
        {
            enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

        }
    }

    public override void TakePoisonDamage(int damage)
    {
        if (enemy.isDead)
            return;

        base.TakePoisonDamage(damage);

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);

        }
        else if (isBoss && enemy.enemyBossManager != null)
        {
            enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemy.isDead = true;
            enemy.enemyAnimationManager.PlayerTargetAnimation("Dead_01", true);
        }
    }

    public void BreakGuard()
    {
        enemy.enemyAnimationManager.PlayerTargetAnimation("Break Guard", true);
    }

    public override void TakeDamage(int damage, int fireDamage, string damageAnimation, CharacterManager enemyCharacterDamageingMe)
    {
        base.TakeDamage(damage, fireDamage, damageAnimation,  enemyCharacterDamageingMe);


        if (!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);
        }
        else if (isBoss && enemy.enemyBossManager != null)
        {
            enemy.enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
        }


        enemy.enemyAnimationManager.PlayerTargetAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        currentHealth = 0;
        enemy.enemyAnimationManager.PlayerTargetAnimation("Dead_01", true);
        enemy.isDead = true;
    }
}
