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

    [Header("Naked Armor Equipment")]
    public EquipmentItem Naked_HelmetEquipment;
    public EquipmentItem Naked_TorsoEquipment;
    public EquipmentItem Naked_LegEquipment;
    public EquipmentItem Naked_HandEquipment;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        player.characterStatsManager.CalculateAndSetMaxEquipload();

        EquipAllArmor();
        
    }

    public void EquipAllArmor()
    {
        // First All UnEquipment
        helmModelChanger     .UnEquipAllEquipmentsModels();
        chestsModelChanger   .UnEquipAllEquipmentsModels();
        gauntletsModelChanger.UnEquipAllEquipmentsModels();
        leggingsModelChanger .UnEquipAllEquipmentsModels();

        float poisonResistance = 0;
        float totalEquipmentLoad = 0;

        // Helm
        if (player.playerInventoryManager.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentHelmetEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.physicalDefense;
            poisonResistance += player.playerInventoryManager.currentHelmetEquipment.poisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHelmetEquipment.weight;
            //Debug.Log("Head Absorption is " + player.playerStatsManager.physicalDamageAbsorptionHead + "%");
        }
        else
        {
            helmModelChanger.EquipEquipmentsModelByName(Naked_HelmetEquipment.itemName);
            //helmModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentHelmetEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;
        }

        // Torso
        if (player.playerInventoryManager.currentTorsoEquipment != null)
        {
            chestsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentTorsoEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = player.playerInventoryManager.currentTorsoEquipment.physicalDefense;
            //Debug.Log("Torso Absorption is " + player.playerStatsManager.physicalDamageAbsorptionBody + "%");
            poisonResistance += player.playerInventoryManager.currentTorsoEquipment.poisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentTorsoEquipment.weight;
        }
        else
        {
            chestsModelChanger.EquipEquipmentsModelByName(Naked_TorsoEquipment.itemName);
            //chestsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentTorsoEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }

        // Legs
        if (player.playerInventoryManager.currentLegEquipment != null)
        {
            leggingsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = player.playerInventoryManager.currentLegEquipment.physicalDefense;
            //Debug.Log("Legs Absorption is " + player.playerStatsManager.physicalDamageAbsorptionLegs + "%");
            poisonResistance += player.playerInventoryManager.currentLegEquipment.poisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentLegEquipment.weight;
        }
        else
        {
            leggingsModelChanger.EquipEquipmentsModelByName(Naked_LegEquipment.itemName);
            //leggingsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }

        // Hands
        if (player.playerInventoryManager.currentHandEquipment != null)
        {
            gauntletsModelChanger.EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.physicalDefense;
            //Debug.Log("Hand Absorption is " + player.playerStatsManager.physicalDamageAbsorptionHands + "%");
            poisonResistance += player.playerInventoryManager.currentHandEquipment.poisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHandEquipment.weight;
        }
        else
        {
            gauntletsModelChanger.EquipEquipmentsModelByName(Naked_HandEquipment.itemName);
            //gauntletsModelChanger.UnEquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.itemName);
            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }

        player.playerStatsManager.poisonResistance = poisonResistance;
        player.playerStatsManager.CaculateAndSetCurrentEquipLoad(totalEquipmentLoad);
    }

}
