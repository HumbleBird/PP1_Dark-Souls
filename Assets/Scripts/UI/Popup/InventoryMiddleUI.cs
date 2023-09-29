using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMiddleUI : UI_Base
{
    enum Texts
    {
        // Weapon Information
        ItemNameText,
        WeaponTypeText,
        AttackTypeText,
        SkillText,
        //FocusCostValueText,
        //WeightValueText,
        //DurabilityValueText,

        AttackPhysicalValueText,
        AttackMagicValueText,
        AttackFireValueText,
        AttackLightningValueText,
        AttackDarkValueText,
        AttackCriticalValueText,


        DamageReductionPhysicalValueText,
        DamageReductionMagicValueText,
        DamageReductionFireValueText,
        DamageReductionLightningValueText,
        DamageReductionDarkValueText,
        DamageReductionStabilitylValueText,


        BleedValueText,
        PoisonValueText,
        FrostValueText,

        ParameterBonusStrengthValueText,
        ParameterBonusDexterityValueText,
        ParameterBonusIntelligenceValueText,
        ParameterBonusFaithValueText,

        RequirementStrengthValueText,
        RequirementDexterityValueText,
        RequirementIntelligenceValueText,
        RequirementFaithValueText,
    }

    enum Images
    {
        ItemBasePlateIcon,
        ItemIcon,
    }

    enum GameObjects
    {
        WeaponStats,
        ArmorStats
    }

    GameObject m_WeaponStats;
    GameObject m_ArmorStats;
    Image m_ItemBasePlateIcon;
    Image m_ItemIcon;

    public TextMeshProUGUI FocusCostValueText;
    public TextMeshProUGUI WeightValueText;
    public TextMeshProUGUI DurabilityValueText;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));

        m_WeaponStats = GetObject((int)GameObjects.WeaponStats);
        m_ArmorStats = GetObject((int)GameObjects.ArmorStats);

        m_ItemBasePlateIcon = GetImage((int)Images.ItemBasePlateIcon);
        m_ItemIcon = GetImage((int)Images.ItemIcon);

        m_ItemIcon.enabled = false;
        m_ItemBasePlateIcon.enabled = false;

        AllItemTypeWindowClose();

        return true;
    }

    public void ShowItemInformation(Item item)
    {
        AllItemTypeWindowClose();

        GetImage((int)Images.ItemIcon).sprite = item.itemIcon;
        m_ItemIcon.enabled = true;
        m_ItemBasePlateIcon.enabled = true;

        switch (item.m_EItemType)
        {
            case Define.ItemType.None:
                break;
            case Define.ItemType.Weapon:
                ShowWeaponItemInfo((WeaponItem)item);
                break;
            case Define.ItemType.Armor:
                break;
            case Define.ItemType.Consumable:
                break;
            case Define.ItemType.Order:
                break;
            default:
                break;
        }
    }

    void ShowWeaponItemInfo(WeaponItem item)
    {
        m_WeaponStats.SetActive(true);

        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.WeaponTypeText).text = item.weaponType.ToString();
        GetText((int)Texts.AttackTypeText).text = item.AttackType;
        GetText((int)Texts.SkillText).text = item.Skill;
        FocusCostValueText.text  = item.FocusCost.ToString();
        WeightValueText.text      =   item.m_iWeight.ToString();
        DurabilityValueText.text = item.m_iDurability.ToString();

        GetText((int)Texts.AttackPhysicalValueText).text = item.m_iPhysicalDamage.ToString();
        GetText((int)Texts.AttackMagicValueText).text = item.m_iMagicDamage.ToString();
        GetText((int)Texts.AttackFireValueText).text = item.m_iFireDamage.ToString();
        GetText((int)Texts.AttackLightningValueText).text = item.m_iLightningDamage.ToString();
        GetText((int)Texts.AttackDarkValueText).text = item.m_iDarkDamage.ToString();
        GetText((int)Texts.AttackCriticalValueText).text = item.m_iCriticalDamage.ToString();

        GetText((int)Texts.DamageReductionPhysicalValueText).text = item.m_iPhysicalDamageReduction.ToString();
        GetText((int)Texts.DamageReductionMagicValueText).text = item.m_iMagicDamageReduction.ToString();
        GetText((int)Texts.DamageReductionFireValueText).text = item.m_iFireDamageReduction.ToString();
        GetText((int)Texts.DamageReductionLightningValueText).text = item.m_iLightningDamageReduction.ToString();
        GetText((int)Texts.DamageReductionDarkValueText).text = item.m_iDarkDamageReduction.ToString();
        GetText((int)Texts.DamageReductionStabilitylValueText).text = item.m_iStability.ToString();


        GetText((int)Texts.BleedValueText).text = item.m_iBleeding.ToString();
        GetText((int)Texts.PoisonValueText).text = item.m_iPoison.ToString();
        GetText((int)Texts.FrostValueText).text = item.m_iFrost.ToString();

        GetText((int)Texts.ParameterBonusStrengthValueText).text = item.m_iParameterBonusStrength.ToString();
        GetText((int)Texts.ParameterBonusDexterityValueText).text = item.m_iParameterBonusDexterity.ToString();
        GetText((int)Texts.ParameterBonusIntelligenceValueText).text = item.m_iParameterBonusIntelligence.ToString();
        GetText((int)Texts.ParameterBonusFaithValueText).text = item.m_iParameterBonusFaith.ToString();
        
        GetText((int)Texts.RequirementStrengthValueText).text = item.m_iRequirementStrength.ToString();
        GetText((int)Texts.RequirementDexterityValueText).text = item.m_iRequirementDexterity.ToString();
        GetText((int)Texts.RequirementIntelligenceValueText).text = item.m_iRequirementIntelligence.ToString();
        GetText((int)Texts.RequirementFaithValueText).text = item.m_iRequirementFaith.ToString();

    }

    void AllItemTypeWindowClose()
    {
        m_WeaponStats.SetActive(false);
        m_ArmorStats.SetActive(false);
    }
}
