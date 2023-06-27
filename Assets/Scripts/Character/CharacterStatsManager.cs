using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 10;
    public float maxStamina;
    public float currentStamina;

    public int focusLevel = 10;
    public float maxfocusPoint;
    public float currentFocusPoints;

    public int soulCount = 0;
    public int soulsAwardedOnDeath = 50;

    [Header("Poise")]
    public float totalPoiseDefence; // poise 동안의 총 방어력
    public float offensivePoiseBonus; // 무기로 공격할 때 얻을 수 있는 자세
    public float armorPoiseBonus; // 장작 동안의 자세 보너스
    public float totalPoiseResetTime = 15;
    public float poiseResetTimer = 0;

    [Header("Armor Absorption")]
    public float physicalDamageAbsorptionHead;
    public float physicalDamageAbsorptionBody;
    public float physicalDamageAbsorptionLegs;
    public float physicalDamageAbsorptionHands;

    public bool isDead;

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    private void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    public virtual void TakeDamage(int physicalDamage, string damageAnimation = "Damage_01")
    {
        if (isDead)
            return;

        float totalPhysicalDamageAbsorption = 1 -
            (1 - physicalDamageAbsorptionHead / 100) *
            (1 - physicalDamageAbsorptionBody / 100) *
            (1 - physicalDamageAbsorptionLegs / 100) *
            (1 - physicalDamageAbsorptionHands / 100);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        Debug.Log("Total Damage Absorption is " + totalPhysicalDamageAbsorption + "%");

        float finalDamage = physicalDamage; // + fire + mage + lightning + dark Damage

        currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

        Debug.Log("Total Damage Default is " + finalDamage);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void TakeDamageNoAnimation(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void HandlePoiseResetTime()
    {
        if(poiseResetTimer > 0)
        {
            poiseResetTimer -= Time.deltaTime;
        }
        else
        {
            totalPoiseDefence = armorPoiseBonus;
        }
    }
}
