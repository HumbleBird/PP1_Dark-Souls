using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ToolItem : Item
{
    public ToolItemType m_ToolType;

    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    public string m_sItemDescription;

    [Header("Parameter Bonus")] // 능력치 보정 // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iParameterBonusStrength;
    public int m_iParameterBonusDexterity;
    public int m_iParameterBonusIntelligence;
    public int m_iParameterBonusFaith;

    public virtual void AttemptToConsumeItem(PlayerManager player)
    {
        if(currentItemAmount > 0)
        {
            player.playerAnimatorManager.PlayTargetAnimation(consumeAnimation, isInteracting, true);
        }
        else
        {
            player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }

    public virtual void SucessToConsumeItem(PlayerManager player)
    {
        currentItemAmount -= 1;
    }

    public virtual bool CanUseThisItem(PlayerManager player)
    {
        return true;
    }
}
