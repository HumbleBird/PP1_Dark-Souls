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

        // 게임을 처음 시작했다면
        if (NewGame)
        {
             m_IntroUI = Managers.UI.ShowPopupUI<IntroUI>();

            // 캐릭터 초기 데이터 가져오기

            // 인트로씬 처음으로 보여주기
            m_IntroUI.PlayMP4("StartOpening");

            // 캐릭터 소환
            Managers.Object.m_MyPlayer.transform.rotation = new Quaternion(0f, -90f, 0f, 0f);
            Managers.Object.m_MyPlayer.transform.position = m_StartPoint;
            Managers.Object.m_MyPlayer.ReStart();

            // 퀵슬롯 Refresh
            Managers.Object.m_MyPlayer.m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.LoadWeaponsOnEquipmentScreen(Managers.Object.m_MyPlayer.playerInventoryManager);
            Managers.Object.m_MyPlayer.m_GameUIManager.m_HUDUI.quickSlotsUI.UpdateAllQuickSlotUI();
        }
        else
        {
            // 캐릭터 데이터 가져오기
        }


    }

    public void SpawnPlayer()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
    }
}
