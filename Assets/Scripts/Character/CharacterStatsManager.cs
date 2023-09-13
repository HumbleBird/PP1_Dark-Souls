using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Name")]
    public string characterName = "Nameless";

    [Header("Team I.D")]
    public int teamIDNumber = 0;

    public int maxHealth;
    public int currentHealth;

    public float maxStamina;
    public float currentStamina;

    public float maxfocusPoint;
    public float currentFocusPoints;

    public int currentSoulCount = 0;
    public int soulsAwardedOnDeath = 50;

    [Header("CHARACTER LEVEL")]
    public int playerLevel;

    [Header("STAT LEVLELS")]
    public int healthLevel = 10; 
    public int staminaLevel = 10; // = Endurance
    public int focusLevel = 10; // = Attunement
    public int poiseLevel = 10;
    public int strengthLevel = 10;
    public int dexterityLevel = 10;
    public int intelligenceLevel = 10;
    public int faithLevel = 10;

    [Header("Equip Load")]
    public float currentEquipLoad = 0;
    public float maxEquipLoad = 0;
    public EncumbranceLevel encumbranceLevel;

    [Header("Poise")]
    public float totalPoiseDefence; // poise ������ �� ����
    public float offensivePoiseBonus; // ����� ������ �� ���� �� �ִ� �ڼ�
    public float armorPoiseBonus; // ���� ������ �ڼ� ���ʽ�
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

    [Header("Resistance")]
    public float poisonResistance;

    [Header("Blocking Absorptions")]
    public float blockingPhysicalDamageAbsorption;
    public float blockingFireDamageAbsorption;
    public float blockingStabilityRating;

    // �� �÷��̾ ó���� ��� ���ش� �� �ݾ׿� ���� �����˴ϴ�
    [Header("Damage Type Modifiers")]
    public float physicalDamagePercentageModifier = 100;
    public float fireDamagePercentageModifier = 100;

    [Header("Damage Absorption Modifiers")]
    public float physicalAbsorptionPercentageModifier = 0;
    public float fireAbsorptionPercentageModifier = 0;

    [Header("Poison")]
    public bool isPoisoned;
    public float poisonBuildup = 0; // �� ��ġ 100�� ������ �� �÷��̾ �ߵ���Ű�� �ð� ����� ���� �����
    public float poisonAmount = 100; // �÷��̾ ����ȭ�Ǳ� ���� ó���ؾ� �ϴ� ���� ��

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    protected virtual void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
        //CalculateAndSetMaxEquipload();
    }


    public virtual void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            character.isDead = true;
        }
    }

    public virtual void TakePoisonDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            character.isDead = true;
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


    public virtual void DeductStamina(float staminaToDeduct)
    {
        currentStamina -= staminaToDeduct;
    }

    public int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public float SetMaxfocusPointsFromStaminaLevel()
    {
        maxfocusPoint = focusLevel * 10;
        return maxfocusPoint;
    }

    public void CalculateAndSetMaxEquipload()
    {
        float totalEquipLoad = 40;

        for (int i = 0; i < staminaLevel; i++)
        {
            if(i < 25)
            {
                totalEquipLoad += 1.2f;
            }
            if(i >= 25 && i <= 50)
            {
                totalEquipLoad += 1.4f;

            }
            if (i > 50)
            {
                totalEquipLoad += 1f;

            }
        }

        maxEquipLoad = totalEquipLoad;
    }

    public void CaculateAndSetCurrentEquipLoad(float equipLoad)
    {
        currentEquipLoad = equipLoad;

        encumbranceLevel = EncumbranceLevel.Light;

        if(currentEquipLoad > (maxEquipLoad * 0.3f))
        {
            encumbranceLevel = EncumbranceLevel.Medium;
        }
        if(currentEquipLoad > (maxEquipLoad * 0.7f))
        {
            encumbranceLevel = EncumbranceLevel.Heavy;
        }
        if(currentEquipLoad > (maxEquipLoad))
        {
            encumbranceLevel = EncumbranceLevel.Overloaded;
        }
    }

    public virtual void HealCharacter(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void UpdateUI()
    {

    }
}
