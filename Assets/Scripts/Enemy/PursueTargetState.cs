using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ���� ����
// ���� �Ÿ� �ȿ� ��ٸ� Combat State�� ����
// Ÿ���� ���� �Ÿ����� ����ٸ� �ٽ� �� ���·� ������ ��ǥ�� ����

public class PursueTargetState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        return this;
    }
}
