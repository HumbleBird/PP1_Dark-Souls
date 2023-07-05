using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(
        PlayerAnimatorManager playerAnimatorManager, 
        PlayerStatsManager playerStatsManager, 
        PlayerWeaponSlotManager weaponSlotMAnager,
        bool isLeftHaned)
    {
        base.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, weaponSlotMAnager, isLeftHaned);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, playerAnimatorManager.transform);
        playerAnimatorManager.PlayerTargetAnimation(spellAnimation, true, false, isLeftHaned);
        Debug.Log("Attempting to cast Spell...");


    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager playerAnimatorManager,
        PlayerStatsManager playerStatsManager,
        CameraHandler cameraHandler,
        PlayerWeaponSlotManager weaponSlotMAnager,
        bool isLeftHaned)
    {
        base.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager,  cameraHandler, weaponSlotMAnager, isLeftHaned);

        GameObject instantiateSpellFX = Instantiate(spellCastFX, playerAnimatorManager.transform);
        playerStatsManager.HealPlayer(healAmount);
        Debug.Log("Spell cast Successfully");
    }
}
