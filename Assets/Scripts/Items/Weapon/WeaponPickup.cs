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

        // 아이템을 픽업하지 않았다면 월드에 추가
        if (!Managers.Save.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            Managers.Save.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, false);
        }

        hasBeenLooted = Managers.Save.currentCharacterSaveData.itemsInWorld[itemPickUpID];

        if (hasBeenLooted)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        // �÷��̾ �ֿ����� �ٽ� ������ �ʿ䰡 ���ٰ� Character Data�� �����ϱ�
        if (Managers.Save.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            Managers.Save.currentCharacterSaveData.itemsInWorld.Remove(itemPickUpID);
        }

        Managers.Save.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, true);

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
        playerInventoryManager.Add(weapon);

        playerManager.m_GameUIManager.m_InteractablePopupUI.m_InteractionText.gameObject.SetActive(false);
        playerManager.m_GameUIManager.m_InteractablePopupUI.m_ItemText.gameObject.SetActive(true);
        playerManager.m_GameUIManager.m_InteractablePopupUI.m_ItemText.text = weapon.itemName;
        playerManager.m_GameUIManager.m_InteractablePopupUI.m_ItemImage.sprite = weapon.itemIcon;

        Destroy(gameObject);

    }
}
