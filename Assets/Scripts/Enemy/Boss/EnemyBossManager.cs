using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;

    UIBossHealthBar bossHealthBar;
    EnemyStatus enemyStatus;
    EnemyAnimationManager enemyAnimationManager;
    BossCombatStanceState bossCombatStanceState;

    [Header("Second Phase FX")]
    public GameObject particleFX;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        enemyStatus = GetComponent<EnemyStatus>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(enemyStatus.maxHealth);
    }

    public void UpdateBossHealthBar(int currentHealth, int maxHealth)
    {
        bossHealthBar.SetBossCurrentHealth(currentHealth);

        if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
        {
            bossCombatStanceState.hasPhaseShifted = true;
            ShiftToSecondPhase();
        }
    }

    public void ShiftToSecondPhase()
    {
        enemyAnimationManager.anim.SetBool("isInvulnerable", true);
        enemyAnimationManager.anim.SetBool("isPhaseShifting", true);
        enemyAnimationManager.PlayerTargetAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }
}
