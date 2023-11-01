using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Tool")]
public class ToolItem : Item
{
    public ToolItem(int id)
    {
        Table_Item_Tool.Info data = Managers.Table.m_Item_Tool.Get(id);

        if (data == null)
            return;

        m_iItemID = id;
        m_ItemName = data.m_sName;
        m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);
        m_sPrefabPath = data.m_sPrefabPath;
        m_ToolType = (ToolItemType)data.m_iisLimitied;
        m_iMaxCount = data.m_iMaxCurrentCount;
        m_iMaxSaveCount = data.m_iMaxSaveCount;
        m_sItemDescription = data.m_sItemEffectDescription;
        m_iAttributeBonusStrength = data.m_iParam_Bonus_Strength;
        m_iAttributeBonusDexterity = data.m_iParam_Bonus_Dexterity;
        m_iAttributeBonusIntelligence = data.m_iParam_Bonus_Intelligence;
        m_iAttributeBonusFaith = data.m_iParam_Bonus_Faith;

        m_eItemType = E_ItemType.Tool;
    }

    public ToolItem()
    {

    }

    public ToolItemType m_ToolType;

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    public string m_sItemDescription;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    
    [Header("Parameter Bonus")] // 능력치 보정 // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
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
