using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<FogWall> fogWalls;
    public UIBossHealthBar bossHealthBar;
    public EnemyBossManager boss;

    public bool bossFightIsActive; // ���� �ο�� �ִ°�
    public bool bossHasBeenAwakened; // �̹� �ο� �߿� �� �� �׾�°�/�̹� ������ ������ �ִ°�
    public bool bossHasBeenDefeated; // ������ �׾��°�

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
