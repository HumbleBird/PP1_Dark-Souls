using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Interactable m_Interactable;
    PlayerManager m_Player;

    public bool m_isNewGame = true;
    public bool isReSeting = false;

    DeadSouls m_goDeadSouls;

    public void GameStart()
    {
        m_Player = Managers.Object.m_MyPlayer;

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
        isReSeting = true;

        // ���̵� �ƿ��� ���� ������ 3�ʰ� ���
        // ���Ŀ��� ���⿡ ���� �����͸� ���� ��.

        // �ҿ��� �̹� �ִٸ�
        if(m_goDeadSouls != null)
            Managers.Resource.Destroy(m_goDeadSouls.gameObject);

        // �ҿ��� ����
        m_goDeadSouls = Managers.Resource.Instantiate("Objects/Interact Object/Dead Soul").GetComponent<DeadSouls>();
        m_goDeadSouls.transform.position = m_Player.transform.position;
        m_goDeadSouls.m_iSoulsCount = m_Player.playerStatsManager.currentSoulCount;

        // UI ������Ʈ
        m_Player.m_GameSceneUI.RefreshUI(Define.E_StatUI.All);

        //��� ĳ���� �ʱ�ȭ
        foreach (GameObject go in Managers.Object._objects)
        {
            CharacterManager character = go.GetComponent<CharacterManager>();

            character.InitCharacterManager();
        }

        yield return new WaitForSeconds(1);
        isReSeting = false;

        // ���̵� ��
        m_Player.m_GameSceneUI.m_FadeInOutScreenUI.FadeIn();

        yield break;
    }

    public void PlayerisStop(bool isStop = true)
    {
        Managers.Camera.m_Camera.m_isCanRotate = !isStop;
        Managers.Object.m_MyPlayer.playerLocomotionManager.m_isCanMove = !isStop;
    }
}
