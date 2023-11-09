using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Draw Arrow Action")]
public class DrawArrowAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;
        if (player != null)
        {
            if (player.playerStatsManager.currentStamina <= 0)
                return;
        }

        if (character.isInteracting)
            return;

        if (character.isHoldingArrow)
            return;

        // ���� ��� ������ ȭ���� ���ٸ� �����ϴ� �ִϸ��̼� �� ����
        // �� ���Կ� UI ǥ��

        //Animator Player
        character.animator.SetBool("isHoldingArrow", true);
        character.characterAnimatorManager.PlayTargetAnimation("Bow_TH_Draw_01", true);

        // Instantiate Arrow
        GameObject loadedArrow = Instantiate(character.characterEquipmentManager.m_CurrentHandAmmo.loadedItemModel, character.characterWeaponSlotManager.rightHandSlot.transform);
        character.characterEffectsManager.instantiatedFXModel = loadedArrow;

        //Animate the bow
        Animator bowAnimator = character.characterWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", true);
        bowAnimator.Play("BowObject_TH_Draw_01");

        // Sound
        Managers.Sound.Play("Item/Weapon/Bow/Bow_Draw_01");
    }
}
