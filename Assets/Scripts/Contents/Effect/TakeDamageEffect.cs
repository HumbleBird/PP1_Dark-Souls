using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Take Damage")]
public class TakeDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // 데미지 유발이 캐릭터라면, they are listed here?

    [Header("Damage")]
    public float finalDamage = 0;
    public float physicalDamage = 0;
    public float fireDamage = 0;

    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false;

    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("SFX")]
    public bool willPlayDamageDFX = true;
    public AudioClip elementalDamageSoundSFX; // ELEMENTAL(Fire, Magic, Darks, Lightning) Damage 일 떄 플레이

    [Header("Direction Damage Taken From")]
    public float angleHitFrom;
    public Vector3 contactPoint; // 캐릭터 바디 데미지 발생 지점

    public override void ProcessEffect(CharacterManager character)
    {
        // 사망 상태
        if (character.isDead)
            return;

        // 취약 상태
        if (character.isInvulnerable)
            return;

        // 데미지 계산
        CalculateDamage(character);

        // 방향과 poise 에 따른 데미지 애니메이션
        CheckWhichDirectionDamageCameFrom(character);

        // 데미지 애니메이션 재생
        PlayDamageAnimation(character);

        // UI 업데이트
        CharacterHealthBarUIUpdate(character);

        // 사운드
        PlayDamageSoundFX(character);

        // 피 효과
        PlayBloodSplatter(character);

        // character가 A.I라면, 공격한 캐릭터를 새로운 타겟으로 설정
        AssignNewAITarget(character);

        // character가 Player라면, 카메라 쉐이크 효과를 줌.
        SetCameraShake(character);
    }

    private void CalculateDamage(CharacterManager  character)
    {
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

        finalDamage = physicalDamage + fireDamage; // + fire + mage + lightning + dark Damage

        character.characterStatsManager.currentHealth = Mathf.RoundToInt(character.characterStatsManager.currentHealth - finalDamage);



        if(character.characterStatsManager.totalPoiseDefence < poiseDamage)
        {
            poiseIsBroken = true;
        }

        if (character.characterStatsManager.currentHealth <= 0)
        {
            character.Dead();
        }
    }

    private  void CheckWhichDirectionDamageCameFrom(CharacterManager character)
    {
        if (manuallySelectDamageAnimation)
            return;

        if (angleHitFrom >= 145 && angleHitFrom <= 180)
        {
            ChooseDamageAnimationForward(character);
        }
        else if (angleHitFrom <= -145 && angleHitFrom >= -180)
        {
            ChooseDamageAnimationForward(character);

        }
        else if (angleHitFrom >= -45 && angleHitFrom <= 45)
        {
            ChooseDamageAnimationBackward(character);

        }
        else if (angleHitFrom >= -144 && angleHitFrom <= -45)
        {
            ChooseDamageAnimationLeft(character);

        }
        else if (angleHitFrom >= 45 && angleHitFrom <= 144)
        {
            ChooseDamageAnimationRight(character);

        }
    }

    private void ChooseDamageAnimationForward(CharacterManager character)
    {
        // Poise backet < 25         small
        // Poise backet > 25 < 50    medium
        // Poise backet > 50 < 75    large
        // Poise backet > 75         colosaal

        if(poiseDamage <= 24 && poiseDamage >= 0)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Forward);
            return;
        }
        else if (poiseDamage <= 49 && poiseDamage >= 25)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Forward);
            return;

        }
        else if (poiseDamage <= 74 && poiseDamage >= 50)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Heavy_Forward);
            return;

        }
        else if (poiseDamage <= 75)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Colssal_Forward);
            return;
        }
    }

    private void ChooseDamageAnimationBackward(CharacterManager character)
    {
        if (poiseDamage <= 24 && poiseDamage >= 0)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Back);
            return;
        }
        else if (poiseDamage <= 49 && poiseDamage >= 25)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Back);
            return;

        }
        else if (poiseDamage <= 74 && poiseDamage >= 50)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Heavy_Back);
            return;

        }
        else if (poiseDamage <= 75)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Colssal_Back);
            return;
        }
    }

    private void ChooseDamageAnimationLeft(CharacterManager character)
    {
        if (poiseDamage <= 24 && poiseDamage >= 0)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Left);
            return;
        }
        else if (poiseDamage <= 49 && poiseDamage >= 25)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Left);
            return;

        }
        else if (poiseDamage <= 74 && poiseDamage >= 50)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Heavy_Left);
            return;

        }
        else if (poiseDamage <= 75)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Colssal_Left);
            return;
        }
    }

    private void ChooseDamageAnimationRight(CharacterManager character)
    {
        if (poiseDamage <= 24 && poiseDamage >= 0)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Right);
            return;
        }
        else if (poiseDamage <= 49 && poiseDamage >= 25)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Medium_Right);
            return;

        }
        else if (poiseDamage <= 74 && poiseDamage >= 50)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Heavy_Right);
            return;

        }
        else if (poiseDamage <= 75)
        {
            damageAnimation = character.characterAnimatorManager.GetRandomDamageAnimationFromList(character.characterAnimatorManager.Damage_Animation_Colssal_Right);
            return;
        }
    }

    private void CharacterHealthBarUIUpdate(CharacterManager character)
    {
        // 몬스터라면 지 아래에 있는 Helath Bar를 수정

        // 플레이어라면 UI를 수정
        character.characterStatsManager.HealthBarUIUpdate(Mathf.RoundToInt( finalDamage));
    }
    

    private void PlayDamageSoundFX(CharacterManager character)
    {
        character.characterSoundFXManager.PlayRandomDamageSound();

        if(fireDamage > 0)
        {
            Managers.Sound.Play(elementalDamageSoundSFX);
        }
    }

    private void PlayDamageAnimation(CharacterManager character)
    {
        // light 혹은 heavy에 의한 데미지 애니메이션이 실행 중이라면
        // light damage 애니메이션이 재생되는 것을 원지 않고, heavy animation이 마무리 되기 원함.

        if (character.isInteracting && character.characterCombatManager.previousPoiseDamageTaken > poiseDamage)
        {
            // interacting 중이고 previous Pose Damage가 0보다 위라면,  damage animation이 재생 되어야 함.
            // previous poise가 current poise보다 크다면 데미지 애니메이션을 더 가벼운 애니메이션으로 변화하지 않는다 
            return;
        }

        if(character.isDead)
        {
            character.characterWeaponSlotManager.CloseDamageCollider();
            character.characterAnimatorManager.PlayTargetAnimation("Dead_01", true);
        }
        else
        {
            // 캐릭터 poise broken이 되지 않았다면, 데미지 애니메이션을 재생하지 않는다
            if (!poiseIsBroken)
            {
                return;
            }
            else
            {
                // 활성/비활성 stun lock

                if (playDamageAnimation)
                {
                    character.characterAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                }
            }
        }

    }

    private void PlayBloodSplatter(CharacterManager character)
    {
        character.characterEffectsManager.PlayBloodSplatterFX(contactPoint);
    }

    private void AssignNewAITarget(CharacterManager character)
    {
        AICharacterManager aiCharacter = character as AICharacterManager;

        if(aiCharacter != null && characterCausingDamage != null)
        {
            aiCharacter.currentTarget = characterCausingDamage;
        }
    }

    private void SetCameraShake(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        if(player != null)
        {
            Managers.Camera.CameraShake(1);
        }
    }
}




