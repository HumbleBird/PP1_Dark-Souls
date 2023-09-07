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

    private void Awake()
    {
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
        Debug.Log("Bonfire Interactecd with");

        if(hasBeenActivated)
        {
            // 지역 이동ㅈ
        }
        else
        {
            playermanager.playerAnimatorManager.PlayTargetAnimation("Bonfire_Activate", true);
            playermanager.m_GameUIManager.ActivateBonfirePopup();
            hasBeenActivated = true;
            interactableText = "Rest";
            activationFX.gameObject.SetActive(true);
            activationFX.Play();
            fireFX.gameObject.SetActive(true);
            fireFX.Play();
            Managers.Sound.Play(bonfireActivationSoundFX);
        }
    }

}
