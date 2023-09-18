using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetHider : MonoBehaviour
{
    PlayerManager player;
    HelmEquipmentItem helmet;

    // Start is called before the first frame update
    void Start()
    {
        player = Managers.Object.m_MyPlayer;    
    }

    public void HideHelmet()
    {
        if (player.playerInventoryManager.currentHelmetEquipment != null && player.playerInventoryManager.currentHelmetEquipment != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            helmet = player.playerInventoryManager.currentHelmetEquipment;
            player.playerInventoryManager.currentHelmetEquipment = null;
            player.playerEquipmentManager.EquipAllEquipmentModel();
        }

    }

    public void UnHiderHelmet()
    {
        if(helmet != null && helmet != player.playerEquipmentManager.Naked_HelmetEquipment)
        {
            player.playerInventoryManager.currentHelmetEquipment = helmet;
            player.playerEquipmentManager.EquipAllEquipmentModel();
            //helmet = null;
        }
    }

}
