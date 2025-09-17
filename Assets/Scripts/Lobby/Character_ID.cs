using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Character_ID
{
    public int characterID;
    public GameObject characterIcon;
    public GameObject characterPrefab;
    public bool isUnlocked;
    public Player_Index player_Index;
}
public static class GameData
{
    public static Character_ID selectedCharacter;
}
[System.Serializable]
public class  StartUI
{
    public string statName;
    public Image barFill;
    public TextMeshProUGUI valueTxt;
}
