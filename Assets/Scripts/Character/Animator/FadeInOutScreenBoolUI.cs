using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScreenBoolUI : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FadeInOutScreenUI ui = animator.GetComponent<FadeInOutScreenUI>();
        Canvas can = animator.GetComponentInParent<Canvas>();

        if (ui.m_bisFadeIn)
        {
            can.sortingOrder = -10;
        }

        ui.m_bisAnimationEnd = true;
    }
}
