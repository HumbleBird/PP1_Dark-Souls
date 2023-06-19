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
        PlayerAnimatorManager animatorHandler, 
        PlayerStatus playerStatus, 
        WeaponSlotManager weaponSlotMAnager)
    {
        base.AttemptToCastSpell(animatorHandler, playerStatus, weaponSlotMAnager);

        GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, weaponSlotMAnager.rightHandSlot.transform);
        instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
        animatorHandler.PlayerTargetAnimation(spellAnimation, true);
    }

    public override void SuccessfullyCastSpell(
        PlayerAnimatorManager animatorHandler, 
        PlayerStatus playerStatus, 
        CameraHandler cameraHandler,
        WeaponSlotManager weaponSlotManager
        )
    {
        base.SuccessfullyCastSpell(animatorHandler, playerStatus, cameraHandler, weaponSlotManager);
        GameObject instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTranform.rotation);
        rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();
        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();

        if(cameraHandler.m_trCurrentLockOnTarget != null)
        {
            instantiatedSpellFX.transform.LookAt(cameraHandler.m_trCurrentLockOnTarget.transform);
        }
        else
        {
            instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTranform.eulerAngles.x, playerStatus.transform.eulerAngles.y, 0);
        }

        rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
        rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
        rigidBody.useGravity = isEffectedByGravity;
        rigidBody.mass = projectileMass;
        instantiatedSpellFX.transform.parent = null;
    }
}
