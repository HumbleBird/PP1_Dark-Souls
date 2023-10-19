using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Ammo")]
public class RangedAmmoItem : Item
{
    public RangedAmmoItem()
    {
        m_EItemType = Define.E_ItemType.Ammo;
    }

    [Header("Property")]
    public AmmoType ammoType;
    public string AttackType;

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    [Header("Ammo Base Damage")]
    public int physicalDamage = 50;

    [Header("Attack Values")]
    public int m_iPhysicalDamage;
    public int m_iMagicDamage;
    public int m_iFireDamage;
    public int m_iLightningDamage;
    public int m_iDarkDamage;
    public int m_iCriticalDamage;

    [Header("Auxiliary Effects")]
    public int m_iBleeding;
    public int m_iPoison;
    public int m_iFrost;

    [Header("Ammo Velocity")]
    public float forwardVelocity = 550;
    public float upwardVelocity = 0;
    public float ammoMass = 0;
    public bool useGravity = false;




    [Header("Item Modles")]
    public GameObject loadedItemModel; // 활을 뒤로 젖힐 동안 보여주는 모델
    public GameObject liveAmmoModel; // 이 모델은 캐릭터에게 데미지를 입힐 수 있음
    public GameObject penetratedModel; // 콜라이더와 접촉하기 위해 소환됨

}
