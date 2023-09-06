using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

public class ModelChanger : MonoBehaviour
{
    public EquipmentArmorParts equipmentArmorParts;
    protected List<GameObject> equipments = new List<GameObject>();
    protected PlayerManager m_playerManager;

    private void Awake()
    {
        m_playerManager = GetComponentInParent<PlayerManager>();

        GetAllEquipmentsModels();

        m_playerManager.playerEquipmentManager.m_dicModelChanger.Add(equipmentArmorParts, this);
    }

    private void GetAllEquipmentsModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            equipments.Add(transform.GetChild(i).gameObject);
        }

    }

    public void UnEquipAllEquipmentsModels()
    {
        foreach (GameObject equipment in equipments)
        {
            equipment.SetActive(false);
        }
    }

    public void EquipEquipmentsModelByName(string torsoMName)
    {
        foreach (GameObject equipment in equipments)
        {
            if (equipment.name == torsoMName)
            {
                equipment.SetActive(true);
            }
        }
    }

    public void UnEquipEquipmentsModelByName(string torsoMName)
    {
        foreach (GameObject equipment in equipments)
        {
            if (equipment.name == torsoMName)
            {
                equipment.SetActive(false);
            }
        }
    }
}
