using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Aim Action")]
public class AimAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        if (character.isAiming)
            return;

        if(player != null)
        {
            player.m_GameUIManager.m_HUDUI.m_goCrosshair.SetActive(true);
        }
        character.isAiming = true;
    }
}
