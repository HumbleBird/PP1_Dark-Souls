using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public HealingSpell()
    {
        spellWarmUpFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Spell/Heal_Spell_Cast_Fx");
        spellCastFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Spell/Heal_Spell_Warm_Up_Fx");
    }

    public int healAmount;

    public override void AttemptToCastSpell(CharacterManager character)
    {
        base.AttemptToCastSpell(character);

        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, character.characterAnimatorManager.transform);
        character.characterAnimatorManager.PlayTargetAnimation(spellAnimation, true, false, character.isUsingLeftHand);
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
