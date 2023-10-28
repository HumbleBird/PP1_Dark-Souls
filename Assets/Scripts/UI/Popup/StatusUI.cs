using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : UI_Popup
{
    enum Texts
    {
        // Left- Property
        PlayerNameText,
        CurrentConvenantText,
        PlayerLevelText,
        SoulsText,
        RequiredSoulsText,

        // Left- Attributes
        VigorValueText,
        AttunementValueText,
        EnduranceValueText,
        VitalityValueText,
        StrengthValueText,
        DexterityValueText,
        IntelligenceValueText,
        FaithValueText,
        LuckValueText,

        // Middle - Base Power
        HPValueText,
        FPValueText,
        StaminaValueText,
        EquipLoadValueText,
        PoiseValueText,
        ItemDiscoveryValueText,

        // Middle - Attack power
        RWeapon1ValueText,
        RWeapon2ValueText,
        RWeapon3ValueText,
        LWeapon1ValueText,
        LWeapon2ValueText,
        LWeapon3ValueText,

        // Right - Defense / Absorption
        PhysicaDefenseAbsorptionlValueText,
        VSStrikeDefenseAbsorptionValueText,
        VSSlashDefenseAbsorptionValueText,
        VSThrustDefenseAbsorptionValueText,
        MagicDefenseAbsorptionValueText,
        FireDefenseAbsorptionValueText,
        LightningDefenseAbsorptionValueText,
        DarkDefenseAbsorptionValueText,

        // Right - Resistances / Armor
        BleedResistancesArmorValueText,
        PoisonResistancesArmorValueText,
        FrostResistancesArmorValueText,
        CurseResistancesArmorValueText,
        AttunementSlotsValueText,
        
    }

    enum Images
    {
        CurrentSpellImage
    }


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        PlayerManager player = Managers.Object.m_MyPlayer;

        // Left- Property
        GetText((int)Texts.PlayerNameText).text = player.playerStatsManager.characterName;

        if (player.playerEquipmentManager.m_CurrentPledge != null)
        {
            GetText((int)Texts.CurrentConvenantText).text = player.playerEquipmentManager.m_CurrentPledge.name;
        }
        else
        {
            GetText((int)Texts.CurrentConvenantText).text = "";
        }

        GetText((int)Texts.PlayerLevelText).text = player.playerStatsManager.playerLevel.ToString();
        GetText((int)Texts.SoulsText).text = player.playerStatsManager.currentSoulCount.ToString();
        GetText((int)Texts.RequiredSoulsText).text = "00"; // 무엇을 의미?

        // Left- Attributes
        GetText((int)Texts.VigorValueText        ).text = player.playerStatsManager.m_iVigorLevel        .ToString();
        GetText((int)Texts.AttunementValueText   ).text = player.playerStatsManager.m_iAttunementLevel   .ToString();
        GetText((int)Texts.EnduranceValueText    ).text = player.playerStatsManager.m_iEnduranceLevel    .ToString();
        GetText((int)Texts.VitalityValueText     ).text = player.playerStatsManager.m_iVitalityLevel     .ToString();
        GetText((int)Texts.StrengthValueText     ).text = player.playerStatsManager.m_iStrengthLevel     .ToString();
        GetText((int)Texts.DexterityValueText    ).text = player.playerStatsManager.m_iDexterityLevel    .ToString();
        GetText((int)Texts.IntelligenceValueText ).text = player.playerStatsManager.m_iIntelligenceLevel .ToString();
        GetText((int)Texts.FaithValueText        ).text = player.playerStatsManager.m_iFaithLevel        .ToString();
        GetText((int)Texts.LuckValueText).text = player.playerStatsManager.m_iLuckLevel         .ToString();

        // Middle - Base Power
        GetText((int)Texts.HPValueText).text = string.Format("{0, 0} / {1, 10}", player.playerStatsManager.currentHealth, player.playerStatsManager.maxHealth);
        GetText((int)Texts.FPValueText).text = string.Format("{0, 0} / {1, 10}", player.playerStatsManager.currentFocusPoints, player.playerStatsManager.maxfocusPoint);
        GetText((int)Texts.StaminaValueText).text = player.playerStatsManager.maxStamina.ToString();
        GetText((int)Texts.EquipLoadValueText).text = string.Format("{0, 0} / {1, 10}", player.playerStatsManager.m_CurrentEquipLoad.ToString("0.00"), player.playerStatsManager.m_MaxEquipLoad.ToString("0.00")); ;
        GetText((int)Texts.PoiseValueText).text = player.playerStatsManager.totalPoiseDefence.ToString("0.00");
        GetText((int)Texts.ItemDiscoveryValueText).text = player.playerStatsManager.m_iItemDiscovery.ToString();

        // Middle - Attack power
        GetText((int)Texts.RWeapon1ValueText).text = player.playerStatsManager.m_iRWeapon1.ToString();
        GetText((int)Texts.RWeapon2ValueText).text = player.playerStatsManager.m_iRWeapon2.ToString();
        GetText((int)Texts.RWeapon3ValueText).text = player.playerStatsManager.m_iRWeapon3.ToString();
        GetText((int)Texts.LWeapon1ValueText).text = player.playerStatsManager.m_iLWeapon1.ToString();
        GetText((int)Texts.LWeapon2ValueText).text = player.playerStatsManager.m_iLWeapon2.ToString();
        GetText((int)Texts.LWeapon3ValueText).text = player.playerStatsManager.m_iLWeapon3.ToString();

        // Right - Defense / Absorption
        GetText((int)Texts.PhysicaDefenseAbsorptionlValueText ).text = string.Format("{0, 0} / {1, 52}", 129, player.playerStatsManager.m_fPhysicalDamageAbsorption .ToString("0.000"));
        GetText((int)Texts.VSStrikeDefenseAbsorptionValueText ).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fVSStrikeDamageAbsorption  .ToString("0.000"));
        GetText((int)Texts.VSSlashDefenseAbsorptionValueText  ).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fVSSlashDamageAbsorption   .ToString("0.000"));
        GetText((int)Texts.VSThrustDefenseAbsorptionValueText ).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fVSThrustDamageAbsorption  .ToString("0.000"));
        GetText((int)Texts.MagicDefenseAbsorptionValueText    ).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fMagicDamageAbsorption     .ToString("0.000"));
        GetText((int)Texts.FireDefenseAbsorptionValueText     ).text = string.Format("{0, 0} / {1, 52}", 129, player.playerStatsManager.m_fFireDamageAbsorption     .ToString("0.000"));
        GetText((int)Texts.LightningDefenseAbsorptionValueText).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fLightningDamageAbsorption .ToString("0.000"));
        GetText((int)Texts.DarkDefenseAbsorptionValueText     ).text = string.Format("{0, 0} / {1, 52}", 00, player.playerStatsManager .m_fDarkDamageAbsorption.ToString("0.000"));


        // Right - Resistances / Armor
        GetText((int)Texts.BleedResistancesArmorValueText).text = string.Format("{0, 0} / {1, 52}", player.playerStatsManager.m_iBleedResistance, player.playerStatsManager.m_fBleedArmor); // 캐릭터 스텟 저항, 값옷
        GetText((int)Texts.PoisonResistancesArmorValueText).text = string.Format("{0, 0} / {1, 52}", player.playerStatsManager.m_iPoisonResistance, player.playerStatsManager.m_iPoisoArmore);
        GetText((int)Texts.FrostResistancesArmorValueText).text = string.Format("{0, 0} / {1, 52}", player.playerStatsManager.m_iFrostResistance, player.playerStatsManager.m_fFrostArmor);
        GetText((int)Texts.CurseResistancesArmorValueText).text = string.Format("{0, 0} / {1, 52}", player.playerStatsManager.m_iCurseResistance, player.playerStatsManager.m_fCurseArmor);

        GetText((int)Texts.AttunementSlotsValueText).text = player.playerStatsManager.m_iAttunementSlots.ToString();

        if (player.playerEquipmentManager.m_CurrentHandSpell != null)
        {
            GetImage((int)Images.CurrentSpellImage).enabled = true;
            GetImage((int)Images.CurrentSpellImage).sprite = player.playerEquipmentManager.m_CurrentHandSpell.itemIcon;
        }
        else
        {
            GetImage((int)Images.CurrentSpellImage).enabled = false;
        }


        return true;
    }
}
