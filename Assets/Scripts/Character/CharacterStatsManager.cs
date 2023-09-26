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
    public int m_iVigorLevel        = 10    ; // 생명력. 최대 생명력이 오름
    public int m_iAttunementLevel   = 10    ; // 집중력. 최대 FP가 오름
    public int m_iEnduranceLevel    = 10    ; // 지구력. 최대 스테미너가 오름
    public int m_iVitalityLevel     = 10    ; // 체력. 장비중량과 물리 방어력, 독 내성이 오름
    public int m_iStrengthLevel     = 10    ; // 근력. 근력 보정을 받는 무기의 공격력과 화염 내성, 물리 방어력을 상승, 손에 든 장비를 양손잡기하면 현 스탯의 1.5배로 계산
    public int m_iDexterityLevel    = 10    ; // 기량. 기량 보정을 받는 무기의 공격력이 상승
    public int m_iIntelligenceLevel = 10    ; // 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름
    public int m_iFaithLevel        = 10    ; // 신앙. 기적과 주술의 위력이 상승, 어둠 방어력이 오름
    public int m_iLuckLevel         = 10    ; // 운. 아이템의 발견율과 속성 내성치가 상승함.

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
    public float totalPoiseDefence; // poise 동안의 총 방어력
    public float offensivePoiseBonus; // 무기로 공격할 때 얻을 수 있는 자세
    public float armorPoiseBonus;     // 장작 동안의 자세 보너스
    public float totalPoiseResetTime = 15;
    public float poiseResetTimer = 0;

    // 이 플레이어가 처리한 모든 피해는 이 금액에 의해 수정됩니다
    [Header("Damage Type Modifiers")]
    public float physicalDamagePercentageModifier = 100;
    public float fireDamagePercentageModifier = 100;

    [Header("Damage Absorption Modifiers")]
    public float physicalAbsorptionPercentageModifier = 0;
    public float fireAbsorptionPercentageModifier = 0;

    [Header("Poison Detail")]
    public bool isPoisoned;
    public float poisonBuildup = 0; // 독 수치 100에 도달한 후 플레이어를 중독시키는 시간 경과에 따른 빌드업
    public float poisonAmount = 100; // 플레이어가 무독화되기 전에 처리해야 하는 독의 양

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
