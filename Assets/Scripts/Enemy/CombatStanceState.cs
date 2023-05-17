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
    public AttackState attackState;
    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        enemyManager.distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        
        if(enemyManager.currentRecoveryTime <= 0 && enemyManager.distancefromTarget <= enemyManager.maximunAttackRange)
        {
            return attackState;
        }
        else if (enemyManager.distancefromTarget > enemyManager.maximunAttackRange)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
