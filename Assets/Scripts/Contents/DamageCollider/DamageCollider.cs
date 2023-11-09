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

                if (charactersDamageDuringThisCalculation.Contains(enemyManager))
                    return;

                charactersDamageDuringThisCalculation.Add(enemyManager);

                CheckForParry(enemyManager);

                CheckForBlock(enemyManager);

                if (hasBeenParried)
                    return;

                enemyManager.characterStatsManager.poiseResetTimer = enemyManager.characterStatsManager.totalPoiseResetTime;
                enemyManager.characterStatsManager.m_fTotalPoiseDefence = enemyManager.characterStatsManager.m_fTotalPoiseDefence - poiseDamage;

                // 무기 콜라이더가 어디 부분에서 처음 부딪치는지 탐지
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                angleHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);

                // 딜 계산
                DealDamage(enemyManager);

                AICharacterManager aiCharacter = enemyManager as AICharacterManager;
                if (aiCharacter != null)
                {

                    if(characterManager.characterStatsManager.teamIDNumber != enemyManager.characterStatsManager.teamIDNumber)
                    // 타겟이 A.I라면, ai가 새로운 타겟은 리서치. 새 타겟을 공격
                    aiCharacter.currentTarget = characterManager;
                }
            }
        }

        if (other.tag == "illusionary Wall")
        {
            IllusionaryWall illusionaryWall = other.GetComponent<IllusionaryWall>();

            if(illusionaryWall != null && characterManager.characterStatsManager.teamIDNumber == (int)E_TeamId.Player)
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
            if (characterManager.isTwoHandingWeapon)
            {
                if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
                {
                    finalPhysicalDamage = finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier + finalPhysicalDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackDamageModifire;
                    finalFireDamage = finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier + finalFireDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackDamageModifire;
                    finalMagicDamage = finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier + finalMagicDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackDamageModifire;
                    finalLightningDamage = finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier + finalLightningDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackDamageModifire;
                    finalDarkDamage = finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier + finalDarkDamage * characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackDamageModifire;
                }
                else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
                {
                    finalPhysicalDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalFireDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalMagicDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalLightningDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalDarkDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                }
            }
            else
            {
                if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
                {
                    finalPhysicalDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier;
                    finalFireDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier;
                    finalMagicDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier;
                    finalLightningDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier;
                    finalDarkDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.lightAttackDamageModifier;
                }
                else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
                {
                    finalPhysicalDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalFireDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalMagicDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalLightningDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                    finalDarkDamage *= characterManager.characterEquipmentManager.m_CurrentHandRightWeapon.heavyAttackDamgeModifier;
                }
            }

        }
        // Left Weapon Modifire
        else if (characterManager.isUsingLeftHand)
        {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalPhysicalDamage    *=      characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackDamageModifier;
                finalFireDamage        *=  characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackDamageModifier;
                finalMagicDamage       *=  characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackDamageModifier;
                finalLightningDamage  *=   characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackDamageModifier;
                finalDarkDamage *= characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.lightAttackDamageModifier;

            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalPhysicalDamage    *=       characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackDamgeModifier;
                finalFireDamage        *=   characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackDamgeModifier;
                finalMagicDamage       *=   characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackDamgeModifier;
                finalLightningDamage  *=    characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackDamgeModifier;
                finalDarkDamage *= characterManager.characterEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackDamgeModifier;
            }
        }



        if (shieldHasBeenHit)
        {
            TakeBlockedDamageEffect takeBlockedDamage = new TakeBlockedDamageEffect();
            takeBlockedDamage.characterCausingDamage = characterManager;
            takeBlockedDamage.m_PhysicalDamage = finalPhysicalDamage;
            takeBlockedDamage.m_MagicDamage = finalMagicDamage;
            takeBlockedDamage.m_FireDamage = finalFireDamage;
            takeBlockedDamage.m_LightningDamage = finalLightningDamage;
            takeBlockedDamage.m_DarkDamage = finalDarkDamage;
            takeBlockedDamage.poiseDamage = poiseDamage;

            enemyManager.characterEffectsManager.ProcessEffectInstantly(takeBlockedDamage);
        }
        else
        {
            TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
            takeDamageEffect.characterCausingDamage = characterManager;
            takeDamageEffect.m_PhysicalDamage = finalPhysicalDamage;
            takeDamageEffect.m_MagicDamage = finalMagicDamage;
            takeDamageEffect.m_FireDamage = finalFireDamage;
            takeDamageEffect.m_LightningDamage = finalLightningDamage;
            takeDamageEffect.m_DarkDamage = finalDarkDamage;
            takeDamageEffect.poiseDamage = poiseDamage;
            takeDamageEffect.contactPoint = contactPoint;
            takeDamageEffect.angleHitFrom = angleHitFrom;
            enemyManager.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);
        }
    }

    void DealDamage_Default()
    {
        finalPhysicalDamage = physicalDamage;
        finalFireDamage = fireDamage;
        finalMagicDamage = fireDamage;
        finalLightningDamage = fireDamage;
        finalDarkDamage = fireDamage;
    }

    void DealDamage_MeleeWeapon()
    {
        finalPhysicalDamage       = physicalDamage + m_MeleeWeapon_BuffDamage_Physical;
        finalFireDamage        = fireDamage        + m_MeleeWeapon_BuffDamage_Fire;
        finalMagicDamage           = fireDamage    + m_MeleeWeapon_BuffDamage_Magic;
        finalLightningDamage      = fireDamage     + m_MeleeWeapon_BuffDamage_Lightning;
        finalDarkDamage           = fireDamage     + m_MeleeWeapon_BuffDamage_Dark;
    }
}
