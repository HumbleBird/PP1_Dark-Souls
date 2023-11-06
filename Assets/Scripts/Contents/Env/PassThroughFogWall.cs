using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughFogWall : Interactable
{
    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        playermanager.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Vector3 rotationDirection = transform.forward;
        Quaternion turnRoation = Quaternion.LookRotation(rotationDirection);
        playermanager.transform.rotation = turnRoation;

        playermanager.playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);

        // Sound
        Managers.Sound.Play("Character/Player/Gate Point");
    }
}
