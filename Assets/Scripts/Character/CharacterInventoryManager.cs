using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterInventoryManager : MonoBehaviour
{
    protected CharacterManager character;

    [Header("Current Item Begin Used")]
    public Item currentItemBeingUsed;

    [Header("Quick Slot Items")]
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public ConsumableItem currentConsumable;
    public RangedAmmoItem currentAmmo;

    [Header("Current Equipment")]
    public EquipmentItem[] currentArmorEquipments = new EquipmentItem[4];

    public EquipmentItem currentHelmetEquipment;
    public EquipmentItem currentTorsoEquipment;
    public EquipmentItem currentLegEquipment;
    public EquipmentItem currentHandEquipment;
    public RingItem ringSlot01;
    public RingItem ringSlot02;
    public RingItem ringSlot03;
    public RingItem ringSlot04;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];

    public int currentRightWeaponIndex = 0;
    public int currentLeftWeaponIndex = 0;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    private void Start()
    {
        character.characterWeaponSlotManager.LoadBothWeaponsOnSlots();
        LoadRingEffects();
    }

    public virtual void LoadRingEffects()
    {
        if(ringSlot01 != null)
        {
            ringSlot01.EquipRing(character);
        }
        if(ringSlot02 != null)
        {
            ringSlot02.EquipRing(character);
        }
        if(ringSlot03 != null)
        {
            ringSlot03.EquipRing(character);
        }
        if(ringSlot04 != null)
        {
            ringSlot04.EquipRing(character);
        }
    }
}
