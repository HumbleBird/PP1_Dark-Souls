using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Charge Attack Action")]
public class ChargeAttackActon : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;


        player.playerAnimatorManager.EraseHandIKForWeapon();
        player.playerEffectsManager.PlayWeaponFX(false);


        // 콤보
        if (player.canDoCombo)
        {
            player.inputHandler.comboFlag = true;
            HandleChargeWeaponCombo(player);
            player.inputHandler.comboFlag = false;
        }

        // 콤보 말고 정해진 공격 (1타)
        else
        {
            if (player.isInteracting)
                return;

            if (player.canDoCombo)
                return;

            HandleChargeAttack(player);
        }

    }

    private void HandleChargeAttack(PlayerManager player)
    {
        if (player.isUsingLeftHand)
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_01, true, false, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_01;
        }
        else if (player.isUsingRightHand)
        {
            if (player.inputHandler.twoHandFlag)
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_charge_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.th_charge_attack_01;
            }
            else
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_01;
            }
        }
    }


    private void HandleChargeWeaponCombo(PlayerManager player)
    {
        if (player.inputHandler.comboFlag)
        {
            player.animator.SetBool("canDoCombo", false);

            if (player.isUsingLeftHand)
            {
                if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_charge_attack_01)
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_02, true, false, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_02;
                }
                else
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_01, true, false, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_01;
                }
            }

            else if (player.isUsingRightHand)
            {
                if (player.isTwoHandingWeapon)
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.th_charge_attack_01)
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_charge_attack_02, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.th_charge_attack_02;
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_charge_attack_01, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.th_charge_attack_01;
                    }
                }
                else
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_charge_attack_01)
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_02, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_02;
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_charge_attack_01, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_charge_attack_01;
                    }
                }
            }
        }
    }
}
