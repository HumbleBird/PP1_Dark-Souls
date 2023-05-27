using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    PlayerStatus playerStatus;
    PlayerManager playerManager;
    PlayerInventory playerInventory;
    InputHandler inputHandler;
    WeaponSlotManager weaponSlotManager;
    public string lastAttack;

    LayerMask backStabLayer = 1<< 12;

    private void Awake()
    {
        animatorHandler = GetComponent<AnimatorHandler>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();

        playerStatus = GetComponentInParent<PlayerStatus>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if(inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);
            if (lastAttack == weapon.oh_light_attack_01)
            {
                animatorHandler.PlayerTargetAnimation(weapon.oh_light_attack_02, true);
            }
            else if (lastAttack == weapon.th_light_attack_01)
            {
                animatorHandler.PlayerTargetAnimation(weapon.th_light_attack_02, true);
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        weaponSlotManager.attackingWeapon = weapon;

        if (inputHandler.twoHandFlag)
        {
            animatorHandler.PlayerTargetAnimation(weapon.th_light_attack_01, true);
            lastAttack = weapon.th_light_attack_01;
        }
        else
        {
            animatorHandler.PlayerTargetAnimation(weapon.oh_light_attack_01, true);
            lastAttack = weapon.oh_light_attack_01;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        weaponSlotManager.attackingWeapon = weapon;

        if (inputHandler.twoHandFlag)
        {

        }
        else
        {
            animatorHandler.PlayerTargetAnimation(weapon.oh_heavy_attack_01, true);
            lastAttack = weapon.oh_heavy_attack_01;
        }
    }

    #region Input Actions
    public void HandleRBAction()
    {
        if(playerInventory.rightWeapon.isMeleeWeapon)
        {
            PerformRBMellAction();
        }
        else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster  || playerInventory.rightWeapon.isPyroCaster )
        {
            PerformRBMagicAction(playerInventory.rightWeapon);
        }


    }
    #endregion

    #region Attack Actions
    private void PerformRBMellAction()
    {
        if (playerManager.canDoCombo)
        {
            inputHandler.comboFlag = true;
            HandleWeaponCombo(playerInventory.rightWeapon);
            inputHandler.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.canDoCombo)
                return;

            animatorHandler.anim.SetBool("isUsingRightHand", true);
            HandleLightAttack(playerInventory.rightWeapon);
        }
    }

    private void PerformRBMagicAction(WeaponItem weapon)
    {
        if (playerManager.isInteracting)
            return;

        if (weapon.isFaithCaster)
        {
            if(playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
            {
                // CHECK FOR FP
                if (playerStatus.currentFocusPoints >= playerInventory.currentSpell.focusPointCost)
                {
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStatus);
                }
                else
                    animatorHandler.PlayerTargetAnimation("Shrug", true);
            }
        }
    }

    private void SuccessfullyCastSpell()
    {
        playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStatus);
    }

    #endregion


    public void AttemptBackStabOrRiposte()
    {
        RaycastHit hit;

        if(Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();

            if(enemyCharacterManager != null)
            {
                playerManager.transform.position = enemyCharacterManager.backStabCollider.backStabberStandPoint.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                animatorHandler.PlayerTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayerTargetAnimation("Back Stabbed", true);
            }
        }
    }
}
