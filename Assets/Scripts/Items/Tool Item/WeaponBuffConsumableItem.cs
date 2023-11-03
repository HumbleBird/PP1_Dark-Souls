using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/Weapon Buff")]
public class WeaponBuffConsumableItem : ToolItem
{
    //public WeaponBuffConsumableItem()
    //{
    //    weaponBuffEffect = Managers.Resource.Load<WeaponBuffEffect>("Data/Character Effects/Fire Buff Weapon Effect");
    //    buffTriggerSound = Managers.Resource.Load<AudioClip>("Item/Weapon/mixkit-fire-woosh-1348");
    //}

    [Header("Effect")]
    [SerializeField] WeaponBuffEffect weaponBuffEffect;

    [Header("Buff SFX")]
    [SerializeField] AudioClip buffTriggerSound;

    public override void AttemptToConsumeItem(PlayerManager player)
    {
        // ������ ��� �Ұ���, �ƹ��͵� ���� �ʱ�
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
        if (player.playerEquipmentManager.m_CurrentHandConsumable.m_iCurrentCount <= 0)
            return false;

        if(player.playerEquipmentManager.m_CurrentHandRightWeapon != null && player.playerEquipmentManager.m_CurrentHandRightWeapon.canBeBeffued)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
