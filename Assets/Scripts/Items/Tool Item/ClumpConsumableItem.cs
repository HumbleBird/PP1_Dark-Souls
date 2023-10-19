using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Cure Effect Clump")]
public class ClumpConsumableItem : ToolItem
{
    [Header("Recovery FX")]
    public GameObject clumpConsumeFX;

    [Header("Cure FX")]
    public bool curePoison;

    public override void AttemptToConsumeItem(PlayerManager player)
    {
        base.AttemptToConsumeItem(player);
        GameObject clump = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);
        player.playerEffectsManager.currentParticleFX = clumpConsumeFX;
        player.playerEffectsManager.instantiatedFXModel2 = clump;

        if(curePoison)
        {
            player.playerStatsManager.poisonBuildup = 0;
            player.playerStatsManager.isPoisoned = false;

            if(player.playerEffectsManager.currentParticleFX != null)
            {
                Destroy(player.playerEffectsManager.currentParticleFX);
            }
        }


        player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
    }
}
