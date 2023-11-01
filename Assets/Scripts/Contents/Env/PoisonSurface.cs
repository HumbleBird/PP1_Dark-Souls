using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSurface: MonoBehaviour
{
    public float poisonBuildUpAmount = 7;

    public List<CharacterManager> charactersInsidePoisonSurface;

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager character = other.GetComponent<CharacterManager>();

        if(character != null)
        {
            charactersInsidePoisonSurface.Add(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterManager character = other.GetComponent<CharacterManager>();

        if (character != null)
        {
            charactersInsidePoisonSurface.Remove(character);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (CharacterManager character in charactersInsidePoisonSurface)
        {
            if (character.characterStatsManager.isPoisoned)
                return;

            PoisonBuildUpEffect poisonBuildUp = new PoisonBuildUpEffect();
            poisonBuildUp.effectID = 69;

            foreach (var effect in character.characterEffectsManager.timedEffects)
            {
                if (effect.effectID == poisonBuildUp.effectID)
                    return;
            }

            character.characterEffectsManager.timedEffects.Add(poisonBuildUp);
        }
    }
}
