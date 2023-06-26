using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatus : CharacterStatus
{
    EnemyManager enemyManager;
    EnemyAnimationManager enemyAnimationManager;
    EnemyBossManager enemyBossManager;
    public UIEnemyHealthBar enemyHealthBar;
    public int soulsAwardedOnDeath = 50;

    public bool isBoss;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
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

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth = currentHealth - damage;

        if(!isBoss)
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
        }
    }

    public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
    {
        base.TakeDamage(damage, damageAnimation = "Damage_01");


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
