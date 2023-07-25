using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;




    LayerMask backStabLayer = 1<< 12;
    LayerMask riposteLayer = 1<< 13;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

    }

    private void SuccessfullyCastSpell()
    {
        player.playerInventoryManager.currentSpell.SuccessfullyCastSpell
            (player.playerAnimatorManager, player.playerStatsManager, player.cameraHandler, player.playerWeaponSlotManager, player.isUsingLeftHand);
        player.animator.SetBool("isFiringSpell", true);
    }

    public void AttemptBackStabOrRiposte()
    {

        if (player.playerStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;

        // Back Stab
        if(Physics.Raycast(player.inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = player.playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null)
            {
                // 팀 ID 체크 (적 한테만 할 수 있게)
                player.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = player.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - player.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                player.transform.rotation = targetRotation;

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                player.playerAnimatorManager.PlayerTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Back Stabbed", true);
            }
        }

        // Riposte
        else if (Physics.Raycast(player.inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            // 팀 ID 체크 (적 한테만 할 수 있게)
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = player.playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                player.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = player.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - player.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(player.transform.rotation, tr, 500 * Time.deltaTime);
                player.transform.rotation = targetRotation;

                int criticalDamage = player.playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                player.playerAnimatorManager.PlayerTargetAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Riposted", true);
            }
        }
    }

    public override void DrainStaminaBasedOnAttack()
    {
        if(player.isUsingRightHand)
        {
            if(currentAttackType == AttackType.light)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.rightWeapon.baseStaminaCost * player.playerInventoryManager.rightWeapon.lightAttackStaminaMultiplier);
            }
            else if (currentAttackType == AttackType.heavy)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.rightWeapon.baseStaminaCost * player.playerInventoryManager.rightWeapon.heavyAttackStaminaMultiplier);
            }
        }
        else if (player.isUsingLeftHand)
        {
            if (currentAttackType == AttackType.light)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.leftWeapon.baseStaminaCost * player.playerInventoryManager.leftWeapon.lightAttackStaminaMultiplier);
            }
            else if (currentAttackType == AttackType.heavy)
            {
                player.playerStatsManager.DeductStamina(player.playerInventoryManager.leftWeapon.baseStaminaCost * player.playerInventoryManager.leftWeapon.heavyAttackStaminaMultiplier);
            }
        }
    }

    public override void AttemptBlock(DamageCollider attackingWeapon, float physicalDamage, float fireDamage, string blockAnimation)
    {
        base.AttemptBlock(attackingWeapon, physicalDamage, fireDamage, blockAnimation);
        player.playerStatsManager.staminaBar.SetCurrentStamina(Mathf.RoundToInt(player.playerStatsManager.currentStamina));
    }
}
