using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public IntroUI m_IntroUI;
    public bool m_bisStartNewGame = false;

    public Vector3 m_StartPoint = new Vector3(36.36f, 4.95f, -14.23298f);

    public void GameStart()
    { 
        Managers.UI.CloseAllPopupUI();
        m_IntroUI = Managers.UI.ShowPopupUI<IntroUI>();

        // ������ ó�� �����ߴٸ�
        if (m_bisStartNewGame == false)
        {
            m_bisStartNewGame = true;
            // ĳ���� �ʱ� ������ ��������

            // ��Ʈ�ξ� ó������ �����ֱ�
            m_IntroUI.PlayMP4("StartOpening");

            // ĳ���� ��ȯ
            Managers.Object.m_MyPlayer.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
            Managers.Object.m_MyPlayer.transform.position = m_StartPoint;
            Managers.Object.m_MyPlayer.ReStart();

            // ������ Refresh
            Managers.Object.m_MyPlayer.m_GameUIManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(Managers.Object.m_MyPlayer.playerInventoryManager);
            Managers.Object.m_MyPlayer.m_GameUIManager.quickSlotsUI.UpdateAllQuickSlotUI();
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
