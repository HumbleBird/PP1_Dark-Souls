using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager
{
    [Header("Save Data Writer")]
    SaveGameDataWriter saveGameDataWriter;

    [Header("Current Character Data")]
    public CharacterSaveData currentCharacterSaveData = new CharacterSaveData();
    [SerializeField]  private string fileName;

    // New Game

    // Save Game
    public void SaveGame()
    {
        saveGameDataWriter = new SaveGameDataWriter();
        saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveGameDataWriter.dataSaveFileName = fileName;

        // ���� �����͸� ���� ���� ���Ϸ� ����
        Managers.Object.m_MyPlayer.SaveCharacterdataToCurrentSaveData(ref currentCharacterSaveData);

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

        //StartCoroutine(LoadSceneAsynchronously());
    }

    private IEnumerator LoadSceneAsynchronously()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(0);

        while(!loadOperation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            // ȭ�� �ε� ����
            // UI ���� Progress�ٷ� ����� ǥ��
            yield return null;
        }

        Managers.Object.m_MyPlayer.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);

    }
}
