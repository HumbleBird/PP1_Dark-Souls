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

        // 문자 데이터를 현재 저장 파일로 전달
        Managers.Object.m_MyPlayer.SaveCharacterdataToCurrentSaveData(ref currentCharacterSaveData);

        // 현재 캐릭터 데이터를 Json File로 작성중, 그리고 저장을 이 저장장치에 함.
        saveGameDataWriter.WriteCharacterDataToSaveFile(currentCharacterSaveData);

        Debug.Log("SAVING SAME...?");
        Debug.Log("fILE SAVE AS: " + fileName);

    }

    // Load Game
    public void LoadGame()
    {
        // 캐릭터 세이브 슬롯에 기반하여 로드 파일 결정

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
            // 화면 로딩 가능
            // UI 상의 Progress바로 진행률 표시
            yield return null;
        }

        Managers.Object.m_MyPlayer.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);

    }
}
