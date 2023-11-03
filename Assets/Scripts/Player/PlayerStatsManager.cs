using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerStatsManager : CharacterStatsManager
{
    PlayerManager player;

    [Header("CHARACTER LEVEL")]
    public int playerLevel;

    [Header("Character Attributes Stat")]
    public int m_iVigorLevel = 10; // �����. �ִ� ������� ����
    public int m_iAttunementLevel = 10; // ���߷�. �ִ� FP�� ����
    public int m_iEnduranceLevel = 10; // ������. �ִ� ���׹̳ʰ� ����
    public int m_iVitalityLevel = 10; // ü��. ����߷��� ���� ����, �� ������ ����
    public int m_iStrengthLevel = 10; // �ٷ�. �ٷ� ������ �޴� ������ ���ݷ°� ȭ�� ����, ���� ������ ���, �տ� �� ��� �������ϸ� �� ������ 1.5��� ���
    public int m_iDexterityLevel = 10; // �ⷮ. �ⷮ ������ �޴� ������ ���ݷ��� ���
    public int m_iIntelligenceLevel = 10; // ����. ������ �ּ��� ������ ���, ���� ������ ����
    public int m_iFaithLevel = 10; // �ž�. ������ �ּ��� ������ ���, ��� ������ ����
    public int m_iLuckLevel = 10; // ��. �������� �߰����� �Ӽ� ����ġ�� �����.

    // FP
    public int maxfocusPoint;
    public int currentFocusPoints;

    // Stamina
    public int maxStamina = 1;
    public int currentStamina = 1;

    // Equip Load
    float currentEqiupLoad = 0;
    public float m_CurrentEquipLoad { set { currentEqiupLoad = value; } get { return Mathf.Floor(currentEqiupLoad * 10f) / 10f; } }
    float maxEquipLoad = 0;
    public float m_MaxEquipLoad { set { maxEquipLoad = value; } get { return Mathf.Floor(maxEquipLoad * 10f) / 10f; } }
    public EncumbranceLevel encumbranceLevel;



    // Item Discovery
    public int m_iItemDiscovery = 10;

    [Header("Character Attack Power")]
    public int m_iRWeapon1;
    public int m_iRWeapon2;
    public int m_iRWeapon3;
    public int m_iLWeapon1;
    public int m_iLWeapon2;
    public int m_iLWeapon3;

    public float staminaRegenerationAmount = 20;
    public float staminaRegenerationAmountWhilstBlocking = 0.1f;
    public float staminaRegenTimer = 0;

    public float spritingTimer = 0;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
        teamIDNumber = (int)E_TeamId.Player;
    }

    protected override void Start()
    {
        base.Start();

        LoadStat();
    }

    // �ɷ�ġ�� CSV ���̺��� �ε�
    public override void LoadStat()
    {
        if (Managers.Game.m_isNewGame == false)
        {
            // �ɷ�ġ �ε�
        }

        InitAbility();
    }

    // ������ ������ �̿��� �ɷ�ġ ���ϱ�
    public override void InitAbility()
    {
        // Stat Clear
        m_iPhysicalDefense = 0;

        // Vigor �����. �ִ� ������� ����
        CalculateVigor();

        // Attunement ���߷�. �ִ� FP�� ����
        CalculateAttunement();

        // Endurance ������. �ִ� ���׹̳ʰ� ����
        CalculateEndurance();

        // Vitality ü��. ����߷��� ���� ����, �� ������ ����
        CalculateVitality();

        // Strength �ٷ�. �ٷ� ������ �޴� ������ ���ݷ°� ȭ�� ����, ���� ������ ���, �տ� �� ��� �������ϸ� �� ������ 1.5��� ���
        CalculateStrength();

        // Dexterity �ⷮ. �ⷮ ������ �޴� ������ ���ݷ��� ���
        CalculateDexterity();

        // Intelligence ����. ������ �ּ��� ������ ���, ���� ������ ����
        CalculateIntelligence();

        // Faith �ž�. ������ �ּ��� ������ ���, ��� ������ ����
        CalculateFaith();

        // Luck ��. �������� �߰����� �Ӽ� ����ġ�� �����.
        CalculateLuck();

        // Poise
        m_fTotalPoiseDefence = m_fStatPoise;

        // ���� HP, FP, Stamina ���� ȸ��
        FullRecovery();

        // HUD Bar Refresh
        if(Managers.GameUI.m_GameSceneUI != null)
             Managers.GameUI.m_GameSceneUI.m_StatBarsUI.SetBGWidthUI(E_StatUI.All);
    }

    // Current HP,Stamina FP ���� ȸ��
    public override void FullRecovery()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentFocusPoints = maxfocusPoint;
        
        if(Managers.GameUI.m_GameSceneUI != null)
            Managers.GameUI.m_GameSceneUI.m_StatBarsUI.RefreshUI();
    }

    public override void HandlePoiseResetTime()
    {
        base.HandlePoiseResetTime();

        if (poiseResetTimer > 0)
        {
            poiseResetTimer -= Time.deltaTime;
        }
        else if (poiseResetTimer <= 0 && !player.isInteracting)
        {
            m_fTotalPoiseDefence = m_fStatPoise;
        }
    }

    public override void TakeDamageNoAnimation(int damage, int fireDamage)
    {
        base.TakeDamageNoAnimation(damage, fireDamage);

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
    }

    public override void TakePoisonDamage(int damage)
    {
        if (player.isDead)
            return;

        base.TakePoisonDamage(damage);
        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);


        if (currentHealth <= 0)
        {
            player.Dead();
            player.playerAnimatorManager.PlayTargetAnimation("Dead_01", true);
        }
    }

    public override void DeductStamina(float staminaToDeduct)
    {
        currentStamina -= Mathf.RoundToInt( staminaToDeduct);
        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Stamina);
    }

    public void DeductSprintingStamina(int staminaToDeduct)
    {
        if (player.isSprinting)
        {
            spritingTimer += Time.deltaTime;

            if (spritingTimer > 0.1f)
            {
                spritingTimer = 0;
                currentStamina -= staminaToDeduct;
                player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Stamina);
            }
        }
        else
        {
            spritingTimer = 0;
        }
    }

    public void RegenerateStamina()
    {
        // �׼� ���̰ų� �޸� �� GENERATE STAMINA  ���� ����
        if (player.isInteracting || player.isSprinting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                if (player.isBlocking)
                {
                    currentStamina += Mathf.RoundToInt(staminaRegenerationAmountWhilstBlocking * Time.deltaTime);
                }
                else
                {
                    currentStamina += Mathf.RoundToInt(staminaRegenerationAmount * Time.deltaTime);
                }

                player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Stamina);

            }
        }


    }

    public override void HealCharacter(int healAmount)
    {
        base.HealCharacter(healAmount);

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
    }

    public void DeductFocusPoints(int focusPoints)
    {
        currentFocusPoints -= focusPoints;

        if (currentFocusPoints < 0)
        {
            currentFocusPoints = 0;
        }

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.FocusPoint);
    }

    public void AddSouls(int souls)
    {
        currentSoulCount += souls;
        player.m_GameSceneUI.SoulsRefreshUI(true, souls);
    }

    public override void HealthBarUIUpdate(int damage)
    {
        base.HealthBarUIUpdate(damage);

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
    }


    #region Set Abillity  From Level

    // Vigor
    // HP, Frost Resistance
    public void CalculateVigor()
    {
        // Vigor �����. �ִ� ������� ����
        maxHealth = CalculateMaxHP(m_iVigorLevel);
        m_iFrostResistance = CalculateFrostResistance(m_iVigorLevel);
    }

    // Attunement
    // FP, Attunement Slot
    public void CalculateAttunement()
    {
        maxfocusPoint = CalculateMaxfocusPoint(m_iAttunementLevel);
        m_iAttunementSlots = CalculateAttunementSlot(m_iAttunementLevel);
    }

    // Endurance
    // Stamina, Lightning Resistance, Bleed Resistance
    public void CalculateEndurance()
    {
        maxStamina = CalculateMaxStamina(m_iEnduranceLevel);
        m_iLightningDefense =  CalculateLightningDefense(m_iEnduranceLevel);
        m_iBleedResistance =  CalculateBleedResistance(m_iEnduranceLevel);
    }

    // Vitality
    // Equip Load, Physical Defense, Posion Resistance
    public void CalculateVitality()
    {
        m_MaxEquipLoad = CalculateAndSetMaxEquipload(m_iVitalityLevel);
        m_iPhysicalDefense += CalculatePhysicalDefenseFromVitalityLevel(m_iVitalityLevel);
        m_iPoisonResistance = CalculatePosionResistance(m_iVitalityLevel);
    }

    // Strength
    // �ٷ� ������ �޴� ������ ���ݷ�, ���� ���, ȭ�� ����
    void CalculateStrength()
    {
        // TODO �ٷ� ������ �޴� ������ ���ݷ�

        m_iPhysicalDefense += CalculatePhysicalDefenseFromStrengthLevel(m_iStrengthLevel);
        m_iFireDefense = CalculateFireDefense(m_iStrengthLevel);
    }

    // Dexterity
    // �ⷮ ������ �޴� ������ ���ط� ����, ��â �ð� �پ��, ���� ������ ����

    void CalculateDexterity()
    {
        // TODO
        // �ⷮ ������ �޴� ������ ���ط� ����, ��â �ð� �پ��, ���� ������ ����
    }

    // Intelligence
    // ����. ������ �ּ��� ������ ���, ���� ������ ����
    void CalculateIntelligence()
    {
        // TODO ������ �ּ��� ������ ���
        m_iMagicDefense = CalculateMagicDefense(m_iMagicDefense);
    }

    // Faith
    // �ž�
    // ������ �ּ��� ������ ���, ��� ������ ����
    void CalculateFaith()
    {
        // TODO ������ �ּ��� ������ ���

        m_iDarkDefense = CalculateDarkDefense(m_iFaithLevel);
    }

    // Luck
    // �������� �߰����� ���� �Ӽ� ����ġ�� �����.
    // Ư�� ���� �� �� ������ ���� ������ ����
    void CalculateLuck()
    {
        m_iItemDiscovery = CalculateItemDiscovery(m_iLuckLevel);
        m_iCurseResistance = CalculateCurseResistance(m_iLuckLevel);
    }
    #endregion

    #region Detail Abililty Stat

    // Vigor
    // HP, Frost Resistance
    public override int CalculateMaxHP(int level)
    {
        return level * 10;
    }

    public int CalculateFrostResistance(int level)
    {
        return level * 7;
    }

    // Attunement
    // FP, Attunement Slot
    public int CalculateMaxfocusPoint(int Attunementlevel)
    {
        return Attunementlevel * 10;
    }

    public int CalculateAttunementSlot(int Attunementlevel)
    {
        int slotCount = 0;
        if (Attunementlevel >= 10)
            slotCount++;
        if (Attunementlevel >= 14)
            slotCount++;
        if (Attunementlevel >= 18)
            slotCount++;
        if (Attunementlevel >= 24)
            slotCount++;
        if (Attunementlevel >= 30)
            slotCount++;
        if (Attunementlevel >= 40)
            slotCount++;
        if (Attunementlevel >= 50)
            slotCount++;
        if (Attunementlevel >= 60)
            slotCount++;
        if (Attunementlevel >= 80)
            slotCount++;
        if (Attunementlevel >= 99)
            slotCount++;

        return slotCount;
    }

    // Endurance
    // Stamina, Lightning Resistance, Bleed Resistance

    public int CalculateMaxStamina(int EnduranceLevel)
    {
        return EnduranceLevel * 10;
    }

    public int CalculateLightningDefense(int Endurancelevel)
    {
        return Endurancelevel * 10;
    }

    public int CalculateBleedResistance(int Endurancelevel)
    {
        return Endurancelevel * 10;
    }

    // Vitality
    // Equip Load, Physical Defense, Posion Resistance

    public float CalculateAndSetMaxEquipload(int EnduranceLevel)
    {
        float totalEquipLoad = 40;

        for (int i = 0; i < EnduranceLevel; i++)
        {
            if (i < 25)
            {
                totalEquipLoad += 1.2f;
            }
            if (i >= 25 && i <= 50)
            {
                totalEquipLoad += 1.4f;

            }
            if (i > 50)
            {
                totalEquipLoad += 1f;

            }
        }

        return totalEquipLoad;
    }

    public void CaculateAndSetCurrentEquipLoad(float equipLoad)
    {
        m_CurrentEquipLoad = equipLoad;

        encumbranceLevel = EncumbranceLevel.Light;

        if (m_CurrentEquipLoad > (m_MaxEquipLoad * 0.3f))
        {
            encumbranceLevel = EncumbranceLevel.Medium;
        }
        if (m_CurrentEquipLoad > (m_MaxEquipLoad * 0.7f))
        {
            encumbranceLevel = EncumbranceLevel.Heavy;
        }
        if (m_CurrentEquipLoad > (m_MaxEquipLoad))
        {
            encumbranceLevel = EncumbranceLevel.Overloaded;
        }
    }

    public int CalculatePhysicalDefenseFromVitalityLevel(int VitalityLevel)
    {
        return VitalityLevel * 5;
    }

    public int CalculatePosionResistance(int VitalityLevel)
    {
        return VitalityLevel * 3;
    }

    // Strength
    // �ٷ� ������ �޴� ������ ���ݷ�, ���� ���, ȭ�� ����

    // TODO �ٷ� ������ �޴� ������ ���ݷ�

    public int CalculatePhysicalDefenseFromStrengthLevel(int StrengthLevel)
    {
        return StrengthLevel * 3;
    }

    public int CalculateFireDefense(int StrengthLevel)
    {
        return StrengthLevel * 4;
    }

    // Dexterity
    // �ⷮ ������ �޴� ������ ���ط� ����, ��â �ð� �پ��, ���� ������ ����

    // TODO

    // Intelligence
    // ����. ������ �ּ��� ������ ���, ���� ������ ����

    // TODO ������ �ּ��� ������ ���

    public int CalculateMagicDefense(int IntelligenceLevel)
    {
        return IntelligenceLevel * 4;
    }

    // Faith
    // �ž�
    // ������ �ּ��� ������ ���, ��� ������ ����

    // TODO ������ �ּ��� ������ ���

    public int CalculateDarkDefense(int FaithLevel)
    {
        return FaithLevel * 4;
    }

    // Luck
    // �������� �߰����� ���� �Ӽ� ����ġ�� �����.
    // Ư�� ���� �� �� ������ ���� ������ ����

    public int CalculateItemDiscovery(int level)
    {
        return level * 1;
    }

    public int CalculateCurseResistance(int FaithLevel)
    {
        return FaithLevel * 4;
    }



    #endregion
}
