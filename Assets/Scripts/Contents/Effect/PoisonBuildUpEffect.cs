using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Poison Build Effect Up")]
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

        if(character.characterStatsManager.poisonResistance > 0)
        {
            // �� ���� ��ġ�� 100�̶�� ������
            if(character.characterStatsManager.poisonResistance >= 100)
            {
                finalPoisonBuildUp = 0;
            }
            else
            {
                float resistancePercentage = character.characterStatsManager.poisonResistance / 100;

                finalPoisonBuildUp = basePoisonBuildUpAmount - (basePoisonBuildUpAmount * resistancePercentage);
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
                player.playerEffectsManager.poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(poisonAmount));
            }

            // �ش� �������� ����Ʈ �Ŵ����� �������� �ʴ´�. ī�Ǻ����� �����ϴ� ����.
            // ���� ���������� �����Ѵٸ�, �׸��� ��� ĳ���Ͱ� ���������� ����Ѵٸ�, ��� ��ġ�� �����ϱ� ������ �� ��.
            PoisonedEffect poisonedEffect = Instantiate(WorldCharacterEffectManager.instance.poisonedEffect);
            poisonedEffect.poisonDamage = PoisonDamagePerTick;
            character.characterEffectsManager.timedEffects.Add(poisonedEffect);
            character.characterEffectsManager.timedEffects.Remove(this);
            character.characterSoundFXManager.PlaySoundFX(WorldCharacterEffectManager.instance.poisonSFX);
           

            character.characterEffectsManager.AddTimedEffectParticle(Instantiate(WorldCharacterEffectManager.instance.poisonFX));
        }

        character.characterEffectsManager.timedEffects.Remove(this);

    }

}
