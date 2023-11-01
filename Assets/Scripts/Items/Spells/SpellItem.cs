using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SpellItem : Item
{
    public SpellItem(int id)
    {
        Table_Item_Spell.Info data = Managers.Table.m_Item_Spell.Get(id);

        if (data == null)
            return;

        m_iItemID = data.m_iID;
        m_ItemName = data.m_sName;
        m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);
        m_sItemDescription = data.m_sItem_Decription;
        m_eSpellType = (E_SpellType)data.m_iSpell_Type;
        m_iCostFP = data.m_iCost_FP;
        m_iMagicUseSlot = data.m_iSlots;
        m_iAttributeRequirementIntelligence = data.m_iRequirment_Intelligence;
        m_iAttributeRequirementFaith = data.m_iRequirment_Faith;

        m_eItemType = Define.E_ItemType.Magic;
    }

    public SpellItem()
    {

    }

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    [Header("Spell Description")]
    public string m_sItemDescription;

    [Header("Requirement Ability")] // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeRequirementIntelligence;
    public int m_iAttributeRequirementFaith;

    [Header("Spell Cost")]
    public int m_iCostFP;

    [Header("Cost Slots")]
    public int m_iMagicUseSlot;

    [Header("Game Models")]
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;

    public string spellAnimation;

    [Header("Spell Type")]
    public E_SpellType m_eSpellType;

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
            player.playerStatsManager.DeductFocusPoints(m_iCostFP);

        }
    }
}
