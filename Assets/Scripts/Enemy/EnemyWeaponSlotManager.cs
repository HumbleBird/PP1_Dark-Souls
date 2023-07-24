using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
{
    EnemyManager enemy;


    public override void GrantWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.totalPoiseDefence + character.characterStatsManager.offensivePoiseBonus;
    }


    public override void ResetWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.totalPoiseDefence = character.characterStatsManager.armorPoiseBonus;
    }

    //TEMP

    //public override void OpenDamageCollider()
    //{
    //    //character.characterSoundFXManager.PlayRandomWeaponWhoosh();

    //    character.isUsingRightHand = true;

    //    if (character.isUsingRightHand)
    //    {
    //        rightHandDamageCollider.EnableDamageCollider();
    //    }
    //    else if (character.isUsingLeftHand)
    //    {
    //        leftHandDamageCollider.EnableDamageCollider();
    //    }
    //}
}
