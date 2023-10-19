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

        WeaponItem parryingWeapon = character.characterEquipmentManager.currentItemBeingUsed as WeaponItem;

        if(parryingWeapon.weaponType == WeaponType.SmallShield)
        {
            character.characterAnimatorManager.PlayTargetAnimation("Parry_01", true);
        }
        else if (parryingWeapon.weaponType == WeaponType.Shield)
        {
            character.characterAnimatorManager.PlayTargetAnimation("Parry_01", true);
        }
    }
}
