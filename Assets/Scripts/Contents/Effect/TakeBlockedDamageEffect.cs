using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Take Blocked Damage")]
public class TakeBlockedDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // ������ ������ ĳ���Ͷ��, they are listed here?

    [Header("Base Damage")]
    public float m_fFinalDamage;
    public float m_PhysicalDamage;
    public float m_MagicDamage;
    public float m_FireDamage;
    public float m_LightningDamage;
    public float m_DarkDamage;

    public float staminaDamage = 10;


    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false;

    [Header("Animation")]
    public string blockAnimation;


    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        // ��� ����
        if (character.isDead)
            return;

        // ��� ����
        if (character.isInvulnerable)
            return;

        // ������ ���
        CalculateDamage(character);

        // Block Stamina ���
        CalculateStaminaDamage(character);

        // ������ block �ִϸ��̼� ���
        PlayBlockDamageAnimation(character);

        // UI ������Ʈ
        CharacterHealthBarUIUpdate(character);

        // Block ����
        PlayBlockDamageSoundFX(character);

        // character�� A.I���, ������ ĳ���͸� ���ο� Ÿ������ ����
        AssignNewAITarget(character);

        // ĳ������ Guard ���� üũ
        CheckPlayerGuardState(character);

        // character�� Player���, ī�޶� ����ũ ȿ���� ��.
        SetCameraShake(character);
    }

    private void CalculateDamage(CharacterManager character)
    {
        if (character.isDead)
            return;

        // �÷��̾�
        if (character.characterStatsManager.teamIDNumber == (int)E_TeamId.Player)
        {
            // ������ ����
            // (���� ���ݷ�-���� ����) * (1-���� ������) + (�Ӽ� ���ݷ� - �Ӽ� ����) * (1-�Ӽ� ������)
            if (characterCausingDamage != null)
            {
                // (���� ���ݷ�-���� ����)
                float physicalDamage = Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fPhysicalDamage - character.characterStatsManager.m_iPhysicalDefense);

                // (1-���� ������)
                float physicalDamageReductionValue = 1 - character.characterStatsManager.m_fPhysicalDamageAbsorption / 100;

                // (�Ӽ� ���ݷ� - �Ӽ� ����) * (1-�Ӽ� ������)
                // Magic
                float MagicDamage = Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fMagicDamage - character.characterStatsManager.m_iMagicDefense);
                float MagicDamageReductionValue = 1 - character.characterStatsManager.m_fMagicDamageAbsorption / 100;

                // Fire
                float FireDamage = Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fFireDamage - character.characterStatsManager.m_iFireDefense);
                float FireDamageReductionValue = 1 - character.characterStatsManager.m_fFireDamageAbsorption / 100;

                // Lightning
                float LightningDamage = Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fLightningDamage - character.characterStatsManager.m_iLightningDefense);
                float LightningDamageReductionValue = 1 - character.characterStatsManager.m_fLightningDamageAbsorption / 100;

                //Dark
                float DarkDamage = Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fDarkDamage - character.characterStatsManager.m_iDarkDefense);
                float DarkDamageReductionValue = 1 - character.characterStatsManager.m_fDarkDamageAbsorption / 100;

                // Ư�� ȿ��
                // Bleed
                // Poison
                // Frost
                // Curse

                m_PhysicalDamage = physicalDamage * physicalDamageReductionValue;
                m_MagicDamage = MagicDamage * MagicDamageReductionValue;
                m_FireDamage = FireDamage * FireDamageReductionValue;
                m_LightningDamage = LightningDamage * LightningDamageReductionValue;
                m_DarkDamage = DarkDamage * DarkDamageReductionValue;
            }
        }
        // ����
        else
        {
            // �鿪 ����
            // ü�� - (���ݷ� * ����) / ����
            //m_PhysicalDamage = characterCausingDamage.characterStatsManager.m_fPhysicalDamage;
            //m_MagicDamage = characterCausingDamage.characterStatsManager.m_fMagicDamage;
            //m_FireDamage = characterCausingDamage.characterStatsManager.m_fFireDamage;
            //m_LightningDamage = characterCausingDamage.characterStatsManager.m_fLightningDamage;
            //m_DarkDamage = characterCausingDamage.characterStatsManager.m_fDarkDamage;
        }

        character.characterAnimatorManager.EraseHandIKForWeapon();

        WeaponItem ShiledItem;
        float Modifier = 1;
        // Block Caculated
        if(character.isUsingRightHand)
        {
            if (character.isTwoHandingWeapon)
            {
                Modifier = 1.5f;
            }
            ShiledItem = character.characterEquipmentManager.m_CurrentHandRightWeapon;

        }
        else if (character.isUsingLeftHand)
        {
            ShiledItem = character.characterEquipmentManager.m_CurrentHandLeftWeapon;
        }

        // Blcok ������ ���
        {

            // (1-������)
            float physicalDamageGuardAbsorption = 1 - character.characterStatsManager.m_fPhysicalDamageAbsorption / 100;

            // Magic
            float MagicDamageGuardAbsorption = 1 - character.characterStatsManager.m_fMagicDamageAbsorption / 100;

            // Fire
            float FireDamageGuardAbsorption = 1 - character.characterStatsManager.m_fFireDamageAbsorption / 100;

            // Lightning
            float LightningDamageGuardAbsorption = 1 - character.characterStatsManager.m_fLightningDamageAbsorption / 100;

            //Dark
            float DarkDamageGuardAbsorption = 1 - character.characterStatsManager.m_fDarkDamageAbsorption / 100;

            m_PhysicalDamage *= physicalDamageGuardAbsorption;
            m_MagicDamage *= MagicDamageGuardAbsorption;
            m_FireDamage *= FireDamageGuardAbsorption;
            m_LightningDamage  *= LightningDamageGuardAbsorption;
            m_DarkDamage *= DarkDamageGuardAbsorption;
        }

        // Ư�� ȿ��
        // Bleed
        // Poison
        // Frost
        // Curse

        m_fFinalDamage = m_PhysicalDamage + m_MagicDamage + m_FireDamage + m_LightningDamage + m_DarkDamage;

        character.characterStatsManager.currentHealth = Mathf.RoundToInt(character.characterStatsManager.currentHealth - m_fFinalDamage);

        // Temp
        poiseDamage = 30;
        if (character.characterStatsManager.m_fTotalPoiseDefence < poiseDamage)
        {
            poiseIsBroken = true;
        }

        if (character.characterStatsManager.currentHealth <= 0)
        {
            character.Dead();
        }
    }

    private void CalculateStaminaDamage(CharacterManager character)
    {
        float staminaDamageAbsorption = staminaDamage * (character.characterStatsManager.blockingStabilityRating / 100);
        float staminaDamageAfterAbsorption = staminaDamage - staminaDamageAbsorption;

        PlayerManager player = character as PlayerManager;
        if (player != null)
        {
            player.playerStatsManager.currentStamina -= Mathf.RoundToInt( staminaDamageAfterAbsorption);
            Managers.GameUI.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Stamina);
        }
    }

    private void PlayBlockDamageAnimation(CharacterManager character)
    {
        if (character.isDead)
        {
            character.characterWeaponSlotManager.CloseDamageCollider();
            character.characterAnimatorManager.PlayTargetAnimation("Dead_01", true);
        }
        else
        {
            // OWN HANDED BLOCK ANIMATION
            if (!character.isTwoHandingWeapon)
            {
                // Poise backet < 25         small
                // Poise backet > 25 < 50    medium
                // Poise backet > 50 < 75    large
                // Poise backet > 75         colosaal

                if (poiseDamage <= 24 && poiseDamage >= 0)
                {
                    blockAnimation = "OH_Block_Guard_Ping_01";
                    return;
                }
                else if (poiseDamage <= 49 && poiseDamage >= 25)
                {
                    blockAnimation = "OH_Block_Guard_Light_01";
                    return;

                }
                else if (poiseDamage <= 74 && poiseDamage >= 50)
                {
                    blockAnimation = "OH_Block_Guard_Medium_01";
                    return;

                }
                else if (poiseDamage <= 75)
                {
                    blockAnimation = "OH_Block_Guard_Heavy_01";
                    return;
                }
            }
            else
            {
                // Poise backet < 25         small
                // Poise backet > 25 < 50    medium
                // Poise backet > 50 < 75    large
                // Poise backet > 75         colosaal

                if (poiseDamage <= 24 && poiseDamage >= 0)
                {
                    blockAnimation = "TH_Block_Guard_Ping_01";
                    return;
                }
                else if (poiseDamage <= 49 && poiseDamage >= 25)
                {
                    blockAnimation = "TH_Block_Guard_Light_01";
                    return;

                }
                else if (poiseDamage <= 74 && poiseDamage >= 50)
                {
                    blockAnimation = "TH_Block_Guard_Medium_01";
                    return;

                }
                else if (poiseDamage <= 75)
                {
                    blockAnimation = "TH_Block_Guard_Heavy_01";
                    return;
                }
            }
        }
    }

    private void PlayBlockDamageSoundFX(CharacterManager character)
    {
        // �� ���̸� ������ ���⸦
        if(character.isTwoHandingWeapon)
        {
            character.characterSoundFXManager.PlayRandomSound(E_RandomSoundType.Block, character.characterEquipmentManager.m_CurrentHandRightWeapon.m_listBlockingNoises);
        }
        // �� ���̸� ���� ������(���� ����)����
        else
        {
            character.characterSoundFXManager.PlayRandomSound(E_RandomSoundType.Block, character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_listBlockingNoises);
        }
    }

    private void AssignNewAITarget(CharacterManager character)
    {
        AICharacterManager aiCharacter = character as AICharacterManager;


        if (aiCharacter != null && characterCausingDamage != null)
        {
            if (aiCharacter.aiCharacterStatsManager.teamIDNumber == characterCausingDamage.characterStatsManager.teamIDNumber)
                return;

            aiCharacter.currentTarget = characterCausingDamage;
        }
    }

    private void CheckPlayerGuardState(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;
        if (player != null)
        {
            if (player.playerStatsManager.currentStamina <= 0)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Guard_Break_01", true);
                player.canBeRiposted = true;
                //player.playerSoundFXManager.PlayGuardBreakSound();
                player.isBlocking = false;
            }
            else
            {
                player.playerAnimatorManager.PlayTargetAnimation(blockAnimation, true);
                player.isAttacking = false;
            }
        }
    }

    private void SetCameraShake(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        if (player != null)
        {
            Managers.Camera.CameraShake(1);
        }
    }

    private void CharacterHealthBarUIUpdate(CharacterManager character)
    {
        // ���Ͷ�� �� �Ʒ��� �ִ� Helath Bar�� ����

        // �÷��̾��� UI�� ����
        character.characterStatsManager.HealthBarUIUpdate(Mathf.RoundToInt(m_fFinalDamage));
    }
}
