using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/Weapon Buff")]
public class WeaponBuffConsumableItem : ToolItem
{
    [Header("Effect")]
    [SerializeField] WeaponBuffEffect weaponBuffEffect;

    [Header("Buff SFX")]
    [SerializeField] AudioClip buffTriggerSound;

    public override void AttemptToConsumeItem(PlayerManager player)
    {
        // 아이템 사용 불가시, 아무것도 하지 않기
        if (!CanUseThisItem(player))
            return;

        if (currentItemAmount > 0)
        {
            player.playerAnimatorManager.PlayTargetAnimation(consumeAnimation, isInteracting, true);
        }
        else
        {
            player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }


    }

    public override void SucessToConsumeItem(PlayerManager player)
    {
        base.SucessToConsumeItem(player);

        Managers.Sound.Play(buffTriggerSound);

        WeaponBuffEffect weaponBuff = Instantiate(weaponBuffEffect);
        weaponBuff.isRightHandedBuff = true;
        player.playerEffectsManager.rightWeaponBuffEffect = weaponBuff;
        player.playerEffectsManager.ProcessWeaponBuffs();
    }

    public override bool CanUseThisItem(PlayerManager player)
    {
        if (player.playerEquipmentManager.m_CurrentHandConsumable.currentItemAmount <= 0)
            return false;

        MeleeWeaponItem meleeWeapon = player.playerEquipmentManager.m_CurrentHandRightWeapon as MeleeWeaponItem;

        if(meleeWeapon != null && meleeWeapon.canBeBeffued)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
