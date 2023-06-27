using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(PlayerAnimatorManager playerAnimatorManager, PlayerStatsManager playerStatsManager, PlayerWeaponSlotManager weaponSlotMAnager)
    {
        base.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, weaponSlotMAnager);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, playerAnimatorManager.transform);
        playerAnimatorManager.PlayerTargetAnimation(spellAnimation, true);
        Debug.Log("Attempting to cast Spell...");


    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager playerAnimatorManager,
        PlayerStatsManager playerStatsManager,
        CameraHandler cameraHandler,
        PlayerWeaponSlotManager weaponSlotMAnager
        )
    {
        base.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager,  cameraHandler, weaponSlotMAnager);

        GameObject instantiateSpellFX = Instantiate(spellCastFX, playerAnimatorManager.transform);
        playerStatsManager.HealPlayer(healAmount);
        Debug.Log("Spell cast Successfully");
    }
}
