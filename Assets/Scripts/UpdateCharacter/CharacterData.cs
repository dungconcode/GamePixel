using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CharacterData
{
    public string characterID;
    public float hp;
    public float armor;
    public float mana;
    public float damage;

    public event Action OnStatsChanged;
    public CharacterData() { }
    public CharacterData(Player_Index ind)
    {
        Debug.Log("Creating CharacterData for " + ind.characterID);
        characterID = ind.characterID;
        hp = ind.hp;
        armor = ind.armor;
        mana = ind.mana;
        damage = ind.damage;
    }
    public void ModifyHP(float amount)
    {
        hp += amount;
        OnStatsChanged?.Invoke();
    }

    public void ModifyArmor(float amount)
    {
        armor += amount;
        OnStatsChanged?.Invoke();
    }
}
