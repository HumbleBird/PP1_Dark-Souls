using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Blocking Action")]
public class BlockingAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.isInteracting)
            return;

        if (player.isBlocking)
            return;

        player.playerAnimatorManager.PlayerTargetAnimation("Block Start", false, true);
        player.playerEquipmentManager.OpenBlockingCollider();
        player.isBlocking = true;
    }


}
