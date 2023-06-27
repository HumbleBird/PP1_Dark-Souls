using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public bool canRotate;

    public void PlayerTargetAnimation(string targetAnim, bool isInteracting, bool canRoate = false)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("canRotate", canRoate);
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    public void PlayerTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isRotatingWithRootMotion", true);
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {

    }
}
