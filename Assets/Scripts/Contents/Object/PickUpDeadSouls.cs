using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDeadSouls : Interactable
{
    protected override void Awake()
    {
        base.Awake();

        interactableText = "Recover lost souls"; // �Ҿ���� �ҿ��� ��ã�´�


    }

    public override void Interact(PlayerManager playermanager)
    {
        // souls restrived �˾� ����
        // sound
        // �ҿ� �ֱ�
    }
}
