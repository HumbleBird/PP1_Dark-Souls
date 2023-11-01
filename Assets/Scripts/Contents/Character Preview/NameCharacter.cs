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
        player.playerStatsManager.m_sCharacterName = inputField.text;

        if(player.characterStatsManager.m_sCharacterName == "")
        {
            player.playerStatsManager.m_sCharacterName = "Nameless";
        }

        nameButtonText.text = player.playerStatsManager.m_sCharacterName;
    }
}
