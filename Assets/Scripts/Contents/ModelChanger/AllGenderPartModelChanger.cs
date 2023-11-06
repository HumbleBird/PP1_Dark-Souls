using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class AllGenderPartModelChanger : ModelChangerManager
{
    public All_GenderItemPartsType m_EAll_GenderItemPartsType = All_GenderItemPartsType.HeadCoverings_Base_Hair;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        m_playerManager.playerEquipmentManager.m_AllGenderPartsModelChanger.Add(m_EAll_GenderItemPartsType, this);

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
