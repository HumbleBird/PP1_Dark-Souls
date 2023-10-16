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
    public float maxfocusPoint;
    public float currentFocusPoints;

    // Stamina
    public float maxStamina = 1;
    public float currentStamina = 1;

    // Equip Load
    public float currentEquipLoad = 0;
    public float maxEquipLoad = 0;
    public EncumbranceLevel encumbranceLevel;

    // Poise
    public int poiseLevel = 10;

    // Item Discovery
    public int m_iItemDiscovery = 10;

    [Header("Character Attack Power")]
    public int m_iRWeapon1;
    public int m_iRWeapon2;
    public int m_iRWeapon3;
    public int m_iLWeapon1;
    public int m_iLWeapon2;
    public int m_iLWeapon3;

    public float staminaRegenerationAmount = 10;
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

        SetAbilityValueFromLevel();
    }

    public override void SetAbilityValueFromLevel()
    {
        maxHealth = SetMaxHealth();
        maxStamina = SetMaxStamina();
        maxfocusPoint = SetMaxfocusPoints();

        InitStats();
    }

    public override void InitStats()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentFocusPoints = maxfocusPoint;
    }


    // ���� �ε�� ĳ���� ���� �ε�
    public void LoadAbilityValue()
    {
        // ����� DB���� �ɷ�ġ�� ������.
        // �ִ�, ���� ���� ��.
    }


    public override int SetMaxHealth()
    {
        maxHealth = m_iVigorLevel * 10;
        return maxHealth;
    }

    public  float SetMaxStamina()
    {
        maxStamina = m_iEnduranceLevel * 10;
        return maxStamina;
    }

    public  float SetMaxfocusPoints()
    {
        maxfocusPoint = m_iAttunementLevel * 10;
        return maxfocusPoint;
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
            totalPoiseDefence = armorPoiseBonus;
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
        currentStamina -= staminaToDeduct;
        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Stamina);
    }

    public  void DeductSprintingStamina(float staminaToDeduct)
    {
        if(player.isSprinting)
        {
            spritingTimer += Time.deltaTime;

            if(spritingTimer > 0.1f)
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
            staminaRegenTimer +=  Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                if(player.isBlocking)
                {
                    currentStamina += staminaRegenerationAmountWhilstBlocking * Time.deltaTime;
                }
                else
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
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

        if(currentFocusPoints < 0 )
        {
            currentFocusPoints = 0;
        }

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.FocusPoint);
    }

    public void AddSouls(int souls)
    {
        currentSoulCount += souls;
        player.m_GameSceneUI.RefreshUI();
    }

    public override void HealthBarUIUpdate()
    {
        base.HealthBarUIUpdate();

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
    }

    public void CalculateAndSetMaxEquipload()
    {
        float totalEquipLoad = 40;

        for (int i = 0; i < m_iEnduranceLevel; i++)
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

        maxEquipLoad = totalEquipLoad;
    }

    public void CaculateAndSetCurrentEquipLoad(float equipLoad)
    {
        currentEquipLoad = equipLoad;

        encumbranceLevel = EncumbranceLevel.Light;

        if (currentEquipLoad > (maxEquipLoad * 0.3f))
        {
            encumbranceLevel = EncumbranceLevel.Medium;
        }
        if (currentEquipLoad > (maxEquipLoad * 0.7f))
        {
            encumbranceLevel = EncumbranceLevel.Heavy;
        }
        if (currentEquipLoad > (maxEquipLoad))
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

}
