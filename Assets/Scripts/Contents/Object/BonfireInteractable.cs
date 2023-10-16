using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireInteractable : Interactable
{
    [Header("Bonfire Teleport Transform")]
    public Transform bonfireTeleportTransform;

    [Header("Activatetion Status")]
    public bool hasBeenActivated;

    [Header("Bonfire FX")]
    public ParticleSystem activationFX;
    public ParticleSystem fireFX;
    public AudioClip bonfireActivationSoundFX;

    protected override void Awake()
    {
        base.Awake();

        if(hasBeenActivated)
        {
            fireFX.gameObject.SetActive(true);
            fireFX.Play();
            interactableText = "Rest";
        }
        else
        {
            interactableText = "Light Bonfire";
        }

    }

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);
        if(hasBeenActivated)
        {
            // �ֺ� ���� �ѿ���
            // ���� �̵�
            // Spell ����
            // ����Ʈ ��ȭ
        }
        else
        {
            playermanager.playerAnimatorManager.PlayTargetAnimation("Bonfire_Activate", true);
            Managers.Resource.Instantiate("UI/Popup/BonfireLitPopupUI");
            hasBeenActivated = true;
            interactableText = "Rest";
            activationFX.gameObject.SetActive(true);
            activationFX.Play();
            fireFX.gameObject.SetActive(true);
            fireFX.Play();
            Managers.Sound.Play(bonfireActivationSoundFX);
        }
    }

    // ���� ���� ���� ������ BonFire ���� Ʋ��
}
