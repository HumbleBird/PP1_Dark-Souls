using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Ring")]
public class RingItem : Item
{
    [SerializeField] StaticCharacterEffect effect;
    private StaticCharacterEffect effectClone;

    // For User UI
    [Header("Item Effect Description")]
    [TextArea] public string itemEffectInformation;

    // �� �������� �������� ��, �� ȿ���� ĳ���Ϳ� �߰�
    public void EquipRing(CharacterManager character)
    {
        effectClone = Instantiate(effect);

        character.characterEffectsManager.AddStaticEffect(effectClone);
    }

    // �� �������� ���� ���� �� ��, �� ȿ���� ĳ���Ϳ� ����
    public void UnEquipRing(CharacterManager character)
    {
        character.characterEffectsManager.RemoveStaticEffect(effect.effectID);
    }


}
