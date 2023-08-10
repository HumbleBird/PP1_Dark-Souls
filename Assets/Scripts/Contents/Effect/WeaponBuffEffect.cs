using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponBuffEffect : CharacterEffect
{
    [Header("Buff Info")]
    [SerializeField] BuffClass buffClass;

    [SerializeField] float lengthOfBuff;

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
}
