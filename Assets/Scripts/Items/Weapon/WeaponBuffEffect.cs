using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Weapon Buff Effect")]
public class WeaponBuffEffect : CharacterEffect
{
    public WeaponBuffEffect(E_WeaponBuffType buffClass)
    {
        m_eBuffClass = buffClass;

        switch (m_eBuffClass)
        {
            case E_WeaponBuffType.Physical:
                buffAmbientSound = null;
                break;
            case E_WeaponBuffType.Fire:
                buffAmbientSound = Managers.Resource.Load<AudioClip>("Sounds/Effect/Buff/Weapon_Buff_Ambient_Fire");
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

    [Header("Buff Info")]
    [SerializeField] E_WeaponBuffType m_eBuffClass;
    [SerializeField] float lengthOfBuff = 60;
    public float timeRemainingOnBuff;
    [HideInInspector] public bool isRightHandedBuff;

    [Header("Buff SFX")]
    [SerializeField] AudioClip buffAmbientSound;
    [SerializeField] float ambientSoundBolum = 0.3f;

    [Header("Damage Info")]
    [SerializeField] float buffBaseDamagePercentageMultiplier = 15f;

    [Header("Poise Buff")]
    [SerializeField] bool buffPoiseDamage;
    [SerializeField] float buffBasePoiseDamagePercentageMultiplier = 15f;

    [Header("General")]
    [SerializeField] bool buffHasStarted = false;
    private WeaponManager weaponManager;

    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        if(!buffHasStarted)
        {
            timeRemainingOnBuff = lengthOfBuff;
            buffHasStarted = true;

            // Change Weapon Whoose
            character.characterEquipmentManager.m_CurrentHandRightWeapon.SetSound(m_eBuffClass);

            // Set Weapon Ambient Sound
            weaponManager = character.characterWeaponSlotManager.rightHandDamageCollider.GetComponentInParent<WeaponManager>();
            if(buffAmbientSound != null)
            {
                weaponManager.audioSource.loop = true;
                weaponManager.audioSource.clip = buffAmbientSound;
                weaponManager.audioSource.volume = ambientSoundBolum;
            }

            // Calcualted Damage
            float baseWeaponDamag =
                weaponManager.damageCollider.physicalDamage +
                weaponManager.damageCollider.fireDamage;

            float physicalBuffDamage = 0;
            float fireBuffDamage = 0;
            float MagicBuffDamage = 0;
            float LightningBuffDamage = 0;
            float DarkBuffDamage = 0;
            float poiseBuffDamage = 0; // 어디다 써먹지?

            // Poise
            if(buffPoiseDamage)
            {
                poiseBuffDamage = weaponManager.damageCollider.poiseDamage * (buffBasePoiseDamagePercentageMultiplier / 100);
            }

            switch (m_eBuffClass)
            {
                case E_WeaponBuffType.Physical:
                    physicalBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                case E_WeaponBuffType.Fire:
                    fireBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                case E_WeaponBuffType.Magic:
                    MagicBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                case E_WeaponBuffType.Lightning:
                    LightningBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                case E_WeaponBuffType.Dark:
                    DarkBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                default:
                    break;
            }

            weaponManager.BuffWeapon(m_eBuffClass, physicalBuffDamage, fireBuffDamage, MagicBuffDamage, LightningBuffDamage, DarkBuffDamage);
        }

        if(buffHasStarted)
        {
            timeRemainingOnBuff -= 1;

            if(timeRemainingOnBuff  <= 0 )
            {
                weaponManager.DebuffWeapon();

                // Change Weapon Whoose
                character.characterEquipmentManager.m_CurrentHandRightWeapon.SetSound();

                if (isRightHandedBuff)
                {
                    character.characterEffectsManager.rightWeaponBuffEffect = null;
                }
            }
        }
    }
}
