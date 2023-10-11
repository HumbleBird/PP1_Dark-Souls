using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    PlayerManager player;

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

    protected override void ProcessBuildUpDecay()
    {
        if(player.characterStatsManager.poisonBuildup >= 0)
        {
            player.characterStatsManager.poisonBuildup -= 1;

            if(player.GameSceneUI != null)
            {
                player.GameSceneUI.m_StatBarsUI.RefreshUI(Define.E_StatUI.Posion);
            }
        }
    }
}
