using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
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
                //player.playerAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                if (player.inputHandler.twoHandFlag)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    player.playerAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);
                }
                else
                {

                    backSlot.UnloadWeaponAndDestroy();

                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                player.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
        else
        {
            weaponItem = unarmWeapon;

            if (isLeft)
            {
                player.playerEquipmentManager.m_CurrentHandLeftWeapon = unarmWeapon;
                leftHandSlot.currentWeapon = unarmWeapon;
                leftHandSlot.LoadWeaponModel(unarmWeapon);
                LoadLeftWeaponDamageCollider();
                //player.playerAnimatorManager.PlayerTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                player.playerEquipmentManager.m_CurrentHandRightWeapon = unarmWeapon;
                rightHandSlot.currentWeapon = unarmWeapon;
                rightHandSlot.LoadWeaponModel(unarmWeapon);
                LoadRightWeaponDamageCollider();
                player.animator.runtimeAnimatorController = weaponItem.weaponController;

            }
        }

        if(player.m_GameSceneUI != null)
        {
            if(player.m_GameSceneUI.quickSlotsUI != null)
            {
                player.m_GameSceneUI.quickSlotsUI.RefreshUI();
            }
        }
    }

    public void SucessfullyThrowFireBomb()
    {
        Destroy(player.playerEffectsManager.instantiatedFXModel2);
        BombConsumeableItem fireBombItem = player.playerEquipmentManager.m_CurrentHandConsumable as BombConsumeableItem;
        GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
        activeModelBomb.GetComponentInChildren<BombDamageColider>().characterManager = player;
        activeModelBomb.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTranform.eulerAngles.x, player.lockOnTransform.eulerAngles.y, 0);
        Rigidbody rigidBody = activeModelBomb.GetComponentInChildren<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.None;

        BombDamageColider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageColider>();

        damageCollider.explosionDamage = fireBombItem.baseDamage;
        damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardVelocity);
        damageCollider.teamIDNumber = player.playerStatsManager.teamIDNumber;
        LoadWeaponOnSlot(player.playerEquipmentManager.m_CurrentHandRightWeapon, false);

    }

    public override void OpenDamageCollider()
    {
        base.OpenDamageCollider();

        player.playerCombatManager.DrainStaminaBasedOnAttack();
        player.characterSoundFXManager.PlayRandomWeaponWhoosheSound();
    }

}
