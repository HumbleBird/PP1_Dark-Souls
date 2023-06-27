using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : AnimatorManager
{
    EnemyManager enemyManager;
    EnemyBossManager enemyBossManager;
    EnemyStatus enemyStatus;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
        enemyBossManager = GetComponentInParent<EnemyBossManager>();
        enemyStatus = GetComponentInParent<EnemyStatus>();
    }

    public override void TakeCriticalDamageAnimationEvent()
    {
        enemyStatus.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
        enemyManager.pendingCriticalDamage = 0;
    }

    public  void CanRotate()
    {
        animator.SetBool("canRotate", true);
    }

    public  void StopRotation()
    {
        animator.SetBool("canRotate", false);
    }

    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }

    public void EnableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", true);
    }

    public void DisableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", false);

    }

    public void EnableIsParrying()
    {
        enemyManager.isParrying = true;
    }

    public void DisableIsParrying()
    {
        enemyManager.isParrying = false;
    }

    public void EnableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        enemyManager.canBeRiposted = false;
    }

    public void AwardSoulsOnDeath()
    {

        PlayerStatsManager playerstatus = FindObjectOfType<PlayerStatsManager>();
        SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

        if (playerstatus != null)
        {
            playerstatus.AddSouls(enemyStatus.soulsAwardedOnDeath);


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
