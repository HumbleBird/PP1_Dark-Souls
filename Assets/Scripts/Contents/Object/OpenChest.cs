using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : Interactable
{
    Animator animator;
    OpenChest openChest;

    public Transform playerStandingPosing;
    public GameObject itemSpawner;
    public WeaponItem itemInChest;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        openChest = GetComponent<OpenChest>();
    }

    public override void Interact(PlayerManager playermanager)
    {
        Vector3 rotationDirection = transform.position - playermanager.transform.position;
        rotationDirection.y = 0;
        rotationDirection.Normalize();

        Quaternion tr = Quaternion.LookRotation(rotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(playermanager.transform.rotation, tr, 300 * Time.deltaTime);
        playermanager.transform.rotation = targetRotation;

        playermanager.OpenChestInteraction(playerStandingPosing);
        animator.Play("Chest Open");
        StartCoroutine(SpawnItemInChest());

        WeaponPickup weaponPickup = itemSpawner.GetComponent<WeaponPickup>();

        if (weaponPickup != null)
        {
            weaponPickup.weapon = itemInChest;
        }
    }

    private IEnumerator SpawnItemInChest()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(itemSpawner, transform);
        Destroy(openChest);
    }
}
