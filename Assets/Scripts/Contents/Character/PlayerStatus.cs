using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    public HealthBar  healthBar;
    public StaminaBar staminaBar;

    AnimatorHandler animatorHandler;

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    protected override void Init()
    {
        base.Init();

        healthBar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        staminaBar.SetMaxStamina(maxStamina);

        currentStamina = maxStamina;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (isDead)
            return;

        currentHealth = currentHealth - damage;

        healthBar.SetCurrentHealth(currentHealth);

        animatorHandler.PlayerTargetAnimation("Damage_01", true);

        if(currentHealth <= 0 )
        {
            currentHealth = 0;
            animatorHandler.PlayerTargetAnimation("Dead_01", true);
            isDead = true;
        }
    }

    private int SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public void TakeStaminsDamage(int damage)
    {
        currentStamina -= damage;
        staminaBar.SetCurrentStamina(currentStamina);
    }
}
