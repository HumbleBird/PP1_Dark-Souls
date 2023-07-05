using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerCombatManager : MonoBehaviour
{
    CameraHandler cameraHandler;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerEquipmentManager playerEquipmentManager;
    PlayerStatsManager playerStatsManager;
    PlayerManager playerManager;
    PlayerInventoryManager playerInventoryManager;
    InputHandler inputHandler;
    PlayerWeaponSlotManager playerWeaponSlotManager;
    PlayerEffectsManager playerEffectsManager;

    [Header("Attack Animations")]
    string oh_light_attack_01 = "OH_Light_Attack_01";
    string oh_light_attack_02 = "OH_Light_Attack_02";
    string oh_heavy_attack_01 = "OH_Heavy_Attack_01";
    string oh_heavy_attack_02 = "OH_Heavy_Attack_02";

    string th_light_attack_01 = "TH_Light_Attack_01";
    string th_light_attack_02 = "TH_Light_Attack_02";
    string th_heavy_attack_01 = "TH_Heavy_Attack_01";
    string th_heavy_attack_02 = "TH_Heavy_Attack_02";

    string weapon_art = "Weapon Art";

    public string lastAttack;

    LayerMask backStabLayer = 1<< 12;
    LayerMask riposteLayer = 1<< 13;

    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();

        playerStatsManager = GetComponent<PlayerStatsManager>();
        inputHandler = GetComponent<InputHandler>();
        playerManager = GetComponent<PlayerManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {

        if (playerStatsManager.currentStamina <= 0)
            return;

        if (inputHandler.comboFlag)
        {
            playerAnimatorManager.animator.SetBool("canDoCombo", false);
            if (lastAttack == oh_light_attack_01)
            {
                playerAnimatorManager.PlayerTargetAnimation(oh_light_attack_02, true);
            }
            else if (lastAttack == th_light_attack_01)
            {
                playerAnimatorManager.PlayerTargetAnimation(th_light_attack_02, true);
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (playerStatsManager.currentStamina <= 0)
            return;

        playerWeaponSlotManager.attackingWeapon = weapon;

        if (inputHandler.twoHandFlag)
        {
            playerAnimatorManager.PlayerTargetAnimation(th_light_attack_01, true);
            lastAttack = th_light_attack_01;
        }
        else
        {
            playerAnimatorManager.PlayerTargetAnimation(oh_light_attack_01, true);
            lastAttack = oh_light_attack_01;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if (playerStatsManager.currentStamina <= 0)
            return;

        playerWeaponSlotManager.attackingWeapon = weapon;

        if (inputHandler.twoHandFlag)
        {

        }
        else
        {
            //playerAnimatorManager.PlayerTargetAnimation(weapon.oh_heavy_attack_01, true);
            //lastAttack = weapon.oh_heavy_attack_01;
        }
    }

    #region Input Actions
    public void HandleRBAction()
    {
        if(playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSwords
            || playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            PerformRBMellAction();
        }
        else if (playerInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster 
            || playerInventoryManager  .rightWeapon.weaponType == WeaponType.FaithCaster  
            || playerInventoryManager.rightWeapon.weaponType == WeaponType.  PyroCaster )
        {
            PerformMagicAction(playerInventoryManager.rightWeapon, false);
        }


    }

    public void HandleLBAction()
    {

        if(playerManager.isTwoHandingWeapon)
        {
            if(playerInventoryManager.rightWeapon.weaponType == WeaponType.Bow)
            {
                PerformLBAimingAction();
            }
        }
        else
        {
            if (playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield ||
                playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSwords)
            {
                PerformLBBlockingAction();

            }
            else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster ||
                playerInventoryManager.leftWeapon.weaponType == WeaponType.PyroCaster)
            {
                PerformMagicAction(playerInventoryManager.leftWeapon, true);
                playerAnimatorManager.animator.SetBool("isUsingLeftHand", true);
            }
        }
    }

    public void HandleLTAction()
    {
        if(playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield || playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            PerformLTWeaponArt(inputHandler.twoHandFlag);
        }
        else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSwords)
        {

        }
    }
    #endregion

    #region Attack Actions
    private void PerformRBMellAction()
    {
        if (playerManager.canDoCombo)
        {
            inputHandler.comboFlag = true;
            HandleWeaponCombo(playerInventoryManager.rightWeapon);
            inputHandler.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.canDoCombo)
                return;

            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
            HandleLightAttack(playerInventoryManager.rightWeapon);
        }

        playerEffectsManager.PlayWeaponFX(false);
    }

    private void PerformLBBlockingAction()
    {
        if (playerManager.isInteracting)
            return;

        if (playerManager.isBlocking)
            return;

        playerAnimatorManager.PlayerTargetAnimation("Block Start", false, true);
        playerEquipmentManager.OpenBlockingCollider();
        playerManager.isBlocking = true;

    }

    private void PerformLBAimingAction()
    {
        playerAnimatorManager.animator.SetBool("isAiming", true);
    }

    private void PerformMagicAction(WeaponItem weapon, bool isLeftHaned)
    {
        if (playerManager.isInteracting)
            return;

        if (weapon.weaponType == WeaponType.FaithCaster)
        {
            if(playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isFaithSpell)
            {
                // CHECK FOR FP
                if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                {
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHaned);
                }
                else
                    playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
            }
        }
        else if (weapon.weaponType == WeaponType.PyroCaster)
        {
            if (playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isPyroSpell)
            {
                // CHECK FOR FP
                if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                {
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHaned);
                }
                else
                    playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
            }
        }
    }

    private void PerformLTWeaponArt(bool isTwoHanding)
    {
        if (playerManager.isInteracting)
            return;

        if (isTwoHanding)
        {

        }
        else
        {
            playerAnimatorManager.PlayerTargetAnimation(weapon_art, true);


        }
    }

    private void SuccessfullyCastSpell()
    {
        playerInventoryManager.currentSpell.SuccessfullyCastSpell
            (playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager, playerManager.isUsingLeftHand);
        playerAnimatorManager.animator.SetBool("isFiringSpell", true);
    }

    #endregion


    public void AttemptBackStabOrRiposte()
    {

        if (playerStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;

        // Back Stab
        if(Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null)
            {
                // 팀 ID 체크 (적 한테만 할 수 있게)
                playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayerTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Back Stabbed", true);
            }
        }

        // Riposte
        else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            // 팀 ID 체크 (적 한테만 할 수 있게)
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayerTargetAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Riposted", true);
            }
        }
    }
}
