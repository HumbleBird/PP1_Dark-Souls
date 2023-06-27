using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 25;

    public void OnTriggerEnter(Collider other)
    {
        PlayerStatsManager playerStatsManager = other.gameObject.GetComponent<PlayerStatsManager>();

        if(playerStatsManager != null)
        {
            playerStatsManager.TakeDamage(damage);
        }
    }
}
