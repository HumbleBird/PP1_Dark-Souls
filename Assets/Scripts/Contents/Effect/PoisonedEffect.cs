using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Poison")]
public class PoisonedEffect : CharacterEffect
{
    public int poisonDamage = 1;

    public override void ProcessEffect(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        if(character.characterStatsManager.isPoisoned)
        {
            if(character.characterStatsManager.poisonAmount > 0)
            {

                character.characterStatsManager.poisonAmount -= 1;
                Debug.Log("Damage");

                if(player != null)
                {
                    player.playerEffectsManager.poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(character.characterStatsManager.poisonAmount));
                }
            }
            else
            {
                character.characterStatsManager.isPoisoned = false;
                character.characterStatsManager.poisonAmount = 0;
                player.playerEffectsManager.poisonAmountBar.SetPoisonAmount(-1);
            }
        }
        else
        {
            character.characterEffectsManager.timedEffects.Remove(this);
            character.characterEffectsManager.RemoveTimedEffectParticle(Define.EffectParticleType.Poison);
        }
    }
}
