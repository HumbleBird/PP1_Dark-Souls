using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatus playerStatus, WeaponSlotManager weaponSlotMAnager)
    {
        base.AttemptToCastSpell(animatorHandler, playerStatus, weaponSlotMAnager);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
        animatorHandler.PlayerTargetAnimation(spellAnimation, true);
        Debug.Log("Attempting to cast Spell...");


    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager animatorHandler,
        PlayerStatus playerStatus,
        CameraHandler cameraHandler,
        WeaponSlotManager weaponSlotMAnager
        )
    {
        base.SuccessfullyCastSpell(animatorHandler, playerStatus,  cameraHandler, weaponSlotMAnager);

        GameObject instantiateSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
        playerStatus.HealPlayer(healAmount);
        Debug.Log("Spell cast Successfully");
    }
}
