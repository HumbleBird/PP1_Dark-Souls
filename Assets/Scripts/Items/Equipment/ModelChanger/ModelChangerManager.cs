using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Util;

public class ModelChangerManager : MonoBehaviour
{
    // 모델 체인저 유형은 3개 (All_Gender, Female, Male)
    // 각 모델 체인지에는 파츠 구분과, 아이템 리스트들이 있음
    // 만약 플레이어가 아이템을 바꾼다면, 어떠한 유형인지, 어떠한 파츠인지, 어떤 아이템인지를 구분해야 함.

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
