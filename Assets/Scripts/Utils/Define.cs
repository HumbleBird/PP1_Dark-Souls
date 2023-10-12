using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region Item


    public enum E_ItemType
    {
        Tool = 1,
        ReinforcedMaterial,
        Valuables,
        Magic,
        MeleeWeapon,
        RangeWeapon,
        Catalyst,
        Shield,
        Helmet,
        ChestArmor,
        Gauntlets,
        Leggings,
        Ammo,
        Ring,
        Pledge
    }

    public enum ToolItemType
    {
        Unlimited,
        limited
    }

    public enum AmmoType
    {
        Arrow,
        Bolt
    }



    public enum WeaponType
    {
        PyroCaster,
        FaithCaster,
        SpellCaster,
        Unarmed,
        StraightSwords,
        SmallShield,
        Shield,
        Bow
    }

    public enum eWeaponTrail
    {
        Normal,
        Fire
    }

    public enum WeaponType2
    {
        Daggers = 1,
        StraightSwords,
        Greatswords,
        UltraGreatswords,
        CurvedSword,
        Katanas,
        CurvedGreatswords,
        PiercingSwords,
        Axes,
        Greataxes,

        Hammers,
        GreatHammers,
        FistAndClaws,
        SpearsAndPikes,
        Halberds,
        Reapers,
        Whips,
        Bows,
        Greatbows,
        Crossbows,

        Staves,
        Flames,
        Talismans,
        SacredChimes,
        Shield = 25
    }
    #endregion

    #region Battle


    public enum EncumbranceLevel
    {
        Light,
        Medium,
        Heavy,
        Overloaded
    }

    public enum EffectParticleType
    {
        Poison,
        Bleed,
        Curse,
        Frost
    }

    public enum BuffClass
    {
        Physical,
        Fire
    }

    public enum Damagetype
    {
        Physical,
        Fire
    }

    public enum AICombatStyle
    { 
        swordAndShield,
        Archer
    }

    public enum AIAttackActionType
    { 
        meleeAttackAction,
        magicAttackACtion,
        rangedAttackaCtion,
    }


    public enum AttackType
    {
        light,
        heavy
    }

    public enum E_CharacterClass
    {
        Knight = 0,
        Mercenary,
        Warrior,
        Herald,
        Thief,
        Assassin,
        Sorcerer,
        Pyromancer,
        Cleric,
        Deprived,
        MaxCount
    }

    #endregion

    #region Character Eternal Appearance
    public enum ExternalFeaturesColorParts
    {
        Hair,
        FacialMask,
        Skin
    }

    public enum E_CharacterCreationPreviewCamera
    {
        None,
        Head,
        Chest,
        Leg,
        Hand,
        Back,
        All
    }

    public enum All_GenderItemPartsType
    {
        // Head
        HeadCoverings_Base_Hair,
        HeadCoverings_No_FacialHair,
        HeadCoverings_No_Hair,
        Hair,
        HelmetAttachment,

        // Torso
        Chest_Attachment,
        Back_Attachment,
        Shoulder_Attachment_Right,
        Shoulder_Attachment_Left,

        // Hand
        Elbow_Attachment_Right,
        Elbow_Attachment_Left,

        // Leg
        Hips_Attachment,
        Knee_Attachement_Right,
        Knee_Attachement_Left,

        // Extra
        Extra_Elf_Ear,
    }

    public enum E_SingleGenderEquipmentArmorParts
    {
        Head,
        Head_No_Elements,
        Eyebrow,
        FacialHair,
        Torso,
        Arm_Upper_Right,
        Arm_Upper_Left,
        Arm_Lower_Right,
        Arm_Lower_Left,
        Hand_Right,
        Hand_Left,
        Hip,
        LeftLegging,
        RightLegging,
    }

    #endregion

    #region UI
    public enum E_StatUI
    {
        Hp,
        Stamina,
        FocusPoint,

        // 특수
        Posion,


        All,
    }

    public enum E_CameraShowPartType
    {
        Head,
        Chest,
        Leg,
        Hand,
        All
    }

    public enum E_EquipmentSlotsPartType
    {
        Right_Hand_Weapon,
        Left_Hand_Weapon,

        Arrow,
        Bolt,

        Helmt,
        Chest_Armor,
        Gantlets,
        Leggings,

        Ring,

        Consumable,

        Pledge,
    }


    #endregion

    #region Other

    public enum E_TeamId
    {
        Player = 0,
        Monster = 1,
    }

    public enum E_RandomSoundType
    {
        Damage,
        Block,
        WeaponWhoose
    }

    public enum Scene
    {
        Unknown = 0,
        Start = 1,
        Lobby = 2,
        Game = 3,
    }

    public enum Sound
    {
        Bgm = 0,
        Effect = 1,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        
    }

    public enum CursorType
    {
        None,
        Arrow,
        Hand,
        Look,
    }
    #endregion
}
