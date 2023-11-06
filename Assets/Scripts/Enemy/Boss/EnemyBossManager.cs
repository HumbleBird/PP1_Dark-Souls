using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;

    AICharacterManager aiCharacter;

    public BossCombatStanceState bossCombatStanceState;

    [Header("Second Phase FX")]
    public GameObject particleFX;

    UIBossHealthBar m_UIBossHealthBar;

    public AudioClip m_audioClip;
    public FogWall m_FogWall;

    public bool bossFightIsActive; // 현재 싸우고 있는가
    public bool bossHasBeenAwakened; // 이미 싸움 중에 한 번 죽어는가/이미 보스가 깨어져 있는가
    public bool bossHasBeenDefeated; // 보스가 죽었는가
    public bool m_isBossSoundPlaying = false;
    public bool m_isNextPhase = false;

    public GameObject m_goBossParticle;

    private void Start()
    {
        aiCharacter = GetComponent<AICharacterManager>();
        m_UIBossHealthBar = Managers.GameUI.m_GameSceneUI.m_BossHealthBar;
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
    }

    public void Update()
    {
        if (m_isNextPhase && aiCharacter.isInteracting == false)
        {
            m_isNextPhase = false;
            ShiftToSecondPhase();
        }
    }

    public void HealthRefresh()
    {
        m_UIBossHealthBar.RefreshUI();

        if (aiCharacter.aiCharacterStatsManager.currentHealth <= aiCharacter.aiCharacterStatsManager.maxHealth / 2 && bossCombatStanceState.hasPhaseShifted == false)
        {
            m_isNextPhase = true;
        }

        if (aiCharacter.aiCharacterStatsManager.currentHealth <= 0)
        {
            Managers.Game.BossHasBeenDefeated(m_FogWall);
        }
    }

    public void ShiftToSecondPhase()
    {
        //aiCharacter.animator.SetBool("isInvulnerable", true);
        aiCharacter.animator.SetBool("isPhaseShifting", true);
        aiCharacter.aiCharacterAnimationManager.PlayTargetAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }

    public void Clear()
    {
        bossCombatStanceState.hasPhaseShifted = false;
        m_isNextPhase = false;

        if (bossHasBeenDefeated == false)
        {
            bossCombatStanceState.hasPhaseShifted = false;

            if (m_goBossParticle != null)
            {
                Managers.Resource.Destroy(m_goBossParticle);
                m_goBossParticle = null;
            }
        }

        // Sound
        Managers.Sound.RemoveBgm();
    }
}
