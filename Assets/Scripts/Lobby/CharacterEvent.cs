using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CharacterEvent
{
    public static Action<Character_ID> OnCharacterSelected;
    public static Action OnBackPressed;
    public static Action OnStartGame;
    public static Action<CharacterData> OnStatsUpdated;

    public static Action<int, int, int> OnLevelUpFailedNotEnoughGold;
    public static Action<int, int, int> OnLevelUpSuccess;
}
