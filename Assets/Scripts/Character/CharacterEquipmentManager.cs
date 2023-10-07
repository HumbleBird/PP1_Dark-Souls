using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipmentManager : MonoBehaviour
{
    protected CharacterManager character;

    [Header("Current Item Begin Used")]
    public Item currentItemBeingUsed;

    // Current Equip Item

    [Header("Quick Slot Items")]
    public SpellItem m_CurrentHandSpell; // Equick Slot Up
    public WeaponItem m_CurrentHandRightWeapon; // Equick Slot Right
    public WeaponItem m_CurrentHandLeftWeapon; // Equick Slot Left
    public ToolItem m_CurrentHandConsumable; // Equick Slot Down
    public RangedAmmoItem m_CurrentHandAmmo; // Equick Slot Left Down

    [Header("Weapon Items")]
    public WeaponItem[] m_RightWeaponsSlots = new WeaponItem[3];
    public WeaponItem[] m_LeftWeaponsSlots = new WeaponItem[3];

    [Header("Ammo Items")]
    public RangedAmmoItem[] m_ArrowAmmoSlots = new RangedAmmoItem[2];
    public RangedAmmoItem[] m_BoltAmmoSlots = new RangedAmmoItem[2];
    
    [Header("Arrmor Items")]
    public HelmEquipmentItem m_HelmetEquipment;
    public TorsoEquipmentItem m_TorsoEquipment;
    public LeggingsEquipmentItem m_LegEquipment;
    public GantletsEquipmentItem m_HandEquipment;

    [Header("Ring Items")]
    public RingItem[] m_RingSlots = new RingItem[4];

    [Header("Consumable Items")]
    public ToolItem[] m_ConsumableItemSlots = new ToolItem[10];

    [Header("Pledge")]
    public Item m_CurrentPledge;

    [Header("Unarme Item")]
    public Item m_Unarmed;

    [Header("Current Item Index")]
    public int m_iCurrentRightWeaponIndex = 0;
    public int m_iCurrentLeftWeaponIndex = 0;
    public int m_iCurrentAmmoArrowIndex = 0;
    public int m_iCurrentAmmoBoltIndex = 0;
    public int m_iCurrentConsumableItemndex = 0;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    private void Start()
    {
        // 현재 장착중인 아이템 효과 불러일으키기
    }
}
