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

    [Header("Weapon Stats")]
    public TextMeshProUGUI physicalDamageText;
    public TextMeshProUGUI magicDamageText;
    public TextMeshProUGUI physicalAbsorptionText;
    public TextMeshProUGUI magicAbsorptionText;

    public void UpdateWeaponItemStats(WeaponItem weapon)
    {
        if(weapon != null)
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
            physicalAbsorptionText.text = weapon.physicalDamageAbsorption.ToString();

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
}
