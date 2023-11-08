using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

    }

    public override void DrainStaminaBasedOnAttack()
    {
        if (player.isUsingRightHand)
        {
            if (player.isTwoHandingWeapon)
            {
                if (currentAttackType == AttackType.light)
                {
                    player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier + player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackStaminaMultiplier);
                }
                else if (currentAttackType == AttackType.heavy)
                {
                    player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier + player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.m_fTHAttackStaminaMultiplier);
                }
            }
            else
            {
                if (currentAttackType == AttackType.light)
                {
                    player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.lightAttackStaminaMultiplier);
                }
                else if (currentAttackType == AttackType.heavy)
                {
                    player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandRightWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandRightWeapon.heavyAttackStaminaMultiplier);
                }
            }

        }
        else if (player.isUsingLeftHand)
        {
            if (currentAttackType == AttackType.light)
            {
                player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandLeftWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandLeftWeapon.lightAttackStaminaMultiplier);
            }
            else if (currentAttackType == AttackType.heavy)
            {
                player.playerStatsManager.DeductStamina(player.playerEquipmentManager.m_CurrentHandLeftWeapon.baseStaminaCost * player.playerEquipmentManager.m_CurrentHandLeftWeapon.heavyAttackStaminaMultiplier);
            }
        }
    }
}
