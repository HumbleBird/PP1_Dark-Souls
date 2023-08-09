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

    // ����Ʈ�� �߰��� ��, respective damage type modifier�� modifier ��ġ ��ŭ �߰�
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

    // ����Ʈ�� ������ ��, �� ��ġ��ŭ �����
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
