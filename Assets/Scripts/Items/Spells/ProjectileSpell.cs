using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
    [Header("Projectile Damage")]
    public float baseDamage;

    [Header("Projectile Physics")]
    public float projectileForwardVelocity;
    public float projectileUpwardVelocity;
    public float projectileMass;
    public bool isEffectedByGravity;
    Rigidbody rigidBody;

    public override void AttemptToCastSpell(
        PlayerAnimatorManager playerAnimatorManager, 
        PlayerStatsManager playerStatsManager, 
        PlayerWeaponSlotManager weaponSlotMAnager,
        bool isLeftHaned)
    {
        base.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, weaponSlotMAnager, isLeftHaned);

        if (isLeftHaned)
        {
            GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, weaponSlotMAnager.leftHandSlot.transform);
            instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            playerAnimatorManager.PlayerTargetAnimation(spellAnimation, true, false, isLeftHaned);
        }
        else
        {
            GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, weaponSlotMAnager.rightHandSlot.transform);
            instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            playerAnimatorManager.PlayerTargetAnimation(spellAnimation, true, false, isLeftHaned);
        }

    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager playerAnimatorManager, 
        PlayerStatsManager playerStatsManager, 
        CameraHandler cameraHandler,
        PlayerWeaponSlotManager playerWeaponSlotManager,
        bool isLeftHaned)
    {
        base.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager, isLeftHaned);


        if (isLeftHaned)
        {
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerWeaponSlotManager.leftHandSlot.transform.position, cameraHandler.cameraPivotTranform.rotation);
            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (cameraHandler.m_trCurrentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(cameraHandler.m_trCurrentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTranform.eulerAngles.x, playerStatsManager.transform.eulerAngles.y, 0);
            }

            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
            rigidBody.useGravity = isEffectedByGravity;
            rigidBody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;

        }
        else
        {
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerWeaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTranform.rotation);
            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (cameraHandler.m_trCurrentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(cameraHandler.m_trCurrentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTranform.eulerAngles.x, playerStatsManager.transform.eulerAngles.y, 0);
            }

            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
            rigidBody.useGravity = isEffectedByGravity;
            rigidBody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }

        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
    }
}
