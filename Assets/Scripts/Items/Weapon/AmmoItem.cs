using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Ammo")]
public class AmmoItem : Item
{
    public AmmoItem(int id)
    {
        m_eItemType = Define.E_ItemType.Ammo;

        ammoType = AmmoType.Arrow;

        loadedItemModel = Managers.Resource.Load<GameObject>("Prefabs/Items/Weapons/Ammo/Standard Arrow_Loaded_Model");
        liveAmmoModel = Managers.Resource.Load<GameObject>("Prefabs/Items/Weapons/Ammo/Standard Arrow_Live_Model");
        penetratedModel = Managers.Resource.Load<GameObject>("Prefabs/Items/Weapons/Ammo/Standard Arrow_Penetrated_Model");
    }

    public AmmoItem()
    {
        m_eItemType = Define.E_ItemType.Ammo;

        ammoType = AmmoType.Arrow;

    }

    [Header("Property")]
    public AmmoType ammoType;
    public string AttackType;

    [Header("Current Count")]
    public int m_iCurrentCount; // ���� ���� ������ ��
    public int m_iMaxCount; // �ִ� ���� ������ ��

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // ���� ������ ��
    public int m_iMaxSaveCount; // �ִ� ������ ������ ��

    [Header("Attack Values")]
    public int m_iPhysicalDamage = 10;
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

    public CharacterManager m_Shooter;


    [Header("Item Modles")]
    public GameObject loadedItemModel; // Ȱ�� �ڷ� ���� ���� �����ִ� ��
    public GameObject liveAmmoModel; // �� ���� ĳ���Ϳ��� �������� ���� �� ����
    public GameObject penetratedModel; // �ݶ��̴��� �����ϱ� ���� ��ȯ��

}
