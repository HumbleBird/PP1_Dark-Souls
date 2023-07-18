using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Draw Arrow Action")]
public class DrawArrowAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.isInteracting)
            return;

        if (player.isHoldingArrow)
            return;



        //Animator Player
        player.animator.SetBool("isHoldingArrow", true);
        player.playerAnimatorManager.PlayerTargetAnimation("Bow_TH_Draw_01", true);

        // Instantiate Arrow
        GameObject loadedArrow = Instantiate(player.playerInventoryManager.currentAmmo.loadedItemModel, player.playerWeaponSlotManager.rightHandSlot.transform);
        player.playerEffectsManager.currentRangeFX = loadedArrow;

        //Animate the bow
        Animator bowAnimator = player.playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", true);
        bowAnimator.Play("BowObject_TH_Draw_01");

    }
}
