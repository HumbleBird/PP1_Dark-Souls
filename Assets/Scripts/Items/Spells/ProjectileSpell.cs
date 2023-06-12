using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
    public float baseDamage;
    public float projectileVelocity;
    Rigidbody rigidBody;

    public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatus playerStatus, WeaponSlotManager weaponSlotMAnager)
    {
        base.AttemptToCastSpell(animatorHandler, playerStatus, weaponSlotMAnager);

        GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, weaponSlotMAnager.rightHandSlot.transform);
        instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
        animatorHandler.PlayerTargetAnimation(spellAnimation, true);
    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatus playerStatus)
    {
        base.SuccessfullyCastSpell(animatorHandler, playerStatus);
    }
}
