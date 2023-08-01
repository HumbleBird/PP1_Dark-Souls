using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponTrail : MonoBehaviour
{
    public eWeaponTrail eWeaponTrail;
    public ParticleSystem[] normalWeaponTrail;

    private void Awake()
    {
        normalWeaponTrail = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayWeaponTrail()
    {
        for (int i = 0; i < normalWeaponTrail.Length; i++)
        {
            normalWeaponTrail[i].Stop();

            if(normalWeaponTrail[i].isStopped)
            {
                normalWeaponTrail[i].Play();
            }
        }
    }
}
