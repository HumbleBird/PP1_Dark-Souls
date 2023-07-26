using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Cure Effect Clump")]
public class ClumpConsumableItem : ConsumableItem
{
    [Header("Recovery FX")]
    public GameObject clumpConsumeFX;

    [Header("Cure FX")]
    public bool curePoison;

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager playerWeaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.AttemptToConsumeItem(playerAnimatorManager, playerWeaponSlotManager, playerEffectsManager);
        GameObject clump = Instantiate(itemModel, playerWeaponSlotManager.rightHandSlot.transform);
        playerEffectsManager.currentParticleFX = clumpConsumeFX;
        playerEffectsManager.instantiatedFXModel2 = clump;

        if(curePoison)
        {
            playerEffectsManager.poisonBuildup = 0;
            playerEffectsManager.poisonAmount = playerEffectsManager.defaultPoisonAmount;
            playerEffectsManager.isPoisoned = false;

            if(playerEffectsManager.currentPoisonParticleFX != null)
            {
                Destroy(playerEffectsManager.currentPoisonParticleFX);
            }
        }


        playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
    }
}
