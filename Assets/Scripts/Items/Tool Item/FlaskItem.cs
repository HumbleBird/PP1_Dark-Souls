using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Flask")]
public class FlaskItem : ToolItem
{
    [Header("Recovery Amount")]
    public int healthRecoverAmount = 250;
    public int focusPointsRecoverAmount = 100;


    public override void AttemptToConsumeItem(PlayerManager player )
    {
        GameObject flask = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);

        base.AttemptToConsumeItem(player);
        //player.playerEffectsManager.currentParticleFX = recoveryFX;
        player.playerEffectsManager.amountToBeHealed = healthRecoverAmount;
        player.playerEffectsManager.instantiatedFXModel2 = flask;
        player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();

    }

}
