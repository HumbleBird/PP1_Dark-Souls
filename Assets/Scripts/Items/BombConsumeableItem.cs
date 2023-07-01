using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Bomb Item")]
public class BombConsumeableItem : ConsumableItem
{
    [Header("Velocity")]
    public int upwardVelocity = 50;
    public int forwardVelocity = 50;
    public int bombMass = 1;

    [Header("Live Bomb Model")]
    public GameObject liveBombModel;

    [Header("Base Damage")]
    public int baseDamage = 200;
    public int explosiveDamage = 75;

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager playerWeaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        if(currentItemAmount > 0 )
        {
            playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
            playerAnimatorManager.PlayerTargetAnimation(consumeAnimation, true);
            GameObject bombModel = Instantiate(itemModel, playerWeaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, playerWeaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.instantiatedFXModel = bombModel;
        }
        else
        {
            playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
        }
    }
}
