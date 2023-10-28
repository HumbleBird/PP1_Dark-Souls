using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInteractable : Interactable
{
    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);


        if(Managers.GameUI.m_InteractableNPCPopupUI == null)
        {
            Managers.Sound.Play("UI/Popup_ButtonShow", 0, Define.Sound.Effect, 0.3f);
            Managers.Sound.Play("Character/Voice/Fire Keeper/1");
            Managers.GameUI.m_InteractableNPCPopupUI = Managers.GameUI.ShowPopupUI<LevelUpUI>();
        }

    }
}
