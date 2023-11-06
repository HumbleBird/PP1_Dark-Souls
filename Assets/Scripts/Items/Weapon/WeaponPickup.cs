using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class WeaponPickup : Interactable
{
    [Header("World Item ID")]
    [SerializeField] int itemPickUpID;
    [SerializeField] bool hasBeenLooted;

    [Header("Item")]
    public Item item;
    public E_ItemType m_ItemType;
    public int m_iItemID;

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

        if (Managers.Save.currentCharacterSaveData.itemsInWorld.ContainsKey(itemPickUpID))
        {
            Managers.Save.currentCharacterSaveData.itemsInWorld.Remove(itemPickUpID);
        }

        Managers.Save.currentCharacterSaveData.itemsInWorld.Add(itemPickUpID, true);

        hasBeenLooted = true;

        PickUpItem(playermanager);

        Managers.Sound.Play("Object/Item_Get");
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        playerManager.playerLocomotionManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerManager.playerAnimatorManager.PlayTargetAnimation("Pick Up Item", true);

        if (item == null)
            item = Managers.Game.MakeItem(m_ItemType, m_iItemID);

        playerManager.playerInventoryManager.Add(item);

        Managers.GameUI.m_InteractableAnnouncementPopupUI.m_InteractionText.gameObject.SetActive(false);
        Managers.GameUI.m_InteractableAnnouncementPopupUI.m_ItemText.gameObject.SetActive(true);
        Managers.GameUI.m_InteractableAnnouncementPopupUI.m_ItemText.text = item.m_ItemName;
        Managers.GameUI.m_InteractableAnnouncementPopupUI.m_ItemImage.sprite = item.m_ItemIcon;

        Destroy(gameObject);

    }
}
