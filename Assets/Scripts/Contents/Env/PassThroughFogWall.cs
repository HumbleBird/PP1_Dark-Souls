using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughFogWall : Interactable
{
    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);
        playermanager.PassThroughFogWallInteraction(transform);
    }
}
