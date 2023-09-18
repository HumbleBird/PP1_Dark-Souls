using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

public class ModelChangerManager : MonoBehaviour
{
    // �� ü���� ������ 3�� (All_Gender, Female, Male)
    // �� �� ü�������� ���� ���а�, ������ ����Ʈ���� ����
    // ���� �÷��̾ �������� �ٲ۴ٸ�, ��� ��������, ��� ��������, � ������������ �����ؾ� ��.

    public List<GameObject> equipments = new List<GameObject>();
    protected PlayerManager m_playerManager;

    protected virtual void Awake()
    {
        m_playerManager = GetComponentInParent<PlayerManager>();

    }

    protected virtual void Start()
    {
        FindItemChild();
    }

    protected virtual void FindItemChild()
    {

    }


    public void UnEquipAllEquipmentsModels()
    {
        foreach (GameObject equipment in equipments)
        {
            equipment.SetActive(false);
        }
    }

    public GameObject EquipEquipmentsModelByName(string torsoMName)
    {
        foreach (GameObject equipment in equipments)
        {
            if (equipment.name == torsoMName)
            {
                equipment.SetActive(true);
                return equipment;
            }

        }

        return null;
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
