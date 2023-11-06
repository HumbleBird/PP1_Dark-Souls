using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    Collider m_Collider;

    public void Start()
    {
        m_Collider = GetComponent<Collider>();
        Managers.Game.m_DicAreas.Add(name, this);
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerManager player = other.GetComponent<PlayerManager>();
        if(player != null)
        {
            // UI로 화면 뛰우기
            Managers.GameUI.m_GameSceneUI.m_AreaUI.ShowNewAreaName(name);
        }
    }

    public void Clear()
    {
        m_Collider.enabled = false;
        m_Collider.enabled = true;
    }
}
