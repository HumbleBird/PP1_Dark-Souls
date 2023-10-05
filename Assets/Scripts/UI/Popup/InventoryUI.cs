using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UI_Popup
{
    // Left Panel - Iten Inventory
    // 1. ���� �� ������ �ι��� �������� Ŭ���ϸ� �ش� �����۸��� �ִ� ������ �κ��丮�� ���� ������.
    // 2. �� ȭ��ǥ�� ������ ���� ������ �ι� �������� ������. ( 1, 2, 3, 4, 5) ���� ������ ȭ��ǥ�� ������ (2, 3, 4, 5, 6) �̷��� �����ְ� ����. ���� ������Ʈ setactive�ϸ� ��.
    // 3. �������� ������ �ش� �ڸ��� ����Ʈ �ڸ��� �����ϰ�, �ش� ���õ� �������� ������ Middle Panel�� ������ â���� ������. ������ ���� �������δ� �ٽ� ������, �ݴ� ���⵵ ����������
    // 4. ������ �ι��� ����, ��ȭ����, ����ǰ, ����, �������� ,���Ÿ� ����, �˸�, ����, ���� ,����, �尩, ��ȭ, ȭ�� / ��Ʈ, ����, �������� ������
    // 5, ������ �ι��� �ٲ㵵 ����Ʈ �ڸ��� �״�� �̱� ������ �ش� �ڸ��� ������ ������ ���� ��� ��
    // 6. �� ������ ���� �����ִ� ������ �ٸ�.
    // 7. Ŭ���ϸ� ������ �� ������ �˾��� ��. yes�� ������ ���â�� �߰� �ش� ���⼭ �ش� ��� ĭ�� ������ ��ü�ϰ� ��.
    // 8. �����ε� ���⸦ Ŭ���ϸ� �Ҹ��� ����, ��ü�� ���� �ʰ� �Ѵ�.

    // Middle Panel - Item Description Detail
    // 1. left Panel���� �������� Ŭ���ϸ� �� â���� �������� ������ ������
    // 2. ������ �̸�, ������ �������� �����ְ�. ������ ������ ���� ����, ���� ���� ������ �� ����.

    // Right Panel - Player State
    // �÷��̾��� ������ �������� ������.

    enum Texts
    {
        SoulText, // ���� �ҿ��
    }

    enum GameObjects
    {
    }

    PlayerManager player;
    public InventoryLeftPanelUI m_InventoryLeftPanelUI;
    public ItemInformationUI m_InventoryMiddleUI;
    public BriefPlayerStatInformationUI m_InventoryRightPanel;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        player =  Managers.Object.m_MyPlayer;

        GetText((int)Texts.SoulText).text = player.playerStatsManager.currentSoulCount.ToString();

        m_InventoryLeftPanelUI = GetComponentInChildren<InventoryLeftPanelUI>();
        m_InventoryMiddleUI = GetComponentInChildren<ItemInformationUI>();
        m_InventoryRightPanel = GetComponentInChildren<BriefPlayerStatInformationUI>();

        return true;
    }

}
