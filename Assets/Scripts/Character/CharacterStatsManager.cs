using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class CharacterStatsManager : MonoBehaviour
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

    public float blockingPhysicalDamageAbsorption;
    public float blockingFireDamageAbsorption;
    public float blockingStabilityRating;

    [Header("Attunement Slots")]
    public int m_iAttunementSlots = 0;

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

    // 플레이어 레벨
    #region Defense 
    public int m_iPhysicalDefense;
    public int m_iVSStrikeDefense;
    public int m_iVSSlashDefense;
    public int m_iVSThrustDefense;
    public int m_iMagicDefense;
    public int m_iFireDefense;
    public int m_iLightningDefense;
    public int m_iDarkDefense;
    #endregion

    // 갑옷, 무기
    #region Absorption
    [Header("Character Defense")]
    public float m_fPhysicalDamageAbsorption        ;
    public float m_fVSStrikeDamageAbsorption            ;
    public float m_fVSSlashDamageAbsorption         ;
    public float m_fVSThrustDamageAbsorption            ;
    public float m_fMagicDamageAbsorption           ;
    public float m_fFireDamageAbsorption            ;
    public float m_fLightningDamageAbsorption           ;
    public float m_fDarkDamageAbsorption            ;

    #endregion

    #region 속성 감소율
    [Header("Character Resistances")]
    public int m_iBleedResistance;
    public int m_iPoisonResistance;
    public int m_iFrostResistance;
    public int m_iCurseResistance;
    #endregion

    #region 속성 값옷 방어력

    [Header("Character Armor")]
    public float m_fBleedArmor;
    public float m_iPoisoArmore;
    public float m_fFrostArmor;
    public float m_fCurseArmor;

    #endregion

    #region Speical Effect Armor

    #endregion

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        // 능력치 로드
    }

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    protected virtual void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    // 능력치를 CSV 테이블에서 로드
    public abstract void LoadStat();

    // 가져온 스텟을 이용해 능력치 정하기
    public abstract void InitAbility();

    // Current HP,Stamina FP 완전 회복
    public abstract void FullRecovery();

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

    public abstract int CalculateMaxHP(int data);

    public virtual void HealCharacter(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void HealthBarUIUpdate(int Damage)
    {

    }
}
