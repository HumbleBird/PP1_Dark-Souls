using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UI_Popup
{
    enum Texts
    {
        CurrentSoulsValueText, // 현재 소울양
    }

    enum GameObjects
    {
    }

    PlayerManager player;
    public InventoryItemMainUI m_InventoryItemMainUI;
    public ItemInformationUI m_ItemInformationUI;
    public BriefPlayerStatInformationUI m_BriefPlayerStatInformationUI;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        player =  Managers.Object.m_MyPlayer;

        GetText((int)Texts.CurrentSoulsValueText).text = player.playerStatsManager.currentSoulCount.ToString();

        m_InventoryItemMainUI = GetComponentInChildren<InventoryItemMainUI>();
        m_ItemInformationUI = GetComponentInChildren<ItemInformationUI>();
        m_BriefPlayerStatInformationUI = GetComponentInChildren<BriefPlayerStatInformationUI>();

        return true;
    }

    public void ShowItemInfo(Item item)
    {
        m_ItemInformationUI.ShowItemInformation(item);
        m_InventoryItemMainUI.ShowItemName(item.itemName);
    }

    public void CloseItemInfo()
    {
        m_ItemInformationUI.CloseShowItemInformation();
    }
}
