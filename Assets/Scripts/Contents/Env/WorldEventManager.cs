using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public UIBossHealthBar bossHealthBar;
    public EnemyBossManager boss;

    public bool bossFightIsActive; // 현재 싸우고 있는가
    public bool bossHasBeenAwakened; // 이미 싸움 중에 한 번 죽어는가/이미 보스가 깨어져 있는가
    public bool bossHasBeenDefeated; // 보스가 죽었는가

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }

    public void ActivateBossFight()
    {
        bossFightIsActive = true;
        bossHasBeenAwakened = true;
        bossHealthBar.SetUIHealthBarToActive();
    }

    public void BossHasBeenDefeated()
    {
        bossHasBeenDefeated = true;
        bossFightIsActive = false;
    }
}
