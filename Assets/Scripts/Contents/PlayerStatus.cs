using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    public HealthBar healthBar;

    AnimatorHandler animatorHandler;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    protected override void Init()
    {
        base.Init();

        healthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        healthBar.SetCurrentHealth(currentHealth);

        animatorHandler.PlayerTargetAnimation("Damage_01", true);

        if(currentHealth <= 0 )
        {
            currentHealth = 0;
            animatorHandler.PlayerTargetAnimation("Dead_01", true);
        }
    }
}
