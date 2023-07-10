using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Light Attack Action")]
public class LightAttackAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        base.PerformAction(player);

        player.playerAnimatorManager.EraseHandIKForWeapon();
        player.playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
        player.playerEffectsManager.PlayWeaponFX(false);

        // 만약 running attack을 수행할 수 있다면 할지 안 할지 결정 가능
        if (player.isSprinting)
        {
            HandleRunningAttack(player.playerInventoryManager.rightWeapon, player);
            return;
        }

        // 콤보
        if (player.canDoCombo)
        {
            player.inputHandler.comboFlag = true;
            HandleLightWeaponCombo(player.playerInventoryManager.rightWeapon, player);
            player.inputHandler.comboFlag = false;
        }

        // 콤보 말고 정해진 공격 (1타)
        else
        {
            if (player.isInteracting)
                return;

            if (player.canDoCombo)
                return;

            HandleLightAttack(player.playerInventoryManager.rightWeapon, player);
        }

    }

    private void HandleLightAttack(WeaponItem weapon, PlayerManager player)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        player.playerWeaponSlotManager.attackingWeapon = weapon;

        if (player.inputHandler.twoHandFlag)
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_light_attack_01, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_01;
        }
        else
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_light_attack_01, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01;
        }
    }

    private void HandleRunningAttack(WeaponItem weapon, PlayerManager player)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;

        player.playerWeaponSlotManager.attackingWeapon = weapon;

        if (player.inputHandler.twoHandFlag)
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_running_attack_01, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.th_running_attack_01;
        }
        else
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_running_attack_01, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_running_attack_01;
        }
    }

    private void HandleLightWeaponCombo(WeaponItem weapon, PlayerManager player)
    {

        if (player.playerStatsManager.currentStamina <= 0)
            return;

        if (player.inputHandler.comboFlag)
        {
            player.playerAnimatorManager.animator.SetBool("canDoCombo", false);

            if(player.isTwoHandingWeapon)
            {
                if (player.playerCombatManager.lastAttack == player.playerCombatManager.th_light_attack_01)
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_light_attack_02, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_02;
                }
                else
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_light_attack_01, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.th_light_attack_01;
                }
            }
            else
            {
                if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_light_attack_01)
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_light_attack_02, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_02;
                }
                else
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_light_attack_01, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_light_attack_01;
                }

            }
        }
    }

}
