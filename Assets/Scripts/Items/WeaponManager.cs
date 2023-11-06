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
    private E_WeaponBuffType weaponBuffClass;

    [HideInInspector] public MeleeWeaponDamageCollider damageCollider;
    public AudioSource audioSource;

    private void Awake()
    {
        damageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        audioSource = gameObject.GetOrAddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        DebuffWeapon();
    }

    public void BuffWeapon(E_WeaponBuffType buffClass, float physicalBuffDamage, float fireBuffDamage, float MagicBuffDamage, float LightningBuffDamage, float DarBuffDamage)
    {
        DebuffWeapon();
        weaponIsBuffed = true;
        weaponBuffClass = buffClass;
        audioSource.Play();

        switch (buffClass)
        {
            case E_WeaponBuffType.Physical:
                physicalBuffFX.SetActive(true);
                break;
            case E_WeaponBuffType.Fire:
                fireBuffFX.SetActive(true);
                break;
            case E_WeaponBuffType.Magic:
                break;
            case E_WeaponBuffType.Lightning:
                break;
            case E_WeaponBuffType.Dark:
                break;
            default:
                break;
        }

        damageCollider.m_MeleeWeapon_BuffDamage_Physical = physicalBuffDamage;
        damageCollider.m_MeleeWeapon_BuffDamage_Fire = fireBuffDamage;
        damageCollider.m_MeleeWeapon_BuffDamage_Magic = MagicBuffDamage;
        damageCollider.m_MeleeWeapon_BuffDamage_Lightning = LightningBuffDamage;
        damageCollider.m_MeleeWeapon_BuffDamage_Dark = DarBuffDamage;
    }

    public void DebuffWeapon()
    {
        weaponIsBuffed = false;
        if(audioSource != null)
            audioSource.Stop();
        physicalBuffFX.SetActive(false);
        fireBuffFX.SetActive(false);

        damageCollider.m_MeleeWeapon_BuffDamage_Physical = 0;
        damageCollider.m_MeleeWeapon_BuffDamage_Fire = 0;
    }

    public void PlayWeaponTrailFX()
    {
        if(weaponIsBuffed)
        {
            switch (weaponBuffClass)
            {
                // ���Ⱑ Physically buffd ���¶��, default trail�� �÷���
                case E_WeaponBuffType.Physical:
                    if (defaultTrailFX == null)
                        return;
                    defaultTrailFX.PlayWeaponTrail();
                    break;
                // ���Ⱑ fire buffd ���¶��, fire trail�� �÷���
                case E_WeaponBuffType.Fire:
                    if (fireTrailFX == null)
                        return;
                    fireTrailFX.PlayWeaponTrail();
                    break;
                case E_WeaponBuffType.Magic:
                    break;
                case E_WeaponBuffType.Lightning:
                    break;
                case E_WeaponBuffType.Dark:
                    break;
                default:
                    break;
            }
        }
        // Default
        else
        {
            if (defaultTrailFX == null)
                return;
            defaultTrailFX.PlayWeaponTrail();
        }
    }

}