using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;
    public bool canRotate;

    public void PlayerTargetAnimation(string targetAnim, bool isInteracting, bool canRoate = false)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("canRotate", canRoate);
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void PlayerTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isRotatingWithRootMotion", true);
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {

    }
}
