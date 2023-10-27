using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBuildUpEffect : CharacterEffect
{
    // �� �� ����� ��ġ�� ���� ��ġ ���� ��� �ȴ�. Tick ���� 
    [SerializeField] float basePoisonBuildUpAmount = 7;

    // ĳ���Ͱ� ���� �ߵ��� ��� �޴� ���� ��
    [SerializeField] float poisonAmount = 100;

    // �� ����� ��ġ�� 100%���, �޴� ƽ ������ �� ������ ��ġ
    [SerializeField] int PoisonDamagePerTick = 5;

    public override void ProcessEffect(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        // �÷��̾� ���� ��ġ�� ��� �Ŀ� posion build up
        float finalPoisonBuildUp = 0;

        if(character.characterStatsManager.m_iPoisonArmorResistance >= 0)
        {
            // �� ���� ��ġ�� 100�̶�� ������
            if(character.characterStatsManager.m_iPoisonArmorResistance >= 100)
            {
                finalPoisonBuildUp = 0;
            }
            else
            {
                float resistancePercentage = character.characterStatsManager.m_iPoisonArmorResistance / 100;

                if (resistancePercentage > 0)
                    finalPoisonBuildUp = basePoisonBuildUpAmount - (basePoisonBuildUpAmount * resistancePercentage);
                else
                    finalPoisonBuildUp = basePoisonBuildUpAmount;
            }
        }

        // ĳ������ Poison Build Up ��ġ�� �߰�
        character.characterStatsManager.poisonBuildup += finalPoisonBuildUp;

        // ĳ���Ͱ� �̹� �ߵ��Ǿ��ٸ�, ��� ������ ����� ����Ʈ�� ����
        if(character.characterStatsManager.isPoisoned)
        {
            character.characterEffectsManager.timedEffects.Remove(this);
        }

        // �ߵ��Ǿ��ٸ�
        if(character.characterStatsManager.poisonBuildup >= 100)
        {
            character.characterStatsManager.isPoisoned = true;
            character.characterStatsManager.poisonAmount = poisonAmount;
            character.characterStatsManager.poisonBuildup = 0;

            if(player != null)
            {
                player.m_GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
            }

            // �ش� �������� ����Ʈ �Ŵ����� �������� �ʴ´�. ī�Ǻ����� �����ϴ� ����.
            // ���� ���������� �����Ѵٸ�, �׸��� ��� ĳ���Ͱ� ���������� ����Ѵٸ�, ��� ��ġ�� �����ϱ� ������ �� ��.
            PoisonedEffect poisonedEffect = new PoisonedEffect();// Managers.Resource.Instantiate("Data/Character Effect/Poisoned Effect").GetComponent<PoisonedEffect>();
            poisonedEffect.effectID = 70;
            poisonedEffect.poisonDamage = PoisonDamagePerTick;
            character.characterEffectsManager.timedEffects.Add(poisonedEffect);
            character.characterEffectsManager.timedEffects.Remove(this);
            Managers.Sound.Play("Object/Poisoned_Alert");
            //character.characterSoundFXManager.PlaySoundFX(Managers.WorldEffect.poisonSFX);

            GameObject posionParticle= Managers.Resource.Instantiate("FX/Particles/Poison_Particle");
            character.characterEffectsManager.AddTimedEffectParticle(posionParticle);
        }

        character.characterEffectsManager.timedEffects.Remove(this);

    }

}
