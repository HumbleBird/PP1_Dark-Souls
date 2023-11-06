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

    public AudioSource m_AudioSource;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        m_AudioSource = gameObject.GetOrAddComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        m_AudioSource.loop = false;
        m_AudioSource.spatialBlend = 1;
        m_AudioSource.maxDistance = 3;
    }

    public void PlayRandomSound(E_RandomSoundType type, List<AudioClip> audioClips)
    {
        if (audioClips.Count <= 0)
            return;

        int index = Random.Range(0, audioClips.Count);
        AudioClip ExpectedplaySound = audioClips[index];

        // �̹� �������� �÷��� �ߴ� Ŭ������ üũ
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

        // ����Ϸ��� Ŭ���� �ش� Ÿ���� ������ Ŭ���� �����ϰ�, Ŭ���� 1�� �̻��̶�� �ٸ� Ŭ������ ��ü
        if (tempLastClip == ExpectedplaySound && audioClips.Count > 1)
        {
            // �ش� ����� Ŭ���� �߿��� �ش� �ε��� ��ȣ�� ���� ����
            audioClips.Remove(ExpectedplaySound);
            int index2 = Random.Range(0, audioClips.Count);
            AudioClip ExpectedplaySound2 = audioClips[index];

            // ������ ���� ������Ʈ
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

        // ���� Ŭ���� �� �� ���̶�� �״�� ����
        Managers.Sound.Play(ExpectedplaySound);
    }

    public void PlayRandomDamageSound()
    {
        PlayRandomSound(E_RandomSoundType.Damage, takingDamageSounds);
    }

    public void PlayRandomWeaponWhoosheSound()
    {
        // �޼� ����� �̰�
        if (character.isUsingLeftHand)
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_SwrodWeaponWhooshes);
        }
        // �� ���̸� ���� ������(���� ����)����
        else
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandRightWeapon.m_SwrodWeaponWhooshes);
        }
    }

    public void PlayWeaponChargeSound()
    {
        // �޼� ����� �̰�
        if (character.isUsingLeftHand)
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_listWeaponChargeSounds);
        }
        // �� ���̸� ���� ������(���� ����)����
        else
        {
            PlayRandomSound(E_RandomSoundType.WeaponWhoose, character.characterEquipmentManager.m_CurrentHandRightWeapon.m_listWeaponChargeSounds);
        }
    }

    // �÷��̾� �� ��ü���� ����.
    public void FootStep()
    {
        string path = null;
        string moveState = null;
        string location = null;

        // ������
        location = "Rock";

        SetMoveStateSoundName(ref moveState);

        path = $"Footsteps";

        // �ش� ĳ������ ����� �ҽ�����
        string audioClipPath = $"Sounds/Effect/Character/{path}/{path}_{location}/{path}_{location}_{moveState}/{path}_{location}_{moveState}_07";

        Managers.Sound.SoundPlayFromCharacter(gameObject, audioClipPath, m_AudioSource);
    }

    protected virtual void SetMoveStateSoundName(ref string moveStateName)
    {

    }

    public void RollSoundPlay()
    {
        string audioClipPath = "Character/Common/Roll";
        Managers.Sound.SoundPlayFromCharacter(gameObject, audioClipPath, m_AudioSource);
    }
}
