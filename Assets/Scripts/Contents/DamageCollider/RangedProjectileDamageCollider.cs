using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectileDamageCollider : DamageCollider
{
    public AmmoItem ammoItem;
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
            enemyManager.characterStatsManager.totalPoiseDefence = enemyManager.characterStatsManager.totalPoiseDefence - poiseDamage;

            contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            angleHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);

            TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
            takeDamageEffect.physicalDamage = physicalDamage;
            takeDamageEffect.fireDamage = fireDamage;
            takeDamageEffect.poiseDamage = poiseDamage;
            takeDamageEffect.contactPoint = contactPoint;
            takeDamageEffect.angleHitFrom = angleHitFrom;
            enemyManager.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);

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
    }

    private void FixedUpdate()
    {
        if(arrowRigidbody.velocity != Vector3.zero)
        {
            arrowRigidbody.rotation = Quaternion.LookRotation(arrowRigidbody.velocity);
        }
    }
}
