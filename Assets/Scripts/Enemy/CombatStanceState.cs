using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격 사정거리 체크
// 플레이어 주변을 원 형태로 걸음
// 공격 사정 거리 안에 있다면 Attack State로 변경
// 공격 쿨타임 중이라면, 이 State로 복귀 후 Circling player 함.
// 플레이어가 사정거리(공격?) 에서 벗어난다면 Pursue State로 변경 

public class CombatStanceState : State
{
    public AttackState attackState;
    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        float distancefromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        
        if(enemyManager.isPreformingAction)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

        if(enemyManager.currentRecoveryTime <= 0 && distancefromTarget <= enemyManager.maximunAttackRange)
        {
            return attackState;
        }
        else if (distancefromTarget > enemyManager.maximunAttackRange)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
