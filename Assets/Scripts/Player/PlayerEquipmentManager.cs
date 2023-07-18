using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Equipment Model Changers")]
    public ModelChanger helmModelChanger;
    public ModelChanger chestsModelChanger;
    public ModelChanger gauntletsModelChanger;
    public ModelChanger leggingsModelChanger;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    private void EquipAllEquipmentModelsOnStart()
    {
        // Helm
        if(player.playerInventoryManager.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentHelmetEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.physicalDefense;
            //Debug.Log("Head Absorption is " + player.playerStatsManager.physicalDamageAbsorptionHead + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            helmModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentHelmetEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;
        }

        // Torso
        if (player.playerInventoryManager.currentTorsoEquipment != null)
        {
            chestsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentTorsoEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = player.playerInventoryManager.currentTorsoEquipment.physicalDefense;
            //Debug.Log("Torso Absorption is " + player.playerStatsManager.physicalDamageAbsorptionBody + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            chestsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentTorsoEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }

        // Legs
        if (player.playerInventoryManager.currentLegEquipment != null)
        {
            leggingsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = player.playerInventoryManager.currentLegEquipment.physicalDefense;
            //Debug.Log("Legs Absorption is " + player.playerStatsManager.physicalDamageAbsorptionLegs + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            leggingsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }

        // Hands
        if (player.playerInventoryManager.currentHandEquipment != null)
        {
            gauntletsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.physicalDefense;
            //Debug.Log("Hand Absorption is " + player.playerStatsManager.physicalDamageAbsorptionHands + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            gauntletsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    public void OpenBlockingCollider()
    {
        if(player.inputHandler.twoHandFlag)
        {
            player.blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.rightWeapon);
        }
        else
        {
            player.blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.leftWeapon);
        }

        player.blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider()
    {
        player.blockingCollider.DisableBlockingCollider();
    }


}
