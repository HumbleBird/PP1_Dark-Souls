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

        // ������ ó�� �����ߴٸ�
        if (m_isNewGame)
        {

            // ��Ʈ�ξ� ó������ �����ֱ�
            Managers.GameUI.ShowIntro("StartOpening");
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

    public void EntryNewArea(string name)
    {
        // UI�� ȭ�� �ٿ��
        m_Player.m_GameSceneUI.m_AreaUI.ShowNewAreaName(name);

    }

    public IEnumerator PlayerDead()
    {

        // ������ Popup (+ ����)
        m_Player.m_GameSceneUI.ShowYouDied();

        // ���̵� �ƿ�
        yield return new WaitForSeconds(5);

        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeOut();

        // ���̵� �ƿ� ���� �ð�
        yield return new WaitForSeconds(3);

        // ���̵� �ƿ��� ���� ������ 3�ʰ� ���
        // ���Ŀ��� ���⿡ ���� �����͸� ���� ��.

        // �ҿ��� ����
        GameObject deadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul");
        deadSouls.transform.position = m_Player.transform.position;

        // UI ������Ʈ
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);

        //��� ĳ���� �ʱ�ȭ
        foreach (GameObject go in Managers.Object._objects)
        {
            CharacterManager character = go.GetComponent<CharacterManager>();

            character.InitCharacterManager();
        }

        // ���̵� ��
        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeIn();

        yield break;
    }
}
