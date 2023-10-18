using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSouls : Interactable
{
    public int m_iSoulsCount;

    protected override void Awake()
    {
        base.Awake();

        interactableText = "Recover lost souls"; // ÀÒ¾î¹ö¸° ¼Ò¿ïÀ» µÇÃ£´Â´Ù
    }

    public override void Interact(PlayerManager playermanager)
    {
        base.Interact(playermanager);

        // souls restrived ÆË¾÷ ¶ÙÀ¨
        Managers.Object.m_MyPlayer.m_GameSceneUI.ShowSoulsRetrieved();

        // sound
        // ¼Ò¿ï ³Ö±â
        Managers.Object.m_MyPlayer.playerStatsManager.AddSouls(m_iSoulsCount);

        Managers.Resource.Destroy(gameObject);
    }
}
