using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerInventory playerInventory;
    PlayerStatus playerStatus;

    [Header("Equipment Model Changers")]
    public ModelChanger helmModelChanger;
    public ModelChanger chestsModelChanger;
    public ModelChanger gauntletsModelChanger;
    public ModelChanger leggingsModelChanger;

    public BlockingCollider blockingCollider;

    private void Awake()
    {
        inputHandler = GetComponentInParent<InputHandler>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        playerStatus = GetComponentInParent<PlayerStatus>();

    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    private void EquipAllEquipmentModelsOnStart()
    {
        // Helm
        if(playerInventory.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
            playerStatus.physicalDamageAbsorptionHead = playerInventory.currentHelmetEquipment.physicalDefense;
            Debug.Log("Head Absorption is " + playerStatus.physicalDamageAbsorptionHead + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            helmModelChanger.UnEquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
            playerStatus.physicalDamageAbsorptionHead = 0;
        }

        // Torso
        if (playerInventory.currentTorsoEquipment != null)
        {
            chestsModelChanger.EquipEquipmentsModelByName(playerInventory.currentTorsoEquipment.itemName);
            playerStatus.physicalDamageAbsorptionBody = playerInventory.currentTorsoEquipment.physicalDefense;
            Debug.Log("Torso Absorption is " + playerStatus.physicalDamageAbsorptionBody + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            chestsModelChanger.UnEquipEquipmentsModelByName(playerInventory.currentTorsoEquipment.itemName);
            playerStatus.physicalDamageAbsorptionBody = 0;
        }

        // Legs
        if (playerInventory.currentLegEquipment != null)
        {
            leggingsModelChanger.EquipEquipmentsModelByName(playerInventory.currentLegEquipment.itemName);
            playerStatus.physicalDamageAbsorptionLegs = playerInventory.currentLegEquipment.physicalDefense;
            Debug.Log("Legs Absorption is " + playerStatus.physicalDamageAbsorptionLegs + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            leggingsModelChanger.UnEquipEquipmentsModelByName(playerInventory.currentLegEquipment.itemName);
            playerStatus.physicalDamageAbsorptionLegs = 0;
        }

        // Hands
        if (playerInventory.currentHandEquipment != null)
        {
            gauntletsModelChanger.EquipEquipmentsModelByName(playerInventory.currentHandEquipment.itemName);
            playerStatus.physicalDamageAbsorptionHands = playerInventory.currentHandEquipment.physicalDefense;
            Debug.Log("Hand Absorption is " + playerStatus.physicalDamageAbsorptionHands + "%");
        }
        else
        {
            //helmModelChanger.EquipEquipmentsModelByName(Naked_Helm);
            gauntletsModelChanger.UnEquipEquipmentsModelByName(playerInventory.currentHandEquipment.itemName);
            playerStatus.physicalDamageAbsorptionHands = 0;
        }
    }

    public void OpenBlockingCollider()
    {
        if(inputHandler.twoHandFlag)
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
        }
        else
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
        }

        blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider()
    {
        blockingCollider.DisableBlockingCollider();
    }


}
