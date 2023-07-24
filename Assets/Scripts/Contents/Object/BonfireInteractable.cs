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

    AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerManager playermanager)
    {
        Debug.Log("Bonfire Interactecd with");

        if(hasBeenActivated)
        {

        }
        else
        {
            playermanager.playerAnimatorManager.PlayerTargetAnimation("Bonfire_Activate", true);
            playermanager.uiManager.ActivateBonfirePopup();
            hasBeenActivated = true;
            interactableText = "Rest";
            activationFX.gameObject.SetActive(true);
            activationFX.Play();
            fireFX.gameObject.SetActive(true);
            fireFX.Play();
            audioSource.PlayOneShot(bonfireActivationSoundFX);
        }
    }

}
