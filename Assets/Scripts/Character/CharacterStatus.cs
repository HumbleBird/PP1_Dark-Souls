using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
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

    [Header("Armor Absorption")]
    public float physicalDamageAbsorptionHead;
    public float physicalDamageAbsorptionBody;
    public float physicalDamageAbsorptionLegs;
    public float physicalDamageAbsorptionHands;

    public bool isDead;

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
}
