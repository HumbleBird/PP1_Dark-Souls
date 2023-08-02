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
            return this; // �� state�� �������� ��, ���� �ִϸ��̼��� ����Ǵ� ������ interacting �̶�� ������ ������ ���⼭ ���� �� �ִ�.

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
