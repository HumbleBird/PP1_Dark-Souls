using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(EnemyManager enemy)
    {
        enemy.animator.SetFloat("Vertical", 0);
        enemy.animator.SetFloat("Horizontal", 0);

        Vector3 targetDirection = enemy.currentTarget.transform.position - enemy.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemy.transform.forward, Vector3.up);

        if (enemy.isInteracting)
            return this; // 그 state에 진입했을 때, 공격 애니메이션이 진행되는 동안의 interacting 이라면 끝나기 전까지 여기서 멈출 수 있다.

        if(viewableAngle >= 100 && viewableAngle <= 180 & !enemy.isInteracting)
        {
            enemy.enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (viewableAngle <= -101 && viewableAngle >= -180 & !enemy.isInteracting)
        {
            enemy.enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (viewableAngle <= -45 && viewableAngle >= -100 & !enemy.isInteracting)
        {
            enemy.enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Right", true);
            return combatStanceState;
        }
        else if (viewableAngle >= 45 && viewableAngle <= 100 & !enemy.isInteracting)
        {
            enemy.enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Left", true);
            return combatStanceState;
        }

        return combatStanceState;
    }
}
