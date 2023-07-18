using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterInventoryManager : MonoBehaviour
{
    protected CharacterWeaponSlotManager characterWeaponSlotManager;

    [Header("Current Item Begin Used")]
    public Item currentItemBeingUsed;

    [Header("Quick Slot Items")]
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public ConsumableItem currentConsumable;
    public RangedAmmoItem currentAmmo;

    [Header("Current Equipment")]
    public EquipmentItem currentHelmetEquipment;
    public EquipmentItem currentTorsoEquipment;
    public EquipmentItem currentLegEquipment;
    public EquipmentItem currentHandEquipment;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];

    public int currentRightWeaponIndex = 0;
    public int currentLeftWeaponIndex = 0;

    private void Awake()
    {
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
    }

    private void Start()
    {
        characterWeaponSlotManager.LoadBothWeaponsOnSlots();
    }
}
