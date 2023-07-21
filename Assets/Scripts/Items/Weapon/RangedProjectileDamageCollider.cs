using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectileDamageCollider : DamageCollider
{
    public RangedAmmoItem ammoItem;
    protected bool hasAlreadyPenetratedASurface;
    protected GameObject penetratedProjectile;
    public LayerMask shootAbleLayers;

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Character")
        {
            shieldHasBeenHit = false;
            hasBeenParried = false;

            CharacterStatsManager enemyStats = other.GetComponent<CharacterStatsManager>();
            CharacterManager enemyManager = other.GetComponent<CharacterManager>();
            CharacterEffectsManager enemyEffects = other.GetComponent<CharacterEffectsManager>();
            BlockingCollider shield = other.GetComponentInChildren<BlockingCollider>();

            if (enemyManager != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                CheckForParry(enemyManager);

                CheckForBlock(enemyManager, enemyStats, shield);
            }

            if (enemyStats != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                if (hasBeenParried)
                    return;

                if (shieldHasBeenHit)
                    return;

                enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;

                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                float directionHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                ChooseWhichDirectionDamageCameFrom(directionHitFrom);

                enemyEffects.PlayBloodSplatterFX(contactPoint);

                if (enemyStats.totalPoiseDefence > poiseBreak)
                {
                    enemyStats.TakeDamageNoAnimation(physicalDamage, fireDamage);
                }
                else
                {
                    enemyStats.TakeDamage(physicalDamage, 0, currentDamageAnimation, characterManager);
                }
            }
        }

        if (other.tag == "illusionary Wall")
        {
            IllusionaryWall illusionaryWall = other.GetComponent<IllusionaryWall>();

            if (illusionaryWall != null)
            {
                illusionaryWall.wallHasBennHit = true;
            }
        }

        if(!hasAlreadyPenetratedASurface && penetratedProjectile == null)
        {
            hasAlreadyPenetratedASurface = true;

            Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            GameObject penetratedArrow = Instantiate(ammoItem.penetratedModel, contactPoint, Quaternion.Euler(0, 0, 0));

            penetratedProjectile = penetratedArrow;
            penetratedArrow.transform.parent = other.transform;

            
            penetratedArrow.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);

            //penetratedArrow.transform.LookAt(gameObject.transform.forward);

            Vector3 childScale = penetratedArrow.transform.localScale;
            Vector3 parentScale = other.transform.localScale;

            penetratedArrow.transform.localScale = new Vector3(childScale.x / parentScale.x, childScale.y / parentScale.y, childScale.z / parentScale.z);

        }

        Destroy(transform.root.gameObject);
    }
}
