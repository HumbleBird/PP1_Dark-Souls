using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Character Effects/static Effects/Modify Damage Type")]
public class ModifyDamageTypeStaticEffect : StaticCharacterEffect
{
    [Header("Damage Type Effected")]
    [SerializeField] int modifiedValue = 0;
    [SerializeField] Damagetype damagetype;

    // 이펙트를 추가할 때, respective damage type modifier에 modifier 수치 만큼 추가
    public override void AddStaticEffect(CharacterManager character)
    {
        base.AddStaticEffect(character);

        switch (damagetype)
        {
            case Damagetype.Physical:
                character.characterStatsManager.physicalDamagePercentageModifier += modifiedValue;
                break;
            case Damagetype.Fire:
                character.characterStatsManager.fireDamagePercentageModifier += modifiedValue;
                break;
            default:
                break;
        }
    }

    // 이펙트를 삭제할 때, 그 수치만큼 빼춘다
    public override void RemoveStaticEffect(CharacterManager character)
    {
        base.RemoveStaticEffect(character);

        switch (damagetype)
        {
            case Damagetype.Physical:
                character.characterStatsManager.physicalDamagePercentageModifier -= modifiedValue;
                break;
            case Damagetype.Fire:
                character.characterStatsManager.fireDamagePercentageModifier -= modifiedValue;
                break;
            default:
                break;
        }
    }
}
