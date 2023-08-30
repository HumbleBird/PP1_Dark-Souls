using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFXManager : MonoBehaviour
{
    int tempcount = 0;

    [Header("Taking Damage Sounds")]
    public AudioClip[] takingDamageSounds;
    private AudioClip lastDamagesoundPlayed;

    [Header("Taking Damage Sounds")]
    private AudioClip lastWeaponWhooshes;

    public void PlayRandomDamageSound()
    {
        int index = Random.Range(0, takingDamageSounds.Length);
        AudioClip playSound = takingDamageSounds[index];

        if(playSound != lastDamagesoundPlayed)
        {
            Managers.Sound.Play(playSound);
            tempcount = 0;
            return;
        }

        while (true)
        {
            PlayRandomDamageSound();
            tempcount++;
            if (tempcount > 5)
                Debug.Log("clip이 없는지 확인 바람");
        }
    }

    public void PlayRandomWeaponWhoosh(AudioClip[] potentialWeaponWhooshes)
    {
        int index = Random.Range(0, potentialWeaponWhooshes.Length);
        AudioClip playSound = potentialWeaponWhooshes[index];

        if (playSound != lastDamagesoundPlayed)
        {
            Managers.Sound.Play(playSound);
            tempcount = 0;
            return;
        }

        while (true)
        {
            PlayRandomDamageSound();
            tempcount++;
            if (tempcount > 5)
                Debug.Log("clip이 없는지 확인 바람");
        }
    }
}
