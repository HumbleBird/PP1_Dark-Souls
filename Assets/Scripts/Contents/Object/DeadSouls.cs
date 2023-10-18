using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSouls : Interactable
{
    public int m_iSoulsCount;

    protected override void Awake()
    {
        base.Awake();

        interactableText = "Recover lost souls"; // �Ҿ���� �ҿ��� ��ã�´�
    }

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        // souls restrived �˾� ����
        Managers.Object.m_MyPlayer.m_GameSceneUI.ShowSoulsRetrieved();

        // sound
        // �ҿ� �ֱ�
        Managers.Object.m_MyPlayer.playerStatsManager.AddSouls(m_iSoulsCount);

        Managers.Resource.Destroy(gameObject);
    }
}
