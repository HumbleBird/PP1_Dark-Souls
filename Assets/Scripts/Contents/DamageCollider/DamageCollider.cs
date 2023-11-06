using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;
    protected Collider damageCollider;
    public bool enabledDamageColliderOnStartUp = false;

    [Header("Team I.D")]
    public int teamIDNumber = 0;

    [Header("Poise")]
    public float poiseDamage;
    public float offensivePoiseBonus;

    [Header("FinalDamage")]
    float finalPhysicalDamage  ;
    float finalFireDamage      ;
    float finalMagicDamage     ;
    float finalLightningDamage ;
    float finalDarkDamage      ;
    float finalDamage;

    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    public int magicDamage;
    public int lightningDamage;
    public int darkDamage;

    [Header("Guard Break Modifier")]
    public float guardBreakModifier = 1;

    [Header("Weapon Buff Damage")]
    public float m_MeleeWeapon_BuffDamage_Physical;
    public float m_MeleeWeapon_BuffDamage_Fire;
    public float m_MeleeWeapon_BuffDamage_Magic;
    public float m_MeleeWeapon_BuffDamage_Lightning;
    public float m_MeleeWeapon_BuffDamage_Dark;

    protected bool shieldHasBeenHit;
    protected bool hasBeenParried;
    public string currentDamageAnimation;

    protected Vector3 contactPoint;
    protected float angleHitFrom;

    private List<CharacterManager> charactersDamageDuringThisCalculation = new List<CharacterManager>();

    protected virtual void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enabledDamageColliderOnStartUp;
        characterManager = GetComponentInParent<CharacterManager>();
    }

    public void EnableDamageCollider()
    {
        charactersDamageDuringThisCalculation = new List<CharacterManager>();
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        if(charactersDamageDuringThisCalculation.Count > 0)
            charactersDamageDuringThisCalculation.Clear();
        damageCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Damageable Character"))
        {
            shieldHasBeenHit = false;
            hasBeenParried = false;

            CharacterManager enemyManager = other.GetComponentInParent<CharacterManager>();

            if (enemyManager != null)
            {
                AICharacterManager aiCharacter = enemyManager as AICharacterManager;

                if (charactersDamageDuringThisCalculation.Contains(enemyManager))
                    return;

                charactersDamageDuringThisCalculation.Add(enemyManager);

                CheckForParry(enemyManager);

                CheckForBlock(enemyManager);

                if (hasBeenParried)
                    return;

                if (shieldHasBeenHit)
                    return;

                enemyManager.characterStatsManager.poiseResetTimer = enemyManager.characterStatsManager.totalPoiseResetTime;
                enemyManager.characterStatsManager.m_fTotalPoiseDefence = enemyManager.characterStatsManager.m_fTotalPoiseDefence - poiseDamage;

                // 무기 콜라이더가 어디 부분에서 처음 부딪치는지 탐지
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                angleHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);

                // 딜 계산
                DealDamage(enemyManager);

                if(aiCharacter != null)
                {
                    // 타겟이 A.I라면, ai가 새로운 타겟은 리서치. 새 타겟을 공격
                    aiCharacter.currentTarget = characterManager;
                }
            }
        }

        if (other.tag == "illusionary Wall")
        {
            IllusionaryWall illusionaryWall = other.GetComponent<IllusionaryWall>();

            if(illusionaryWall != null)
            {
                illusionaryWall.wallHasBennHit = true;
            }
        }
    }



    protected virtual void CheckForParry(CharacterManager enemyManager)
    {
        if (enemyManager.isParrying)
        {
            characterManager.characterAnimatorManager.PlayTargetAnimation("Parried", true);
            hasBeenParried = true;
        }
    }

    protected virtual void CheckForBlock(CharacterManager enemyManager)
    {
        CharacterStatsManager enemyShield = enemyManager.characterStatsManager;
        Vector3 directionFromPlayerToEnemy = (characterManager.transform.position - enemyManager.transform.position);
        float dotValueFromPlayerToEnemy = Vector3.Dot(directionFromPlayerToEnemy, enemyManager.transform.forward);

        if (enemyManager.isBlocking && dotValueFromPlayerToEnemy > 0.3f)
        {
            shieldHasBeenHit = true;

            TakeBlockedDamageEffect takeBlockedDamage = new TakeBlockedDamageEffect();// Instantiate(Managers.WorldEffect.takeBlockedDamageEffect);
            takeBlockedDamage.physicalDamage = physicalDamage;
            takeBlockedDamage.fireDamage = fireDamage;
            takeBlockedDamage.poiseDamage = poiseDamage;
            takeBlockedDamage.staminaDamage = poiseDamage;

            enemyManager.characterEffectsManager.ProcessEffectInstantly(takeBlockedDamage);
        }
    }

    protected virtual void DealDamage(CharacterManager enemyManager)
    {
        // Damage 계산 방식 정하기
        if (characterManager.isUsingRightHand)
        {
            switch (characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_eItemType)
            {
                case E_ItemType.MeleeWeapon:
                    DealDamage_MeleeWeapon();
                    break;
                case E_ItemType.RangeWeapon:
                    DealDamage_Default();
                    break;
                case E_ItemType.Catalyst:
                    DealDamage_Default();
                    break;
                case E_ItemType.Shield:
                    DealDamage_Default();
                    break;
            }
        }
        else if (characterManager.isUsingLeftHand)
        {
            switch (characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.m_eItemType)
            {
                case E_ItemType.MeleeWeapon:
                    DealDamage_MeleeWeapon();
                    break;
                case E_ItemType.RangeWeapon:
                    DealDamage_Default();
                    break;
                case E_ItemType.Catalyst:
                    DealDamage_Default();
                    break;
                case E_ItemType.Shield:
                    DealDamage_Default();
                    break;
            }
        }

        // Right Weapon Modifire
        if (characterManager.isUsingRightHand)
            {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier;
            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier;
            }
        }
        // Left Weapon Modifire
        else if (characterManager.isUsingLeftHand)
        {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier;

            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier;

            }
        }

        TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
        takeDamageEffect.characterCausingDamage = characterManager;
        takeDamageEffect.m_PhysicalDamage = physicalDamage;
        takeDamageEffect.m_MagicDamage = magicDamage;
        takeDamageEffect.m_FireDamage = fireDamage;
        takeDamageEffect.m_LightningDamage = lightningDamage;
        takeDamageEffect.m_DarkDamage = darkDamage;
        takeDamageEffect.poiseDamage = poiseDamage;
        takeDamageEffect.contactPoint = contactPoint;
        takeDamageEffect.angleHitFrom = angleHitFrom;
        enemyManager.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);
    }

    void DealDamage_Default()
    {
        finalPhysicalDamage = physicalDamage;
        finalFireDamage = fireDamage;
        finalMagicDamage = fireDamage;
        finalLightningDamage = fireDamage;
        finalDarkDamage = fireDamage;
        finalDamage = 0;
    }

    void DealDamage_MeleeWeapon()
    {
        finalPhysicalDamage       = physicalDamage + m_MeleeWeapon_BuffDamage_Physical;
        finalFireDamage        = fireDamage        + m_MeleeWeapon_BuffDamage_Fire;
        finalMagicDamage           = fireDamage    + m_MeleeWeapon_BuffDamage_Magic;
        finalLightningDamage      = fireDamage     + m_MeleeWeapon_BuffDamage_Lightning;
        finalDarkDamage           = fireDamage     + m_MeleeWeapon_BuffDamage_Dark;
        finalDamage            = 0;
    }
}
