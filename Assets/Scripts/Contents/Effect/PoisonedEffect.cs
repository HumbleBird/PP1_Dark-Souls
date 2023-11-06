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
                character.characterStatsManager.currentHealth -= 1;

                if(player != null)
                {
                    player.m_GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
                    player.m_GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Hp);
                }

                if (character.characterStatsManager.currentHealth <= 0)
                    character.Dead();

            }
            else
            {
                character.characterStatsManager.isPoisoned = false;
                character.characterStatsManager.poisonAmount = 0;
                player.m_GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
            }
        }
        else
        {
            character.characterEffectsManager.timedEffects.Remove(this);
            character.characterEffectsManager.RemoveTimedEffectParticle(Define.EffectParticleType.Poison);
        }
    }
}
