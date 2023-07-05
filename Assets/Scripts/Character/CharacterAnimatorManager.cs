using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimatorManager : MonoBehaviour
{
    public Animator animator;
    protected CharacterManager characterManager;
    protected CharacterStatsManager characterStatsManager;

    public bool canRotate;

    protected RigBuilder rigBuilder;
    public TwoBoneIKConstraint leftHandConstraint;
    public TwoBoneIKConstraint rightHandConstraint;

    bool handIKWeightReset = false;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        characterManager = GetComponent<CharacterManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        rigBuilder = GetComponentInChildren<RigBuilder>();


    }

    public void PlayerTargetAnimation(string targetAnim, bool isInteracting, bool canRoate = false, bool mirrorAnim = false)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("canRotate", canRoate);
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isMirrored", mirrorAnim);
        animator.CrossFade(targetAnim, 0.2f);
    }

    public void PlayerTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isRotatingWithRootMotion", true);
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    public virtual void CanRotate()
    {
        animator.SetBool("canRotate", true);
    }

    public virtual void StopRotation()
    {
        animator.SetBool("canRotate", false);
    }

    public virtual void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public virtual void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);

    }

    public virtual void EnableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", true);
    }

    public virtual void DisableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", false);

    }

    public void EnableIsParrying()
    {
        characterManager.isParrying = true;
    }

    public void DisableIsParrying()
    {
        characterManager.isParrying = false;
    }

    public void EnableCanBeRiposted()
    {
        characterManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        characterManager.canBeRiposted = false;
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {
        characterStatsManager.TakeDamageNoAnimation(characterManager.pendingCriticalDamage, 0);
        characterManager.pendingCriticalDamage = 0;
    }

    public virtual void SetHandIKForWeapon(RightHandIKTarget rightHandTarget, LeftHandIKTarget leftHandTarget, bool isTwoHandingWeapon)
    {
        if(isTwoHandingWeapon)
        {
            if(rightHandTarget != null)
            {
                rightHandConstraint.data.target = rightHandTarget.transform;
                rightHandConstraint.data.targetPositionWeight = 1; // 원한다면 각 무기 별로 할당 가능
                rightHandConstraint.data.targetRotationWeight = 1;
            }
            
            if(leftHandTarget != null)
            {
                leftHandConstraint.data.target = leftHandTarget.transform;
                leftHandConstraint.data.targetPositionWeight = 1;
                leftHandConstraint.data.targetRotationWeight = 1;
            }
        }
        else
        {
            rightHandConstraint.data.target = null;
            leftHandConstraint.data.target = null;
        }

        rigBuilder.Build();
    }

    public virtual void CheckHandIKWeight(RightHandIKTarget rightHandIK, LeftHandIKTarget leftHandIK, bool isTwoHandingWeapon)
    {
        if (characterManager.isInteracting)
            return;

        if(handIKWeightReset)
        {
            handIKWeightReset = false;

            if (rightHandConstraint.data.target != null)
            {
                rightHandConstraint.data.target = rightHandIK.transform;
                rightHandConstraint.data.targetPositionWeight = 1;
                rightHandConstraint.data.targetRotationWeight = 1;
            }

            if (leftHandConstraint.data.target != null)
            {
                leftHandConstraint.data.target = leftHandIK.transform;
                leftHandConstraint.data.targetPositionWeight = 1;
                leftHandConstraint.data.targetRotationWeight = 1;
            }
        }
    }

    public virtual void EraseHandIKForWeapon()
    {
        handIKWeightReset = true;

        if(rightHandConstraint.data.target != null)
        {
            rightHandConstraint.data.targetPositionWeight = 0; 
            rightHandConstraint.data.targetRotationWeight = 0;
        }

        if(leftHandConstraint.data.target != null)
        {
            leftHandConstraint.data.targetPositionWeight = 0;
            leftHandConstraint.data.targetRotationWeight = 0;
        }
    }
}
