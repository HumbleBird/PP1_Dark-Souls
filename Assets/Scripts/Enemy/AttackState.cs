using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스코어 점수에 따른 공격들 중 하나 선택
// 선택한 공격이 각도, 거리 상의 문제가 있다면 새롭게 다시 선택
// 공격이 변형 가능하다면, 공격자의 움직임을 멈추거나, 타겟을 공격한다.
// 공격 쿨타임 체크 
// combat State로 복귀

public class AttackState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStatus enemyStates, EnemyAnimationManager enemyAnimationManager)
    {
        return this;
    }
}
