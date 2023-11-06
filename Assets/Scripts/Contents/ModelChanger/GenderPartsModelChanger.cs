using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GenderPartsModelChanger : ModelChangerManager
{
    public E_SingleGenderEquipmentArmorParts m_EEquipmentArmorParts;
    public bool m_bisFemalePart = true;

    protected override void Awake()
    {
        base.Awake();


    }

    private void Start()
    {
        // Bind
        if (m_bisFemalePart)
        {
            m_playerManager.playerEquipmentManager.m_FemaleGenderPartsModelChanger.Add(m_EEquipmentArmorParts, this);

        }
        else
        {
            m_playerManager.playerEquipmentManager.m_MaleGenderPartsModelChanger.Add(m_EEquipmentArmorParts, this);

        }

    }

    protected override void FindItemChild()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            equipments.Add(transform.GetChild(i).gameObject);
        }
    }
}
