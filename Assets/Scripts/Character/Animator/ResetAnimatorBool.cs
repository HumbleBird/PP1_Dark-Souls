using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public string isInvulerable = "isInvulerable";
    public bool isInvulerableStatus = false;

    public string isInteractingBool = "isInteracting";
    public bool isInteractingStatus = false;

    public string isFiringSpellBool = "isFiringSpell";
    public bool isFiringSpellStatus = false;

    public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
    public bool isRotatingWithRootMotionStatus = false;

    public string canRotateBool = "canRotate";
    public bool canRotateStatus = true;

    public string isMirroredBool = "isMirrored";
    public bool isMirroredStatus = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterManager character = animator.GetComponent<CharacterManager>();

        character.isUsingLeftHand = false;
        character.isUsingRightHand = false;
        character.isAttacking = false;
        character.isBeingBackstabbed = false;
        character.isBeingRiposted = false;
        character.isPerformingBackstab = false;
        character.isPerformingRipost = false;
        character.canBeParryied = false;
        character.canBeRiposted = false;
        character.canRoll = true;

        // 데미지 애니메이션 이후,Reset previous poise damage  to 0
        character.characterCombatManager.previousPoiseDamageTaken = 0;

        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isFiringSpellBool, isFiringSpellStatus);
        animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
        animator.SetBool(canRotateBool, canRotateStatus);
        animator.SetBool(isInvulerable, isInvulerableStatus);
        animator.SetBool(isMirroredBool, isMirroredStatus);
    }
}
