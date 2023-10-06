using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Magic Spell Action")]
public class MagicSpellAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.isInteracting)
            return;

        if (character.characterEquipmentManager.m_CurrentHandSpell != null && character.characterEquipmentManager.m_CurrentHandSpell.isMagicSpell)
        {
            // CHECK FOR FP
            if (character.characterStatsManager.currentFocusPoints >= character.characterEquipmentManager.m_CurrentHandSpell.focusPointCost)
            {
                character.characterEquipmentManager.m_CurrentHandSpell.AttemptToCastSpell(character);
            }
            else
                character.characterAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}
