using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Take Damage")]
public class TakeDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // 데미지 유발이 캐릭터라면, they are listed here?

    [Header("Damage")]
    public float m_fFinalDamage;
    public float m_PhysicalDamage;
    public float m_MagicDamage;
    public float m_FireDamage;
    public float m_LightningDamage;
    public float m_DarkDamage;
    public int m_iCritiCalDamage;

    [Header("SpeicalEffect")]
    public int m_iBleed ;
    public int m_iPosion;
    public int m_iForst ;


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

    private void CalculateDamage(CharacterManager  character) // character가 공격 당하는 놈
    {

        // 플레이어
        if(character.characterStatsManager.teamIDNumber == (int)E_TeamId.Player)
        {
            // 데미지 공식
            // (물리 공격력-물리 방어력) * (1-물리 감소율) + (속성 공격력 - 속성 방어력) * (1-속성 감소율)
            PlayerManager player = character as PlayerManager;

            if (characterCausingDamage != null)
            {
                // (물리 공격력-물리 방어력)
                float physicalDamage =  Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fPhysicalDamage - character.characterStatsManager.m_iPhysicalDefense);

                // (1-물리 감소율)
                float physicalDamageReductionValue = 1 - character.characterStatsManager.m_fPhysicalDamageAbsorption / 100;

                // (속성 공격력 - 속성 방어력) * (1-속성 감소율)
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

                // 특수 효과
                // Bleed
                // Poison
                // Frost
                // Curse

               m_PhysicalDamage         = physicalDamage * physicalDamageReductionValue;
               m_MagicDamage            = MagicDamage * MagicDamageReductionValue;
               m_FireDamage             = FireDamage * FireDamageReductionValue;
               m_LightningDamage        = LightningDamage * LightningDamageReductionValue;
               m_DarkDamage             = DarkDamage * DarkDamageReductionValue;
            }
        }
        // 몬스터
        else
        {
            // 면역 판정
            // 체력 - (공격력 * 약점) / 저항
            //m_PhysicalDamage = characterCausingDamage.characterStatsManager.m_fPhysicalDamage;
            //m_MagicDamage = characterCausingDamage.characterStatsManager.m_fMagicDamage;
            //m_FireDamage = characterCausingDamage.characterStatsManager.m_fFireDamage;
            //m_LightningDamage = characterCausingDamage.characterStatsManager.m_fLightningDamage;
            //m_DarkDamage = characterCausingDamage.characterStatsManager.m_fDarkDamage;
        }

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
        character.characterStatsManager.HealthBarUIUpdate(Mathf.RoundToInt(m_fFinalDamage));
    }
    

    private void PlayDamageSoundFX(CharacterManager character)
    {
        character.characterSoundFXManager.PlayRandomDamageSound();

        if(m_FireDamage > 0)
        {
            Managers.Sound.Play(elementalDamageSoundSFX);
        }
    }

    private void PlayDamageAnimation(CharacterManager character)
    {
        // light 혹은 heavy에 의한 데미지 애니메이션이 실행 중이라면
        // light damage 애니메이션이 재생되는 것을 원지 않고, heavy animation이 마무리 되기 원함.
        if (character.isInteracting && character.isDead == false)//&& character.characterCombatManager.previousPoiseDamageTaken > poiseDamage)
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
            AICharacterManager boss = character.GetComponent<AICharacterManager>();
            if (boss != null && boss.aiCharacterStatsManager.isBoss)
                return;

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
            if (aiCharacter.aiCharacterStatsManager.teamIDNumber == characterCausingDamage.characterStatsManager.teamIDNumber)
                return;

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




