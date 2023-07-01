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

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        Debug.Log("COLLIDED");

        if (!hasColliede)
        {
            spellTarget = collision.transform.GetComponent<CharacterStatsManager>();

            // 자꾸 플레이어 손에서 터져서 임시로 넣음
            if (collision.gameObject.tag == "Player")
                return;

            if (spellTarget != null && spellTarget.teamIDNumber != teamIDNumber)
            {
                spellTarget.TakeDamage(0, fireDamage);
            }

            hasColliede = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

            Destroy(projectileParticles);
            Destroy(impactParticles, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
