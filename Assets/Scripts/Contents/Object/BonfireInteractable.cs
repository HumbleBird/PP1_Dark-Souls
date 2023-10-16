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
            // 주변 지역 뿌옇게
            // 지역 이동
            // Spell 저장
            // 에스트 강화
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

    // 일정 범위 내에 들어오면 BonFire 사운드 틀기
}
