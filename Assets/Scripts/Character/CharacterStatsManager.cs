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
    // Stat Poise�� Poise Health�� ���ϴ� Poise Damage�� Stat Poise ��ŭ ���ҽ�Ų��.

    float fStatPoise;
    public float m_fStatPoise 
    { 
        set { fStatPoise = value; } 
        get { return Mathf.Floor(fStatPoise * 10f) / 10f; }
    } 
    // �ִ� Poise Value
    public float m_fPoiseHealth = 100; // ĳ���Ͱ� ������ ������ �� ���� ���� ��. 0�� �Ǹ� ���� ���� ���ο� ��� ���� ��Ʋ�Ÿ�.

    public float m_fTotalPoiseDefence; // ���� �� ���� ��, �����۾Ƹ� ������(��Ʈ�� ���, �׷���Ʈ �ظ�, 2�� ��� �� Ư�� WA) ������ �߰� Poise ���� ���� + Stat poise�� ��
    public float offensivePoiseBonus; // �����۾Ƹ� ������(��Ʈ�� ���, �׷���Ʈ �ظ�, 2�� ��� �� Ư�� WA) ������ �߰� Poise ���� ����
    public float totalPoiseResetTime = 15; // Poise Reset Time
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

    // �÷��̾� ����
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

    // ����, ����
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

    #region �Ӽ� ������
    [Header("Character Resistances")]
    public int m_iBleedResistance           ;
    public int m_iPoisonResistance          ;
    public int m_iFrostResistance           ;
    public int m_iCurseResistance           ;
    #endregion

    #region �Ӽ� ���� ����

    [Header("Character Armor")]
    public float m_fBleedArmor ;
    public float m_iPoisoArmore;
    public float m_fFrostArmor ;
    public float m_fCurseArmor ;

    #endregion

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        // �ɷ�ġ �ε�
    }

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    protected virtual void Start()
    {
    }

    // �ɷ�ġ�� CSV ���̺��� �ε�
    public abstract void LoadStat();

    // ������ ������ �̿��� �ɷ�ġ ���ϱ�
    public abstract void InitAbility();

    // Current HP,Stamina FP ���� ȸ��
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
