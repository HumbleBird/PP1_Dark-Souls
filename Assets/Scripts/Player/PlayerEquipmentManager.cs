using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerInventory playerInventory;

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
    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    private void EquipAllEquipmentModelsOnStart()
    {
        if(playerInventory.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
        }

        if (playerInventory.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
        }

        if (playerInventory.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
        }

        if (playerInventory.currentHelmetEquipment != null)
        {
            helmModelChanger.EquipEquipmentsModelByName(playerInventory.currentHelmetEquipment.itemName);
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
