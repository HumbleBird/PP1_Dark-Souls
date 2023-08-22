using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    bool hasColliede = false;

    CharacterManager spellTarget;
    Rigidbody rigidBody;

    Vector3 impactNormal; //Used to rotate the impact particles

    protected override void Awake()
    {
        base.Awake();

        rigidBody = GetComponent<Rigidbody>();
        damageCollider.isTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
        projectileParticles.transform.parent = transform;

        if(muzzleParticles)
        {
            muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
            Destroy(muzzleParticles, 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasColliede)
        {
            spellTarget = collision.transform.GetComponent<CharacterManager>();

            if (spellTarget != null && spellTarget.characterStatsManager.teamIDNumber != teamIDNumber)
            {

                TakeDamageEffect takeDamageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
                takeDamageEffect.physicalDamage = physicalDamage;
                takeDamageEffect.fireDamage = fireDamage;
                takeDamageEffect.poiseDamage = poiseDamage;
                takeDamageEffect.contactPoint = contactPoint;
                takeDamageEffect.angleHitFrom = angleHitFrom;
                spellTarget.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);
            }
            else
                return;

            hasColliede = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

            Destroy(projectileParticles);
            Destroy(impactParticles, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
