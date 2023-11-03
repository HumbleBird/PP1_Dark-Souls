using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    //public override void AttemptToCastSpell(CharacterManager character)
    //{
    //    GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, character.characterAnimatorManager.transform);
    //    character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
    //}

    //public override void SuccessfullyCastSpell(CharacterManager character)
    //{
    //    GameObject instantiateSpellFX = Instantiate(spellCastFX, character.characterAnimatorManager.transform);
    //    character.characterStatsManager.HealCharacter(healAmount);
    //}
}
