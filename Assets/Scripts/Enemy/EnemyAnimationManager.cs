using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : AnimatorManager
{
    EnemyBossManager enemyBossManager;
    EnemyManager enemyManager;

    protected override  void Awake()
    {
        base.Awake();
        enemyBossManager = GetComponent<EnemyBossManager>();
        enemyManager = GetComponent<EnemyManager>();
    }

    public void AwardSoulsOnDeath()
    {

        PlayerStatsManager playerstatus = FindObjectOfType<PlayerStatsManager>();
        SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

        if (playerstatus != null)
        {
            playerstatus.AddSouls(characterStatsManager.soulsAwardedOnDeath);


            if (soulCountBar != null)
            {
                soulCountBar.SetSoulCountText(playerstatus.soulCount);
            }
        }

    }

    public void InstantiateBossParticleFX()
    {
        BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
        GameObject phaseFX = Instantiate(enemyBossManager.particleFX, bossFXTransform.transform);
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRigidbody.velocity = velocity;

        if(enemyManager.isRotatingWithRootMotion)
        {
            enemyManager.transform.rotation *= animator.deltaRotation;
        }
    }
}
