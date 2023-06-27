using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;
    Collider damageCollider;
    public bool enabledDamageColliderOnStartUp = false;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseBonus;

    [Header("Damage")]
    public int currentWeaponDamage = 25;

    private void Awake()
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
        if (other.tag == "Player")
        {
            PlayerStatsManager playerStatsManager = other.GetComponent<PlayerStatsManager>();
            CharacterManager enemyCharacterManager = other.GetComponent<CharacterManager>();
            BlockingCollider shield = other.GetComponentInChildren<BlockingCollider>();

            if (enemyCharacterManager != null)
            {
                if(enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Parried", true);
                    return;
                }
                else if (shield && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock =
                        currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (playerStatsManager != null)
                    {
                        playerStatsManager.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        return;
                    }

                }
            }

            if (playerStatsManager != null)
            {
                playerStatsManager.poiseResetTimer = playerStatsManager.totalPoiseResetTime;
                playerStatsManager.totalPoiseDefence = playerStatsManager.totalPoiseDefence - poiseBreak;
                Debug.Log("Player's Poise is currently " + playerStatsManager.totalPoiseResetTime);

                if (playerStatsManager.totalPoiseDefence > poiseBreak)
                {
                    playerStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                }
                else
                {
                    playerStatsManager.TakeDamage(currentWeaponDamage);
                }
            }
        }

        if (other.tag == "Enemy")
        {
            EnemyStatsManager enemyStatsManager = other.GetComponent<EnemyStatsManager>();
            CharacterManager enemyCharacterManager = other.GetComponent<CharacterManager>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Parried", true);
                    return;
                }
            }

            if (enemyStatsManager != null)
            {
                enemyStatsManager.poiseResetTimer = enemyStatsManager.totalPoiseResetTime;
                enemyStatsManager.totalPoiseDefence = enemyStatsManager.totalPoiseDefence - poiseBreak;
                Debug.Log("Enemy Poise is currently " + enemyStatsManager.totalPoiseResetTime);

                if(enemyStatsManager.isBoss)
                {
                    if (enemyStatsManager.totalPoiseDefence > poiseBreak)
                    {
                        enemyStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStatsManager.TakeDamage(currentWeaponDamage);
                        enemyStatsManager.BreakGuard();
                    }
                }
                else
                {
                    if (enemyStatsManager.totalPoiseDefence > poiseBreak)
                    {
                        enemyStatsManager.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStatsManager.TakeDamage(currentWeaponDamage);
                    }
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
}
