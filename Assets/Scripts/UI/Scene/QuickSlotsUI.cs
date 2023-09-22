using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : MonoBehaviour
{
    public Image currentSpellIcon;
    public Image currentConsumableIcon;
    public Image rightWeaponIcon;
    public Image leftWeaponIcon;

    public void UpdateWeaponQuickSlotUI(bool isLeft, WeaponItem weapon)
    {
        if(isLeft)
        {
            if(weapon != null)
            {
                leftWeaponIcon.sprite = weapon.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = false;
            }

        }
        else
        {
            if (weapon != null)
            {
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;
            }
            else
            {
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = false;
            }
        }
    }

    public void UpdateCurrentSpellIcon(SpellItem spell)
    {
        if(spell.itemIcon != null)
        {
            currentSpellIcon.sprite = spell.itemIcon;
            currentSpellIcon.enabled = true;
        }
        else
        {
            currentSpellIcon.sprite = null;
            currentSpellIcon.enabled = false;
        }
    }

    public void UpdateCurrentConsumableIcon(ConsumableItem consumable)
    {
        if(consumable.itemIcon != null)
        {
            currentConsumableIcon.sprite = consumable.itemIcon;
            currentConsumableIcon.enabled = true;
        }
        else
        {
            currentConsumableIcon.sprite = null;
            currentConsumableIcon.enabled = false;
        }
    }

    public void UpdateAllQuickSlotUI()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;

        UpdateWeaponQuickSlotUI(true,  player.playerInventoryManager.rightWeapon);
        UpdateWeaponQuickSlotUI(false, player.playerInventoryManager.leftWeapon);

        if (player.playerInventoryManager.currentSpell != null)
        {
            UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
        }

        if (player.playerInventoryManager.currentConsumable != null)
        {
            UpdateCurrentConsumableIcon(player.playerInventoryManager.currentConsumable);
        }
    }
}
