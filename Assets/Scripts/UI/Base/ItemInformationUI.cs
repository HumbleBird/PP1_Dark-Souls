using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationUI : UI_Base
{
    public Image m_ItemBasePlateIcon;
    public Image m_ItemIcon;

    public ItemInfo_Tool                           m_ItemInfo_Tool;
    public ItemInfo_ReinforcedMaterial_Valuables   m_ItemInfo_ReinforcedMaterial_Valuables;
    public ItemInfo_Magic                          m_ItemInfo_Magic;
    public ItemInfo_Weapon                         m_ItemInfo_Weapon;
    public ItemInfo_Armor                          m_ItemInfo_Armor;
    public ItemInfo_Ammo                           m_ItemInfo_Ammo;
    public ItemInfo_Ring                           m_ItemInfo_Ring;
    public ItemInfo_Pledge                         m_ItemInfo_Pledge;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        m_ItemInfo_Tool = GetComponentInChildren<ItemInfo_Tool                        >();
        m_ItemInfo_ReinforcedMaterial_Valuables = GetComponentInChildren<ItemInfo_ReinforcedMaterial_Valuables>();
        m_ItemInfo_Magic = GetComponentInChildren<ItemInfo_Magic                       >();
        m_ItemInfo_Weapon = GetComponentInChildren<ItemInfo_Weapon                      >();
        m_ItemInfo_Armor = GetComponentInChildren<ItemInfo_Armor                       >();
        m_ItemInfo_Ammo = GetComponentInChildren<ItemInfo_Ammo                        >();
        m_ItemInfo_Ring = GetComponentInChildren<ItemInfo_Ring                        >();
        m_ItemInfo_Pledge = GetComponentInChildren<ItemInfo_Pledge                      >();

        CloseShowItemInformation();

        return true;
    }

    public void ShowItemInformation(Item item)
    {
        CloseShowItemInformation();

        m_ItemBasePlateIcon.enabled = true;
        m_ItemIcon.enabled = true;
        m_ItemIcon.sprite = item.itemIcon;

        switch (item.m_EItemType)
        {
            case Define.E_ItemType.Tool:
                m_ItemInfo_Tool.gameObject.SetActive(true);
                m_ItemInfo_Tool.ShowItemInfo(item);
                break;
            case Define.E_ItemType.ReinforcedMaterial:
            case Define.E_ItemType.Valuables:
                m_ItemInfo_ReinforcedMaterial_Valuables.gameObject.SetActive(true);
                m_ItemInfo_ReinforcedMaterial_Valuables.ShowItemInfo(item);
                break;
            case Define.E_ItemType.Magic:
                m_ItemInfo_Magic.gameObject.SetActive(true);
                m_ItemInfo_Magic.ShowItemInfo(item);
                break;
            case Define.E_ItemType.MeleeWeapon:
            case Define.E_ItemType.RangeWeapon:
            case Define.E_ItemType.Catalyst:
            case Define.E_ItemType.Shield:
                m_ItemInfo_Weapon.gameObject.SetActive(true);
                m_ItemInfo_Weapon.ShowItemInfo(item);
                break;
            case Define.E_ItemType.Helmet:
            case Define.E_ItemType.ChestArmor:
            case Define.E_ItemType.Gauntlets:
            case Define.E_ItemType.Leggings:
                m_ItemInfo_Armor.gameObject.SetActive(true);
                m_ItemInfo_Armor.ShowItemInfo(item);
                break;
            case Define.E_ItemType.Ammo:
                m_ItemInfo_Ammo.gameObject.SetActive(true);
                m_ItemInfo_Ammo.ShowItemInfo(item);
                break;
            case Define.E_ItemType.Ring:
                m_ItemInfo_Ring.gameObject.SetActive(true);
                m_ItemInfo_Ring.ShowItemInfo(item);
                break;
            case Define.E_ItemType.Pledge:
                m_ItemInfo_Pledge.gameObject.SetActive(true);
                m_ItemInfo_Pledge.ShowItemInfo(item);
                break;
            default:
                break;
        }
    }


    public void CloseShowItemInformation()
    {
        m_ItemBasePlateIcon.enabled = false;
        m_ItemIcon.enabled = false;

        m_ItemInfo_Tool.gameObject.SetActive(false);
        m_ItemInfo_ReinforcedMaterial_Valuables.gameObject.SetActive(false);
        m_ItemInfo_Magic.gameObject.SetActive(false);
        m_ItemInfo_Weapon.gameObject.SetActive(false);
        m_ItemInfo_Armor.gameObject.SetActive(false);
        m_ItemInfo_Ammo.gameObject.SetActive(false);
        m_ItemInfo_Ring.gameObject.SetActive(false);
        m_ItemInfo_Pledge.gameObject.SetActive(false);
    }
}
