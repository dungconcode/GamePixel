using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonHandler : MonoBehaviour
{
    [SerializeField]private GameObject currentCharacter;

    void Start()
    {
        // Tự động tìm GameObject có tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.GetComponent<IAttackable>() != null)
        {
            currentCharacter = player;
        }
    }

    public void OnAttackButtonPressed()
    {
        currentCharacter?.GetComponent<IAttackable>()?.Attack();
    }
}
