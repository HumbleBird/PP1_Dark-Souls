using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Ammo")]
public class RangedAmmoItem : Item
{


    [Header("Ammo Type")]
    public AmmoType ammoType;

    [Header("Ammo Velocity")]
    public float forwardVelocity = 550;
    public float upwardVelocity = 0;
    public float ammoMass = 0;
    public bool useGravity = false;

    [Header("Ammo Capacity")]
    public int carryLimit = 99;
    public int currentAmount = 99;

    [Header("Ammo Base Damage")]
    public int physicalDamage = 50;


    [Header("Item Modles")]
    public GameObject loadedItemModel; // 활을 뒤로 젖힐 동안 보여주는 모델
    public GameObject liveAmmoModel; // 이 모델은 캐릭터에게 데미지를 입힐 수 있음
    public GameObject penetratedModel; // 콜라이더와 접촉하기 위해 소환됨

}
