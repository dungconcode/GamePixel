using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropGold : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefabs;

    [Header("Coin Count")]
    public int minCoin = 1;
    public int maxCoin = 3;

    [Header("Coin Value")]
    public int minValue = 1;
    public int maxValue = 2;

    [Header("Spawn")]
    public float radius = 0.5f;

    public void DropGold()
    {
        int coinCount = Random.Range(minCoin, maxCoin + 1);
        for (int i = 0; i < coinCount; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * radius;
            GameObject gold = Instantiate(goldPrefabs, spawnPos, Quaternion.identity);
            GoldPickup goldPickup = gold.GetComponent<GoldPickup>();
            if (goldPickup != null)
            {
                goldPickup.amount = Random.Range(minValue, maxValue + 1);
            }
        }
    }
}
