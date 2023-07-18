using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : CharacterAnimatorManager
{
    EnemyManager enemy;

    protected override  void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyManager>();
    }

    public void AwardSoulsOnDeath()
    {

        PlayerStatsManager playerstatus = FindObjectOfType<PlayerStatsManager>();
        SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

        if (playerstatus != null)
        {
            playerstatus.AddSouls(character.characterStatsManager.soulsAwardedOnDeath);


            if (soulCountBar != null)
            {
                soulCountBar.SetSoulCountText(playerstatus.currentSoulCount);
            }
        }

    }

    public void InstantiateBossParticleFX()
    {
        BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
        GameObject phaseFX = Instantiate(enemy.enemyBossManager.particleFX, bossFXTransform.transform);
    }

    public void PlayWeaponTrailFX()
    {
        enemy.enemyEffectsManager.PlayWeaponFX(false);
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemy.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = enemy.animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemy.enemyRigidbody.velocity = velocity;

        if(enemy.isRotatingWithRootMotion)
        {
            enemy.transform.rotation *= enemy.animator.deltaRotation;
        }
    }
}
