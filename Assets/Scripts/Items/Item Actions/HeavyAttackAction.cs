using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Item Actions/Heavy Attack Action")]
public class HeavyAttackAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.characterStatsManager.currentStamina <= 0)
            return;

        character.isAttacking = true;

        character.characterAnimatorManager.EraseHandIKForWeapon();
        character.characterEffectsManager.PlayWeaponFX(false);

        // 만약 running attack을 수행할 수 있다면 할지 안 할지 결정 가능
        if (character.isSprinting)
        {
            HandleJumpingAttack(character);
            return;
        }

        // 콤보
        if (character.canDoCombo)
        {
            HandleHeavyWeaponCombo(character);
        }

        // 콤보 말고 정해진 공격 (1타)
        else
        {
            if (character.isInteracting)
                return;

            if (character.canDoCombo)
                return;

            HandleHeavyAttack(character);
        }

        character.characterCombatManager.currentAttackType = AttackType.heavy ;

    }

    private void HandleHeavyAttack(CharacterManager character)
    {
        if (character.isUsingLeftHand)
        {
            character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_01, true, false, true);
            character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_01;
        }
        else if (character.isUsingRightHand)
        {
            if (character.isTwoHandingWeapon)
            {
                character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.th_heavy_attack_01, true);
                character.characterCombatManager.lastAttack = character.characterCombatManager.th_heavy_attack_01;
            }
            else
            {
                character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_01, true);
                character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_01;
            }
        }
    }

    private void HandleJumpingAttack(CharacterManager character)
    {
        if (character.isUsingLeftHand)
        {
            character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_jumping_attack_01, true, false, true);
            character.characterCombatManager.lastAttack = character.characterCombatManager.oh_jumping_attack_01;
        }
        else if (character.isUsingRightHand)
        {
            if (character.isTwoHandingWeapon)
            {
                character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.th_jumping_attack_01, true);
                character.characterCombatManager.lastAttack = character.characterCombatManager.th_jumping_attack_01;
            }
            else
            {
                character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_jumping_attack_01, true);
                character.characterCombatManager.lastAttack = character.characterCombatManager.oh_jumping_attack_01;
            }
        }


    }

    private void HandleHeavyWeaponCombo(CharacterManager character)
    {
        if (character.canDoCombo)
        {
            character.animator.SetBool("canDoCombo", false);

            if (character.isUsingLeftHand)
            {
                if (character.characterCombatManager.lastAttack == character.characterCombatManager.oh_heavy_attack_01)
                {
                    character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_02, true, false, true);
                    character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_02;
                }
                else
                {
                    character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_01, true, false, true);
                    character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_01;
                }
            }

            else if (character.isUsingRightHand)
            {
                if (character.isTwoHandingWeapon)
                {
                    if (character.characterCombatManager.lastAttack == character.characterCombatManager.th_heavy_attack_01)
                    {
                        character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.th_heavy_attack_02, true);
                        character.characterCombatManager.lastAttack = character.characterCombatManager.th_heavy_attack_02;
                    }
                    else
                    {
                        character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.th_heavy_attack_01, true);
                        character.characterCombatManager.lastAttack = character.characterCombatManager.th_heavy_attack_01;
                    }
                }
                else
                {
                    if (character.characterCombatManager.lastAttack == character.characterCombatManager.oh_heavy_attack_01)
                    {
                        character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_02, true);
                        character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_02;
                    }
                    else
                    {
                        character.characterAnimatorManager.PlayerTargetAnimation(character.characterCombatManager.oh_heavy_attack_01, true);
                        character.characterCombatManager.lastAttack = character.characterCombatManager.oh_heavy_attack_01;
                    }
                }
            }
        }
    }
}
