using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UpdateController : MonoBehaviour
{
    public Player_Index characterSO;  // Kéo vào từ Inspector
    public CharacterData runtimeData { get; private set;}
    private PlayerHealth playerHealth;
    private void Awake()
    {
        runtimeData = SaveFileJson.LoadCharacterData(characterSO.characterID, characterSO);
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        playerHealth = PlayerHealth.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            runtimeData.hp += 1;
            SaveFileJson.SaveCharacterData(runtimeData);
            playerHealth.LoadIndex();
            Debug.Log("HP increased: " + runtimeData.hp);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            runtimeData.armor += 5;
            SaveFileJson.SaveCharacterData(runtimeData);
            playerHealth.LoadIndex();
            Debug.Log("Armor increased: " + runtimeData.armor);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            runtimeData.hp = characterSO.hp;
            runtimeData.armor = characterSO.armor;
            Debug.Log("Reset");
            SaveFileJson.SaveCharacterData(runtimeData);
            playerHealth.LoadIndex();
        }
    }
}