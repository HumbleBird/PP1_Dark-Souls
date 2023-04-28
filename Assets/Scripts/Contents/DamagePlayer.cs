using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 25;

    public void OnTriggerEnter(Collider other)
    {
        PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();

        if(playerStatus != null)
        {
            playerStatus.TakeDamage(damage);
        }
    }
}
