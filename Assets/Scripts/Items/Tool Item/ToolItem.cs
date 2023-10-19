using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Tool")]
public class ToolItem : Item
{
    public ToolItem()
    {
        m_EItemType = E_ItemType.Tool;
    }

    public ToolItemType m_ToolType;

    [Header("Current Count")]
    public int m_iCurrentCount; // ���� ���� ������ ��
    public int m_iMaxCount; // �ִ� ���� ������ ��

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // ���� ������ ��
    public int m_iMaxSaveCount; // �ִ� ������ ������ ��

    public string m_sItemDescription;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    
    [Header("Parameter Bonus")] // �ɷ�ġ ���� // �ʿ� �ɷ�ġ F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeBonusStrength;
    public int m_iAttributeBonusDexterity;
    public int m_iAttributeBonusIntelligence;
    public int m_iAttributeBonusFaith;

    public virtual void AttemptToConsumeItem(PlayerManager player)
    {
        if(m_iCurrentCount > 0)
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
        m_iCurrentCount -= 1;
    }

    public virtual bool CanUseThisItem(PlayerManager player)
    {
        return true;
    }
}
