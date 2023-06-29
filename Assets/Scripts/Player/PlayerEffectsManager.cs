using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    PlayerStatsManager playerStatsManager;
    PlayerWeaponSlotManager playerWeaponSlotManager;

    PoisonBuildUpBar poisonBuildUpBar;
    PoisonAmountBar poisonAmountBar;

    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;

    protected override void Awake()
    {
        base.Awake();

        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();

        poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
        poisonAmountBar = FindObjectOfType<PoisonAmountBar>();
    }

    public void HealPlayerFromEffect()
    {
        playerStatsManager.HealPlayer(amountToBeHealed);
        if(currentParticleFX != null)
        {
            GameObject healParticles = Instantiate(currentParticleFX, playerStatsManager.transform);
        }
        Destroy(instantiatedFXModel.gameObject);
        playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }

    protected override void HandleIsPoisonedEffect()
    {
        if(poisonBuildup <= 0)
        {
            poisonBuildUpBar.gameObject.SetActive(false);
        }
        else
        {
            poisonBuildUpBar.gameObject.SetActive(true);
        }

        base.HandleIsPoisonedEffect();
        poisonBuildUpBar.SetPoisonBuildUpAmount(Mathf.RoundToInt(poisonBuildup));
    }

    protected override void HandlePoisonBuildUp()
    {
        if (isPoisoned == false)
        {
            poisonAmountBar.gameObject.SetActive(false);
        }
        else
        {
            poisonAmountBar.gameObject.SetActive(true);
        }

        base.HandlePoisonBuildUp();
        poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(poisonAmount));

    }
}
