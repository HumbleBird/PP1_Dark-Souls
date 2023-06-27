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
        PlayerWeaponSlotManager weaponSlotMAnager)
    {
        base.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, weaponSlotMAnager);

        GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, weaponSlotMAnager.rightHandSlot.transform);
        instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
        playerAnimatorManager.PlayerTargetAnimation(spellAnimation, true);
    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager playerAnimatorManager, 
        PlayerStatsManager playerStatsManager, 
        CameraHandler cameraHandler,
        PlayerWeaponSlotManager playerWeaponSlotManager
        )
    {
        base.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager);
        GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerWeaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTranform.rotation);
        rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();
        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();

        if(cameraHandler.m_trCurrentLockOnTarget != null)
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
}
