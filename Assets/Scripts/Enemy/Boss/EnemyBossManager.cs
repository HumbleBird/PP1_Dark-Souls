using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;

    AICharacterManager enemy;

    BossCombatStanceState bossCombatStanceState;

    [Header("Second Phase FX")]
    public GameObject particleFX;

    UIBossHealthBar m_UIBossHealthBar;

    private void Awake()
    {
        enemy = GetComponent<AICharacterManager>();
        Managers.GameUI.m_GameSceneUI.m_BossHealthBar.SetInfo(enemy);

        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
    }

    private void Start()
    {
        m_UIBossHealthBar = Managers.GameUI.m_GameSceneUI.m_BossHealthBar;
    }

    public void RefreshUI()
    {
        m_UIBossHealthBar.RefreshUI();
    }

    public void ShiftToSecondPhase()
    {
        enemy.animator.SetBool("isInvulnerable", true);
        enemy.animator.SetBool("isPhaseShifting", true);
        enemy.aiCharacterAnimationManager.PlayTargetAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }
}
