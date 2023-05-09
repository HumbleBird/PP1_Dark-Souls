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
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        AnimatorHandler animatorHandler;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

        playerLocomotion.rigidbody.velocity = Vector3.zero;
        animatorHandler.PlayerTargetAnimation("Pick Up Item", true);
        playerInventory.weaponsInventory.Add(weapon);

        playerManager.itemInteractableUIGameObject.SetActive(true);
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.itemName;
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<Image>().sprite = weapon.itemIcon; 
        // TODO UI Bind로 고치기

        Destroy(gameObject);

    }
}
