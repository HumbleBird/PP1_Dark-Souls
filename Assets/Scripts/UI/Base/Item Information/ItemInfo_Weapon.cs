using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Weapon : ItemInfo_Base
{
    enum Texts
    {
        // Property
        ItemNameText,

        WeaponTypeText,
        AttackTypeText,
        SkillText,
        FocusCostValueText,
        EquipLoadValueText,
        DurabilityValueText,

        // Attack Power
        AttackPhysicalValueText,
        AttackMagicValueText,
        AttackFireValueText,
        AttackLightningValueText,
        AttackDarkValueText,
        AttackCriticalValueText,
        AttackRangeValueText,
        MagicAdjustmentValueText,

        // Guard Absorption
        DamageReductionPhysicalValueText,
        DamageReductionMagicValueText,
        DamageReductionFireValueText,
        DamageReductionLightningValueText,
        DamageReductionDarkValueText,
        StabilityValueText,

        //Additional Effects
        AdditionalEffectBleedValueText  ,
        AdditionalEffectPoisonValueText  ,
        AdditionalEffectFrostValueText  ,

        //Attribute Bonus
        AttributeBonusStrengthValueText,
        AttributeBonusDexterityValueText,
        AttributeBonusIntelligenceValueText,
        AttributeBonusFaithValueText,

        //Attribute Requirement
        AttributeRequirementStrengthValueText,
        AttributeRequirementDexterityValueText,
        AttributeRequirementIntelligenceValueText,
        AttributeRequirementFaithValueText,
    }

    enum Objects
    {
        AttackRange,
        MagicAdjustment
    }

    GameObject m_goAttackRange;
    GameObject m_goMagicAdjustment;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(Objects));

        m_goAttackRange = GetObject((int)Objects.AttackRange);
        m_goMagicAdjustment = GetObject((int)Objects.MagicAdjustment);

        return true;
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);

        ShowWeaponItem((WeaponItem)item);
    }

    void ShowWeaponItem(WeaponItem item)
    {
        // Property
        GetText((int)Texts.ItemNameText).text = item.m_ItemName;

        GetText((int)Texts.WeaponTypeText).text = item.m_eWeaponType.ToString();
        GetText((int)Texts.AttackTypeText).text = item.AttackType;
        GetText((int)Texts.SkillText).text = item.Skill;
        GetText((int)Texts.FocusCostValueText).text = item.FocusCost.ToString();
        GetText((int)Texts.EquipLoadValueText).text = item.m_iWeight.ToString();
        GetText((int)Texts.DurabilityValueText).text = item.m_iDurability.ToString();

        // Attack Power
        GetText((int)Texts.AttackPhysicalValueText).text = item.m_iPhysicalDamage.ToString();
        GetText((int)Texts.AttackMagicValueText).text = item.m_iMagicDamage.ToString();
        GetText((int)Texts.AttackFireValueText).text = item.m_iFireDamage.ToString();
        GetText((int)Texts.AttackLightningValueText).text = item.m_iLightningDamage.ToString();
        GetText((int)Texts.AttackDarkValueText).text = item.m_iDarkDamage.ToString();
        GetText((int)Texts.AttackCriticalValueText).text = item.m_iCriticalDamage.ToString();

        // Guard Absorption
        GetText((int)Texts.DamageReductionPhysicalValueText).text = item.m_iPhysicalDamageReduction.ToString();
        GetText((int)Texts.DamageReductionMagicValueText).text = item.m_iMagicDamageReduction.ToString();
        GetText((int)Texts.DamageReductionFireValueText).text = item.m_iFireDamageReduction.ToString();
        GetText((int)Texts.DamageReductionLightningValueText).text = item.m_iLightningDamageReduction.ToString();
        GetText((int)Texts.DamageReductionDarkValueText).text = item.m_iDarkDamageReduction.ToString();
        GetText((int)Texts.StabilityValueText).text = item.m_iStability.ToString();


        //Additional Effects
        GetText((int)Texts.AdditionalEffectBleedValueText ).text = item.m_iBleeding.ToString();
        GetText((int)Texts.AdditionalEffectPoisonValueText).text = item.m_iPoison.ToString();
        GetText((int)Texts.AdditionalEffectFrostValueText).text = item.m_iFrost.ToString();

        //Attribute Bonus
        GetText((int)Texts.AttributeBonusStrengthValueText).text = item.m_iAttributeBonusStrength.ToString();
        GetText((int)Texts.AttributeBonusDexterityValueText).text = item.m_iAttributeBonusDexterity.ToString();
        GetText((int)Texts.AttributeBonusIntelligenceValueText).text = item.m_iAttributeBonusIntelligence.ToString();
        GetText((int)Texts.AttributeBonusFaithValueText).text = item.m_iAttributeBonusFaith.ToString();

        //Attribute Bonus
        GetText((int)Texts.AttributeRequirementStrengthValueText).text = item.m_iAttributeRequirementStrength.ToString();
        GetText((int)Texts.AttributeRequirementDexterityValueText).text = item.m_iAttributeRequirementDexterity.ToString();
        GetText((int)Texts.AttributeRequirementIntelligenceValueText).text = item.m_iAttributeRequirementIntelligence.ToString();
        GetText((int)Texts.AttributeRequirementFaithValueText).text = item.m_iAttributeRequirementFaith.ToString();

        switch (item.m_eItemType)
        {
            case Define.E_ItemType.RangeWeapon:
                m_goMagicAdjustment.SetActive(false);
                m_goAttackRange.SetActive(true);
                GetText((int)Texts.AttackRangeValueText).text = item.m_iAttackRange.ToString();
                break;
            case Define.E_ItemType.Catalyst:
                m_goAttackRange.SetActive(false);
                m_goMagicAdjustment.SetActive(true);
                GetText((int)Texts.MagicAdjustmentValueText).text = item.m_iMagicAdjustment.ToString();
                break;
            case Define.E_ItemType.MeleeWeapon:
            case Define.E_ItemType.Shield:
                m_goAttackRange.SetActive(false);
                m_goMagicAdjustment.SetActive(false);
                break;
        }
    }
}