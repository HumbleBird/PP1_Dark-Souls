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
    public float poiseBreak;
    public float offensivePoiseBonus;

    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    public int magicDamage;
    public int lightningDamage;
    public int darkDamage;

    [Header("Guard Break Modifier")]
    public float guardBreakModifier = 1;


    protected bool shieldHasBeenHit;
    protected bool hasBeenParried;
    public string currentDamageAnimation;

    protected virtual void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enabledDamageColliderOnStartUp;

    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            shieldHasBeenHit = false;
            hasBeenParried = false;

            CharacterStatsManager enemyStats = other.GetComponent<CharacterStatsManager>();
            CharacterManager enemyManager = other.GetComponent<CharacterManager>();
            CharacterEffectsManager enemyEffects = other.GetComponent<CharacterEffectsManager>();

            if (enemyManager != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                CheckForParry(enemyManager);

                CheckForBlock(enemyManager);
            }

            if (enemyStats != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                if (hasBeenParried)
                    return;

                if (shieldHasBeenHit)
                    return;

                enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;

                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                float directionHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                ChooseWhichDirectionDamageCameFrom(directionHitFrom);
                enemyEffects.PlayBloodSplatterFX(contactPoint);

                DealDamage(enemyStats);
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
            characterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Parried", true);
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
            float physicalDamageAfterBlock = physicalDamage - (physicalDamage * enemyShield.blockingPhysicalDamageAbsorption) / 100;
            float fireDamageAfterBlock = fireDamage - (fireDamage * enemyShield.blockingFireDamageAbsorption) / 100;

            enemyManager.characterCombatManager.AttemptBlock(this, physicalDamage, fireDamage, "Block_01");
            enemyShield.TakeDamageAfterBlock(Mathf.RoundToInt(physicalDamageAfterBlock), Mathf.RoundToInt(fireDamageAfterBlock), characterManager);

        }
    }

    protected virtual void DealDamage(CharacterStatsManager enemyStats)
    {
        float finalPhysicalDamage = physicalDamage;

        // Right Weapon Modifire
        if(characterManager.isUsingRightHand)
        {
            if(characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalPhysicalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.rightWeapon.lightAttackStaminaMultiplier;
            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalPhysicalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.rightWeapon.heavyAttackStaminaMultiplier;
            }
        }

        // Left Weapon Modifire
        else if (characterManager.isUsingLeftHand)
        {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalPhysicalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.leftWeapon.lightAttackStaminaMultiplier;

            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalPhysicalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.leftWeapon.heavyAttackStaminaMultiplier;

            }
        }

        // Deal modifired Damage
        if (enemyStats.totalPoiseDefence > poiseBreak)
        {
            enemyStats.TakeDamageNoAnimation(Mathf.RoundToInt(finalPhysicalDamage), 0);
        }
        else
        {
            enemyStats.TakeDamage(Mathf.RoundToInt(finalPhysicalDamage), 0, currentDamageAnimation, characterManager);
        }
    }

    protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
    {
        if(direction >= 145 && direction <= 180)
        {
            currentDamageAnimation = "Damage_Forward_01";
        }
        else if (direction <= -145 && direction >= -180)
        {
            currentDamageAnimation = "Damage_Forward_01";
        }
        else if (direction >= -45 && direction <= 45)
        {
            currentDamageAnimation = "Damage_Back_01";
        }
        else if (direction >= -144 && direction <= -45)
        {
            currentDamageAnimation = "Damage_Left_01";
        }
        else if (direction >= 45 && direction <= 144)
        {
            currentDamageAnimation = "Damage_Right_01";
        }
    }
}
