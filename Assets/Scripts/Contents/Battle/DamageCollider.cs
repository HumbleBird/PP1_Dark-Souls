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
            EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();
            CharacterManager enemyCharacterManager = other.GetComponent<CharacterManager>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Parried", true);
                    return;
                }
            }

            if (enemyStatus != null)
            {
                enemyStatus.poiseResetTimer = enemyStatus.totalPoiseResetTime;
                enemyStatus.totalPoiseDefence = enemyStatus.totalPoiseDefence - poiseBreak;
                Debug.Log("Enemy Poise is currently " + enemyStatus.totalPoiseResetTime);

                if(enemyStatus.isBoss)
                {
                    if (enemyStatus.totalPoiseDefence > poiseBreak)
                    {
                        enemyStatus.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStatus.TakeDamage(currentWeaponDamage);
                        enemyStatus.BreakGuard();
                    }
                }
                else
                {
                    if (enemyStatus.totalPoiseDefence > poiseBreak)
                    {
                        enemyStatus.TakeDamageNoAnimation(currentWeaponDamage);
                    }
                    else
                    {
                        enemyStatus.TakeDamage(currentWeaponDamage);
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
