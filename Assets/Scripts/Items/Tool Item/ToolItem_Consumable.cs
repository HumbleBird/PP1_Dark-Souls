using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Cure Effect Clump")]
public class ToolItem_Consumable : ToolItem
{
    public ToolItem_Consumable(int id)
    {
        SetInfo(id);
    }

    #region AttemptToConsumeItem
    public override void AttemptToConsumeItem(PlayerManager player)
    {
        base.AttemptToConsumeItem(player);

        if(m_iItemID == 1) // Estus Flask
        {
            EstusFlask(player);
        }

        else if (m_iItemID == 2) // Fire Buff Item
        {
            BuffItem(player);
        }

        else if(m_iItemID == 4) // PuppleMossClump
        {
            PuppleMossClump(player);
        }

        player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
    }

    void EstusFlask(PlayerManager player)
    {
        // Recovery Amount
        int healthRecoverAmount = 250;
        int focusPointsRecoverAmount = 100;

        GameObject flask = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);

        base.AttemptToConsumeItem(player);
        player.playerEffectsManager.amountToBeHealed = healthRecoverAmount;
        player.playerEffectsManager.instantiatedFXModel2 = flask;
        player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
    }

    void PuppleMossClump(PlayerManager player)
    {
        GameObject clump = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);
        player.playerEffectsManager.instantiatedFXModel2 = clump;

        if (m_iItemID == 4) // PuppleMossClump
        {
            player.playerStatsManager.poisonBuildup = 0;
            player.playerStatsManager.isPoisoned = false;

            if (player.playerEffectsManager.currentParticleFX != null)
            {
                Destroy(player.playerEffectsManager.currentParticleFX);
            }
        }
    }

    void BuffItem(PlayerManager player)
    {


        // 아이템 사용 불가시, 아무것도 하지 않기
        if (!CanUseThisItem(player))
            return;

        if (m_iCurrentCount > 0)
        {
            player.playerAnimatorManager.PlayTargetAnimation(m_sPlayAnimationName, isInteracting, true);
        }
        else
        {
            player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }

    #endregion

    #region SucessToConsumeItem
    public override void SucessToConsumeItem(PlayerManager player)
    {
        base.SucessToConsumeItem(player);

        if (m_iItemID == 2) // Fire Buff Item
        {
            BuffItem(player);
        }
    }

    void SucessBuffItem(PlayerManager player)
    {
        // Effect
        WeaponBuffEffect weaponBuffEffect;
        weaponBuffEffect = Managers.Resource.Load<WeaponBuffEffect>("Data/Character Effects/Fire Buff Weapon Effect");

        // Sound
        AudioClip buffTriggerSound;
        buffTriggerSound = Managers.Resource.Load<AudioClip>("Item/Weapon/Fire_WeaponWhooshe_01");

        Managers.Sound.Play(buffTriggerSound);

        WeaponBuffEffect weaponBuff = Instantiate(weaponBuffEffect);
        weaponBuff.isRightHandedBuff = true;
        player.playerEffectsManager.rightWeaponBuffEffect = weaponBuff;
        player.playerEffectsManager.ProcessWeaponBuffs();
    }

    #endregion

    public override bool CanUseThisItem(PlayerManager player)
    {
        if (player.playerEquipmentManager.m_CurrentHandConsumable.m_iCurrentCount <= 0)
            return false;

        if(m_iItemID == 2)
        {
            return CanUseBuffItem(player);
        }

        return false;
    }

    bool CanUseBuffItem(PlayerManager player)
    {
        if (player.playerEquipmentManager.m_CurrentHandRightWeapon != null && player.playerEquipmentManager.m_CurrentHandRightWeapon.canBeBeffued)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
