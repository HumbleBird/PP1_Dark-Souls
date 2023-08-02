using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectileDamageCollider : DamageCollider
{
    public RangedAmmoItem ammoItem;
    protected bool hasAlreadyPenetratedSurface;

    Rigidbody arrowRigidbody;
    CapsuleCollider arrowCapsuleCollider;

    protected override void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.enabled = true;

        characterManager = GetComponentInParent<CharacterManager>();

        arrowCapsuleCollider = GetComponent<CapsuleCollider>();
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        shieldHasBeenHit = false;
        hasBeenParried = false;

        CharacterManager enemyManager = collision.gameObject.GetComponentInParent<CharacterManager>();

        if (enemyManager != null)
        {
            if (enemyManager.characterStatsManager.teamIDNumber == teamIDNumber)
                return;

            CheckForParry(enemyManager);

            CheckForBlock(enemyManager);

            if (hasBeenParried)
                return;

            if (shieldHasBeenHit)
                return;

            enemyManager.characterStatsManager.poiseResetTimer = enemyManager.characterStatsManager.totalPoiseResetTime;
            enemyManager.characterStatsManager.totalPoiseDefence = enemyManager.characterStatsManager.totalPoiseDefence - poiseBreak;

            Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            float directionHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
            ChooseWhichDirectionDamageCameFrom(directionHitFrom);

            enemyManager.characterEffectsManager.PlayBloodSplatterFX(contactPoint);

            if (enemyManager.characterStatsManager.totalPoiseDefence > poiseBreak)
            {
                enemyManager.characterStatsManager.TakeDamageNoAnimation(physicalDamage, fireDamage);
            }
            else
            {
                enemyManager.characterStatsManager.TakeDamage(physicalDamage, 0, currentDamageAnimation, characterManager);
            }
        }

        if (collision.gameObject.tag == "illusionary Wall")
        {
            IllusionaryWall illusionaryWall = collision.gameObject.GetComponent<IllusionaryWall>();

            if (illusionaryWall != null)
            {
                illusionaryWall.wallHasBennHit = true;
            }
        }

        if(!hasAlreadyPenetratedSurface)
        {
            hasAlreadyPenetratedSurface = true;
            arrowRigidbody.isKinematic = true;
            arrowCapsuleCollider.enabled = false;

            gameObject.transform.position = collision.GetContact(0).point;
            gameObject.transform.rotation = Quaternion.LookRotation(transform.forward);
            gameObject.transform.parent = collision.collider.transform;
        }

        //Destroy(transform.root.gameObject);
    }

    private void FixedUpdate()
    {
        if(arrowRigidbody.velocity != Vector3.zero)
        {
            arrowRigidbody.rotation = Quaternion.LookRotation(arrowRigidbody.velocity);
        }
    }
}
