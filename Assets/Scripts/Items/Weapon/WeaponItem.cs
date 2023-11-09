using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public WeaponItem(int id)
    {
        Table_Item_Weapon.Info data = Managers.Table.m_Item_Weapon.Get(id);

        if (data == null)
            return;
        {
            Count = 1;
            m_bStackable = false;

            m_iItemID = data.m_nID;                                       ;
            m_ItemName= data.m_sName;                                     ;
            m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath)                                  ;
            m_sPrefabPath = data.m_sPrefabPath                                ;
            m_eWeaponType =  (WeaponType)data.m_iWeaponType                               ;
            AttackType = data.m_sAttackType                                ;
            Skill = data.m_sSkill                                     ;
            FocusCost = data.m_iCostFP                                    ;
            m_iAttributeRequirementStrength = data.m_iRequirement_Strength                      ;
            m_iAttributeRequirementDexterity = data.m_iRequirement_Dexterity                     ;
            m_iAttributeRequirementIntelligence = data.m_iRequirement_Intelligence                  ;
            m_iAttributeRequirementFaith = data.m_iRequirement_Faith                         ;
            m_iAttributeBonusStrength = data.m_iParameter_Bonus_Strength                  ;
            m_iAttributeBonusDexterity = data.m_iParameter_Bonus_Dexterity                 ;
            m_iAttributeBonusIntelligence = data.m_iParameter_Bonus_Intelligence              ;
            m_iAttributeBonusFaith = data.m_iParameter_Bonus_Faith                     ;
           m_iPhysicalDamage= data.m_iAttack_Physical                           ;
           m_iMagicDamage= data.m_iAttack_Magic                              ;
           m_iFireDamage= data.m_iAttack_Fire                               ;
           m_iLightningDamage= data.m_iAttack_Lightning                          ;
           m_iDarkDamage= data.m_iAttack_Dark                               ;
           m_iCriticalDamage= data.m_iAttack_Critical                           ;
           m_iMagicAdjustment= data.m_iAttack_Spell_Buff                         ;
            m_iAttackRange= data.m_iAttack_Range                              ;
           m_iPhysicalDamageReduction= data.m_iDamage_Reduction_Physical                 ;
           m_iMagicDamageReduction= data.m_iDamage_Reduction_Magic                    ;
           m_iFireDamageReduction= data.m_iDamage_Reduction_Fire                     ;
           m_iLightningDamageReduction= data.m_iDamage_Reduction_Lightning                ;
            m_iDarkDamageReduction= data.m_iDamage_Reduction_Dark                     ;

            m_iBleeding= data.m_iAuxiliary_Effects_Bleeding                ;
            m_iPoison= data.m_iAuxiliary_Effects_Posion                  ;
            m_iFrost= data.m_iAuxiliary_Effects_Frost                   ;
            m_iCurse= data.m_iAuxiliary_Effects_Curse                   ;
            m_iDurability = data.m_iDurability                                ;
            m_iWeight = data.m_fWeight                                    ;
            m_iStability = data.m_iStability                                 ;

            SetAttackAction(m_eWeaponType);
            SetAnimatorControllerOver(m_eWeaponType);
            SetAttackSound(m_eWeaponType);
            Typeclassification(m_eWeaponType);
            SetBaseStaminaCost(m_eWeaponType);
            SetSound();
        }
    }

    public WeaponItem()
    {
    }

    public bool isUnarmed;

    [Header("Animator Replacer")]
    public AnimatorOverrideController weaponController;
    //public string offHandIdleAnimation = "Left_Arm_Idle_01";

    [Header("Weapon Type")]
    public WeaponType m_eWeaponType;
    public string AttackType;
    public string Skill;
    public int FocusCost;

    [Header("Requirement Ability")] // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeRequirementStrength;
    public int m_iAttributeRequirementDexterity;
    public int m_iAttributeRequirementIntelligence;
    public int m_iAttributeRequirementFaith;

    [Header("Parameter Bonus")] // 능력치 보정  F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeBonusStrength;
    public int m_iAttributeBonusDexterity;
    public int m_iAttributeBonusIntelligence;
    public int m_iAttributeBonusFaith;

    [Header("Attack Values")]
    public int m_iPhysicalDamage;
    public int m_iMagicDamage;
    public int m_iFireDamage;
    public int m_iLightningDamage;
    public int m_iDarkDamage;
    public int m_iCriticalDamage;
    public int m_iMagicAdjustment;
    public int m_iAttackRange;

    [Header("Damage Reduction %")]
    public float m_iPhysicalDamageReduction;
    public float m_iMagicDamageReduction;
    public float m_iFireDamageReduction;
    public float m_iLightningDamageReduction;
    public float m_iDarkDamageReduction;
    public int m_iStability;

    [Header("Auxiliary Effects")]
    public int m_iBleeding;
    public int m_iPoison;
    public int m_iFrost;
    public int m_iCurse;

    [Header("Others")]
    public int m_iDurability;
    public float m_iWeight;

    [Header("Damage Modifiers")]
    public float lightAttackDamageModifier = 1;
    public float heavyAttackDamgeModifier = 1.5f;
    public float m_fOHAttackDamageModifire = 1;
    public float m_fTHAttackDamageModifire = 1.5f;
    public int criticalDamagemuiltiplier = 4;
    public float guardBreakModifier = 1;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseBonus;

    [Header("Absorption")]
    public float physicalBlockingDamageAbsorption;
    public float fireBlockingDamageAbsorption;

    [Header("Stamina Costs")]
    public int baseStaminaCost;
    public float lightAttackStaminaMultiplier = 1;
    public float heavyAttackStaminaMultiplier = 2;
    public float m_fOHAttackStaminaMultiplier = 1;
    public float m_fTHAttackStaminaMultiplier = 1.5f;

    [Header("Item Actions")]
    public ItemAction oh_tap_RB_Action;
    public ItemAction oh_hold_RB_Action;
    public ItemAction oh_tap_RT_Action;
    public ItemAction oh_hold_RT_Action;
    public ItemAction oh_tap_LB_Action;
    public ItemAction oh_hold_LB_Action;
    public ItemAction oh_tap_LT_Action;
    public ItemAction oh_hold_LT_Action;

    [Header("Two Handed Item Actions")]
    public ItemAction th_tap_RB_Action;
    public ItemAction th_hold_RB_Action;
    public ItemAction th_tap_RT_Action;
    public ItemAction th_hold_RT_Action;
    public ItemAction th_tap_LB_Action;
    public ItemAction th_hold_LB_Action;
    public ItemAction th_tap_LT_Action;
    public ItemAction th_hold_LT_Action;

    [Header("Battle Weapon State")]
    public bool canBeBeffued = true;

    [Header("SOUND FX")]
    public List<AudioClip> m_SwrodWeaponWhooshes = new List<AudioClip>();
    public List<AudioClip> m_listBlockingNoises = new List<AudioClip>();
    public List<AudioClip> m_listWeaponChargeSounds = new List<AudioClip>();
    public List<AudioClip> m_listBowWhooshes = new List<AudioClip>();

    [Header("Equip State")]
    public bool m_isLeftHandEquiping = false;
    public E_EquipmentSlotsPartType m_SecondE_EquipmentSlotsPartType;

    void SetAttackAction(WeaponType m_eWeaponType)
    {
        switch (m_eWeaponType)
        {
            case WeaponType.Daggers:
            case WeaponType.StraightSwords:
            case WeaponType.Greatswords:
            case WeaponType.UltraGreatswords:
            case WeaponType.CurvedSword:
            case WeaponType.Katanas:
            case WeaponType.CurvedGreatswords:
            case WeaponType.PiercingSwords:
            case WeaponType.Axes:
            case WeaponType.Greataxes:
            case WeaponType.Hammers:
            case WeaponType.GreatHammers:
            case WeaponType.FistAndClaws:
            case WeaponType.SpearsAndPikes:
            case WeaponType.Halberds:
            case WeaponType.Reapers:
            case WeaponType.Whips:
            case WeaponType.Staves:
                oh_tap_RB_Action = new LightAttackAction();
                oh_hold_RB_Action = new CriticalAttackAction();
                oh_tap_RT_Action = new HeavyAttackAction();
                oh_hold_RT_Action = new ChargeAttackActon();

                th_tap_RB_Action = new LightAttackAction();
                th_hold_RB_Action = new CriticalAttackAction();
                th_tap_RT_Action = new HeavyAttackAction();
                th_hold_RT_Action = new ChargeAttackActon();

                oh_tap_LB_Action = new LightAttackAction();
                oh_hold_LB_Action = new CriticalAttackAction();
                oh_tap_LT_Action = new HeavyAttackAction();
                oh_hold_LT_Action = new ChargeAttackActon();

                th_tap_LB_Action = new LightAttackAction();
                th_hold_LB_Action = new CriticalAttackAction();
                th_tap_LT_Action = new HeavyAttackAction();
                th_hold_LT_Action = new ChargeAttackActon();
                break;
            case WeaponType.Bows:
            case WeaponType.Greatbows:
            case WeaponType.Crossbows:
                oh_tap_RB_Action = new FireArrowAction();
                oh_hold_RB_Action = new DrawArrowAction();
                oh_hold_LB_Action = new AimAction();

                th_tap_RB_Action = new FireArrowAction();
                th_hold_RB_Action = new DrawArrowAction();
                th_hold_LB_Action = new AimAction();


                oh_tap_LB_Action = new FireArrowAction();
                oh_hold_LB_Action = new DrawArrowAction();
                oh_hold_RB_Action = new AimAction();

                th_tap_LB_Action = new FireArrowAction();
                th_hold_LB_Action = new DrawArrowAction();
                th_hold_RB_Action = new AimAction();
                break;
            case WeaponType.Flames:
                oh_tap_RB_Action = new PyromancySpellAction();
                oh_tap_LB_Action = new PyromancySpellAction();
                break;
            case WeaponType.Talismans:
                oh_tap_RB_Action = new MiracleSpellAction();
                oh_tap_LB_Action = new MiracleSpellAction();
                break;
            case WeaponType.SacredChimes:
                break;
            case WeaponType.SmallShield:
            case WeaponType.Shield:
                oh_hold_LB_Action = new BlockingAction();
                oh_tap_LT_Action = new ParryAction();

                oh_hold_RB_Action = new BlockingAction();
                oh_tap_RT_Action = new ParryAction();
                break;
            default:
                break;
        }
    }

    public void SetAnimatorControllerOver(WeaponType m_eWeaponType)
    {
        switch (m_eWeaponType)
        {
            case WeaponType.Daggers:
            case WeaponType.StraightSwords:
            case WeaponType.Greatswords:
            case WeaponType.UltraGreatswords:
            case WeaponType.CurvedSword:
            case WeaponType.Katanas:
            case WeaponType.CurvedGreatswords:
            case WeaponType.PiercingSwords:
            case WeaponType.Axes:
            case WeaponType.Greataxes:
            case WeaponType.Hammers:
            case WeaponType.GreatHammers:
            case WeaponType.FistAndClaws:
            case WeaponType.SpearsAndPikes:
            case WeaponType.Halberds:
            case WeaponType.Reapers:
            case WeaponType.Whips:
            case WeaponType.Staves:
            case WeaponType.Flames:
            case WeaponType.Talismans:
            case WeaponType.SacredChimes:
                weaponController = Managers.Resource.Load<AnimatorOverrideController>("Animations/HumonoidAnimator/Straight Sword Controller");
                break;
            case WeaponType.Bows:
            case WeaponType.Greatbows:
            case WeaponType.Crossbows:
                weaponController = Managers.Resource.Load<AnimatorOverrideController>("Animations/HumonoidAnimator/Bow Controller");
                break;
            case WeaponType.SmallShield:
            case WeaponType.Shield:
                weaponController = Managers.Resource.Load<AnimatorOverrideController>("Animations/HumonoidAnimator/Middle Shield Controller");
                break;
            default:
                break;
        }
    }

    void SetAttackSound(WeaponType m_eWeaponType)
    {
        AudioClip clip = null;

        switch (m_eWeaponType)
        {
            case WeaponType.Daggers:
            case WeaponType.StraightSwords:
            case WeaponType.Greatswords:
            case WeaponType.UltraGreatswords:
            case WeaponType.CurvedSword:
            case WeaponType.Katanas:
            case WeaponType.CurvedGreatswords:
            case WeaponType.PiercingSwords:
            case WeaponType.Axes:
            case WeaponType.Greataxes:
            case WeaponType.Hammers:
            case WeaponType.GreatHammers:
            case WeaponType.FistAndClaws:
            case WeaponType.SpearsAndPikes:
            case WeaponType.Halberds:
            case WeaponType.Reapers:
            case WeaponType.Whips:
            case WeaponType.Staves:
                clip = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/mixkit-dagger-woosh-1487");
                if(clip != null)
                   m_SwrodWeaponWhooshes.Add(clip);
                clip = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/ixkit-fast-sword-whoosh-2792");
                if(clip != null)
                  m_SwrodWeaponWhooshes.Add(clip);
                break;
            case WeaponType.Bows:
            case WeaponType.Greatbows:
            case WeaponType.Crossbows:
                break;
            case WeaponType.Flames:
                break;
            case WeaponType.Talismans:
                break;
            case WeaponType.SacredChimes:
                break;
            case WeaponType.SmallShield:
            case WeaponType.Shield:
                break;
            default:
                break;
        }
    }

    void Typeclassification(WeaponType m_eWeaponType)
    {
        switch (m_eWeaponType)
        {
            case WeaponType.Daggers:
            case WeaponType.StraightSwords:
            case WeaponType.Greatswords:
            case WeaponType.UltraGreatswords:
            case WeaponType.CurvedSword:
            case WeaponType.Katanas:
            case WeaponType.CurvedGreatswords:
            case WeaponType.PiercingSwords:
            case WeaponType.Axes:
            case WeaponType.Greataxes:
            case WeaponType.Hammers:
            case WeaponType.GreatHammers:
            case WeaponType.FistAndClaws:
            case WeaponType.SpearsAndPikes:
            case WeaponType.Halberds:
            case WeaponType.Reapers:
            case WeaponType.Whips:
            case WeaponType.Staves:
                m_eItemType = E_ItemType.MeleeWeapon;
                break;
            case WeaponType.Bows:
            case WeaponType.Greatbows:
            case WeaponType.Crossbows:
                m_eItemType = E_ItemType.RangeWeapon;
                break;
            case WeaponType.Flames:
            case WeaponType.Talismans:
                m_eItemType = E_ItemType.Catalyst;
                break;
            case WeaponType.SacredChimes:
                m_eItemType = E_ItemType.MeleeWeapon;
                break;
            case WeaponType.SmallShield:
            case WeaponType.Shield:
                m_eItemType = E_ItemType.Shield;
                break;
            default:
                break;
        }
    }

    void SetBaseStaminaCost(WeaponType m_eWeaponType)
    {
        switch (m_eWeaponType)
        {
            case WeaponType.Daggers:
            case WeaponType.StraightSwords:
            case WeaponType.Greatswords:
            case WeaponType.UltraGreatswords:
            case WeaponType.CurvedSword:
            case WeaponType.Katanas:
            case WeaponType.CurvedGreatswords:
            case WeaponType.PiercingSwords:
            case WeaponType.Axes:
            case WeaponType.Greataxes:
            case WeaponType.Hammers:
            case WeaponType.GreatHammers:
            case WeaponType.FistAndClaws:
            case WeaponType.SpearsAndPikes:
            case WeaponType.Halberds:
            case WeaponType.Reapers:
            case WeaponType.Whips:
            case WeaponType.Staves:
                baseStaminaCost = 10;
                break;
            case WeaponType.Bows:
            case WeaponType.Greatbows:
            case WeaponType.Crossbows:
                baseStaminaCost = 15;

                break;
            case WeaponType.Flames:
                baseStaminaCost = 10;

                break;
            case WeaponType.Talismans:
                baseStaminaCost = 10;

                break;
            case WeaponType.SacredChimes:
                break;
            case WeaponType.SmallShield:
            case WeaponType.Shield:
                baseStaminaCost = 10;

                break;
            default:
                break;
        }
    }

    public void SetSound(E_WeaponBuffType buffClass = E_WeaponBuffType.Physical)
    {
        m_SwrodWeaponWhooshes.Clear();

        if(buffClass == E_WeaponBuffType.Physical)
        {
            switch (m_eWeaponType)
            {
                case WeaponType.Daggers:
                case WeaponType.StraightSwords:
                case WeaponType.Greatswords:
                case WeaponType.UltraGreatswords:
                case WeaponType.CurvedSword:
                case WeaponType.Katanas:
                case WeaponType.CurvedGreatswords:
                case WeaponType.PiercingSwords:
                case WeaponType.Axes:
                case WeaponType.Greataxes:
                case WeaponType.Hammers:
                case WeaponType.GreatHammers:
                case WeaponType.FistAndClaws:
                case WeaponType.SpearsAndPikes:
                case WeaponType.Halberds:
                case WeaponType.Reapers:
                case WeaponType.Whips:
                    {
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Daggers_WeaponWhooshe_01");
                            m_SwrodWeaponWhooshes.Add(audio);
                        }
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/StraightSwords_WeaponWhooshe_01");
                            m_SwrodWeaponWhooshes.Add(audio);
                        }
                    }

                    break;
                case WeaponType.Bows:
                case WeaponType.Greatbows:
                case WeaponType.Crossbows:
                    break;
                case WeaponType.Staves:
                case WeaponType.Flames:
                case WeaponType.Talismans:
                case WeaponType.SacredChimes:
                    break;
                case WeaponType.SmallShield:
                case WeaponType.Shield:
                    {
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Shield/Shield_Block_01");
                            m_listBlockingNoises.Add(audio);
                        }
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Shield/Shield_Block_02");
                            m_listBlockingNoises.Add(audio);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
        else if (buffClass == E_WeaponBuffType.Fire)
        {
            switch (m_eWeaponType)
            {
                case WeaponType.Daggers:
                case WeaponType.StraightSwords:
                case WeaponType.Greatswords:
                case WeaponType.UltraGreatswords:
                case WeaponType.CurvedSword:
                case WeaponType.Katanas:
                case WeaponType.CurvedGreatswords:
                case WeaponType.PiercingSwords:
                case WeaponType.Axes:
                case WeaponType.Greataxes:
                case WeaponType.Hammers:
                case WeaponType.GreatHammers:
                case WeaponType.FistAndClaws:
                case WeaponType.SpearsAndPikes:
                case WeaponType.Halberds:
                case WeaponType.Reapers:
                case WeaponType.Whips:
                    {
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Fire_WeaponWhooshe_01");
                            m_SwrodWeaponWhooshes.Add(audio);
                        }
                    }

                    break;
                case WeaponType.Bows:
                case WeaponType.Greatbows:
                case WeaponType.Crossbows:
                    break;
                case WeaponType.Staves:
                case WeaponType.Flames:
                case WeaponType.Talismans:
                case WeaponType.SacredChimes:
                    break;
                case WeaponType.SmallShield:
                case WeaponType.Shield:
                    {
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Shield/Shield_Block_01");
                            m_listBlockingNoises.Add(audio);
                        }
                        {
                            AudioClip audio = Managers.Resource.Load<AudioClip>("Sounds/Effect/Item/Weapon/Shield/Shield_Block_02");
                            m_listBlockingNoises.Add(audio);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
