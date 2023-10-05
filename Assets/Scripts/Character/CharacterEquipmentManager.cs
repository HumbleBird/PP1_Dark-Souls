using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipmentManager : MonoBehaviour
{
    // Current Equip Item

    // Quick Slots
    public SpellItem m_CurrentSpell; // Equick Slot Up
    public WeaponItem m_CurrentRightWeapon; // Equick Slot Right
    public WeaponItem m_CurrentLeftWeapon; // Equick Slot Left
    public ToolItem m_CurrentConsumable; // Equick Slot Down
    public RangedAmmoItem m_CurrentAmmo; // Equick Slot Left Down

    // Arrmor
    public HelmEquipmentItem currentHelmetEquipment;
    public TorsoEquipmentItem currentTorsoEquipment;
    public LeggingsEquipmentItem currentLegEquipment;
    public GantletsEquipmentItem currentHandEquipment;
}
