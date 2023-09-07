using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Equipment Model Changers")]
    public Dictionary<EquipmentArmorParts, ModelChanger> m_dicModelChanger = new Dictionary<EquipmentArmorParts, ModelChanger>();

    [Header("Naked Armor Equipment")]
    public HelmEquipmentItem Naked_HelmetEquipment;
    public TorsoEquipmentItem Naked_TorsoEquipment;
    public LeggingsEquipmentItem Naked_LegEquipment;
    public GantletsEquipmentItem Naked_HandEquipment;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        player.characterStatsManager.CalculateAndSetMaxEquipload();
    }

    public void EquipAllArmor()
    {
        // First All UnEquipment
        foreach (ModelChanger modelchanger in m_dicModelChanger.Values)
        {
            modelchanger.UnEquipAllEquipmentsModels();
        }


        float poisonResistance = 0;
        float totalEquipmentLoad = 0;

        // Helm
        if (player.playerInventoryManager.currentHelmetEquipment != null)
        {
            m_dicModelChanger[EquipmentArmorParts.Helm].EquipEquipmentsModelByName(player.playerInventoryManager.currentHelmetEquipment.m_HelmEquipmentItemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHelmetEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHelmetEquipment.m_fWeight;
        }
        else
        {
            m_dicModelChanger[EquipmentArmorParts.NoArmorHead].EquipEquipmentsModelByName(Naked_HelmetEquipment.m_HelmEquipmentItemName);
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;
        }

        // Torso
        if (player.playerInventoryManager.currentTorsoEquipment != null)
        {
            m_dicModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(player.playerInventoryManager.currentTorsoEquipment.m_TorsoEquipmentItemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = player.playerInventoryManager.currentTorsoEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentTorsoEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentTorsoEquipment.m_fWeight;
        }
        else
        {
            m_dicModelChanger[EquipmentArmorParts.Torso].EquipEquipmentsModelByName(Naked_TorsoEquipment.m_TorsoEquipmentItemName);
            player.playerStatsManager.physicalDamageAbsorptionBody = 0;
        }

        // Legs
        if (player.playerInventoryManager.currentLegEquipment != null)
        {
            m_dicModelChanger[EquipmentArmorParts.LeftLegging ].EquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.m_LeftLeggingName);
            m_dicModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.m_RightLeggingName);
            m_dicModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(player.playerInventoryManager.currentLegEquipment.m_HipName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = player.playerInventoryManager.currentLegEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentLegEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentLegEquipment.m_fWeight;
        }
        else
        {
            m_dicModelChanger[EquipmentArmorParts.LeftLegging ].EquipEquipmentsModelByName(Naked_LegEquipment.m_LeftLeggingName);
            m_dicModelChanger[EquipmentArmorParts.RightLegging].EquipEquipmentsModelByName(Naked_LegEquipment.m_RightLeggingName);
            m_dicModelChanger[EquipmentArmorParts.Hip].EquipEquipmentsModelByName(Naked_LegEquipment.m_HipName);
            player.playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }

        // Hands
        if (player.playerInventoryManager.currentHandEquipment != null)
        {
            m_dicModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_RightName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Upper_Left ].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Upper_LeftName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_RightName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Lower_Left ].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Arm_Lower_LeftName);
            m_dicModelChanger[EquipmentArmorParts.Hand_Right     ].EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_RightName);
            m_dicModelChanger[EquipmentArmorParts.Hand_Left]      .EquipEquipmentsModelByName(player.playerInventoryManager.currentHandEquipment.m_Hand_LeftName);
            player.playerStatsManager.physicalDamageAbsorptionHands = player.playerInventoryManager.currentHandEquipment.m_fPhysicalDefense;
            poisonResistance += player.playerInventoryManager.currentHandEquipment.m_fPoisonResistance;
            totalEquipmentLoad += player.playerInventoryManager.currentHandEquipment.m_fWeight;
        }
        else
        {
            m_dicModelChanger[EquipmentArmorParts.Arm_Upper_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_RightName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Upper_Left ].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Upper_LeftName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Lower_Right].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_RightName);
            m_dicModelChanger[EquipmentArmorParts.Arm_Lower_Left ].EquipEquipmentsModelByName(Naked_HandEquipment.m_Arm_Lower_LeftName);
            m_dicModelChanger[EquipmentArmorParts.Hand_Right     ].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_RightName);
            m_dicModelChanger[EquipmentArmorParts.Hand_Left].EquipEquipmentsModelByName(Naked_HandEquipment.m_Hand_LeftName);
            player.playerStatsManager.physicalDamageAbsorptionHands = 0;
        }

        player.playerStatsManager.poisonResistance = poisonResistance;
        player.playerStatsManager.CaculateAndSetCurrentEquipLoad(totalEquipmentLoad);
    }

}
