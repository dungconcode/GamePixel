using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public CharacterData runtimeData;
    private Player_Index currentCharacterSO;

    private void OnEnable()
    {
        CharacterEvent.OnCharacterSelected += SetCurrentCharacter;
    }
    private void OnDisable()
    {
        CharacterEvent.OnCharacterSelected -= SetCurrentCharacter;
    }
    private void SetCurrentCharacter(Character_ID characterID)
    {
        currentCharacterSO = characterID.player_Index;
        runtimeData = SaveFileJson.LoadCharacterData(currentCharacterSO.characterID, currentCharacterSO);
    }
    public void LevelUP()
    {
        if (runtimeData == null)
        {
            Debug.LogWarning("runtimeData is null, cannot level up.");
            return;
        }
        runtimeData.level += 1;
        switch(runtimeData.level)
        {
            case 1:
                runtimeData.hp += 1;
                break;
            case 2:
                runtimeData.armor += 2;
                break;
            case 3:
                runtimeData.mana += 20;
                break;
            case 4:
                runtimeData.damage += 3;
                break;
            case 5:
                runtimeData.hp += 2;
                runtimeData.armor += 2;
                break;
            default:
                Debug.Log("Max level");
                break;
        }
        SaveFileJson.SaveCharacterData(runtimeData);
        CharacterEvent.OnStatsUpdated?.Invoke(runtimeData);
    }
    
}
