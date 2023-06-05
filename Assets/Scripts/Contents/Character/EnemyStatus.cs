using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatus : CharacterStatus
{
    EnemyAnimationManager enemyAnimationManager;

    public int soulsAwardedOnDeath = 50;

    private void Awake()
    {
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public  void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth = currentHealth - damage;

        enemyAnimationManager.PlayerTargetAnimation("Damage_01", true);

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
