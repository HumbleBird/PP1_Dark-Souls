using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponFX : MonoBehaviour
{
    [Header("Weapon FX")]
    public WeaponTrail[] m_WeaponTrails;

    private void Awake()
    {
        m_WeaponTrails = GetComponentsInChildren<WeaponTrail>();
    }

    public void PlayWeaponFX(eWeaponTrail eWeaponTrail = eWeaponTrail.Normal)
    {
        for (int i = 0; i < m_WeaponTrails.Length; i++)
        {
            if (m_WeaponTrails[i].eWeaponTrail == eWeaponTrail)
            {
                m_WeaponTrails[i].PlayWeaponTrail();
                return;
            }

        }
    }
}
