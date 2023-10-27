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
    public int m_iVigorLevel            = 10; // 생명력. 최대 생명력이 오름
    public int m_iAttunementLevel           = 10; // 집중력. 최대 FP가 오름
    public int m_iEnduranceLevel            = 10; // 지구력. 최대 스테미너가 오름
    public int m_iVitalityLevel             = 10; // 체력. 장비중량과 물리 방어력, 독 내성이 오름
    public int m_iStrengthLevel         = 10; // 근력. 근력 보정을 받는 무기의 공격력과 화염 내성, 물리 방어력을 상승, 손에 든 장비를 양손잡기하면 현 스탯의 1.5배로 계산
    public int m_iDexterityLevel        = 10; // 기량. 기량 보정을 받는 무기의 공격력이 상승
    public int m_iIntelligenceLevel         = 10; // 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름
    public int m_iFaithLevel             = 10; // 신앙. 기적과 주술의 위력이 상승, 어둠 방어력이 오름
    public int m_iLuckLevel             = 10; // 운. 아이템의 발견율과 속성 내성치가 상승함.

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
    public int CurrentPoise = 10;

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

        LoadStat();
    }

    // 능력치를 CSV 테이블에서 로드
    public override void LoadStat()
    {
        if (Managers.Game.m_isNewGame == false)
        {
            // 능력치 로드
        }

        InitAbility();
    }

    // 가져온 스텟을 이용해 능력치 정하기
    public override void InitAbility()
    {
        // Vigor 생명력. 최대 생명력이 오름
        maxHealth = SetMaxHealth();

        // Attunement 집중력. 최대 FP가 오름
        maxfocusPoint = SetMaxfocusPoints();

        // Endurance 지구력. 최대 스테미너가 오름
        maxStamina = SetMaxStamina();

        // Vitality 체력. 장비중량과 물리 방어력, 독 내성이 오름
        maxEquipLoad = CalculateAndSetMaxEquipload(m_iEnduranceLevel);

        // Strength 근력. 근력 보정을 받는 무기의 공격력과 화염 내성, 물리 방어력을 상승, 손에 든 장비를 양손잡기하면 현 스탯의 1.5배로 계산
        CalculateStrength();

        // Dexterity 기량. 기량 보정을 받는 무기의 공격력이 상승
        CalculateDexterity();

        // Intelligence 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름
        CalculateIntelligence();

        // Faith 신앙. 기적과 주술의 위력이 상승, 어둠 방어력이 오름
        CalculateFaith();

        // Luck 운. 아이템의 발견율과 속성 내성치가 상승함.
        CalculateLuck();

        FullRecovery();

        Managers.GameUI.m_GameSceneUI.m_StatBarsUI.SetBGWidthUI(E_StatUI.All);
    }

    // Current HP,Stamina FP 완전 회복
    public override void FullRecovery()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentFocusPoints = maxfocusPoint;

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
        // 액션 중이거나 달릴 때 GENERATE STAMINA  하지 않음
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
        player.m_GameSceneUI.SoulsRefreshUI(true, souls);
    }

    public override void HealthBarUIUpdate(int damage)
    {
        base.HealthBarUIUpdate(damage);

        player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
    }


    #region Set Abillity  From Level
    protected override int SetMaxHealth()
    {
        maxHealth = m_iVigorLevel * 10;
        return maxHealth;
    }

    float SetMaxStamina()
    {
        maxStamina = m_iEnduranceLevel * 10;
        return maxStamina;
    }

    float SetMaxfocusPoints()
    {
        maxfocusPoint = m_iAttunementLevel * 10;
        return maxfocusPoint;
    }

    public float CalculateAndSetMaxEquipload(int EnduranceLevel)
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

        return totalEquipLoad;
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

    void CalculateStrength()
    {

    }

    void CalculateDexterity()
    {

    }

    void CalculateIntelligence()
    {

    }

    void CalculateFaith()
    {

    }

    void CalculateLuck()
    {

    }
    #endregion
}
