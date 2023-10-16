using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Interactable m_Interactable;
    PlayerManager m_Player;

    public bool m_isNewGame = true;

    float m_fRestartWaitTime = 3f;


    public void GameStart()
    {
        m_Player = Managers.Object.m_MyPlayer;

        // 게임을 처음 시작했다면
        if (m_isNewGame)
        {

            // 인트로씬 처음으로 보여주기
            Managers.GameUI.ShowIntro("StartOpening");
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

    public void EntryNewArea(string name)
    {
        // UI로 화면 뛰우기
        m_Player.m_GameSceneUI.m_AreaUI.ShowNewAreaName(name);

    }

    public IEnumerator PlayerDead()
    {

        // 유다이 Popup (+ 사운드)
        m_Player.m_GameSceneUI.ShowYouDied();

        // 페이드 아웃
        yield return new WaitForSeconds(5);

        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeOut();

        // 페이드 아웃 유지 시간
        yield return new WaitForSeconds(3);

        // 페이드 아웃이 전부 끝나면 3초간 대기
        // 이후에는 여기에 서버 데이터를 받을 것.

        // 소울을 남김
        GameObject deadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul");
        deadSouls.transform.position = m_Player.transform.position;

        // UI 업데이트
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);

        //모든 캐릭터 초기화
        foreach (GameObject go in Managers.Object._objects)
        {
            CharacterManager character = go.GetComponent<CharacterManager>();

            character.InitCharacterManager();
        }

        // 페이드 인
        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeIn();

        yield break;
    }
}
