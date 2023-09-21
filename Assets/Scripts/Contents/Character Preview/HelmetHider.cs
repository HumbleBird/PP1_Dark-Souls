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

    public void HideEquipment(E_ArmorEquipmentType type)
    {
        switch (type)
        {
            case E_ArmorEquipmentType.Helmet:
                HideHelmet();
                break;
            case E_ArmorEquipmentType.Torso:
                HideTorso();
                break;
            case E_ArmorEquipmentType.Leggings:
                HideLeggings();
                break;
            case E_ArmorEquipmentType.Gauntlet:
                HideGauntlet();
                break;
            case E_ArmorEquipmentType.All:
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

    public void UnHideEquipment(E_ArmorEquipmentType type)
    {
        switch (type)
        {
            case E_ArmorEquipmentType.Helmet:
                UnHiderHelmet();
                break;
            case E_ArmorEquipmentType.Torso:
                UnHiderTorso();
                break;
            case E_ArmorEquipmentType.Leggings:
                UnHiderLeggings();
                break;
            case E_ArmorEquipmentType.Gauntlet:
                UnHiderGauntlet();
                break;
            case E_ArmorEquipmentType.All:
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
        if (player.playerInventoryManager.currentHelmetEquipment != null && player.playerInventoryManager.currentHelmetEquipment != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            helmet = player.playerInventoryManager.currentHelmetEquipment;
            player.playerInventoryManager.currentHelmetEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderHelmet()
    {
        if(helmet != null && helmet != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            player.playerInventoryManager.currentHelmetEquipment = helmet;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideTorso()
    {
        if (player.playerInventoryManager.currentTorsoEquipment != null && player.playerInventoryManager.currentTorsoEquipment != player.playerEquipmentManager.Naked_TorsoEquipment)
        {
            torso = player.playerInventoryManager.currentTorsoEquipment;
            player.playerInventoryManager.currentTorsoEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderTorso()
    {
        if(torso != null && torso != player.playerEquipmentManager.Naked_TorsoEquipment)
        {
            player.playerInventoryManager.currentTorsoEquipment = torso;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideLeggings()
    {
        if (player.playerInventoryManager.currentLegEquipment != null && player.playerInventoryManager.currentLegEquipment != player.playerEquipmentManager.Naked_LegEquipment)
        {
            leggings = player.playerInventoryManager.currentLegEquipment;
            player.playerInventoryManager.currentLegEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderLeggings()
    {
        if(leggings != null && leggings != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            player.playerInventoryManager.currentLegEquipment = leggings;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }

    private void HideGauntlet()
    {
        if (player.playerInventoryManager.currentHandEquipment != null && player.playerInventoryManager.currentHandEquipment != player.playerEquipmentManager.Naked_HandEquipment)
        {
            gantlets = player.playerInventoryManager.currentHandEquipment;
            player.playerInventoryManager.currentHandEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    private void UnHiderGauntlet()
    {
        if(gantlets != null && helmet != player.playerEquipmentManager.Naked_HandEquipment)
        {
            player.playerInventoryManager.currentHandEquipment = gantlets;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }
    }


}
