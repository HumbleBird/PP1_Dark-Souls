using System;
using System.IO;
using UnityEngine;

public class SaveGameDataWriter
{
    public string saveDataDirectoryPath = "";
    public string dataSaveFileName = "";

    public CharacterSaveData LoadCharacterdataFromJson()
    {
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);

        CharacterSaveData loadedSaveData = null;

        if(File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";

                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    using (StreamReader reder = new StreamReader(stream))
                    {
                        saveDataToLoad = reder.ReadToEnd();
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
        else
        {
            Debug.Log("SAVE FILE DOES NOT EXIST");
        }

        return loadedSaveData;
    }

    public void WriteCharacterDataToSaveFile(CharacterSaveData characterData)
    {
        // 세이브 파일 경로 생성
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("SAVE PATH = " + savePath);

            string dataToStore = JsonUtility.ToJson(characterData, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("ERROR WHILE TRYING TO SAVE DATA, GAME COULD NOT BE SAVED" + e);
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, dataSaveFileName));
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, dataSaveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
