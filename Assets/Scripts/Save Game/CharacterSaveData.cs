using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    public string characterName;

    public int characterLevel;

    // ���߿� ���� dic, enum���� ������ ��
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
    public SerializebleDictionary<int, bool> itemsInWorld; // int�� ID, bool�� loot ����

    public CharacterSaveData()
    {
        itemsInWorld = new SerializebleDictionary<int, bool>();
    }
}
