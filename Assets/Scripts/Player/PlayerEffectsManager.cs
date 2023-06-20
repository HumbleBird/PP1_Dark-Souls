using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour
{
    PlayerStatus playerStatus;
    WeaponSlotManager weaponSlotManager;

    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;

    private void Awake()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    public void HealPlayerFromEffect()
    {
        playerStatus.HealPlayer(amountToBeHealed);
        GameObject healParticles = Instantiate(currentParticleFX, playerStatus.transform);
        Destroy(instantiatedFXModel.gameObject);
        weaponSlotManager.LoadBothWeaponsOnSlots();
    }
}
