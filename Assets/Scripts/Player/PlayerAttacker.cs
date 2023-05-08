using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    public string lastAttack;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animatorHandler.PlayerTargetAnimation(weapon.OH_Light_Attack_1, true);
        lastAttack = weapon.OH_Light_Attack_1;
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animatorHandler.PlayerTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        lastAttack = weapon.OH_Heavy_Attack_1;
    }
}
