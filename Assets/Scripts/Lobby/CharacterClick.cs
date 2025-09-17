using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClick : MonoBehaviour
{
    public int characterID;

    private void OnMouseDown()
    {
        LobbyCharacterManager lcm = FindObjectOfType<LobbyCharacterManager>();
        Character_ID c = System.Array.Find(lcm.characters, character => character.characterID == characterID);
        if(c != null && c.isUnlocked)
        {
            CharacterEvent.OnCharacterSelected?.Invoke(c);
        }
            
    }
}
