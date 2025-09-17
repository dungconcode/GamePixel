using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnGame : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject spawnAnimation;
    private void Start()
    {
        if (GameData.selectedCharacter != null)
        {
            var prefab = Instantiate(GameData.selectedCharacter.characterPrefab, spawnPoint.position, Quaternion.identity);
            Transform playerChild = prefab.transform.Find("Player");
            if (playerChild != null)
            {
                playerChild.position = spawnPoint.position;
            }
            Destroy(GameData.selectedCharacter.characterIcon);
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }

    }
    IEnumerator DelaySpawnAnim()
    {
        GameObject animInstance = null;

        if (spawnAnimation != null)
        {
            animInstance = Instantiate(spawnAnimation, spawnPoint.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.8f);

        if (animInstance != null)
        {
            Destroy(animInstance); 
        }

        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        if (GameData.selectedCharacter != null)
        {
            var prefab = Instantiate(GameData.selectedCharacter.characterPrefab, spawnPoint.position, Quaternion.identity);
            Transform playerChild = prefab.transform.Find("Player");
            if (playerChild != null)
            {
                playerChild.position = spawnPoint.position;
            }
            Destroy(GameData.selectedCharacter.characterIcon);
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }    
}
