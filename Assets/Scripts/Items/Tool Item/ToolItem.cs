using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Tool")]
public class ToolItem : Item
{
    public ToolItem(int id)
    {

    }

    public ToolItem()
    {

    }

    public E_Tool_LimitiedType m_Tool_LimitiedType;
    public E_ToolType m_eToolType;

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
    public string m_sPlayAnimationName;
    public bool isInteracting;
    
    [Header("Parameter Bonus")] // 능력치 보정 // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeBonusStrength;
    public int m_iAttributeBonusDexterity;
    public int m_iAttributeBonusIntelligence;
    public int m_iAttributeBonusFaith;

    [Header("Sound")]
    public AudioClip m_EatSound;


    public virtual void AttemptToConsumeItem(PlayerManager player)
    {
        if(m_iCurrentCount > 0)
        {
            player.playerAnimatorManager.PlayTargetAnimation(m_sPlayAnimationName, isInteracting, true);
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

    protected void SetInfo(int id)
    {
        Table_Item_Tool.Info data = Managers.Table.m_Item_Tool.Get(id);

        if (data == null)
            return;

        m_iItemID = id;
        m_ItemName = data.m_sName;
        m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);
        m_sPrefabPath = data.m_sPrefabPath;
        m_Tool_LimitiedType = (E_Tool_LimitiedType)data.m_iisLimitied;
        m_eToolType = (E_ToolType)data.m_iToolType;
        m_iMaxCount = data.m_iMaxCurrentCount;
        m_iMaxSaveCount = data.m_iMaxSaveCount;
        m_sItemDescription = data.m_sItemEffectDescription;
        m_iAttributeBonusStrength = data.m_iParam_Bonus_Strength;
        m_iAttributeBonusDexterity = data.m_iParam_Bonus_Dexterity;
        m_iAttributeBonusIntelligence = data.m_iParam_Bonus_Intelligence;
        m_iAttributeBonusFaith = data.m_iParam_Bonus_Faith;
        itemModel = Managers.Resource.Load<GameObject>(m_sPrefabPath);

        switch (m_eToolType)
        {
            case E_ToolType.KeyItems:
                break;
            case E_ToolType.MultiplayerItmes:
                break;
            case E_ToolType.Consumables:
               m_sPlayAnimationName = "Drinking";
                m_EatSound = Managers.Resource.Load<AudioClip>("Item/Consumable/Estus Flask Drink");
                break;
            case E_ToolType.Tools:
                break;
            case E_ToolType.Projectiles:
               m_sPlayAnimationName = "Throw";
                break;
            case E_ToolType.Ammunition:
                break;
            case E_ToolType.Souls:
                break;
            case E_ToolType.BossSouls:
                break;
            case E_ToolType.Ore:
                break;
            case E_ToolType.Ashes:
                break;
            default:
                break;
        }

        m_eItemType = E_ItemType.Tool;
    }

}
