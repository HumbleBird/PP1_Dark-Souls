using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    [SerializeField] PlayerManager player;

    [Header("Save Data Writer")]
    SaveGameDataWriter saveGameDataWriter;

    [Header("Current Character Data")]
    public CharacterSaveData currentCharacterSaveData;
    [SerializeField]  private string fileName;

    [Header("Save/Load")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(saveGame)
        {
            saveGame = false;
            SaveGame();
        }
        else if (loadGame)
        {
            loadGame = false;
            // Load Save Data
        }
    }

    // New Game

    // Save Game
    public void SaveGame()
    {
        saveGameDataWriter = new SaveGameDataWriter();
        saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveGameDataWriter.dataSaveFileName = fileName;

        // 문자 데이터를 현재 저장 파일로 전달
        player.SaveCharacterdataToCurrentSaveData(ref currentCharacterSaveData);

        // 현재 캐릭터 데이터를 Json File로 작성중, 그리고 저장을 이 저장장치에 함.
        saveGameDataWriter.WriteCharacterDataToSaveFile(currentCharacterSaveData);

        Debug.Log("SAVING SAME...?");
        Debug.Log("fILE SAVE AS: " + fileName);

    }

    // Load Game
}
