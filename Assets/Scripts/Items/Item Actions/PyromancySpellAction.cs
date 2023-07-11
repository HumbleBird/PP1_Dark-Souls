using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Pyromancy Spell Action")]
public class PyromancySpellAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.isInteracting)
            return;

        if (player.playerInventoryManager.currentSpell != null && player.playerInventoryManager.currentSpell.isFaithSpell)
        {
            // CHECK FOR FP
            if (player.playerStatsManager.currentFocusPoints >= player.playerInventoryManager.currentSpell.focusPointCost)
            {
                player.playerInventoryManager.currentSpell.AttemptToCastSpell(
                    player.playerAnimatorManager,
                    player.playerStatsManager,
                    player.playerWeaponSlotManager,
                    player.isUsingLeftHand);
            }
            else
                player.playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
        }
    }
}
