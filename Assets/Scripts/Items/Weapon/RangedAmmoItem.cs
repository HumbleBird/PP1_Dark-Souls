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
    public GameObject loadedItemModel; // Ȱ�� �ڷ� ���� ���� �����ִ� ��
    public GameObject liveAmmoModel; // �� ���� ĳ���Ϳ��� �������� ���� �� ����
    public GameObject penetratedModel; // �ݶ��̴��� �����ϱ� ���� ��ȯ��

}
