using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Item Actions/Parry Action")]
public class ParryAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        if (character.isInteracting)
            return;

        character.characterAnimatorManager.EraseHandIKForWeapon();

        WeaponItem parryingWeapon = character.characterInventoryManager.currentItemBeingUsed as WeaponItem;

        if(parryingWeapon.weaponType == WeaponType.SmallShield)
        {
            character.characterAnimatorManager.PlayerTargetAnimation("Parry_01", true);
        }
        else if (parryingWeapon.weaponType == WeaponType.Shield)
        {
            character.characterAnimatorManager.PlayerTargetAnimation("Parry_01", true);
        }
    }
}
