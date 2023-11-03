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

        if (character.characterEquipmentManager.m_CurrentHandSpell != null && character.characterEquipmentManager.m_CurrentHandSpell.m_eSpellType == Define.E_SpellType.Pyromancie)
        {
            PlayerManager player = character as PlayerManager;
            if (player != null)
            {
                // CHECK FOR FP
                if (player.playerStatsManager.currentFocusPoints >= player.playerEquipmentManager.m_CurrentHandSpell.m_iCostFP)
                {
                    player.playerEquipmentManager.m_CurrentHandSpell.AttemptToCastSpell(player);
                }
                else
                    player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
            }
        }
    }
}
