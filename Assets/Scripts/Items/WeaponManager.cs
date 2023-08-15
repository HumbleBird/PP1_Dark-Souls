using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponManager : MonoBehaviour
{
    [Header("Buff FX")]
    [SerializeField] GameObject physicalBuffFX;
    [SerializeField] GameObject fireBuffFX;

    [Header("Trail FX")]
    [SerializeField] WeaponTrail defaultTrailFX;
    [SerializeField] WeaponTrail fireTrailFX;

    private bool weaponIsBuffed;
    private BuffClass weaponBuffClass;

    [HideInInspector] public MeleeWeaponDamageCollider damageCollider;
    public AudioSource audioSource;

    private void Awake()
    {
        damageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void BuffWeapon(BuffClass buffClass, float physicalBuffDamage, float fireBuffDamage, float poiseBuffDamage)
    {
        DebuffWeapon();
        weaponIsBuffed = true;
        weaponBuffClass = buffClass;
        audioSource.Play();

        switch (buffClass)
        {
            case BuffClass.Physical:
                physicalBuffFX.SetActive(true);
                break;
            case BuffClass.Fire:
                fireBuffFX.SetActive(true);
                break;
            default:
                break;
        }

        damageCollider.physicalBuffDamage = physicalBuffDamage;
        damageCollider.fireBuffDamage = fireBuffDamage;
        damageCollider.poiseBuffDamage = poiseBuffDamage;
    }

    public void DebuffWeapon()
    {
        weaponIsBuffed = false;
        audioSource.Stop();
        physicalBuffFX.SetActive(false);
        fireBuffFX.SetActive(false);

        damageCollider.physicalBuffDamage = 0;
        damageCollider.fireBuffDamage = 0;
        damageCollider.poiseBuffDamage = 0;
    }

    public void PlayWeaponTrailFX()
    {
        if(weaponIsBuffed)
        {
            switch (weaponBuffClass)
            {
                // 무기가 Physically buffd 상태라면, default trail을 플레이
                case BuffClass.Physical:
                    if (defaultTrailFX == null)
                        return;
                    defaultTrailFX.PlayWeaponTrail();
                    break;
                // 무기가 fire buffd 상태라면, fire trail을 플레이
                case BuffClass.Fire:
                    if (fireTrailFX == null)
                        return;
                    fireTrailFX.PlayWeaponTrail();
                    break;
                default:
                    break;
            }
        }
    }

}