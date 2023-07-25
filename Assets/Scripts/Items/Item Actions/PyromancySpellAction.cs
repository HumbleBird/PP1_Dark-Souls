using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Pyromancy Spell Action")]
public class PyromancySpellAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.isInteracting)
            return;

        if (character.characterInventoryManager.currentSpell != null && character.characterInventoryManager.currentSpell.isFaithSpell)
        {
            // CHECK FOR FP
            if (character.characterStatsManager.currentFocusPoints >= character.characterInventoryManager.currentSpell.focusPointCost)
            {
                character.characterInventoryManager.currentSpell.AttemptToCastSpell(character);
            }
            else
                character.characterAnimatorManager.PlayerTargetAnimation("Shrug", true);
        }
    }
}
