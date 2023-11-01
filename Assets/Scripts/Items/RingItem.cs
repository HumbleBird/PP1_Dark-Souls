using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Ring")]
public class RingItem : Item
{
    //RingItem()
    //{
    //    m_eItemType = Define.E_ItemType.Ring;
    //    effect = Managers.Resource.Load<StaticCharacterEffect>("Data/Character Effects/Modify Physical Damage");
    //}

    public float m_iWeight;

    [Header("Item Description")]
    public string m_sItemDescription;

    [SerializeField] StaticCharacterEffect effect;
    private StaticCharacterEffect effectClone;

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
