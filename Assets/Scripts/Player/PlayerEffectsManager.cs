using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    PlayerManager player;

    PoisonBuildUpBar poisonBuildUpBar;
    PoisonAmountBar poisonAmountBar;

    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel2;
    public int amountToBeHealed;

    // Instance Effect (Taking Damage, Add Build Up, ect)
    // Static Effects (Ring Effect, Armor Effet ect)
    // Timed/Status Effect (Poison Build Up, curse, Toxic ect)


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

        poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
        poisonAmountBar = FindObjectOfType<PoisonAmountBar>();
    }

    public void HealPlayerFromEffect()
    {
        player.playerStatsManager.HealCharacter(amountToBeHealed);
        if(currentParticleFX != null)
        {
            GameObject healParticles = Instantiate(currentParticleFX, player.playerStatsManager.transform);
        }
        Destroy(instantiatedFXModel2.gameObject);
        player.playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }


}
