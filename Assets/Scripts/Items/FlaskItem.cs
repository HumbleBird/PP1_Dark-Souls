using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Flask")]
public class FlaskItem : ConsumableItem
{
    [Header("Flask Type")]
    public bool estusFlask;
    public bool ashenFlask;

    [Header("Recovery Amount")]
    public int healthRecoverAmount;
    public int focusPointsRecoverAmount;

    [Header("Recovery FX")]
    public GameObject recoveryFX;

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager playerWeaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        GameObject flask = Instantiate(itemModel, playerWeaponSlotManager.rightHandSlot.transform);

        base.AttemptToConsumeItem(playerAnimatorManager, playerWeaponSlotManager, playerEffectsManager);
        playerEffectsManager.currentParticleFX = recoveryFX;
        playerEffectsManager.amountToBeHealed = healthRecoverAmount;
        playerEffectsManager.instantiatedFXModel = flask;
        playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
    }

}
