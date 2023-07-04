using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool shieldHasBeenHit;
    bool hasBeenParried;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            shieldHasBeenHit = false;
            hasBeenParried = false;

            CharacterStatsManager enemyStats = other.GetComponent<CharacterStatsManager>();
            CharacterManager enemyManager = other.GetComponent<CharacterManager>();
            CharacterEffectsManager enemyEffects = other.GetComponent<CharacterEffectsManager>();
            BlockingCollider shield = other.GetComponentInChildren<BlockingCollider>();

            if (enemyManager != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                CheckForParry(enemyManager);

                CheckForBlock(enemyManager, enemyStats, shield);
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

                if (enemyStats.totalPoiseDefence > poiseBreak)
                {
                    enemyStats.TakeDamageNoAnimation(physicalDamage, fireDamage);
                }
                else
                {
                    enemyStats.TakeDamage(physicalDamage, 0, currentDamageAnimation);
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
            characterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Parried", true);
            hasBeenParried = true;
        }
    }

    protected virtual void CheckForBlock(CharacterManager enemyManager, CharacterStatsManager enemyStats, BlockingCollider shield)
    {
        if (shield && enemyManager.isBlocking)
        {
            float physicalDamageAfterBlock = physicalDamage - (physicalDamage * shield.blockingPhysicalDamageAbsorption) / 100;
            float fireDamageAfterBlock = fireDamage - (fireDamage * shield.blockingFireDamageAbsorption) / 100;

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), 0, "Block Guard");
                shieldHasBeenHit = true;
            }

        }
    }

    protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
    {
        Debug.Log(direction);

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
