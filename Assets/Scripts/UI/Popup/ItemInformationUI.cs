using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationUI : UI_Base
{
    enum Texts
    {
        // Toll
        ToolItemNameText,
        ToolItemTypeText,
        CountValueText,
        SaveCountValueText,
        ItemEffectDescriptionText,
        Tool_ParameterBonusStrengthValueText,
        Tool_ParameterBonusDexterityValueText,
        Tool_ParameterBonusIntelligenceValueText,
        Tool_ParameterBonusFaithValueText,


        // Weapon Information
        WeaponItemNameText,
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
        ArmorStats,
        ToolStats,
        ItemStats,
        AmmoStats,
    }

    GameObject m_ToolStats;
    GameObject m_ItemStats;
    GameObject m_WeaponStats;
    GameObject m_ArmorStats;
    GameObject m_AmmoStats;


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
        m_ToolStats = GetObject((int)GameObjects.ToolStats);
        m_ItemStats = GetObject((int)GameObjects.ItemStats);
        m_AmmoStats = GetObject((int)GameObjects.AmmoStats);

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
            case Define.E_ItemType.Tool:
                ShowToolItemInfo((ToolItem)item); // 툴 타입, 소지수, 저장수, 아이템 효과, 능력 보정
                break;
            case Define.E_ItemType.ReinforcedMaterial: // 소지수 (1/  1), 저장수(- / -) 고정, 아이템 효과
            case Define.E_ItemType.Valuables: // 툴 타입, 소지수, 저장수
                break;
            case Define.E_ItemType.Magic: // 아이템 타입, 소지수, 저장수, 아이템 효과, 능력 보정
                break;
            case Define.E_ItemType.MeleeWeapon:
            case Define.E_ItemType.RangeWeapon:
            case Define.E_ItemType.Catalyst:
            case Define.E_ItemType.Shield:
                ShowWeaponItemInfo((WeaponItem)item);
                break;
            case Define.E_ItemType.Helmet: // 중략, 내구도, 감소율, 내성치
            case Define.E_ItemType.ChestArmor:
            case Define.E_ItemType.Gauntlets:
            case Define.E_ItemType.Leggings:
                break;
            case Define.E_ItemType.Ammo:// 아이템 속성, 공격력, 특수 효과
                break;
            case Define.E_ItemType.Ring: // 중량, 아이템 효과
                break;
            case Define.E_ItemType.Pledge: // 아이템 효과
                break;
            default:
                break;
        }
    }

    void ShowNomalItemInfo(ToolItem item)
    {

    }

    void ShowToolItemInfo(ToolItem item)
    {
        m_ToolStats.SetActive(true);

        GetText((int)Texts.ToolItemNameText).text = item.itemName;
        GetText((int)Texts.ToolItemTypeText).text = item.m_ToolType.ToString();
        GetText((int)Texts.CountValueText).text = item.currentItemAmount.ToString() + " / " + item.maxItemAmount.ToString();
        //GetText((int)Texts.SaveCountValueText).text = item.currentItemAmount.ToString();

        GetText((int)Texts.ItemEffectDescriptionText).text = item.m_sItemDescription.ToString();

        GetText((int)Texts.Tool_ParameterBonusStrengthValueText).text = item.m_iParameterBonusStrength.ToString();
        GetText((int)Texts.Tool_ParameterBonusDexterityValueText).text = item.m_iParameterBonusDexterity.ToString();
        GetText((int)Texts.Tool_ParameterBonusIntelligenceValueText).text = item.m_iParameterBonusIntelligence.ToString();
        GetText((int)Texts.Tool_ParameterBonusFaithValueText).text = item.m_iParameterBonusFaith.ToString();
    }

    void ShowWeaponItemInfo(WeaponItem item)
    {
        m_WeaponStats.SetActive(true);

        GetText((int)Texts.WeaponItemNameText).text = item.itemName;
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
        m_AmmoStats.SetActive(false);
        m_ToolStats.SetActive(false);
        m_ItemStats.SetActive(false);
    }
}
