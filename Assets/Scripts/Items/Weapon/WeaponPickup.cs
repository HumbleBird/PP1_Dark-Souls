using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : Interactable
{
    public WeaponItem weapon;

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        PickUpItem(playermanager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventoryManager playerInventoryManager;
        PlayerLocomotionManager playerLocomotionManager;
        PlayerAnimatorManager playerAnimatorManager;

        playerInventoryManager = playerManager.GetComponent<PlayerInventoryManager>();
        playerLocomotionManager = playerManager.GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

        playerLocomotionManager.rigidbody.velocity = Vector3.zero;
        playerAnimatorManager.PlayTargetAnimation("Pick Up Item", true);
        playerInventoryManager.weaponsInventory.Add(weapon);

        playerManager.itemInteractableUIGameObject.SetActive(true);
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.itemName;
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<Image>().sprite = weapon.itemIcon; 
        // TODO UI Bind로 고치기

        Destroy(gameObject);

    }
}
