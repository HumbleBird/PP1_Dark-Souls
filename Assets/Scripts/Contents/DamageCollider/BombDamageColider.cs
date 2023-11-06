using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamageColider : DamageCollider
{
    [Header("Explosive Damage & Radius")]
    public int explosiveRaius = 1;
    public int explosionDamage;
    public int explosionSplashDamage;

    public Rigidbody bombRigidBody;
    private bool hasCollided = false;
    public GameObject impactPaticles;

    protected override void Awake()
    {
        damageCollider = GetComponent<Collider>();
        bombRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!hasCollided && collision.gameObject != characterManager.gameObject)
        {
            hasCollided = true;
            impactPaticles = Instantiate(impactPaticles, transform.position, Quaternion.identity);
            Explode();

            CharacterManager character = collision.transform.GetComponent<CharacterManager>();

            if(character != null)
            {
                if(character.characterStatsManager.teamIDNumber != teamIDNumber)
                {

                    TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
                    takeDamageEffect.characterCausingDamage = characterManager;
                    takeDamageEffect.m_PhysicalDamage = physicalDamage;
                    takeDamageEffect.m_MagicDamage = magicDamage;
                    takeDamageEffect.m_FireDamage = fireDamage;
                    takeDamageEffect.m_LightningDamage = lightningDamage;
                    takeDamageEffect.m_DarkDamage = darkDamage;
                    takeDamageEffect.poiseDamage = poiseDamage;
                    takeDamageEffect.contactPoint = contactPoint;
                    takeDamageEffect.angleHitFrom = angleHitFrom;
                    character.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);
                }
            }

            Destroy(impactPaticles, 5f);
            Destroy(transform.parent.gameObject);
        }
    }

    private void Explode()
    {
        Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRaius);

        foreach (Collider objectsInExplosion in characters)
        {
            CharacterManager character = objectsInExplosion.GetComponent<CharacterManager>();

            if(character != null)
            {
                if (character.characterStatsManager.teamIDNumber != teamIDNumber)
                {
                    TakeDamageEffect takeDamageEffect = new TakeDamageEffect();
                    // Instantiate(Managers.WorldEffect.takeDamageEffect);
                    takeDamageEffect.characterCausingDamage = characterManager;
                    takeDamageEffect.m_PhysicalDamage = physicalDamage;
                    takeDamageEffect.m_MagicDamage = magicDamage;
                    takeDamageEffect.m_FireDamage = fireDamage;
                    takeDamageEffect.m_LightningDamage = lightningDamage;
                    takeDamageEffect.m_DarkDamage = darkDamage;
                    takeDamageEffect.poiseDamage = poiseDamage;
                    takeDamageEffect.contactPoint = contactPoint;
                    takeDamageEffect.angleHitFrom = angleHitFrom;
                    character.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);
                }

            }
        }
    }
}
