using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    player.GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
                }
            }
            else
            {
                character.characterStatsManager.isPoisoned = false;
                character.characterStatsManager.poisonAmount = 0;
                player.GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
            }
        }
        else
        {
            character.characterEffectsManager.timedEffects.Remove(this);
            character.characterEffectsManager.RemoveTimedEffectParticle(Define.EffectParticleType.Poison);
        }
    }
}
