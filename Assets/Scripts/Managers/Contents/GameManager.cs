using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Interactable m_Interactable;
    PlayerManager m_Player;

    public Vector3 m_StartPoint = new Vector3(36.36f, 4.95f, -14.23298f);
    public bool m_isNewGame = true;

    public void GameStart()
    {
        m_Player = Managers.Object.m_MyPlayer;

        // ������ ó�� �����ߴٸ�
        if (m_isNewGame)
        {
            // TODO ĳ���� �ʱ� ������ ��������

            // ��Ʈ�ξ� ó������ �����ֱ�
            Managers.GameUI.ShowIntro("StartOpening");

            // ������ Refresh
            //Managers.Object.m_MyPlayer.m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.RefreshUI();
            //Managers.Object.m_MyPlayer.m_GameUIManager.quickSlotsUI.UpdateAllQuickSlotUI();
        }
        else
        {
            // ĳ���� ������ ��������
        }

        m_Player.StartGame();
    }

    public void PlayAction(Action action)
    {
        action.Invoke();
    }

    public void PlayerDead()
    {
        // ��� ������ CurrentTarget null
        // ���� ���ڸ� ��ġ
        // ���� �� ȸ��
        // �÷��̾��� �ҿ��� ����� Interactable Object ����
        // �÷��̾� ������ �����̾ ������
        // �÷��̾��� ü�� ȸ��
        // ��ũ�� �� �ƿ�
    }

    public void EntryNewArea(string name)
    {
        // UI�� ȭ�� �ٿ��
        m_Player.GameSceneUI.m_AreaUI.ShowNewAreaName(name);

    }
}
