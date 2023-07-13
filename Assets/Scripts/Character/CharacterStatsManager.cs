using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterAnimatorManager characterAnimatorManager;

    [Header("Team I.D")]
    public int teamIDNumber = 0;

    public int maxHealth;
    public int currentHealth;

    public float maxStamina;
    public float currentStamina;

    public float maxfocusPoint;
    public float currentFocusPoints;

    public int soulCount = 0;
    public int soulsAwardedOnDeath = 50;

    [Header("CHARACTER LEVEL")]
    public int playerLvel = 1;

    [Header("STAT LEVLELS")]
    public int healthLevel       = 10; 
    public int staminaLevel     = 10; // = Endurance
    public int focusLevel        = 10; // = Attunement
    public int poiseLevel        = 10;
    public int strengthLevel        = 10;
    public int dexterityLevel    = 10;
    public int intelligenceLevel = 10;
    public int faithLevel           = 10;


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

    public float fireDamageAbsorptionHead;
    public float fireDamageAbsorptionBody;
    public float fireDamageAbsorptionLegs;
    public float fireDamageAbsorptionHands;

    public bool isDead;

    protected virtual void Awake()
    {
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
    }

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    private void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation)
    {
        if (isDead)
            return;

        characterAnimatorManager.EraseHandIKForWeapon();

        float totalPhysicalDamageAbsorption = 1 -
            (1 - physicalDamageAbsorptionHead / 100) *
            (1 - physicalDamageAbsorptionBody / 100) *
            (1 - physicalDamageAbsorptionLegs / 100) *
            (1 - physicalDamageAbsorptionHands / 100);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        float totalfireDamageAbsorption = 1 -
            (1 - fireDamageAbsorptionHead / 100) *
            (1 - fireDamageAbsorptionBody / 100) *
            (1 - fireDamageAbsorptionLegs / 100) *
            (1 - fireDamageAbsorptionHands / 100);

        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalfireDamageAbsorption));

        float finalDamage = physicalDamage + fireDamage; // + fire + mage + lightning + dark Damage

        currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);


        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void TakePoisonDamage(int damage)
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


    public void DrainStaminaBasedOnAttackType()
    {

    }
}
