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

    // �÷��̾� ����
    #region Defense 
    public int m_fPhysicalDefense;
    public int m_fVSStrikeDefense;
    public int m_fVSSlashDefense;
    public int m_fVSThrustDefense;
    public int m_fMagicDefense;
    public int m_fFireDefense;
    public int m_fLightningDefense;
    public int m_fDarkDefense;
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
    public int m_fBleedResistance;
    public int m_iPoisonArmorResistance;
    public int m_fFrostResistance;
    public int m_fCurseResistance;
    #endregion

    #region �Ӽ� ����

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

        // �ɷ�ġ �ε�
    }

    protected virtual void Update()
    {
        HandlePoiseResetTime();
    }

    protected virtual void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
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
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    public virtual void DeductStamina(float staminaToDeduct)
    {
    }

    protected abstract int SetMaxHealth();

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
