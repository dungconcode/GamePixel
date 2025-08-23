using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileJson
{
    public static void SaveCharacterData(CharacterData data)
    {
        string path = Path.Combine(Application.persistentDataPath, data.characterID + ".json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Character data saved" + data.characterID + " to " + path);
    }
    public static CharacterData LoadCharacterData(string characterID, Player_Index ind)
    {
        string path = Path.Combine(Application.persistentDataPath, characterID + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<CharacterData>(json);
        }
        else
        {
            Debug.LogWarning("No save found, creating new data from SO");
            CharacterData newData = new CharacterData(ind);
            SaveCharacterData(newData); // tạo file luôn
            return newData;
        }
    }
}
