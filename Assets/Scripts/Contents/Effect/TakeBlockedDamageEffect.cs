using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Take Blocked Damage")]
public class TakeBlockedDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // 데미지 유발이 캐릭터라면, they are listed here?

    [Header("Base Damage")]
    public float physicalDamage = 0;
    public float fireDamage = 0;
    public float staminaDamage = 0;
    public float poiseDamage = 0;

    [Header("Animation")]
    public string blockAnimation;


    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        // 사망 상태
        if (character.isDead)
            return;

        // 취약 상태
        if (character.isInvulnerable)
            return;

        // 데미지 계산
        CalculateDamage(character);

        // Block Stamina 계산
        CalculateStaminaDamage(character);

        // 데미지 block 애니메이션 재생
        PlayBlockDamageAnimation(character);

        // Block 사운드
        PlayBlockDamageSoundFX(character);

        // character가 A.I라면, 공격한 캐릭터를 새로운 타겟으로 설정
        AssignNewAITarget(character);

        // 캐릭터의 Guard 상태 체크
        CheckPlayerGuardState(character);

        // character가 Player라면, 카메라 쉐이크 효과를 줌.
        SetCameraShake(character);
    }

    private void CalculateDamage(CharacterManager character)
    {
        if (character.isDead)
            return;

        if(characterCausingDamage != null)
        {
            // Damage defense 계산 전, 공격 데미지 수치 체크
            physicalDamage = Mathf.RoundToInt(physicalDamage * (characterCausingDamage.characterStatsManager.physicalDamagePercentageModifier / 100));
            fireDamage = Mathf.RoundToInt(fireDamage * (characterCausingDamage.characterStatsManager.fireDamagePercentageModifier / 100));
        }

        character.characterAnimatorManager.EraseHandIKForWeapon();

        float totalPhysicalDamageAbsorption = 1 -
            (1 - character.characterStatsManager.physicalDamageAbsorptionHead / 100) *
            (1 - character.characterStatsManager.physicalDamageAbsorptionBody / 100) *
            (1 - character.characterStatsManager.physicalDamageAbsorptionLegs / 100) *
            (1 - character.characterStatsManager.physicalDamageAbsorptionHands / 100);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        float totalfireDamageAbsorption = 1 -
            (1 - character.characterStatsManager.fireDamageAbsorptionHead / 100) *
            (1 - character.characterStatsManager.fireDamageAbsorptionBody / 100) *
            (1 - character.characterStatsManager.fireDamageAbsorptionLegs / 100) *
            (1 - character.characterStatsManager.fireDamageAbsorptionHands / 100);

        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalfireDamageAbsorption));

        physicalDamage = physicalDamage - Mathf.RoundToInt(physicalDamage * (character.characterStatsManager.physicalAbsorptionPercentageModifier / 100));
        fireDamage = fireDamage - Mathf.RoundToInt(fireDamage * (character.characterStatsManager.fireAbsorptionPercentageModifier / 100));

        float finalDamage = physicalDamage + fireDamage; // + magic + lightning + dark Damage

        character.characterStatsManager.currentHealth = Mathf.RoundToInt(character.characterStatsManager.currentHealth - finalDamage);

        // character가 Player라면 UI 갱신
        PlayerManager player = character as PlayerManager;
        if (player != null)
        {
            player.m_GameSceneUI.m_StatBarsUI.RefreshUI(E_StatUI.Hp);
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
            player.playerStatsManager.currentStamina -= staminaDamageAfterAbsorption;
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
        // 두 손이면 오른쪽 무기를
        if(character.isTwoHandingWeapon)
        {
            character.characterSoundFXManager.PlayRandomSound(E_RandomSoundType.Block, character.characterEquipmentManager.m_CurrentHandRightWeapon.blockingNoises);
        }
        // 한 손이면 왼족 아이템(보통 방패)으로
        else
        {
            character.characterSoundFXManager.PlayRandomSound(E_RandomSoundType.Block, character.characterEquipmentManager.m_CurrentHandLeftWeapon.blockingNoises);
        }
    }

    private void AssignNewAITarget(CharacterManager character)
    {
        AICharacterManager aiCharacter = character as AICharacterManager;

        if (aiCharacter != null && characterCausingDamage != null)
        {
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
}
