using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : Interactable
{
    [Header("World Item ID")]
    [SerializeField] int itemPickUpID;
    [SerializeField] bool hasBeenLooted;

    [Header("Item")]
    public WeaponItem weapon;

    protected override void Awake()
    {
        base.Awake();

    }


    protected override void Start()
    {
        base.Start();

        // ���̺� �����Ϳ� �� ������ ������ ���ٸ�, ���� ���� ���� ��. not loot���� ����
        if (!WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, false);
        }

        hasBeenLooted = WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld[itemPickUpID];

        if (hasBeenLooted)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        // �÷��̾ �ֿ����� �ٽ� ������ �ʿ䰡 ���ٰ� Character Data�� �����ϱ�
        if (WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.Remove(itemPickUpID);
        }

        WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, true);

        hasBeenLooted = true;

        // �κ��丮�� ����ֱ�
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

        playerLocomotionManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerAnimatorManager.PlayTargetAnimation("Pick Up Item", true);
        playerInventoryManager.weaponsInventory.Add(weapon);

        playerManager.itemInteractableUIGameObject.SetActive(true);
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.itemName;
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<Image>().sprite = weapon.itemIcon; 
        // TODO UI Bind�� ��ġ��

        Destroy(gameObject);

    }
}
