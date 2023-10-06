using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class HelmetHider : MonoBehaviour
{
    PlayerManager player;
    HelmEquipmentItem helmet;
    TorsoEquipmentItem torso;
    LeggingsEquipmentItem leggings;
    GantletsEquipmentItem gantlets;

    // Start is called before the first frame update
    void Start()
    {
        player = Managers.Object.m_MyPlayer;    
    }

    public void HideEquipment(E_CameraShowPartType type)
    {
        switch (type)
        {
            case E_CameraShowPartType.Head:
                HideHelmet();
                break;
            case E_CameraShowPartType.Chest:
                HideTorso();
                break;
            case E_CameraShowPartType.Leg:
                HideLeggings();
                break;
            case E_CameraShowPartType.Hand:
                HideGauntlet();
                break;
            case E_CameraShowPartType.All:
                {
                    HideHelmet();
                    HideTorso();
                    HideLeggings();
                    HideGauntlet();
                }
                break;
            default:
                break;
        }
    }

    public void UnHideEquipment(E_CameraShowPartType type)
    {
        switch (type)
        {
            case E_CameraShowPartType.Head:
                UnHiderHelmet();
                break;
            case E_CameraShowPartType.Chest:
                UnHiderTorso();
                break;
            case E_CameraShowPartType.Leg:
                UnHiderLeggings();
                break;
            case E_CameraShowPartType.Hand:
                UnHiderGauntlet();
                break;
            case E_CameraShowPartType.All:
                {
                    UnHiderHelmet();
                    UnHiderTorso();
                    UnHiderLeggings();
                    UnHiderGauntlet();
                }
                break;
            default:
                break;
        }
    }

    private void HideHelmet()
    {
        if (player.playerEquipmentManager.m_HelmetEquipment != null && player.playerEquipmentManager.m_HelmetEquipment != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            helmet = player.playerEquipmentManager.m_HelmetEquipment;
            player.playerEquipmentManager.m_HelmetEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderHelmet()
    {
        if(helmet != null && helmet != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            player.playerEquipmentManager.m_HelmetEquipment = helmet;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideTorso()
    {
        if (player.playerEquipmentManager.m_TorsoEquipment != null && player.playerEquipmentManager.m_TorsoEquipment != player.playerEquipmentManager.Naked_TorsoEquipment)
        {
            torso = player.playerEquipmentManager.m_TorsoEquipment;
            player.playerEquipmentManager.m_TorsoEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderTorso()
    {
        if(torso != null && torso != player.playerEquipmentManager.Naked_TorsoEquipment)
        {
            player.playerEquipmentManager.m_TorsoEquipment = torso;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideLeggings()
    {
        if (player.playerEquipmentManager.m_LegEquipment != null && player.playerEquipmentManager.m_LegEquipment != player.playerEquipmentManager.Naked_LegEquipment)
        {
            leggings = player.playerEquipmentManager.m_LegEquipment;
            player.playerEquipmentManager.m_LegEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderLeggings()
    {
        if(leggings != null && leggings != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            player.playerEquipmentManager.m_LegEquipment = leggings;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideGauntlet()
    {
        if (player.playerEquipmentManager.m_HandEquipment != null && player.playerEquipmentManager.m_HandEquipment != player.playerEquipmentManager.Naked_HandEquipment)
        {
            gantlets = player.playerEquipmentManager.m_HandEquipment;
            player.playerEquipmentManager.m_HandEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderGauntlet()
    {
        if(gantlets != null && helmet != player.playerEquipmentManager.Naked_HandEquipment)
        {
            player.playerEquipmentManager.m_HandEquipment = gantlets;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }


}
