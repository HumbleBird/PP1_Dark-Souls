using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFXManager : MonoBehaviour
{
    CharacterManager character;
    AudioSource audioSource;

    [Header("Taking Damage Sounds")]
    public AudioClip[] takingDamageSounds;
    private List<AudioClip> potentialDamageSounds;
    private AudioClip lastDamagesoundPlayed;

    [Header("Taking Damage Sounds")]
    private List<AudioClip> potentialWeaponWhooshes;
    private AudioClip lastWeaponWhooshes;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        character = GetComponent<CharacterManager>();
    }

    public virtual void PlayRandomDamageSoundFX()
    {
        potentialWeaponWhooshes = new List<AudioClip>();

        foreach (var damageSound in takingDamageSounds)
        {
            if(damageSound != lastDamagesoundPlayed)
            {
                potentialDamageSounds.Add(damageSound);
            }
        }

        int randomValue = Random.Range(0, potentialDamageSounds.Count);
        lastDamagesoundPlayed = takingDamageSounds[randomValue];
        audioSource.PlayOneShot(takingDamageSounds[randomValue], 0.4f);
    }

    public virtual void PlayRandomWeaponWhoosh()
    {
        potentialWeaponWhooshes = new List<AudioClip>();

        if (character.isUsingRightHand)
        {
            foreach (var whooshSound in character.characterInventoryManager.rightWeapon.weaponWhooshes)
            {
                if (whooshSound != lastWeaponWhooshes)
                {
                    potentialWeaponWhooshes.Add(whooshSound);
                }
            }

            int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
            lastWeaponWhooshes = character.characterInventoryManager.rightWeapon.weaponWhooshes[randomValue];
            audioSource.PlayOneShot(character.characterInventoryManager.rightWeapon.weaponWhooshes[randomValue], 0.4f);
        }
        else
        {
            foreach (var whooshSound in character.characterInventoryManager.leftWeapon.weaponWhooshes)
            {
                if (whooshSound != lastWeaponWhooshes)
                {
                    potentialWeaponWhooshes.Add(whooshSound);
                }
            }

            int randomValue = Random.Range(0, potentialWeaponWhooshes.Count - 1);
            lastWeaponWhooshes = character.characterInventoryManager.leftWeapon.weaponWhooshes[randomValue];
            audioSource.PlayOneShot(character.characterInventoryManager.leftWeapon.weaponWhooshes[randomValue], 0.4f);
        }
    }
}
