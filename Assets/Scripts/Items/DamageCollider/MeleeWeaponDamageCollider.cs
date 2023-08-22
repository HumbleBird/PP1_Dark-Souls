using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MeleeWeaponDamageCollider : DamageCollider
{
    [Header("Weapon Buff Damage")]
    public float physicalBuffDamage;
    public float fireBuffDamage;
    public float poiseBuffDamage;

    protected override void DealDamage(CharacterStatsManager enemyStats)
    {
        float finalPhysicalDamage = physicalDamage + physicalBuffDamage;
        float finalFireDamage = fireDamage + fireBuffDamage;
        float finalDamage = 0;

        // Right Weapon Modifire
        if (characterManager.isUsingRightHand)
        {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.rightWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterInventoryManager.rightWeapon.lightAttackStaminaMultiplier;
            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.rightWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterInventoryManager.rightWeapon.lightAttackStaminaMultiplier;
            }
        }

        // Left Weapon Modifire
        else if (characterManager.isUsingLeftHand)
        {
            if (characterManager.characterCombatManager.currentAttackType == AttackType.light)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.leftWeapon.lightAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterInventoryManager.leftWeapon.lightAttackStaminaMultiplier;

            }
            else if (characterManager.characterCombatManager.currentAttackType == AttackType.heavy)
            {
                finalDamage = finalPhysicalDamage * characterManager.characterInventoryManager.leftWeapon.heavyAttackStaminaMultiplier;
                finalDamage += finalFireDamage * characterManager.characterInventoryManager.leftWeapon.lightAttackStaminaMultiplier;

            }
        }

        // Deal modifired Damage
        if (enemyStats.totalPoiseDefence > poiseBreak)
        {
            enemyStats.TakeDamageNoAnimation(Mathf.RoundToInt(finalDamage), Mathf.RoundToInt(finalFireDamage));
        }
        else
        {
            //enemyStats.TakeDamage(Mathf.RoundToInt(finalPhysicalDamage), Mathf.RoundToInt(finalFireDamage), currentDamageAnimation, characterManager);
        }
    }
}
