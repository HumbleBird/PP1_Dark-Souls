using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterSoundFXManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Taking Damage Sounds")]
    public List<AudioClip> takingDamageSounds;

    private AudioClip tempLastClip;
    private AudioClip m_LastDamagesoundPlayed;
    private AudioClip m_LastBlockDamageAudioClip;
    private AudioClip m_LastWeaponWhooshes;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public void PlayRandomSound(E_RandomSoundType type, List<AudioClip> audioClips)
    {
        if (audioClips.Count <= 0)
            return;

        int index = Random.Range(0, audioClips.Count);
        AudioClip ExpectedplaySound = audioClips[index];

        // 이미 마지막에 플레이 했던 클립인지 체크
        if (type == E_RandomSoundType.Damage)
        {
            tempLastClip = m_LastDamagesoundPlayed;
        }
        else if (type == E_RandomSoundType.Block)
        {
            tempLastClip = m_LastBlockDamageAudioClip;

        }
        else if (type == E_RandomSoundType.WeaponWhoose)
        {
            tempLastClip = m_LastWeaponWhooshes;

        }

        // 재생하려는 클립이 해당 타입의 마지막 클립과 동일하고, 클립이 1개 이상이라면 다른 클립으로 대체
        if (tempLastClip == ExpectedplaySound && audioClips.Count > 1)
        {
            // 해당 오디오 클립들 중에서 해당 인덱스 번호만 빼고 진행
            audioClips.Remove(ExpectedplaySound);
            int index2 = Random.Range(0, audioClips.Count);
            AudioClip ExpectedplaySound2 = audioClips[index];

            // 마지막 사운드 업데이트
            if (type == E_RandomSoundType.Damage)
            {
                m_LastDamagesoundPlayed = ExpectedplaySound;
            }
            else if (type == E_RandomSoundType.Block)
            {
                m_LastBlockDamageAudioClip = ExpectedplaySound;

            }
            else if (type == E_RandomSoundType.WeaponWhoose)
            {
                m_LastWeaponWhooshes = ExpectedplaySound;
            }
        }

        // 만약 클립이 한 개 뿐이라면 그대로 진행
        Managers.Sound.Play(ExpectedplaySound);
    }

    public void PlayRandomDamageSound()
    {
        PlayRandomSound(E_RandomSoundType.Damage, takingDamageSounds);
    }

    public void PlayRandomWeaponWhoosheSound()
    {
        // 왼순 무기면 이것
        if (character.isUsingLeftHand)
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandLeftWeapon.weaponWhooshes);
        }
        // 한 손이면 왼족 아이템(보통 방패)으로
        else
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandRightWeapon.weaponWhooshes);
        }
    }

    // 플레이어 몸 자체에서 보냄.
    public virtual void FootStep()
    {
        // 현재 지형에 따라

        // 현재 걷는지, 띄는지, 점프 하는지에 따라

        Managers.Sound.Play("character/FootStep/");

    }

    public virtual void DeadSoundPlay()
    {

    }
}
