using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSelected : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterEvent.OnStartGame += SpawnPlayer;
    }
    private void OnDisable()
    {
        CharacterEvent.OnStartGame -= SpawnPlayer;
    }
    private void SpawnPlayer()
    {
        if(GameData.selectedCharacter != null)
        {
            var prefab = Instantiate(GameData.selectedCharacter.characterPrefab, Vector3.zero, Quaternion.identity);
            Transform playerChild = prefab.transform.Find("Player");
            if (playerChild != null)
            {
                playerChild.position = GameData.selectedCharacter.characterIcon.transform.position;
            }
            Destroy(GameData.selectedCharacter.characterIcon);
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }
}
