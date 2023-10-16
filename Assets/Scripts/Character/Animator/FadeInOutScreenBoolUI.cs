using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScreenBoolUI : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FadeInOutScreenUI ui = animator.GetComponent<FadeInOutScreenUI>();

        ui.m_bisAnimationEnd = true;

    }
}
