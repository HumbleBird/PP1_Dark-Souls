using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Ring")]
public class RingItem : Item
{
    public RingItem(int id)
    {

        Table_Item_Ring.Info data = Managers.Table.m_Item_Ring.Get(id);

        if (data == null)
            return;

        m_iItemID= data.m_nID;
        m_ItemName= data.m_sName               ;
        m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);
        m_iWeight = data.m_fWeight             ;
        m_sItemDescription = data.m_sItem_Description;
    }

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
