using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Animator Replacer")]
    public AnimatorOverrideController weaponController;
    //public string offHandIdleAnimation = "Left_Arm_Idle_01";

    [Header("Weapon Type")]
    public WeaponType weaponType;
    public string AttackType;
    public string Skill;
    public int FocusCost;

    [Header("Requirement")] // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iRequirementStrength;
    public int m_iRequirementDexterity;
    public int m_iRequirementIntelligence;
    public int m_iRequirementFaith;

    [Header("Parameter Bonus")] // 능력치 보정 // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iParameterBonusStrength;
    public int m_iParameterBonusDexterity;
    public int m_iParameterBonusIntelligence;
    public int m_iParameterBonusFaith;

    [Header("Attack Values")]
    public int m_iPhysicalDamage;
    public int m_iMagicDamage;
    public int m_iFireDamage;
    public int m_iLightningDamage;
    public int m_iDarkDamage;
    public int m_iCriticalDamage;
    public int m_iSpellBuff;
    public int m_iRange;

    [Header("Damage Reduction %")]
    public int m_iPhysicalDamageReduction;
    public int m_iMagicDamageReduction;
    public int m_iFireDamageReduction;
    public int m_iLightningDamageReduction;
    public int m_iDarkDamageReduction;
    public int m_iStability;

    [Header("Auxiliary Effects")]
    public int m_iBleeding;
    public int m_iPoison;
    public int m_iFrost;
    public int m_iCurse;

    [Header("Others")]
    public int m_iDurability;
    public int m_iWeight;

    [Header("Damage Modifiers")]
    public float lightAttackDamageModifier;
    public float heavyAttackDaamgeModifier;
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
    public float lightAttackStaminaMultiplier;
    public float heavyAttackStaminaMultiplier;

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

    [Header("SOUND FX")]
    public List<AudioClip> weaponWhooshes;
    public List<AudioClip> blockingNoises;


}
