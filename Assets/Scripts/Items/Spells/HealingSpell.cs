using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatus playerStatus)
    {
        base.AttemptToCastSpell(animatorHandler, playerStatus);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
        animatorHandler.PlayerTargetAnimation(spellAnimation, true);
        Debug.Log("Attempting to cast Spell...");


    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatus playerStatus)
    {
        base.SuccessfullyCastSpell(animatorHandler, playerStatus);

        GameObject instantiateSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
        playerStatus.HealPlayer(healAmount);
        Debug.Log("Spell cast Successfully");
    }
}
