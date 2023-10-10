using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public Interactable m_Interactable;

    public Vector3 m_StartPoint = new Vector3(36.36f, 4.95f, -14.23298f);

    public void GameStart(bool NewGame)
    { 
        Managers.UI.CloseAllPopupUI();

        // ������ ó�� �����ߴٸ�
        if (NewGame)
        {
            // ĳ���� �ʱ� ������ ��������

            // ��Ʈ�ξ� ó������ �����ֱ�
            Managers.GameUI.ShowIntro("StartOpening");

            // ĳ���� ��ȯ
            Managers.Object.m_MyPlayer.transform.rotation = new Quaternion(0f, -90f, 0f, 0f);
            Managers.Object.m_MyPlayer.transform.position = m_StartPoint;
            Managers.Object.m_MyPlayer.ReStart();

            // ������ Refresh
            //Managers.Object.m_MyPlayer.m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.RefreshUI();
            //Managers.Object.m_MyPlayer.m_GameUIManager.quickSlotsUI.UpdateAllQuickSlotUI();
        }
        else
        {
            // ĳ���� ������ ��������
        }
    }

    public void SpawnPlayer()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
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
}
