using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyEntry
    {
        public GameObject prefab;
        public int count;
    }

    [System.Serializable]
    public class EnemyWave
    {
        public int waveIndex;
        public List<EnemyEntry> enemies = new List<EnemyEntry>();
    }

    [System.Serializable]
    public class EnemySpawnRule
    {
        public GameObject prefab;
        public int minCount;
        public int maxCount;
        public int unlockWave;
    }

    public bool endlessMode = false;
    public List<EnemyWave> waves = new List<EnemyWave>();
    public List<EnemySpawnRule> spawnRules = new List<EnemySpawnRule>();

    private int currentWave = 0;

    public void StartNextWave()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave);

        if (!endlessMode)
        {
            foreach (var wave in waves)
            {
                if (wave.waveIndex == currentWave)
                {
                    foreach (var entry in wave.enemies)
                    {
                        for (int i = 0; i < entry.count; i++)
                        {
                            SpawnEnemy(entry.prefab);
                        }
                    }
                }
            }
        }
        else
        {
            foreach (var rule in spawnRules)
            {
                if (currentWave >= rule.unlockWave)
                {
                    int count = Random.Range(rule.minCount, rule.maxCount + 1);
                    for (int i = 0; i < count; i++)
                    {
                        SpawnEnemy(rule.prefab);
                    }
                }
            }
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        if (prefab == null) return;
        Vector3 pos = transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
