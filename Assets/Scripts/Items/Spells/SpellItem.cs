using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : Item
{
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;
    public string spellAnimation;

    [Header("Spell Cost")]
    public int focusPointCost;


    [Header("Spell Type")]
    public bool isFaithSpell;
    public bool isMagicSpell;
    public bool isPyroSpell;

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell(CharacterManager character)
    {
        Debug.Log("You Attempt to cast a spell");
    }

    public virtual void SuccessfullyCastSpell(CharacterManager character)
    {
        Debug.Log("You Successfully cast a spell");
        PlayerManager player = character as PlayerManager;

        if(player != null)
        {
            player.playerStatsManager.DeductFocusPoints(focusPointCost);

        }
    }
}
