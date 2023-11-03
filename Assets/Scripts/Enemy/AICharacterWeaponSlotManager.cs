using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterWeaponSlotManager : CharacterWeaponSlotManager
{
    AICharacterManager aiCharacter;


    public override void GrantWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.m_fTotalPoiseDefence = character.characterStatsManager.m_fTotalPoiseDefence + character.characterStatsManager.offensivePoiseBonus;
    }


    public override void ResetWeaponAttackingPoiseBonus()
    {
        character.characterStatsManager.m_fTotalPoiseDefence = character.characterStatsManager.m_fStatPoise;
    }
}
