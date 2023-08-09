using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    public string characterName;

    public int characterLevel;

    // 나중에 전부 dic, enum으로 정리할 것
    [Header("Equipment")]
    public int currentRightHandWeaponID;
    public int currentLeftHandWeaponID;

    public int currentHeadGearItemID    ;
    public int currentChestGearItemID   ;
    public int currentLegGearItemID     ;   
    public int currentHandGearItemID    ;

    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    [Header("Items Looted From World")]
    public SerializebleDictionary<int, bool> itemsInWorld; // int는 ID, bool은 loot 여부

    public CharacterSaveData()
    {
        itemsInWorld = new SerializebleDictionary<int, bool>();
    }
}
