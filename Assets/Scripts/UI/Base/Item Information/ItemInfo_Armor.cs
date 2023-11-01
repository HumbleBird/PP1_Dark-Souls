using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Armor : ItemInfo_Base
{
    enum Texts
    {
        // Property
        Armor_ItemNameText,
        Armor_EquipLoadValueText,
        Armor_DurabilityValueText,

        // Absorption
        Armor_AbsorptionPhysicalValueText,
        Armor_AbsorptionVsStrikeValueText,
        Armor_AbsorptionVsSlashValueText,
        Armor_AbsorptionVsThrustValueText,
        Armor_AbsorptionMagicValueText,
        Armor_AbsorptionFireValueText,
        Armor_AbsorptionLightningValueText,
        Armor_AbsorptionDarkValueText,


        // Resistance
        Armor_ResistanceBleedValueText,
        Armor_ResistancePoisonValueText,
        Armor_ResistanceFrostValueText,
        Armor_ResistanceCurseValueText,
        Armor_ResistancePoiseValueText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        return true;
    }


    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        ShowItem((EquipmentItem)item);
    }

    void ShowItem(EquipmentItem item)
    {
        GetText((int)Texts.Armor_ItemNameText).text = item.m_ItemName;
        GetText((int)Texts.Armor_EquipLoadValueText).text = item.m_fWeight.ToString();
        GetText((int)Texts.Armor_DurabilityValueText).text = item.m_fDurability.ToString();


        GetText((int)Texts.Armor_AbsorptionPhysicalValueText ).text = item.m_fPhysicalDamageAbsorption  .ToString();
        GetText((int)Texts.Armor_AbsorptionVsStrikeValueText ).text = item.m_fVSStrikeDamageAbsorption  .ToString();
        GetText((int)Texts.Armor_AbsorptionVsSlashValueText  ).text = item.m_fVSSlashDamageAbsorption   .ToString();
        GetText((int)Texts.Armor_AbsorptionVsThrustValueText ).text = item.m_fVSThrustDamageAbsorption  .ToString();
        GetText((int)Texts.Armor_AbsorptionMagicValueText    ).text = item.m_fMagicDamageAbsorption     .ToString();
        GetText((int)Texts.Armor_AbsorptionFireValueText     ).text = item.m_fFireDamageAbsorption      .ToString();
        GetText((int)Texts.Armor_AbsorptionLightningValueText).text = item.m_fLightningDamageAbsorption .ToString();
        GetText((int)Texts.Armor_AbsorptionDarkValueText).text =      item.m_fDarkDamageAbsorption.ToString();

        GetText((int)Texts.Armor_ResistanceBleedValueText ).text = item.m_fBleedResistance .ToString();
        GetText((int)Texts.Armor_ResistancePoisonValueText).text = item.m_fPoisonResistance.ToString();
        GetText((int)Texts.Armor_ResistanceFrostValueText ).text = item.m_fFrostResistance .ToString();
        GetText((int)Texts.Armor_ResistanceCurseValueText ).text = item.m_fCurseResistance .ToString();
        GetText((int)Texts.Armor_ResistancePoiseValueText).text = item.m_fPoiseResistance.ToString();

    }
}