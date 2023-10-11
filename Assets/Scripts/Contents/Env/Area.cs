using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PlayerManager player = other.GetComponent<PlayerManager>();
        if(player != null)
        {
            Managers.Game.EntryNewArea(gameObject.name);
        }
    }
}
