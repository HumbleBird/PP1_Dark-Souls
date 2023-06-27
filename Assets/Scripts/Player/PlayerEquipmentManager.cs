using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerInventoryManager playerInventoryManager;
    PlayerStatsManager playerStatsManager;

    [Header("Equipment Model Changers")]
    public ModelChanger helmModelChanger;
    public ModelChanger chestsModelChanger;
    public ModelChanger gauntletsModelChanger;
    public ModelChanger leggingsModelChanger;

    public BlockingCollider blockingCollider;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();

    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    private void EquipAllEquipmentModelsOnStart()
    {
        // Helm
        if(playerInventoryManager.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventoryManager.currentHelmetEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.physicalDefense;
            //Debug.Log("Head Absorption is " + playerStatsManager.physicalDamageAbsorptionHead + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            helmModelChanger.UnEquipEquipmentsModelByName(playerInventoryManager.currentHelmetEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionHead = 0;
        }

        // Torso
        if (playerInventoryManager.currentTorsoEquipment != null)
        {
            chestsModelChanger.EquipEquipmentsModelByName(playerInventoryManager.currentTorsoEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.physicalDefense;
            //Debug.Log("Torso Absorption is " + playerStatsManager.physicalDamageAbsorptionBody + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            chestsModelChanger.UnEquipEquipmentsModelByName(playerInventoryManager.currentTorsoEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionBody = 0;
        }

        // Legs
        if (playerInventoryManager.currentLegEquipment != null)
        {
            leggingsModelChanger.EquipEquipmentsModelByName(playerInventoryManager.currentLegEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.physicalDefense;
            //Debug.Log("Legs Absorption is " + playerStatsManager.physicalDamageAbsorptionLegs + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            leggingsModelChanger.UnEquipEquipmentsModelByName(playerInventoryManager.currentLegEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }

        // Hands
        if (playerInventoryManager.currentHandEquipment != null)
        {
            gauntletsModelChanger.EquipEquipmentsModelByName(playerInventoryManager.currentHandEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.physicalDefense;
            //Debug.Log("Hand Absorption is " + playerStatsManager.physicalDamageAbsorptionHands + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            gauntletsModelChanger.UnEquipEquipmentsModelByName(playerInventoryManager.currentHandEquipment.itemName);
            playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    public void OpenBlockingCollider()
    {
        if(inputHandler.twoHandFlag)
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
        }
        else
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.leftWeapon);
        }

        blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider()
    {
        blockingCollider.DisableBlockingCollider();
    }


}
