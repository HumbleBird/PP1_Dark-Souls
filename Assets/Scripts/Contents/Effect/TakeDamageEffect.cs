using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Take Damage")]
public class TakeDamageEffect : CharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // ������ ������ ĳ���Ͷ��, they are listed here?

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

    private void CalculateDamage(CharacterManager  character)
    {
        if(characterCausingDamage != null)
        {
            // Damage defense ��� ��, ���� ������ ��ġ üũ
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
        // ���Ͷ�� �� �Ʒ��� �ִ� Helath Bar�� ����

        // �÷��̾��� UI�� ����
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
        // light Ȥ�� heavy�� ���� ������ �ִϸ��̼��� ���� ���̶��
        // light damage �ִϸ��̼��� ����Ǵ� ���� ���� �ʰ�, heavy animation�� ������ �Ǳ� ����.

        if (character.isInteracting && character.characterCombatManager.previousPoiseDamageTaken > poiseDamage)
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




