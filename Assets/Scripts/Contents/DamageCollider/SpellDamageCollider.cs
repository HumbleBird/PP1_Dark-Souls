using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    bool hasColliede = false;
    public bool m_isCanCollide = false;

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
        if (m_isCanCollide == false)
            return;

        if (!hasColliede)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticles);
                Destroy(impactParticles, 5f);
                Destroy(gameObject, 5f);

                hasColliede = true;
            }

            spellTarget = collision.transform.GetComponent<CharacterManager>();

            if (spellTarget != null && spellTarget.characterStatsManager.teamIDNumber != teamIDNumber)
            {

                TakeDamageEffect takeDamageEffect = new TakeDamageEffect();//Instantiate(Managers.WorldEffect.takeDamageEffect);
                takeDamageEffect.characterCausingDamage = characterManager;
                takeDamageEffect.m_PhysicalDamage = physicalDamage;
                takeDamageEffect.m_MagicDamage = magicDamage;
                takeDamageEffect.m_FireDamage = fireDamage;
                takeDamageEffect.m_LightningDamage = lightningDamage;
                takeDamageEffect.m_DarkDamage = darkDamage;
                takeDamageEffect.poiseDamage = poiseDamage;
                takeDamageEffect.contactPoint = contactPoint;
                takeDamageEffect.angleHitFrom = angleHitFrom;
                spellTarget.characterEffectsManager.ProcessEffectInstantly(takeDamageEffect);


                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticles);
                Destroy(impactParticles, 5f);
                Destroy(gameObject, 5f);

                hasColliede = true;
            }
            else
                return;

        }
    }
}
