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

        // 세이브 데이터에 이 아이템 정보가 없다면, 아직 줍지 않은 것. not loot으로 저장
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

        // 플레이어가 주웠으니 다시 스폰할 필요가 없다고 Character Data에 공지하기
        if (WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.Remove(itemPickUpID);
        }

        WorldSaveGameManager.instance.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, true);

        hasBeenLooted = true;

        // 인벤토리에 집어넣기
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
