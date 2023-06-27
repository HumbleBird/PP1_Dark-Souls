using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStatsManager, EnemyAnimationManager enemyAnimationManager)
    {
        enemyAnimationManager.animator.SetFloat("Vertical", 0);
        enemyAnimationManager.animator.SetFloat("Horizontal", 0);

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

        if (enemyManager.isInteracting)
            return this; // �� state�� �������� ��, ���� �ִϸ��̼��� ����Ǵ� ������ interacting �̶�� ������ ������ ���⼭ ���� �� �ִ�.

        if(viewableAngle >= 100 && viewableAngle <= 180 & !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (viewableAngle <= -101 && viewableAngle >= -180 & !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Behind", true);
            return combatStanceState;
        }
        else if (viewableAngle <= -45 && viewableAngle >= -100 & !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Right", true);
            return combatStanceState;
        }
        else if (viewableAngle >= 45 && viewableAngle <= 100 & !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayerTargetAnimationWithRootRotation("Turn Left", true);
            return combatStanceState;
        }

        return combatStanceState;
    }
}
