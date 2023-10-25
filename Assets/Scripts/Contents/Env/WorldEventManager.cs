using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<FogWall> fogWalls;
    public UIBossHealthBar bossHealthBar;
    public EnemyBossManager boss;

    public bool bossFightIsActive; // 현재 싸우고 있는가
    public bool bossHasBeenAwakened; // 이미 싸움 중에 한 번 죽어는가/이미 보스가 깨어져 있는가
    public bool bossHasBeenDefeated; // 보스가 죽었는가

    private void Start()
    {
        bossHealthBar = Managers.GameUI.m_GameSceneUI.m_BossHealthBar;
    }

    public void ActivateBossFight()
    {
        bossFightIsActive = true;
        bossHasBeenAwakened = true;
        bossHealthBar.SetUIHealthBarToActive();

        foreach (var fogWall in fogWalls)
        {
            fogWall.ActivateFogWall();
        }
    }

    public void BossHasBeenDefeated()
    {
        bossHasBeenDefeated = true;
        bossFightIsActive = false;

        foreach (var fogWall in fogWalls)
        {
            fogWall.DeactivateFogWall();
        }
    }
}
