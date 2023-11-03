using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventColliderBeginBossFight : MonoBehaviour
{
    public AICharacterManager m_boss;
    public FogWall m_FogWall;

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager player = other.GetComponent<PlayerManager>();

        if (player != null)
        {
            if(m_boss.isDead == false)
            {
                Managers.Game.ActivateBossFight(m_FogWall, m_boss);
            }
        }
    }
}
