using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRepository : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public string characterID;
        public SkillData[] skills = new SkillData[3];
    }
    [SerializeField] private Entry[] entries;
    private Dictionary<string, SkillData[]> skillDictionary = new Dictionary<string, SkillData[]>();
    private void Awake()
    {
        foreach (var entry in entries)
        {
            skillDictionary[entry.characterID] = entry.skills;
        }
    }
    public SkillData[] GetSkills(string characterID)
    {
        if (skillDictionary.TryGetValue(characterID, out var skills))
        {
            return skills;
        }
        else
        {
            Debug.LogWarning($"No skills found for character ID: {characterID}");
            return new SkillData[0];
        }
    }
}
