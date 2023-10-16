using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDeadSouls : Interactable
{
    protected override void Awake()
    {
        base.Awake();

        interactableText = "Recover lost souls"; // 잃어버린 소울을 되찾는다


    }

    public override void Interact(PlayerManager playermanager)
    {
        // souls restrived 팝업 뛰윅
        // sound
        // 소울 넣기
    }
}
