using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[CreateAssetMenu(menuName ="Items/Consumables/Cure Effect Clump")]
public class ToolItem_Consumable : ToolItem
{
    public ToolItem_Consumable(int id)
    {
        SetInfo(id);
    }

    public ToolItem_Consumable()
    {
    }

    #region AttemptToConsumeItem
    public override void AttemptToConsumeItem(PlayerManager player)
    {
        base.AttemptToConsumeItem(player);

        if(m_iItemID == 1) // Estus Flask
        {
            Attempt_EstusFlask(player);
        }

        else if (m_iItemID == 2) // Fire Buff Item
        {
            Attempt_BuffItem(player);
        }

        else if(m_iItemID == 4) // PuppleMossClump
        {
            Attempt_PuppleMossClump(player);
        }



        if (m_iItemID == 2) // Fire Buff Item
        {
        }
        else
        {
            player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
        }
    }

    void Attempt_EstusFlask(PlayerManager player)
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

    void Attempt_PuppleMossClump(PlayerManager player)
    {
        GameObject clump = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);
        player.playerEffectsManager.instantiatedFXModel2 = clump;

        if (m_iItemID == 4) // PuppleMossClump
        {
            player.playerStatsManager.poisonBuildup = 0;
            player.playerStatsManager.isPoisoned = false;
            Managers.GameUI.m_GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);

            if (player.playerEffectsManager.currentParticleFX != null)
            {
                Destroy(player.playerEffectsManager.currentParticleFX);
            }
        }
    }

    void Attempt_BuffItem(PlayerManager player)
    {
        // 아이템 사용 불가시, 아무것도 하지 않기
        if (!CanUseThisItem(player))
            return;

        player.playerAnimatorManager.PlayTargetAnimation(m_sPlayAnimationName, isInteracting, true);
        Managers.Sound.Play("Item/Weapon/Buff_Ambient_Fire");
    }

    #endregion

    #region SucessToConsumeItem
    public override void SucessToConsumeItem(PlayerManager player)
    {
        base.SucessToConsumeItem(player);

        if (m_iItemID == 2) // Fire Buff Item
        {
            SucessBuffItem(player);
        }
    }

    void SucessBuffItem(PlayerManager player)
    {
        // Sound
        AudioClip buffTriggerSound = Managers.Resource.Load<AudioClip>("Item/Weapon/Fire_WeaponWhooshe_01");
        Managers.Sound.Play(buffTriggerSound);

        // Effect
        WeaponBuffEffect weaponBuffEffect = new WeaponBuffEffect(E_WeaponBuffType.Fire);
        weaponBuffEffect.isRightHandedBuff = true;
        player.playerEffectsManager.rightWeaponBuffEffect = weaponBuffEffect;
        player.playerEffectsManager.ProcessWeaponBuffs();
    }

    #endregion

    public override bool CanUseThisItem(PlayerManager player)
    {
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
