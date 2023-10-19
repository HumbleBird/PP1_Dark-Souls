using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Armor : ItemInfo_Base
{
    new protected enum Texts
    {
        // Property
        ItemNameText,
        EquipLoadValueText,
        DurabilityValueText,

        // Absorption
        AbsorptionPhysicalValueText,
        AbsorptionVsStrikeValueText,
        AbsorptionVsSlashValueText,
        AbsorptionVsThrustValueText,
        AbsorptionMagicValueText,
        AbsorptionFireValueText,
        AbsorptionLightningValueText,
        AbsorptionDarkValueText,


        // Resistance
        ResistanceBleedValueText,
        ResistancePoisonValueText,
        ResistanceFrostValueText,
        ResistanceCurseValueText,
        ResistancePoiseValueText,
    }

    public override void ShowItemInfo(Item item)
    {
        base.ShowItemInfo(item);


        ShowItem((EquipmentItem)item);
    }

    void ShowItem(EquipmentItem item)
    {
        GetText((int)Texts.ItemNameText).text = item.itemName;
        GetText((int)Texts.EquipLoadValueText).text = item.m_fWeight.ToString();
        GetText((int)Texts.DurabilityValueText).text = item.m_fDurability.ToString();


        GetText((int)Texts.AbsorptionPhysicalValueText ).text = item.m_fPhysicalDefense  .ToString();
        GetText((int)Texts.AbsorptionVsStrikeValueText ).text = item.m_fVSStrikeDefense  .ToString();
        GetText((int)Texts.AbsorptionVsSlashValueText  ).text = item.m_fVSSlashDefense   .ToString();
        GetText((int)Texts.AbsorptionVsThrustValueText ).text = item.m_fVSThrustDefense  .ToString();
        GetText((int)Texts.AbsorptionMagicValueText    ).text = item.m_fMagicDefense     .ToString();
        GetText((int)Texts.AbsorptionFireValueText     ).text = item.m_fFireDefense      .ToString();
        GetText((int)Texts.AbsorptionLightningValueText).text = item.m_fLightningDefense .ToString();
        GetText((int)Texts.AbsorptionDarkValueText).text =      item.m_fDarkDefense.ToString();

        GetText((int)Texts.ResistanceBleedValueText ).text = item.m_fBleedResistance .ToString();
        GetText((int)Texts.ResistancePoisonValueText).text = item.m_fPoisonResistance.ToString();
        GetText((int)Texts.ResistanceFrostValueText ).text = item.m_fFrostResistance .ToString();
        GetText((int)Texts.ResistanceCurseValueText ).text = item.m_fCurseResistance .ToString();
        GetText((int)Texts.ResistancePoiseValueText ).text = item.m_fPoiseResistance.ToString();

    }
}