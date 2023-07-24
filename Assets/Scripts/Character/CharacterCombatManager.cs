using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterCombatManager : MonoBehaviour
{
    [Header("Attack Type")]
    public AttackType currentAttackType;

    public virtual void DrainStaminaBasedOnAttack()
    {

    }
}
