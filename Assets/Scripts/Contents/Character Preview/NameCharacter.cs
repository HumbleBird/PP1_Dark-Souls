using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameCharacter : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI nameButtonText; 

    public void NameMyCharacter()
    {
        PlayerManager player = Managers.Object.m_MyPlayer;
        player.playerStatsManager.characterName = inputField.text;

        if(player.characterStatsManager.characterName == "")
        {
            player.playerStatsManager.characterName = "Nameless";
        }

        nameButtonText.text = player.playerStatsManager.characterName;
    }
}
