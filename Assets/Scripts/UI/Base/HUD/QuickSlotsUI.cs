using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : UI_Base
{
    enum Images
    {
        ConsumableItemSlotImage,
        SpellSlotImage,
        RightHandSlotImage,
        LeftHandSlotImage,

        ConsumableItemPlateImage,
        SpellPlateImage,
        RightHandPlateImage,
        LeftHandPlateImage
    }

    PlayerManager player;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        player = Managers.Object.m_MyPlayer;

        return true;
    }

    public void Start()
    {
        player = Managers.Object.m_MyPlayer;
        RefreshUI();
    }

    public override void RefreshUI()
    {


        // Spell Slot
        if(player.playerEquipmentManager.m_CurrentHandSpell != null)
        {
            GetImage((int)Images.SpellSlotImage).sprite = player.playerEquipmentManager.m_CurrentHandSpell.itemIcon;
            GetImage((int)Images.SpellSlotImage).enabled = true;
            GetImage((int)Images.SpellPlateImage).enabled = true;
        }
        else
        {
            GetImage((int)Images.SpellSlotImage).sprite = null;
            GetImage((int)Images.SpellSlotImage).enabled = false;
            GetImage((int)Images.SpellPlateImage).enabled = false;
        }

        // Consumable Item Slot (Down Slot)
        if(player.playerEquipmentManager.m_CurrentHandConsumable != null)
        {
            GetImage((int)Images.ConsumableItemSlotImage).sprite = player.playerEquipmentManager.m_CurrentHandConsumable.itemIcon;
            GetImage((int)Images.ConsumableItemSlotImage).enabled = true;
            GetImage((int)Images.ConsumableItemPlateImage).enabled = true;
        }
        else
        {
            GetImage((int)Images.ConsumableItemSlotImage).sprite = null;
            GetImage((int)Images.ConsumableItemSlotImage).enabled = false;
            GetImage((int)Images.ConsumableItemPlateImage).enabled = false;
        }

        // Right Hand Slot (Right Slot)
        if(player.playerEquipmentManager.m_CurrentHandRightWeapon != null)
        {
            GetImage((int)Images.RightHandSlotImage).sprite = player.playerEquipmentManager.m_CurrentHandRightWeapon.itemIcon;
            GetImage((int)Images.RightHandSlotImage).enabled = true;
            GetImage((int)Images.RightHandPlateImage).enabled = true;
        }
        else
        {
            GetImage((int)Images.RightHandSlotImage).sprite = null;
            GetImage((int)Images.RightHandSlotImage).enabled = false;
            GetImage((int)Images.RightHandPlateImage).enabled = false;
        }

        // Left Hand Slot (Left Slot)
        if (player.playerEquipmentManager.m_CurrentHandLeftWeapon != null)
        {
            GetImage((int)Images.LeftHandSlotImage).sprite = player.playerEquipmentManager.m_CurrentHandLeftWeapon.itemIcon;
            GetImage((int)Images.LeftHandSlotImage).enabled = true;
            GetImage((int)Images.LeftHandPlateImage).enabled = true;
        }
        else
        {
            GetImage((int)Images.LeftHandSlotImage).sprite = null;
            GetImage((int)Images.LeftHandSlotImage).enabled = false;
            GetImage((int)Images.LeftHandPlateImage).enabled = false;
        }

    }
}
