using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public CharacterData runtimeData;
    private Player_Index currentCharacterSO;

    private static int[] LEVEL_COST = { 1, 200, 300, 400, 500 };
    private int MAXLEVEL = LEVEL_COST.Length;
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
        CharacterEvent.OnStatsUpdated?.Invoke(runtimeData);
    }
    public bool IsMaxLevel() => runtimeData != null && runtimeData.level >= MAXLEVEL;
    public void LevelUP()
    {
        if (runtimeData == null)
        {
            Debug.LogWarning("runtimeData is null, cannot level up.");
            return;
        }
        int current = runtimeData.level;
        if(current >= MAXLEVEL)
        {
            Debug.Log("Already at max level.");
            return;
        }
        int nextLevel = current + 1;
        int cost = GetLevelCost(nextLevel);
        if(cost <= 0)
        {
            Debug.LogWarning("Invalid level cost.");
            return;
        }
        if(GoldService.Instance == null)
        {
            Debug.LogWarning("GoldService instance is null.");
            return;
        }
        if(!GoldService.Instance.TrySpend(cost))
        {
            Debug.Log($"Not enough gold to level up to {nextLevel}. Need {cost}, have {GoldService.Instance.TotalGold}.");
            //CharacterEvent.OnLevelUpFailedNotEnoughGold?.Invoke(nextLevel, cost, GoldService.Instance.TotalGold);
            return;
        }
        runtimeData.level = nextLevel;
        ApplyLevel(nextLevel);

        SaveFileJson.SaveCharacterData(runtimeData);
        CharacterEvent.OnStatsUpdated?.Invoke(runtimeData);

        //CharacterEvent.OnLevelUpSuccess?.Invoke(nextLevel, cost, GoldService.Instance.TotalGold);
        Debug.Log($"Leveled up to {runtimeData.level}. Spent {cost} gold.");
    }
    private int GetLevelCost(int currentlv)
    {
        if (currentlv < 1 || currentlv > MAXLEVEL) return 0;
        return LEVEL_COST[currentlv - 1];
    }
    public int GetNextLevelCost()
    {
        if(runtimeData == null) return 0;
        int nextlv = runtimeData.level + 1;
        if (nextlv < 1 || nextlv > MAXLEVEL) return 0;
        return LEVEL_COST[nextlv - 1];
    }
    private void ApplyLevel(int newlv)
    {
        switch(newlv)
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
    }
}
