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
    public ToolItem currentConsumable;
    public RangedAmmoItem currentAmmo;

    [Header("Current Equipment")]
    public HelmEquipmentItem currentHelmetEquipment;
    public TorsoEquipmentItem currentTorsoEquipment;
    public LeggingsEquipmentItem currentLegEquipment;
    public GantletsEquipmentItem currentHandEquipment;
    public RingItem ringSlot01;
    public RingItem ringSlot02;
    public RingItem ringSlot03;
    public RingItem ringSlot04;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];

    [Header("Current External Features")]
    public GameObject currentHairStyle;
    public GameObject currentHairItem;
    public GameObject currentEyelashesBtn;
    public GameObject currentEyebrows;
    public GameObject currentFacialHair;
    public GameObject currentFacialMask;
    public GameObject currentNose;
    public GameObject currentExtra;

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
