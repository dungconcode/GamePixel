using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawn : MonoBehaviour
{
    public static TestEnemySpawn Instance { get; private set; }

    [SerializeField] public List<SpawnAreaData> spawnAreas = new(); // danh sách khu vực spawn

    private Coroutine spawnCoroutine;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //public void StartSpawnEnemy()
    //{
    //    if (!isSpawning)
    //    {
    //        isSpawning = true;
    //        spawnCoroutine = StartCoroutine(SpawnWavesLoop());
    //    }
    //}
    public void StartSpawnInArea(SpawnAreaData area)
    {
        BoxTrigger roomCollider = area.spawnArea.GetComponent<BoxTrigger>();
        if (area.activeEnimies.Count == 0 && roomCollider.isPlayerInside)
        {
            StartCoroutine(SpawnWavesInArea(area));
        }    
    }
    public void StopSpawnEnemy(SpawnAreaData area)
    {
        StartCoroutine(DelayClearSpawn(area));
    }
    IEnumerator DelayClearSpawn(SpawnAreaData area)
    {
        yield return new WaitForSeconds(8f);
        for (int i = area.activeEnimies.Count - 1; i >= 0; i--)
        {
            var enemy = area.activeEnimies[i];
            if (enemy != null)
            {
                Destroy(enemy);
            }
            area.activeEnimies.RemoveAt(i);
        }
    }
    private IEnumerator SpawnWavesInArea(SpawnAreaData area)
    {
        foreach (var wave in area.enemyWaves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                Vector2 pos = GetRandomSpawnPosition(area.spawnArea);
                GameObject enemy = Instantiate(wave.enemyPrefab, pos, Quaternion.identity);
                Enemy_Patrol patrol = enemy.GetComponent<Enemy_Patrol>();
                if (patrol != null)
                {
                    patrol.Initialize(pos); // Khởi tạo AI với vị trí spawn và vùng di chuyển
                }
                area.activeEnimies.Add(enemy);
                
                Enemy_Health deathHandler = enemy.GetComponent<Enemy_Health>();
                if (deathHandler != null)
                {
                    
                    deathHandler.onDeath += () => OnEnemyDeath(area, enemy);
                }

                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void OnEnemyDeath(SpawnAreaData area, GameObject enemy)
    {
        if (this == null || enemy == null) return;

        if (area.activeEnimies.Contains(enemy))
        {
            area.activeEnimies.Remove(enemy);
        }
        if(area.activeEnimies.Count == 0)
        {
            Debug.Log("All enemies in area " + area.areaName + " have been defeated!");
            StartSpawnInArea(area);
        }
    }

    //private IEnumerator SpawnWavesLoop()
    //{
    //    while (true) // vô hạn wave
    //    {
    //        foreach (var area in spawnAreas)
    //        {
    //            foreach (var wave in area.enemyWaves)
    //            {
    //                for (int i = 0; i < wave.count; i++)
    //                {
    //                    Vector2 pos = GetRandomSpawnPosition(area.spawnArea);
    //                    GameObject enemy = Instantiate(wave.enemyPrefab, pos, Quaternion.identity);
    //                    spawnedEnemies.Add(enemy);
    //                    yield return new WaitForSeconds(0.2f);
    //                }
    //            }
    //        }
    //        yield return new WaitForSeconds(3f); // delay giữa các wave
    //    }
    //}

    private Vector2 GetRandomSpawnPosition(BoxCollider2D area)
    {
        if (area == null)
        {
            Debug.LogWarning("Spawn area is null, using (0,0) as fallback!");
            return Vector2.zero; // fallback tránh crash
        }
        Vector2 min = area.bounds.min;
        Vector2 max = area.bounds.max;
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        return new Vector2(x, y);
    }
}
