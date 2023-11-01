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

    }

    private void Start()
    {
        enemy = GetComponent<AICharacterManager>();
        m_UIBossHealthBar = Managers.GameUI.m_GameSceneUI.m_BossHealthBar;
        m_UIBossHealthBar.SetInfo(enemy);

        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
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
