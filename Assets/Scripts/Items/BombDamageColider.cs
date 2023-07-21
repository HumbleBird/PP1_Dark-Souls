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
        if(!hasCollided)
        {
            hasCollided = true;
            impactPaticles = Instantiate(impactPaticles, transform.position, Quaternion.identity);
            Explode();

            CharacterStatsManager character = collision.transform.GetComponent<CharacterStatsManager>();

            if(character != null)
            {
                if(character.teamIDNumber != teamIDNumber)
                {
                    character.TakeDamage(0, explosionDamage, currentDamageAnimation, characterManager);
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
            CharacterStatsManager character = objectsInExplosion.GetComponent<CharacterStatsManager>();

            if(character != null)
            {
                if (character.teamIDNumber != teamIDNumber)
                {
                    character.TakeDamage(0, explosionSplashDamage, currentDamageAnimation, characterManager);
                }

            }
        }
    }
}
