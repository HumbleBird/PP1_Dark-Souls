using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/Take Damage")]
public class TakeDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // ������ ������ ĳ���Ͷ��, they are listed here?

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
    public AudioClip elementalDamageSoundSFX; // ELEMENTAL(Fire, Magic, Darks, Lightning) Damage �� �� �÷���

    [Header("Direction Damage Taken From")]
    public float angleHitFrom;
    public Vector3 contactPoint; // ĳ���� �ٵ� ������ �߻� ����

    public override void ProcessEffect(CharacterManager character)
    {
        // ��� ����
        if (character.isDead)
            return;

        // ��� ����
        if (character.isInvulnerable)
            return;

        // ������ ���
        CalculateDamage(character);

        // ����� poise �� ���� ������ �ִϸ��̼�
        CheckWhichDirectionDamageCameFrom(character);

        // ������ �ִϸ��̼� ���
        PlayDamageAnimation(character);

        // UI ������Ʈ
        CharacterHealthBarUIUpdate(character);

        // ����
        PlayDamageSoundFX(character);

        // �� ȿ��
        PlayBloodSplatter(character);

        // character�� A.I���, ������ ĳ���͸� ���ο� Ÿ������ ����
        AssignNewAITarget(character);

        // character�� Player���, ī�޶� ����ũ ȿ���� ��.
        SetCameraShake(character);
    }

    private void CalculateDamage(CharacterManager  character) // character�� ���� ���ϴ� ��
    {

        // �÷��̾�
        if(character.characterStatsManager.teamIDNumber == (int)E_TeamId.Player)
        {
            // ������ ����
            // (���� ���ݷ�-���� ����) * (1-���� ������) + (�Ӽ� ���ݷ� - �Ӽ� ����) * (1-�Ӽ� ������)
            PlayerManager player = character as PlayerManager;

            if (characterCausingDamage != null)
            {
                // (���� ���ݷ�-���� ����)
                float physicalDamage =  Mathf.Max(0, characterCausingDamage.characterStatsManager.m_fPhysicalDamage - character.characterStatsManager.m_iPhysicalDefense);

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

               m_PhysicalDamage         = physicalDamage * physicalDamageReductionValue;
               m_MagicDamage            = MagicDamage * MagicDamageReductionValue;
               m_FireDamage             = FireDamage * FireDamageReductionValue;
               m_LightningDamage        = LightningDamage * LightningDamageReductionValue;
               m_DarkDamage             = DarkDamage * DarkDamageReductionValue;
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
        // ���Ͷ�� �� �Ʒ��� �ִ� Helath Bar�� ����

        // �÷��̾��� UI�� ����
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
        // light Ȥ�� heavy�� ���� ������ �ִϸ��̼��� ���� ���̶��
        // light damage �ִϸ��̼��� ����Ǵ� ���� ���� �ʰ�, heavy animation�� ������ �Ǳ� ����.
        if (character.isInteracting && character.isDead == false)//&& character.characterCombatManager.previousPoiseDamageTaken > poiseDamage)
        {
            // interacting ���̰� previous Pose Damage�� 0���� �����,  damage animation�� ��� �Ǿ�� ��.
            // previous poise�� current poise���� ũ�ٸ� ������ �ִϸ��̼��� �� ������ �ִϸ��̼����� ��ȭ���� �ʴ´� 
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

            // ĳ���� poise broken�� ���� �ʾҴٸ�, ������ �ִϸ��̼��� ������� �ʴ´�
            if (!poiseIsBroken)
            {
                return;
            }
            else
            {
                // Ȱ��/��Ȱ�� stun lock

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




