using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    PlayerWeaponSlotManager playerWeaponSlotManager;

    [Header("Quick Slot Items")]
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public ConsumableItem currentConsumable;

    [Header("Current Equipment")]
    public EquipmentItem currentHelmetEquipment;
    public EquipmentItem currentTorsoEquipment;
    public EquipmentItem currentLegEquipment;
    public EquipmentItem currentHandEquipment;


    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];

    public int currentRightWeaponIndex = -1;
    public int currentLeftWeaponIndex = -1;

    public List<WeaponItem> weaponsInventory;

    private void Awake()
    {
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
    }

    private void Start()
    {
        rightWeapon = weaponsInRightHandSlots[0];
        leftWeapon = weaponsInLeftHandSlots[0];
        playerWeaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        playerWeaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
                break;
            }
            else if (currentRightWeaponIndex == index && weaponsInRightHandSlots[index] == null)
            {
                currentRightWeaponIndex += 1;
            }
        }

        if(currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            currentRightWeaponIndex = -1;
            rightWeapon = playerWeaponSlotManager.unarmWeapon;
            playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmWeapon, false);

        }
    }

    public void ChangeLeftWeapon()
    {
        currentLeftWeaponIndex += 1;

        for (int index = 0; index < 3; index++)
        {
            if (currentLeftWeaponIndex == index && weaponsInLeftHandSlots[index] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
                break;
            }
            else if (currentLeftWeaponIndex == index && weaponsInLeftHandSlots[index] == null)
            {
                currentLeftWeaponIndex += 1;
            }
        }

        if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            currentLeftWeaponIndex = -1;
            leftWeapon = playerWeaponSlotManager.unarmWeapon;
            playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmWeapon, true);
        }
    }
}
