using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Item Actions/Parry Action")]
public class ParryAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        if (player.isInteracting)
            return;

        player.playerAnimatorManager.EraseHandIKForWeapon();

        WeaponItem parryingWeapon = player.playerInventoryManager.currentItemBeingUsed as WeaponItem;

        if(parryingWeapon.weaponType == WeaponType.SmallShield)
        {
            player.playerAnimatorManager.PlayerTargetAnimation("Parry_01", true);
        }
        else if (parryingWeapon.weaponType == WeaponType.Shield)
        {
            player.playerAnimatorManager.PlayerTargetAnimation("Parry_01", true);
        }
    }
}
