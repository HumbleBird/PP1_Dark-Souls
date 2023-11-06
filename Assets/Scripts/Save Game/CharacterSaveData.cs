using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    // Name
    public string m_sCharacterName;

    // Player Level
    public int m_iCharacterLevel;

    // Stat Level
    public int m_iVigorLevel;
    public int m_iAttunementLevel;
    public int m_iEnduranceLevel;
    public int m_iVitalityLevel;
    public int m_iStrengthLevel;
    public int m_iDexterityLevel;
    public int m_iIntelligenceLevel;
    public int m_iFaithLevel;
    public int m_iLuckLevel;

    // Inventory

    // Equipment
    // 나중에 전부 dic, enum으로 정리할 것
    [Header("Equipment")]

    // Right Hand Slots
    public int currentRightHandWeaponID; 
    public int m_iRightHandWeapon1ID;
    public int m_iRightHandWeapon2ID;
    public int m_iRightHandWeapon3ID;

    // Left Hand Slots
    public int currentLeftHandWeaponID;
    public int m_iLeftHandWeapon1ID;
    public int m_iLeftHandWeapon2ID;
    public int m_iLeftHandWeapon3ID;

    // Ammo
    public int m_iCurrentAmmoID;
    public int m_iArrow1ID;
    public int m_iArrow2ID;
    public int m_iBolt1ID;
    public int m_iBolt2ID;

    // Armor
    public int currentHeadGearItemID;
    public int currentChestGearItemID;
    public int currentLegGearItemID;
    public int currentHandGearItemID;

    // Ring
    public int m_iRing1ID;
    public int m_iRing2ID;
    public int m_iRing3ID;
    public int m_iRing4ID;

    // Consumable
    public int m_iCurrentConsumableItemID;
    public int m_iConsumableItem1ID;
    public int m_iConsumableItem2ID;
    public int m_iConsumableItem3ID;
    public int m_iConsumableItem4ID;
    public int m_iConsumableItem5ID;
    public int m_iConsumableItem6ID;
    public int m_iConsumableItem7ID;
    public int m_iConsumableItem8ID;
    public int m_iConsumableItem9ID;
    public int m_iConsumableItem10ID;

    // Spell
    public int m_iSpellID;

    // Peldge
    public int m_iCurrentPledgeID;

    // Pos
    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    // index
    public int m_iCurrentRightWeaponIndex ;
    public int m_iCurrentLeftWeaponIndex ;
    public int m_iCurrentAmmoArrowIndex ;
    public int m_iCurrentAmmoBoltIndex ;
    public int m_iCurrentConsumableItemndex ;

    // 업적

    // Pick Item, Boss Defetead, BonFire

    [Header("Items Looted From World")]
    public SerializebleDictionary<int, bool> itemsInWorld; // int는 ID, bool은 loot 여부

    public CharacterSaveData()
    {
        itemsInWorld = new SerializebleDictionary<int, bool>();
    }
}
