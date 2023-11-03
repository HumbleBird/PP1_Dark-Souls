using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Name")]
    public string m_sCharacterName = "Nameless";

    [Header("Team I.D")]
    public int teamIDNumber = 0;

    public int currentSoulCount = 0;
    
    [Header("Character Base Power")]
    public int maxHealth;
    public int currentHealth;

    public float blockingPhysicalDamageAbsorption;
    public float blockingFireDamageAbsorption;
    public float blockingStabilityRating;

    [Header("Attunement Slots")]
    public int m_iAttunementSlots = 0;

    [Header("Poise Details")]
    // Stat Poise는 Poise Health에 가하는 Poise Damage를 Stat Poise 만큼 감소시킨다.

    float fStatPoise;
    public float m_fStatPoise 
    { 
        set { fStatPoise = value; } 
        get { return Mathf.Floor(fStatPoise * 10f) / 10f; }
    } 
    // 최대 Poise Value
    public float m_fPoiseHealth = 100; // 캐릭터가 공격을 받으면 이 값도 감소 됨. 0이 되면 현재 공격 여부와 상관 없이 비틀거림.

    public float m_fTotalPoiseDefence; // 현재 총 균형 값, 하이퍼아머 프레임(울트라 대검, 그레이트 해머, 2손 대검 밑 특정 WA) 동안의 추가 Poise 증가 스텟 + Stat poise의 합
    public float offensivePoiseBonus; // 하이퍼아머 프레임(울트라 대검, 그레이트 해머, 2손 대검 밑 특정 WA) 동안의 추가 Poise 증가 스텟
    public float totalPoiseResetTime = 15; // Poise Reset Time
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

    #region Attack Value
    public float m_fPhysicalDamage;
    public float m_fMagicDamage;
    public float m_fFireDamage;
    public float m_fLightningDamage;
    public float m_fDarkDamage;
    public float m_fAuxiliaryEffectsBleedDamage;
    public float m_fAuxiliaryEffectsPoisonDamage;
    public float m_fAuxiliaryEffectsFrostDamage;
    public float m_fAuxiliaryEffectsCurseDamage;
    #endregion

    // 플레이어 레벨
    #region Defense 
    public int m_iPhysicalDefense                   ;
    public int m_iVSStrikeDefense                   ;
    public int m_iVSSlashDefense                   ;
    public int m_iVSThrustDefense                   ;
    public int m_iMagicDefense                   ;
    public int m_iFireDefense                   ;
    public int m_iLightningDefense                   ;
    public int m_iDarkDefense                   ;
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
    public int m_iBleedResistance           ;
    public int m_iPoisonResistance          ;
    public int m_iFrostResistance           ;
    public int m_iCurseResistance           ;
    #endregion

    #region 속성 값옷 방어력

    [Header("Character Armor")]
    public float m_fBleedArmor ;
    public float m_iPoisoArmore;
    public float m_fFrostArmor ;
    public float m_fCurseArmor ;

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
            m_fTotalPoiseDefence = m_fStatPoise;
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
