using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    PlayerManager player;

    public HealthBar  healthBar;
    public StaminaBar staminaBar;
    public FocusPointBar focusPointBar;

    public float staminaRegenerationAmount = 1;
    public float staminaRegenerationAmountWhilstBlocking = 0.1f;
    public float staminaRegenTimer = 0;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();

        staminaBar = FindObjectOfType<StaminaBar>();
        focusPointBar = FindObjectOfType<FocusPointBar>();
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
        staminaBar.SetCurrentStamina(currentStamina);

        maxfocusPoint = SetMaxfocusPointsFromStaminaLevel();
        currentFocusPoints = maxfocusPoint;
        focusPointBar.SetMaxFocusPoints(maxfocusPoint);
        focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
    }

    public override void HandlePoiseResetTime()
    {
        base.HandlePoiseResetTime();

        if (poiseResetTimer > 0)
        {
            poiseResetTimer -= Time.deltaTime;
        }
        else if (poiseResetTimer <= 0 && !player.isInteracting)
        {
            totalPoiseDefence = armorPoiseBonus;
        }
    }


    public override void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        base.TakeDamageNoAnimation(damage, fireDamage);

        healthBar.SetCurrentHealth(currentHealth);


    }

    public override void TakePoisonDamage(int damage)
    {
        if (player.isDead)
            return;

        base.TakePoisonDamage(damage);
        healthBar.SetCurrentHealth(currentHealth);


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            player.isDead = true;
            player.playerAnimatorManager.PlayTargetAnimation("Dead_01", true);
        }
    }

    public override void DeductStamina(float staminaToDeduct)
    {
        base.DeductStamina(staminaToDeduct);
        staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
    }



    public void RegenerateStamina()
    {
        if(player.isInteracting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                if(player.isBlocking)
                {
                    currentStamina += staminaRegenerationAmountWhilstBlocking * Time.deltaTime;
                }
                else
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                }

                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));

            }
        }


    }

    public override void HealCharacter(int healAmount)
    {
        base.HealCharacter(healAmount);

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

    public void AddSouls(int souls)
    {
        currentSoulCount += souls;
    }
}
