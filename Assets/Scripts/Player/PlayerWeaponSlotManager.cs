using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
{
    QuickSlotsUI quickSlotsUI;
    InputHandler inputHandler;

    PlayerManager playerManager;
    PlayerInventoryManager playerInventoryManager;
    PlayerStatsManager playerStatsManager;
    PlayerEffectsManager playerEffectsManager;
    PlayerAnimatorManager playerAnimatorManager;
    CameraHandler cameraHandler;


    protected override void Awake()
    {
        base.Awake();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();

        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerManager = GetComponent<PlayerManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
    }

    public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(weaponItem != null)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                playerAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                if (inputHandler.twoHandFlag)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    playerAnimatorManager.PlayerTargetAnimation("Left Arm Empty", false, true);
                }
                else
                {

                    backSlot.UnloadWeaponAndDestroy();

                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                playerAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }

            quickSlotsUI.UpdateWeaponQuickSlotUI(isLeft, weaponItem);
        }
        else
        {
            weaponItem = unarmWeapon;

            if (isLeft)
            {
                playerInventoryManager.leftWeapon = unarmWeapon;
                leftHandSlot.currentWeapon = unarmWeapon;
                leftHandSlot.LoadWeaponModel(unarmWeapon);
                LoadLeftWeaponDamageCollider();
                playerAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                playerInventoryManager.rightWeapon = unarmWeapon;
                rightHandSlot.currentWeapon = unarmWeapon;
                rightHandSlot.LoadWeaponModel(unarmWeapon);
                LoadRightWeaponDamageCollider();
                playerAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;

            }

            quickSlotsUI.UpdateWeaponQuickSlotUI(isLeft, unarmWeapon);
        }

    }

    public void SucessfullyThrowFireBomb()
    {
        Destroy(playerEffectsManager.instantiatedFXModel);
        BombConsumeableItem fireBombItem = playerInventoryManager.currentConsumable as BombConsumeableItem;

        GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, cameraHandler.cameraPivotTranform.rotation);
        activeModelBomb.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTranform.eulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
        BombDamageColider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageColider>();

        damageCollider.explosionDamage = fireBombItem.baseDamage;
        damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardVelocity);
        damageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
        LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);

    }

    public override void DrainStaminaLightAttack()
    {
        playerStatsManager.TakeStaminsDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
    }

    public override void DrainStaminaHeavyAttack()
    {
        playerStatsManager.TakeStaminsDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
    }

}
