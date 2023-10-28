using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EquipmentUI : UI_Popup
{
    // ���� �г��� ���� ���� ��� �����ش�.
    // ��� �г��� ���� ���õǾ� �ִ� �������� ������ �����ش�.
    // ������ �г��� �÷��̾��� ������ �����ش�.

    enum Texts
    {
        EquipLoadValueText,
        WeightRatioValueText
    }

    public CurrentEquipmentsUI m_CurrentEquipmentsUI;
    public ItemInformationUI m_ItemInformationUI;
    public BriefPlayerStatInformationUI m_BriefPlayerStatInformationUI;
    public ShowItemInventoryUI m_ShowItemInventoryUI;
    public Item m_TempPrivateItem; // �κ��丮���� �������� ��ü�� �� ���� ��.

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        PlayerManager player = Managers.Object.m_MyPlayer;
        GetText((int)Texts.EquipLoadValueText).text = string.Format("{0:0.0} /   {1:0.0}", player.playerStatsManager.m_CurrentEquipLoad, player.playerStatsManager.m_MaxEquipLoad);

        float weightRatio = (player.playerStatsManager.m_CurrentEquipLoad / player.playerStatsManager.m_MaxEquipLoad) * 100;
        GetText((int)Texts.WeightRatioValueText).text = string.Format("{0:0.0}%", weightRatio);

        m_CurrentEquipmentsUI = GetComponentInChildren<CurrentEquipmentsUI>();
        m_ItemInformationUI = GetComponentInChildren<ItemInformationUI>();
        m_BriefPlayerStatInformationUI = GetComponentInChildren<BriefPlayerStatInformationUI>();
        m_ShowItemInventoryUI = GetComponentInChildren<ShowItemInventoryUI>();

        m_ShowItemInventoryUI.gameObject.SetActive(false);

        return true;
    }
}
