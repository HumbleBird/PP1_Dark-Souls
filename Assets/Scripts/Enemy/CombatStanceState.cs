using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����Ÿ� üũ
// �÷��̾� �ֺ��� �� ���·� ����
// ���� ���� �Ÿ� �ȿ� �ִٸ� Attack State�� ����
// ���� ��Ÿ�� ���̶��, �� State�� ���� �� Circling player ��.
// �÷��̾ �����Ÿ�(����?) ���� ����ٸ� Pursue State�� ���� 

public class CombatStanceState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        return this;
    }
}
