using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    public PlayerManager player;

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
            LoadGame();
        }
    }

    // New Game

    // Save Game
    public void SaveGame()
    {
        saveGameDataWriter = new SaveGameDataWriter();
        saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveGameDataWriter.dataSaveFileName = fileName;

        // ���� �����͸� ���� ���� ���Ϸ� ����
        player.SaveCharacterdataToCurrentSaveData(ref currentCharacterSaveData);

        // ���� ĳ���� �����͸� Json File�� �ۼ���, �׸��� ������ �� ������ġ�� ��.
        saveGameDataWriter.WriteCharacterDataToSaveFile(currentCharacterSaveData);

        Debug.Log("SAVING SAME...?");
        Debug.Log("fILE SAVE AS: " + fileName);

    }

    // Load Game
    public void LoadGame()
    {
        // ĳ���� ���̺� ���Կ� ����Ͽ� �ε� ���� ����

        saveGameDataWriter = new SaveGameDataWriter();
        saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveGameDataWriter.dataSaveFileName = fileName;
        currentCharacterSaveData = saveGameDataWriter.LoadCharacterdataFromJson();

        StartCoroutine(LoadSceneAsynchronously());
    }

    private IEnumerator LoadSceneAsynchronously()
    {
        if(player == null)
        {
            player = FindObjectOfType < PlayerManager>();
        }


        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(0);

        while(!loadOperation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            // ȭ�� �ε� ����
            // UI ���� Progress�ٷ� ����� ǥ��
            yield return null;
        }

        player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);

    }
}
