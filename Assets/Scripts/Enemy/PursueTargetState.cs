using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표물을 쫓음
// 공격 거리 안에 든다면 Combat State로 변경
// 타겟이 사정 거리에서 벗어난다면 다시 이 상태로 변경후 목표물 쫓음

public class PursueTargetState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        return this;
    }
}
