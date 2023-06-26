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
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
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

                    if (playerStatus != null)
                    {
                        playerStatus.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        return;
                    }

                }
            }

            if (playerStatus != null)
            {
                playerStatus.poiseResetTimer = playerStatus.totalPoiseResetTime;
                playerStatus.totalPoiseDefence = playerStatus.totalPoiseDefence - poiseBreak;

                if (playerStatus.totalPoiseDefence > poiseBreak)
                {
                    playerStatus.TakeDamageNoAnimation(currentWeaponDamage);
                    Debug.Log("Player Poise is currently " + playerStatus.totalPoiseResetTime);
                }
                else
                {
                    playerStatus.TakeDamage(currentWeaponDamage);
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

                if(enemyStatus.totalPoiseDefence > poiseBreak)
                {
                    enemyStatus.TakeDamageNoAnimation(currentWeaponDamage);
                    Debug.Log("Enemy Poise is currently " + enemyStatus.totalPoiseResetTime);
                }
                else
                {
                    enemyStatus.TakeDamage(currentWeaponDamage);
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
