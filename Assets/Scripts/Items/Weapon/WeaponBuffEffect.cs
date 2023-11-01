using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Weapon Buff Effect")]
public class WeaponBuffEffect : CharacterEffect
{

    //public WeaponBuffEffect()
    //{
    //    buffAmbientSound = Managers.Resource.Load<AudioClip>("Item/Weapon/mixkit-big-fire-spell-burning-1332");

    //}

    [Header("Buff Info")]
    [SerializeField] BuffClass buffClass;
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

            weaponManager = character.characterWeaponSlotManager.rightHandDamageCollider.GetComponentInParent<WeaponManager>();
            weaponManager.audioSource.loop = true;
            weaponManager.audioSource.clip = buffAmbientSound;
            weaponManager.audioSource.volume = ambientSoundBolum;

            float baseWeaponDamag =
                weaponManager.damageCollider.physicalDamage +
                weaponManager.damageCollider.fireDamage;

            float physicalBuffDamage = 0;
            float fireBuffDamage = 0;
            float poiseBuffDamage = 0;

            if(buffPoiseDamage)
            {
                poiseBuffDamage = weaponManager.damageCollider.poiseDamage * (buffBasePoiseDamagePercentageMultiplier / 100);
            }

            switch (buffClass)
            {
                case BuffClass.Physical:
                    physicalBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                case BuffClass.Fire:
                    fireBuffDamage = baseWeaponDamag * (buffBaseDamagePercentageMultiplier / 100);
                    break;
                default:
                    break;
            }

            weaponManager.BuffWeapon(buffClass, physicalBuffDamage, fireBuffDamage, poiseBuffDamage);
        }

        if(buffHasStarted)
        {
            timeRemainingOnBuff -= 1;

            Debug.Log("TIME REMAING ON BUFF: + " + timeRemainingOnBuff);

            if(timeRemainingOnBuff  <= 0 )
            {
                weaponManager.DebuffWeapon();

                if(isRightHandedBuff)
                {
                    character.characterEffectsManager.rightWeaponBuffEffect = null;
                }
            }
        }
    }
}
