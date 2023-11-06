using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class OpenChest : Interactable
{
    Animator animator;

    public Transform playerStandingPosing;

    public E_ItemType m_ItemType;
    public int m_iItemID;
    public Item itemInChest;

    bool m_isOpen = false;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    public override void Interact(PlayerManager playermanager)
    {
        if (m_isOpen)
            return;

        Vector3 rotationDirection = transform.position - playermanager.transform.position;
        rotationDirection.y = 0;
        rotationDirection.Normalize();

        Quaternion tr = Quaternion.LookRotation(rotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(playermanager.transform.rotation, tr, 300 * Time.deltaTime);
        playermanager.transform.rotation = targetRotation;

        playermanager.playerLocomotionManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playermanager.transform.position = playerStandingPosing.transform.position;
        playermanager.playerAnimatorManager.PlayTargetAnimation("Open Chest", true);

        // Animation
        animator.Play("Chest Open");

        // Sound
        Managers.Sound.Play("Object/Chest Open");

        m_isOpen = true;

        Managers.GameUI.ClosePopupUI();

        StartCoroutine(SpawnItemInChest());
    }

    private IEnumerator SpawnItemInChest()
    {
        yield return new WaitForSeconds(1f);
        GameObject item = Managers.Resource.Instantiate("Objects/Interact Object/Pick Item", transform);

        WeaponPickup weaponPickup = item.GetComponent<WeaponPickup>();

        if (weaponPickup != null)
        {
            if (itemInChest != null)
            {
                weaponPickup.item = itemInChest;
            }
            else
            {
                weaponPickup.item = Managers.Game.MakeItem(m_ItemType, m_iItemID);
            }
        }

        weaponPickup.gameObject.transform.localPosition = new Vector3(0, 1, 0);

        Destroy(GetComponent<OpenChest>());
    }
}
