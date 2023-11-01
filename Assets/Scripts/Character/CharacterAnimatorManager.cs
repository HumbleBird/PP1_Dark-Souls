using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimatorManager : MonoBehaviour
{
    protected CharacterManager character;

    protected RigBuilder rigBuilder;
    public TwoBoneIKConstraint leftHandConstraint;
    public TwoBoneIKConstraint rightHandConstraint;

    [Header("DAMAGE ANIMATIONS")]
    [HideInInspector] public string Damage_Forward_Medium_01    ="Damage_Forward_01"; //= "Damage_Forward_Medium_01";
    [HideInInspector] public string Damage_Forward_Medium_02    = "Damage_Forward_01"; //= "Damage_Forward_Medium_02";
                                                             
    [HideInInspector] public string Damage_Back_Medium_01       ="Damage_Back_01"; //= "Damage_Back_Medium_01";
    [HideInInspector] public string Damage_Back_Medium_02       = "Damage_Back_01"; //= "Damage_Back_Medium_02";
                                                               
    [HideInInspector] public string Damage_Left_Medium_01       ="Damage_Left_01"; //= "Damage_Left_Medium_01";
    [HideInInspector] public string Damage_Left_Medium_02       = "Damage_Left_01"; //= "Damage_Left_Medium_02";
                                                              
    [HideInInspector] public string Damage_Right_Medium_01      ="Damage_Right_01"; //= "Damage_Right_Medium_01";
    [HideInInspector] public string Damage_Right_Medium_02      = "Damage_Right_01"; //= "Damage_Right_Medium_02";
                                                               
    [HideInInspector] public string Damage_Forward_Heavy_01     ="Damage_Forward_01"; //= "Damage_Forward_Heavy_01";
    [HideInInspector] public string Damage_Forward_Heavy_02     = "Damage_Forward_01"; //= "Damage_Forward_Heavy_02";
                                                              
    [HideInInspector] public string Damage_Back_Heavy_01        ="Damage_Back_01"; //= "Damage_Back_Heavy_01";
    [HideInInspector] public string Damage_Back_Heavy_02        = "Damage_Back_01"; //= "Damage_Back_Heavy_02";
                                                               
    [HideInInspector] public string Damage_Left_Heavy_01        ="Damage_Left_01"; //= "Damage_Left_Heavy_01";
    [HideInInspector] public string Damage_Left_Heavy_02        = "Damage_Left_01"; //= "Damage_Left_Heavy_02";
                                                               
    [HideInInspector] public string Damage_Right_Heavy_01       ="Damage_Right_01"; //= "Damage_Right_Heavy_01";
    [HideInInspector] public string Damage_Right_Heavy_02       = "Damage_Right_01"; //= "Damage_Right_Heavy_02";
                                                              
    [HideInInspector] public string Damage_Forward_Colssal_01   ="Damage_Forward_01"; //= "Damage_Forward_Colssal_01";
    [HideInInspector] public string Damage_Forward_Colssal_02   = "Damage_Forward_01"; //= "Damage_Forward_Colssal_02";
                                                              
    [HideInInspector] public string Damage_Back_Colssal_01      ="Damage_Back_01"; //= "Damage_Back_Colssal_01";
    [HideInInspector] public string Damage_Back_Colssal_02      = "Damage_Back_01"; //= "Damage_Back_Colssal_02";
                                                              
    [HideInInspector] public string Damage_Left_Colssal_01      ="Damage_Left_01"; //= "Damage_Left_Colssal_01";
    [HideInInspector] public string Damage_Left_Colssal_02      = "Damage_Left_01"; //= "Damage_Left_Colssal_02";
                                                               
    [HideInInspector] public string Damage_Right_Colssal_01     ="Damage_Right_01"; //= "Damage_Right_Colssal_01";
    [HideInInspector] public string Damage_Right_Colssal_02     = "Damage_Right_01"; //= "Damage_Right_Colssal_02";

    [HideInInspector] public List<string> Damage_Animation_Medium_Forward       = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Medium_Back      = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Medium_Left      = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Medium_Right      = new List<string>();

    [HideInInspector] public List<string> Damage_Animation_Heavy_Forward         = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Heavy_Back       = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Heavy_Left       = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Heavy_Right       = new List<string>();

    [HideInInspector] public List<string> Damage_Animation_Colssal_Forward      = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Colssal_Back         = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Colssal_Left      = new List<string>();
    [HideInInspector] public List<string> Damage_Animation_Colssal_Right         = new List<string>();

    bool handIKWeightReset = false;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        rigBuilder = GetComponentInChildren<RigBuilder>();
    }

    protected virtual void Start()
    {
        Damage_Animation_Medium_Forward .Add(Damage_Forward_Medium_01);
        Damage_Animation_Medium_Forward .Add(Damage_Forward_Medium_02);
        Damage_Animation_Medium_Back    .Add(Damage_Back_Medium_01);
        Damage_Animation_Medium_Back    .Add(Damage_Back_Medium_02);
        Damage_Animation_Medium_Left    .Add(Damage_Left_Medium_01);
        Damage_Animation_Medium_Left    .Add(Damage_Left_Medium_02);
        Damage_Animation_Medium_Right   .Add(Damage_Right_Medium_01);
        Damage_Animation_Medium_Right   .Add(Damage_Right_Medium_02);
                                       
        Damage_Animation_Heavy_Forward  .Add(Damage_Forward_Heavy_01  );
        Damage_Animation_Heavy_Forward  .Add(Damage_Forward_Heavy_02  );
        Damage_Animation_Heavy_Back     .Add(Damage_Back_Heavy_01     );
        Damage_Animation_Heavy_Back     .Add(Damage_Back_Heavy_02     );
        Damage_Animation_Heavy_Left     .Add(Damage_Left_Heavy_01     );
        Damage_Animation_Heavy_Left     .Add(Damage_Left_Heavy_02     );
        Damage_Animation_Heavy_Right    .Add(Damage_Right_Heavy_01    );
        Damage_Animation_Heavy_Right    .Add(Damage_Right_Heavy_02    );

        Damage_Animation_Colssal_Forward.Add(Damage_Forward_Colssal_01);
        Damage_Animation_Colssal_Forward.Add(Damage_Forward_Colssal_02);
        Damage_Animation_Colssal_Back   .Add(Damage_Back_Colssal_01   );
        Damage_Animation_Colssal_Back   .Add(Damage_Back_Colssal_02   );
        Damage_Animation_Colssal_Left   .Add(Damage_Left_Colssal_01   );
        Damage_Animation_Colssal_Left   .Add(Damage_Left_Colssal_02   );
        Damage_Animation_Colssal_Right  .Add(Damage_Right_Colssal_01  );
        Damage_Animation_Colssal_Right  .Add(Damage_Right_Colssal_02);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRoate = false, bool mirrorAnim = false, bool canRoll = false)
    {
        character.animator.applyRootMotion = isInteracting;
        character.animator.SetBool("canRotate", canRoate);
        character.animator.SetBool("isInteracting", isInteracting);
        character.animator.SetBool("isMirrored", mirrorAnim);
        character.animator.CrossFade(targetAnim, 0.2f);
        character.canRoll = canRoll;
    }

    public void PlayerTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
    {
        character.animator.applyRootMotion = isInteracting;
        character.animator.SetBool("isRotatingWithRootMotion", true);
        character.animator.SetBool("isInteracting", isInteracting);
        character.animator.CrossFade(targetAnim, 0.2f);
    }

    public string GetRandomDamageAnimationFromList(List<string> animationList)
    {
        int randomValue = Random.Range(0, animationList.Count);

        return animationList[randomValue];
    }

    public virtual void CanRotate()
    {
        character.animator.SetBool("canRotate", true);
    }

    public virtual void StopRotation()
    {
        character.animator.SetBool("canRotate", false);
    }

    public virtual void EnableCombo()
    {
        character.animator.SetBool("canDoCombo", true);
    }

    public virtual void DisableCombo()
    {
        character.animator.SetBool("canDoCombo", false);

    }

    public virtual void EnableCanRoll()
    {
        character.canRoll = true;
    }

    public virtual void EnableIsInvulnerable()
    {
        character.animator.SetBool("isInvulnerable", true);
    }

    public virtual void DisableIsInvulnerable()
    {
        character.animator.SetBool("isInvulnerable", false);

    }

    public void EnableIsParrying()
    {
        character.isParrying = true;
    }

    public void DisableIsParrying()
    {
        character.isParrying = false;
    }

    public void EnableCanBeRiposted()
    {
        character.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        character.canBeRiposted = false;
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {
        character.characterStatsManager.TakeDamageNoAnimation(character.pendingCriticalDamage, 0);
        character.pendingCriticalDamage = 0;
    }

    public virtual void SetHandIKForWeapon(RightHandIKTarget rightHandTarget, LeftHandIKTarget leftHandTarget, bool isTwoHandingWeapon)
    {
        if (rightHandConstraint == null || leftHandConstraint == null)
            return;

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
        if (character.isInteracting)
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

    public virtual void OnAnimatorMove()
    {
        if (character.isInteracting == false)
            return;

        Vector3 velocity = character.animator.deltaPosition;
        character.characterController.Move(velocity);
        character.transform.rotation *= character.animator.deltaRotation;
    }

    public virtual void AwardSoulsOnDeath()
    {

    }

    public virtual void RollCameraShake()
    {

    }
}


