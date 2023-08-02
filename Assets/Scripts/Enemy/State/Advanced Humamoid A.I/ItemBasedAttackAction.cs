using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "A.I/Humanoid Actions/Item Based Attack Action")]
public class ItemBasedAttackAction : ScriptableObject
{
    [Header("Attack Type")]
    public AIAttackActionType actionAttackType = AIAttackActionType.meleeAttackAction;
    public AttackType attackType = AttackType.light;

    [Header("Action Combo Settings")]
    public bool actionCanCombo = false;

    [Header("Right Hand Or Left Hand Action")]
    bool isRightHandedAction = true;

    [Header("Action Settings")]
    public int attackScore = 3;
    public float recoveryTime = 2;
    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;
    public float maximumDistanceNeededToAttack = 3;
    public float minimumDistanceNeededToAttack = 0;

    public void PerformAttackAction(AICharacterManager enemy)
    {
        if(isRightHandedAction)
        {
            enemy.UpdateWhichHandCharacterIsUsing(true);
            PerformRightHandItemActionBasedOnAttackType(enemy);
        }
        else
        {
            enemy.UpdateWhichHandCharacterIsUsing(false);
            PerformLeftHandItemActionBasedOnAttackType(enemy);
        }
    }

    private void PerformRightHandItemActionBasedOnAttackType(AICharacterManager enemy)
    {
        if(actionAttackType == AIAttackActionType.meleeAttackAction)
        {
            PerformRightHandMellAction(enemy);
        }
        else if (actionAttackType == AIAttackActionType.rangedAttackaCtion)
        {

        }
    }

    private void PerformLeftHandItemActionBasedOnAttackType(AICharacterManager enemy)
    {
        if (actionAttackType == AIAttackActionType.meleeAttackAction)
        {

        }
        else if (actionAttackType == AIAttackActionType.rangedAttackaCtion)
        {

        }
    }

    private void PerformRightHandMellAction(AICharacterManager enemy)
    {
        if(enemy.isTwoHandingWeapon)
        {
            if(attackType == AttackType.light)
            {
                enemy.characterInventoryManager.rightWeapon.th_tap_RB_Action.PerformAction(enemy);
            }
            else if (attackType == AttackType.heavy)
            {
                enemy.characterInventoryManager.rightWeapon.th_tap_RT_Action.PerformAction(enemy);
            }
        }
        else
        {
            if (attackType == AttackType.light)
            {
                enemy.characterInventoryManager.rightWeapon.oh_tap_RB_Action.PerformAction(enemy);
            }
            else if (attackType == AttackType.heavy)
            {
                enemy.characterInventoryManager.rightWeapon.oh_tap_RT_Action.PerformAction(enemy);
            }
        }
    }
}
