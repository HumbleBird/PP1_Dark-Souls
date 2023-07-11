using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Heavy Attack Action")]
public class HeavyAttackAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.playerStatsManager.currentStamina <= 0)
            return;


        player.playerAnimatorManager.EraseHandIKForWeapon();
        player.playerEffectsManager.PlayWeaponFX(false);

        // 만약 running attack을 수행할 수 있다면 할지 안 할지 결정 가능
        if (player.isSprinting)
        {
            HandleJumpingAttack(player);
            return;
        }

        // 콤보
        if (player.canDoCombo)
        {
            player.inputHandler.comboFlag = true;
            HandleHeavyWeaponCombo(player);
            player.inputHandler.comboFlag = false;
        }

        // 콤보 말고 정해진 공격 (1타)
        else
        {
            if (player.isInteracting)
                return;

            if (player.canDoCombo)
                return;

            HandleHeavyAttack(player);
        }

    }

    private void HandleHeavyAttack(PlayerManager player)
    {
        if (player.isUsingLeftHand)
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true, false, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
        }
        else if (player.isUsingRightHand)
        {
            if (player.inputHandler.twoHandFlag)
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_heavy_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_01;
            }
            else
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
            }
        }
    }

    private void HandleJumpingAttack(PlayerManager player)
    {
        if (player.isUsingLeftHand)
        {
            player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_jumping_attack_01, true, false, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.oh_jumping_attack_01;
        }
        else if (player.isUsingRightHand)
        {
            if (player.inputHandler.twoHandFlag)
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_jumping_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.th_jumping_attack_01;
            }
            else
            {
                player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_jumping_attack_01, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.oh_jumping_attack_01;
            }
        }


    }

    private void HandleHeavyWeaponCombo(PlayerManager player)
    {
        if (player.inputHandler.comboFlag)
        {
            player.playerAnimatorManager.animator.SetBool("canDoCombo", false);

            if (player.isUsingLeftHand)
            {
                if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_heavy_attack_01)
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_02, true, false, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_02;
                }
                else
                {
                    player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true, false, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
                }
            }

            else if (player.isUsingRightHand)
            {
                if (player.isTwoHandingWeapon)
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.th_heavy_attack_01)
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_heavy_attack_02, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_02;
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.th_heavy_attack_01, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.th_heavy_attack_01;
                    }
                }
                else
                {
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.oh_heavy_attack_01)
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_02, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_02;
                    }
                    else
                    {
                        player.playerAnimatorManager.PlayerTargetAnimation(player.playerCombatManager.oh_heavy_attack_01, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.oh_heavy_attack_01;
                    }
                }
            }
        }
    }
}
