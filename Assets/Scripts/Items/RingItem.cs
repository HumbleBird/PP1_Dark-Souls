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
