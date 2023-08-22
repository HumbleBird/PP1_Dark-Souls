using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    bool hasColliede = false;

    CharacterStatsManager spellTarget;
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
            spellTarget = collision.transform.GetComponent<CharacterStatsManager>();

            if (spellTarget != null && spellTarget.teamIDNumber != teamIDNumber)
            {
                //spellTarget.TakeDamage(0, fireDamage, currentDamageAnimation, characterManager);
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
