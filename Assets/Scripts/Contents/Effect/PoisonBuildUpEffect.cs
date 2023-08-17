using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Poison Build Effect Up")]
public class PoisonBuildUpEffect : CharacterEffect
{
    // 이 독 빌드업 수치는 저항 수치 전에 계산 된다. Tick 마다 
    [SerializeField] float basePoisonBuildUpAmount = 7;

    // 캐릭터가 독에 중독된 경우 받는 독의 양
    [SerializeField] float poisonAmount = 100;

    // 독 빌드업 수치가 100%라면, 받는 틱 마다의 독 데미지 수치
    [SerializeField] int PoisonDamagePerTick = 5;

    public override void ProcessEffect(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        // 플레이어 저항 수치를 계산 후에 posion build up
        float finalPoisonBuildUp = 0;

        if(character.characterStatsManager.poisonResistance > 0)
        {
            // 독 저항 수치가 100이라면 무적임
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

        // 캐릭터의 Poison Build Up 수치에 추가
        character.characterStatsManager.poisonBuildup += finalPoisonBuildUp;

        // 캐릭터가 이미 중독되었다면, 모든 포이즌 빌드업 이펙트를 제거
        if(character.characterStatsManager.isPoisoned)
        {
            character.characterEffectsManager.timedEffects.Remove(this);
        }

        // 중독되었다면
        if(character.characterStatsManager.poisonBuildup >= 100)
        {
            character.characterStatsManager.isPoisoned = true;
            character.characterStatsManager.poisonAmount = poisonAmount;
            character.characterStatsManager.poisonBuildup = 0;

            if(player != null)
            {
                player.playerEffectsManager.poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(poisonAmount));
            }

            // 해당 오리지널 이펙트 매니저는 수정하지 않는다. 카피본만을 수정하는 거지.
            // 만약 오리지널을 수정한다면, 그리고 모든 캐릭터가 오리지널을 사용한다면, 모든 수치를 공유하기 때문에 안 됨.
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
