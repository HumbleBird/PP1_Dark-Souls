using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

public class ModelChanger : MonoBehaviour
{
    public EquipmentArmorParts equipmentArmorParts = EquipmentArmorParts.Helm;
    protected List<GameObject> equipments = new List<GameObject>();
    protected PlayerEquipmentManager playerEquipmentManager;

    private void Awake()
    {
        playerEquipmentManager = GetComponentInParent<PlayerEquipmentManager>();

        GetAllEquipmentsModels();

        switch (equipmentArmorParts)
        {
            case EquipmentArmorParts.Helm:
                playerEquipmentManager.helmModelChanger = this;
                break;
            case EquipmentArmorParts.Chest:
                playerEquipmentManager.chestsModelChanger = this;
                break;
            case EquipmentArmorParts.Gauntlet:
                playerEquipmentManager.gauntletsModelChanger = this;
                break;
            case EquipmentArmorParts.Legging:
                playerEquipmentManager.leggingsModelChanger = this;
                break;
            default:
                break;
        }
    }

    public virtual void SetInfo()
    {
        GetAllEquipmentsModels();
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
