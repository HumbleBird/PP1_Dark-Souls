using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatWindowUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage;

    [Header("Equipment Stats Windows")]
    public GameObject weaponsStats;
    public GameObject armorStats;

    [Header("Weapon Stats")]
    public TextMeshProUGUI physicalDamageText;
    public TextMeshProUGUI magicDamageText;
    public TextMeshProUGUI physicalAbsorptionText;
    public TextMeshProUGUI magicAbsorptionText;

    [Header("Armor Stats")]
    public TextMeshProUGUI armorPhysicalAbsorptionText;
    public TextMeshProUGUI armorMagicAbsorptionText;
    public TextMeshProUGUI armorPoisonResistanceText;
    
    public void UpdateWeaponItemStats(WeaponItem weapon)
    {
        CloseAllStatWindows();

        if (weapon != null)
        {
            if (weapon.itemName != null)
            {
                itemNameText.text = weapon.itemName;
            }
            else
            {
                itemNameText.text = "";

            }

            if (weapon.itemIcon != null)
            {
                itemIconImage.gameObject.SetActive(true);
                itemIconImage.enabled = true;
                itemIconImage.sprite = weapon.itemIcon;
            }
            else
            {
                itemIconImage.gameObject.SetActive(false);
                itemIconImage.enabled = false;
                itemIconImage.sprite = null;
            }

            physicalDamageText.text = weapon.physicalDamage.ToString();
            physicalAbsorptionText.text = weapon.physicalBlockingDamageAbsorption.ToString();

            weaponsStats.SetActive(true);
        }
        else
        {
            itemNameText.text = "";
            itemIconImage.gameObject.SetActive(false);
            itemIconImage.sprite = null;
            weaponsStats.SetActive(false);
        }
    }
    
    public void UpdateArmorItemStats(EquipmentItem armor)
    {
        CloseAllStatWindows();


        if (armor != null)
        {
            if (armor.itemName != null)
            {
                itemNameText.text = armor.itemName;
            }
            else
            {
                itemNameText.text = "";

            }

            if (armor.itemIcon != null)
            {
                itemIconImage.gameObject.SetActive(true);
                itemIconImage.enabled = true;
                itemIconImage.sprite = armor.itemIcon;
            }
            else
            {
                itemIconImage.gameObject.SetActive(false);
                itemIconImage.enabled = false;
                itemIconImage.sprite = null;
            }

            armorPhysicalAbsorptionText.text = armor.m_fPhysicalDefense.ToString();
            armorMagicAbsorptionText.text = armor.m_fMagicDefense.ToString();

            armorStats.SetActive(true);
        }
        else
        {
            itemNameText.text = "";
            itemIconImage.gameObject.SetActive(false);
            itemIconImage.sprite = null;
            armorStats.SetActive(false);
        }
    }

    private void CloseAllStatWindows()
    {
        weaponsStats.SetActive(false);
        armorStats.SetActive(false);
    }
    
}
