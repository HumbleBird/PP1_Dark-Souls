using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : Item
{
    public SpellItem()
    {
        m_EItemType = Define.E_ItemType.Magic;
    }

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    [Header("Spell Description")]
    public string m_sItemDescription;

    public int m_iUseSlot;

    [Header("Requirement Ability")] // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeRequirementIntelligence;
    public int m_iAttributeRequirementFaith;

    [Header("Spell Cost")]
    public int focusPointCost;

    [Header("Magic Use Slot")]
    public int m_iMagicUseSlot;

    [Header("Game Models")]
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;

    public string spellAnimation;

    [Header("Spell Type")]
    public bool isFaithSpell;
    public bool isMagicSpell;
    public bool isPyroSpell;



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
