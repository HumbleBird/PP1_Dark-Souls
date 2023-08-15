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

    // 링 아이템을 장착했을 떄, 링 효과를 캐릭터에 추가
    public void EquipRing(CharacterManager character)
    {
        effectClone = Instantiate(effect);

        character.characterEffectsManager.AddStaticEffect(effectClone);
    }

    // 링 아이템을 장착 해제 할 떄, 링 효과를 캐릭터에 제거
    public void UnEquipRing(CharacterManager character)
    {
        character.characterEffectsManager.RemoveStaticEffect(effect.effectID);
    }


}
