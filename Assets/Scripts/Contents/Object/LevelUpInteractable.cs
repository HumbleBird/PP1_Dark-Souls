using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInteractable : Interactable
{
    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        if(Managers.GameUI.m_InteractableNPCPopupUI == null)
            Managers.GameUI.m_InteractableNPCPopupUI = Managers.GameUI.ShowPopupUI<LevelUpUI>();

    }
}
