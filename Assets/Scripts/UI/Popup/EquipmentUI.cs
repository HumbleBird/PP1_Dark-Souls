using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EquipmentUI : UI_Popup
{
    // 왼쪽 패널은 장착 중인 장비를 보여준다.
    // 가운데 패널은 현재 선택되어 있는 아이템의 정보를 보여준다.
    // 오른쪽 패널은 플레이어의 스텟을 보여준다.

    enum Texts
    {
        EquipLoadValueText,
        WeightRatioValueText
    }

    public CurrentEquipmentsUI m_CurrentEquipmentsUI;
    public ItemInformationUI m_ItemInformationUI;
    public BriefPlayerStatInformationUI m_BriefPlayerStatInformationUI;
    public ShowItemInventoryUI m_ShowItemInventoryUI;
    public Item m_TempPrivateItem; // 인벤토리에서 아이템을 교체할 때 쓰는 것.

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
