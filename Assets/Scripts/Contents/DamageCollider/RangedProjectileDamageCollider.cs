using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class RangedProjectileDamageCollider : DamageCollider
{
    public AmmoItem ammoItem;
    protected bool hasAlreadyPenetratedSurface;

    Rigidbody arrowRigidbody;
    CapsuleCollider arrowCapsuleCollider;
    public WeaponItem m_BowItem = null;

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

        if (enemyManager == ammoItem.m_Shooter)
            return;

        if (enemyManager != null)
        {
            //if (enemyManager.characterStatsManager.teamIDNumber == teamIDNumber)
            //    return;

            CheckForParry(enemyManager);

            CheckForBlock(enemyManager);

            if (hasBeenParried)
                return;

            if (shieldHasBeenHit)
                return;

            enemyManager.characterStatsManager.poiseResetTimer = enemyManager.characterStatsManager.totalPoiseResetTime;
            enemyManager.characterStatsManager.m_fTotalPoiseDefence = enemyManager.characterStatsManager.m_fTotalPoiseDefence - poiseDamage;

            contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            angleHitFrom = Vector3.SignedAngle(characterManager.transform.forward, enemyManager.transform.forward, Vector3.up);



            TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
            takeDamageEffect.characterCausingDamage = characterManager;
            takeDamageEffect.m_PhysicalDamage = m_BowItem.m_iPhysicalDamage + ammoItem.m_iPhysicalDamage;
            takeDamageEffect.m_MagicDamage = m_BowItem.m_iMagicDamage + ammoItem.m_iMagicDamage;
            takeDamageEffect.m_FireDamage = m_BowItem.m_iFireDamage + ammoItem.m_iFireDamage;
            takeDamageEffect.m_LightningDamage = m_BowItem.m_iLightningDamage + ammoItem.m_iLightningDamage;
            takeDamageEffect.m_DarkDamage = m_BowItem.m_iDarkDamage + ammoItem.m_iDarkDamage;
            takeDamageEffect.m_iCritiCalDamage = m_BowItem.m_iCriticalDamage + ammoItem.m_iCriticalDamage;

            takeDamageEffect.m_iBleed  = m_BowItem.m_iBleeding + ammoItem.m_iBleeding; 
            takeDamageEffect.m_iPosion = m_BowItem.m_iPoison + ammoItem.m_iPoison;
            takeDamageEffect.m_iForst = m_BowItem.m_iFrost + ammoItem.m_iFrost;

            takeDamageEffect.poiseDamage = poiseDamage + ammoItem.m_iPhysicalDamage;
            takeDamageEffect.contactPoint = contactPoint;
            takeDamageEffect.angleHitFrom = angleHitFrom;
            enemyManager.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);

        }

        if (collision.gameObject.tag == "illusionary Wall")
        {
            IllusionaryWall illusionaryWall = collision.gameObject.GetComponent<IllusionaryWall>();

            if (illusionaryWall != null && characterManager.characterStatsManager.teamIDNumber == (int)E_TeamId.Player)
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
