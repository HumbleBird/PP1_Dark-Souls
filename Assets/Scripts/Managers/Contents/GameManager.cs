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

        // 게임을 처음 시작했다면
        if (m_isNewGame)
        {
            // TODO 캐릭터 초기 데이터 가져오기

            // 인트로씬 처음으로 보여주기
            Managers.GameUI.ShowIntro("StartOpening");

            // 퀵슬롯 Refresh
            //Managers.Object.m_MyPlayer.m_GameUIManager.m_PlayerPrivateUI.m_EquipmentUI.RefreshUI();
            //Managers.Object.m_MyPlayer.m_GameUIManager.quickSlotsUI.UpdateAllQuickSlotUI();
        }
        else
        {
            // 캐릭터 데이터 가져오기
        }

        m_Player.StartGame();
    }

    public void PlayAction(Action action)
    {
        action.Invoke();
    }

    public void PlayerDead()
    {
        // 모든 몬스터의 CurrentTarget null
        // 몬스터 제자리 위치
        // 몬스터 피 회복
        // 플레이어의 소울을 남기는 Interactable Object 생성
        // 플레이어 마지막 본파이어에 리스폰
        // 플레이어의 체력 회복
        // 스크린 인 아웃
    }

    public void EntryNewArea(string name)
    {
        // UI로 화면 뛰우기
        m_Player.GameSceneUI.m_AreaUI.ShowNewAreaName(name);

    }
}
