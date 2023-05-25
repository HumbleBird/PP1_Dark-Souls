using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    PlayerManager playerManager;

    public HealthBar  healthBar;
    public StaminaBar staminaBar;
    public FocusPointBar focusPointBar;
    AnimatorHandler animatorHandler;

    public float staminaRegenerationAmount = 1;
    public float staminaRegenTimer = 0;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        focusPointBar = FindObjectOfType<FocusPointBar>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentHealth);

        maxfocusPoint = SetMaxfocusPointsFromStaminaLevel();
        currentFocusPoints = maxfocusPoint;
        focusPointBar.SetMaxFocusPoints(maxfocusPoint);
        focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
    }

    public  void TakeDamage(int damage)
    {
        if (playerManager.isInvulnerable)
            return;

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

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    private float SetMaxfocusPointsFromStaminaLevel()
    {
        maxfocusPoint = focusLevel * 10;
        return maxfocusPoint;
    }

    public void TakeStaminsDamage(int damage)
    {
        currentStamina -= damage;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if(playerManager.isInteracting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                currentStamina += staminaRegenerationAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }


    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void DeductFocusPoints(int focusPoints)
    {
        currentFocusPoints -= focusPoints;

        if(currentFocusPoints < 0 )
        {
            currentFocusPoints = 0;
        }

        focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
    }
}
