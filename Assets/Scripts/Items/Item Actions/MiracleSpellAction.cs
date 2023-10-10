using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Miracle Spell Action")]
public class MiracleSpellAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.isInteracting)
            return;

        if (character.characterEquipmentManager.m_CurrentHandSpell != null && character.characterEquipmentManager.m_CurrentHandSpell.isFaithSpell)
        {

            PlayerManager player = character as PlayerManager;
            if (player != null)
            {
                // CHECK FOR FP
                if (player.playerStatsManager.currentFocusPoints >= player.playerEquipmentManager.m_CurrentHandSpell.focusPointCost)
                {
                    player.playerEquipmentManager.m_CurrentHandSpell.AttemptToCastSpell(player);
                }
                else
                    player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
            }


        }
    }
}
