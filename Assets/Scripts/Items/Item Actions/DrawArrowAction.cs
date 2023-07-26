using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Draw Arrow Action")]
public class DrawArrowAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.isInteracting)
            return;

        if (character.isHoldingArrow)
            return;



        //Animator Player
        character.animator.SetBool("isHoldingArrow", true);
        character.characterAnimatorManager.PlayTargetAnimation("Bow_TH_Draw_01", true);

        // Instantiate Arrow
        GameObject loadedArrow = Instantiate(character.characterInventoryManager.currentAmmo.loadedItemModel, character.characterWeaponSlotManager.rightHandSlot.transform);
        character.characterEffectsManager.instantiatedFXModel = loadedArrow;

        //Animate the bow
        Animator bowAnimator = character.characterWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", true);
        bowAnimator.Play("BowObject_TH_Draw_01");

    }
}
