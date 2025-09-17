using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    private Vector2 spawnPoint;
    private EnemySpawn enemySpawn;

    public void Initialize(Vector2 point, EnemySpawn spawnManager)
    {
        spawnPoint = point;
        enemySpawn = spawnManager;
    }
    public void OnIndicatorComplete()
    {
        if(enemySpawn != null)
        {
            //enemySpawn.SpawnEnemyAtPoint(spawnPoint);
        }
        Destroy(gameObject);
    }
}
