using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
    //public override void AttemptToCastSpell(CharacterManager character)
    //{
    //    if (character.isUsingLeftHand)
    //    {
    //        GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, character.characterWeaponSlotManager.leftHandSlot.transform);
    //        instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
    //        character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
    //    }
    //    else
    //    {
    //        GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, character.characterWeaponSlotManager.rightHandSlot.transform);
    //        instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
    //        character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
    //    }
    //}

    //public override void SuccessfullyCastSpell(CharacterManager character)
    //{
    //    PlayerManager player = character as PlayerManager;

    //    // player
    //    if (player != null)
    //    {
    //        if (player.isUsingLeftHand)
    //        {
    //            GameObject instantiatedSpellFX = Instantiate(spellCastFX, player.playerWeaponSlotManager.leftHandSlot.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
    //            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
    //            spellDamageCollider.teamIDNumber = player.playerStatsManager.teamIDNumber;
    //            spellDamageCollider.characterManager = character;
    //            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();

    //            if (player.cameraHandler.m_trCurrentLockOnTarget != null)
    //            {
    //                instantiatedSpellFX.transform.LookAt(player.cameraHandler.m_trCurrentLockOnTarget.transform);
    //            }
    //            else
    //            {
    //                instantiatedSpellFX.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTranform.eulerAngles.x, player.playerStatsManager.transform.eulerAngles.y, 0);
    //            }

    //            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
    //            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
    //            rigidBody.useGravity = isEffectedByGravity;
    //            rigidBody.mass = projectileMass;
    //            rigidBody.constraints = RigidbodyConstraints.None;
    //            instantiatedSpellFX.transform.parent = null;

    //        }
    //        else
    //        {
    //            GameObject instantiatedSpellFX = Instantiate(spellCastFX, player.playerWeaponSlotManager.rightHandSlot.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
    //            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
    //            spellDamageCollider.teamIDNumber = player.playerStatsManager.teamIDNumber;
    //            spellDamageCollider.characterManager = character;
    //            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();

    //            if (player.cameraHandler.m_trCurrentLockOnTarget != null)
    //            {
    //                instantiatedSpellFX.transform.LookAt(player.cameraHandler.m_trCurrentLockOnTarget.transform);
    //            }
    //            else
    //            {
    //                instantiatedSpellFX.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTranform.eulerAngles.x, player.playerStatsManager.transform.eulerAngles.y, 0);
    //            }

    //            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
    //            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
    //            rigidBody.useGravity = isEffectedByGravity;
    //            rigidBody.mass = projectileMass;
    //            rigidBody.constraints = RigidbodyConstraints.None;
    //            instantiatedSpellFX.transform.parent = null;
    //        }
    //    }

    //    // A.I
    //    else
    //    {

    //    }

        
    //}
}
