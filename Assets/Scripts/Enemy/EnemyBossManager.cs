using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;

    UIBossHealthBar bossHealthBar;
    EnemyStatus enemyStatus;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        enemyStatus = GetComponent<EnemyStatus>();
    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(enemyStatus.maxHealth);
    }

    public void UpdateBossHealthBar(int currentHealth)
    {
        bossHealthBar.SetBossCurrentHealth(currentHealth);
    }
}
