using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInteractable : Interactable
{
    public override void Interact(PlayerManager playermanager)
    {
        playermanager.gameUIManager.levelUpWindow.SetActive(true); 
    }
}
