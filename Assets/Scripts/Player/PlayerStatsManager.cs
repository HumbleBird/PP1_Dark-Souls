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
    public int m_iVigorLevel = 10; // 생명력. 최대 생명력이 오름
    public int m_iAttunementLevel = 10; // 집중력. 최대 FP가 오름
    public int m_iEnduranceLevel = 10; // 지구력. 최대 스테미너가 오름
    public int m_iVitalityLevel = 10; // 체력. 장비중량과 물리 방어력, 독 내성이 오름
    public int m_iStrengthLevel = 10; // 근력. 근력 보정을 받는 무기의 공격력과 화염 내성, 물리 방어력을 상승, 손에 든 장비를 양손잡기하면 현 스탯의 1.5배로 계산
    public int m_iDexterityLevel = 10; // 기량. 기량 보정을 받는 무기의 공격력이 상승
    public int m_iIntelligenceLevel = 10; // 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름
    public int m_iFaithLevel = 10; // 신앙. 기적과 주술의 위력이 상승, 어둠 방어력이 오름
    public int m_iLuckLevel = 10; // 운. 아이템의 발견율과 속성 내성치가 상승함.

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
        // Stat Clear
        m_iPhysicalDefense = 0;

        // Vigor 생명력. 최대 생명력이 오름
        CalculateVigor();

        // Attunement 집중력. 최대 FP가 오름
        CalculateAttunement();

        // Endurance 지구력. 최대 스테미너가 오름
        CalculateEndurance();

        // Vitality 체력. 장비중량과 물리 방어력, 독 내성이 오름
        CalculateVitality();

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

        // Poise
        m_fTotalPoiseDefence = m_fStatPoise;

        // 현재 HP, FP, Stamina 완전 회복
        FullRecovery();

        // HUD Bar Refresh
        if(Managers.GameUI.m_GameSceneUI != null)
             Managers.GameUI.m_GameSceneUI.m_StatBarsUI.SetBGWidthUI(E_StatUI.All);
    }

    // Current HP,Stamina FP 완전 회복
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
        // 액션 중이거나 달릴 때 GENERATE STAMINA  하지 않음
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
        // Vigor 생명력. 최대 생명력이 오름
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
    // 근력 보정을 받는 무기의 공격력, 물리 방어, 화염 내성
    void CalculateStrength()
    {
        // TODO 근력 보정을 받는 무기의 공격력

        m_iPhysicalDefense += CalculatePhysicalDefenseFromStrengthLevel(m_iStrengthLevel);
        m_iFireDefense = CalculateFireDefense(m_iStrengthLevel);
    }

    // Dexterity
    // 기량 보정을 받는 무기의 피해량 증가, 영창 시간 줄어듬, 낙하 데미지 감소

    void CalculateDexterity()
    {
        // TODO
        // 기량 보정을 받는 무기의 피해량 증가, 영창 시간 줄어듬, 낙하 데미지 감소
    }

    // Intelligence
    // 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름
    void CalculateIntelligence()
    {
        // TODO 마술과 주술의 위력이 상승
        m_iMagicDefense = CalculateMagicDefense(m_iMagicDefense);
    }

    // Faith
    // 신앙
    // 기적과 주술의 위력이 상승, 어둠 방어력이 오름
    void CalculateFaith()
    {
        // TODO 기적과 주술의 위력이 상승

        m_iDarkDefense = CalculateDarkDefense(m_iFaithLevel);
    }

    // Luck
    // 아이템의 발견율과 저주 속성 내성치가 상승함.
    // 특정 출혈 및 독 무기의 질병 축적을 증가
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
    // 근력 보정을 받는 무기의 공격력, 물리 방어, 화염 내성

    // TODO 근력 보정을 받는 무기의 공격력

    public int CalculatePhysicalDefenseFromStrengthLevel(int StrengthLevel)
    {
        return StrengthLevel * 3;
    }

    public int CalculateFireDefense(int StrengthLevel)
    {
        return StrengthLevel * 4;
    }

    // Dexterity
    // 기량 보정을 받는 무기의 피해량 증가, 영창 시간 줄어듬, 낙하 데미지 감소

    // TODO

    // Intelligence
    // 지성. 마술과 주술의 위력이 상승, 마력 방어력이 오름

    // TODO 마술과 주술의 위력이 상승

    public int CalculateMagicDefense(int IntelligenceLevel)
    {
        return IntelligenceLevel * 4;
    }

    // Faith
    // 신앙
    // 기적과 주술의 위력이 상승, 어둠 방어력이 오름

    // TODO 기적과 주술의 위력이 상승

    public int CalculateDarkDefense(int FaithLevel)
    {
        return FaithLevel * 4;
    }

    // Luck
    // 아이템의 발견율과 저주 속성 내성치가 상승함.
    // 특정 출혈 및 독 무기의 질병 축적을 증가

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
