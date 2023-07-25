using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(CharacterManager character)
    {
        base.AttemptToCastSpell(character);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, character.characterAnimatorManager.transform);
        character.characterAnimatorManager.PlayerTargetAnimation(spellAnimation, true, false, character.isUsingLeftHand);
        Debug.Log("Attempting to cast Spell...");


    }

    public override void SuccessfullyCastSpell(CharacterManager character)
    {
        base.SuccessfullyCastSpell(character);

        GameObject instantiateSpellFX = Instantiate(spellCastFX, character.characterAnimatorManager.transform);
        character.characterStatsManager.HealCharacter(healAmount);
        Debug.Log("Spell cast Successfully");
    }
}
