using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [Header("Weapon FX")]
    public ParticleSystem[] normalWeaponTrail;

    public void PlayWeaponFX()
    {
        for (int i = 0; i < normalWeaponTrail.Length; i++)
        {
            normalWeaponTrail[i].Stop();

            if (normalWeaponTrail[i].isStopped)
            {
                normalWeaponTrail[i].Play();
            }
        }
    }
}
