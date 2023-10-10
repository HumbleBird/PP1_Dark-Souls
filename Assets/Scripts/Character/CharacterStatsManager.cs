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

    public int currentSoulCount = 0;


    
    [Header("Character Base Power")]
    // HP
    public int maxHealth;
    public int currentHealth;

    

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
    }

    public virtual int SetMaxHealth()
    {
        // 플레이어는 vigorlevel에 따라 결정

        // 그 외는 전부 테이블에서 가져오기
        return 0;
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

    public virtual void Dead()
    {
        // 몬스터가 죽었다면 기본적으로 소울 및 아이템 추가
        // 보스 : 보스 관련 이벤트 실행, 화톳불 생성 가능

        // 플레이어 : 게임 매니저에서 모든 몬스터 재배치, 소울 잃어버림, 시작 지점 스폰, 현재 모든 체력 및 기타 회복
    }
}
