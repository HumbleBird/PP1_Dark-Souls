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

    [Header("CHARACTER LEVEL")]
    public int playerLevel;
    public int currentSoulCount = 0;

    [Header("Character Attributes Stat")]
    public int m_iVigorLevel        = 10    ; // �����. �ִ� ������� ����
    public int m_iAttunementLevel   = 10    ; // ���߷�. �ִ� FP�� ����
    public int m_iEnduranceLevel    = 10    ; // ������. �ִ� ���׹̳ʰ� ����
    public int m_iVitalityLevel     = 10    ; // ü��. ����߷��� ���� ����, �� ������ ����
    public int m_iStrengthLevel     = 10    ; // �ٷ�. �ٷ� ������ �޴� ������ ���ݷ°� ȭ�� ����, ���� ������ ���, �տ� �� ��� �������ϸ� �� ������ 1.5��� ���
    public int m_iDexterityLevel    = 10    ; // �ⷮ. �ⷮ ������ �޴� ������ ���ݷ��� ���
    public int m_iIntelligenceLevel = 10    ; // ����. ������ �ּ��� ������ ���, ���� ������ ����
    public int m_iFaithLevel        = 10    ; // �ž�. ������ �ּ��� ������ ���, ��� ������ ����
    public int m_iLuckLevel         = 10    ; // ��. �������� �߰����� �Ӽ� ����ġ�� �����.

    [Header("Character Base Power")]
    // HP
    public int maxHealth;
    public int currentHealth;

    // FP
    public float maxfocusPoint;
    public float currentFocusPoints;

    // Stamina
    public float maxStamina;
    public float currentStamina;

    // Equip Load
    public float currentEquipLoad = 0;
    public float maxEquipLoad = 0;
    public EncumbranceLevel encumbranceLevel;

    // Poise
    public int poiseLevel         = 10;

    // Item Discovery
    public int m_iItemDiscovery = 10;

    [Header("Character Attack Power")]
    public int m_iRWeapon1;
    public int m_iRWeapon2;
    public int m_iRWeapon3;
    public int m_iLWeapon1;
    public int m_iLWeapon2;
    public int m_iLWeapon3;

    [Header("Character Defense")]
    public float physicalDamageAbsorptionHead;
    public float physicalDamageAbsorptionBody;
    public float physicalDamageAbsorptionLegs;
    public float physicalDamageAbsorptionHands;

    public float fireDamageAbsorptionHead;
    public float fireDamageAbsorptionBody;
    public float fireDamageAbsorptionLegs;
    public float fireDamageAbsorptionHands;

    public float blockingPhysicalDamageAbsorption;
    public float blockingFireDamageAbsorption;
    public float blockingStabilityRating;

    public float m_fMagicDefense;
    public float m_fLightningDefense;
    public float m_fDarkDefense;

    [Header("Character Resistances")]
    public float poisonResistance;
    public float m_fBleedResistance;
    public float m_fFrostResistance;
    public float m_fCurseResistance;

    [Header("Attunement Slots")]
    public int AttunementSlots = 0;

    [Header("Poise Details")]
    public float totalPoiseDefence; // poise ������ �� ����
    public float offensivePoiseBonus; // ����� ������ �� ���� �� �ִ� �ڼ�
    public float armorPoiseBonus;     // ���� ������ �ڼ� ���ʽ�
    public float totalPoiseResetTime = 15;
    public float poiseResetTimer = 0;

    // �� �÷��̾ ó���� ��� ���ش� �� �ݾ׿� ���� �����˴ϴ�
    [Header("Damage Type Modifiers")]
    public float physicalDamagePercentageModifier = 100;
    public float fireDamagePercentageModifier = 100;

    [Header("Damage Absorption Modifiers")]
    public float physicalAbsorptionPercentageModifier = 0;
    public float fireAbsorptionPercentageModifier = 0;

    [Header("Poison Detail")]
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
        maxHealth = m_iVigorLevel * 10;
        return maxHealth;
    }

    public float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = m_iEnduranceLevel * 10;
        return maxStamina;
    }

    public float SetMaxfocusPointsFromStaminaLevel()
    {
        maxfocusPoint = m_iAttunementLevel * 10;
        return maxfocusPoint;
    }

    public void CalculateAndSetMaxEquipload()
    {
        float totalEquipLoad = 40;

        for (int i = 0; i < m_iEnduranceLevel; i++)
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

    public void CalculateStrength()
    {

    }

    public void CalculateDexterity()
    {

    }

    public void CalculateIntelligence()
    {

    }

    public void CalculateFaith()
    {

    }

    public void CalculateLuck()
    {

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
