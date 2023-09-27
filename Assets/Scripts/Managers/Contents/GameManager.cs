using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public IntroUI m_IntroUI;

    public Vector3 m_StartPoint = new Vector3(36.36f, 4.95f, -14.23298f);

    public void GameStart(bool NewGame)
    { 
        Managers.UI.CloseAllPopupUI();

        // ������ ó�� �����ߴٸ�
        if (NewGame)
        {
             m_IntroUI = Managers.UI.ShowPopupUI<IntroUI>();

            // ĳ���� �ʱ� ������ ��������

            // ��Ʈ�ξ� ó������ �����ֱ�
            m_IntroUI.PlayMP4("StartOpening");

            // ĳ���� ��ȯ
            Managers.Object.m_MyPlayer.transform.rotation = new Quaternion(0f, -90f, 0f, 0f);
            Managers.Object.m_MyPlayer.transform.position = m_StartPoint;
            Managers.Object.m_MyPlayer.ReStart();

            // ������ Refresh
            Managers.Object.m_MyPlayer.m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.LoadWeaponsOnEquipmentScreen(Managers.Object.m_MyPlayer.playerInventoryManager);
            Managers.Object.m_MyPlayer.m_GameUIManager.m_HUDUI.quickSlotsUI.UpdateAllQuickSlotUI();
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
}
