using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(AICharacterManager enemy)
    {
        enemy.animator.SetFloat("Vertical", 0);
        enemy.animator.SetFloat("Horizontal", 0);

        if (enemy.isInteracting)
            return this; // 그 state에 진입했을 때, 공격 애니메이션이 진행되는 동안의 interacting 이라면 끝나기 전까지 여기서 멈출 수 있다.

        if(enemy.viewableAngle >= 100 && enemy.viewableAngle <= 180 & !enemy.isInteracting)
        {
            enemy.aiCharacterAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (enemy.viewableAngle <= -101 && enemy.viewableAngle >= -180 & !enemy.isInteracting)
        {
            enemy.aiCharacterAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (enemy.viewableAngle <= -45 && enemy.viewableAngle >= -100 & !enemy.isInteracting)
        {
            enemy.aiCharacterAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Right", true);
            return combatStanceState;
        }
        else if (enemy.viewableAngle >= 45 && enemy.viewableAngle <= 100 & !enemy.isInteracting)
        {
            enemy.aiCharacterAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Left", true);
            return combatStanceState;
        }

        return combatStanceState;
    }
}
