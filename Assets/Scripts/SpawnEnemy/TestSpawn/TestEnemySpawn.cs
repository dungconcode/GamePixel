using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawn : MonoBehaviour
{
    public static TestEnemySpawn Instance { get; private set; }

    [SerializeField] public List<SpawnAreaData> spawnAreas = new(); // danh sách khu vực spawn

    private Coroutine spawnCoroutine;

    [SerializeField] private GameObject indicatorAnimator;
    private float warningDuration = 0.2f;
    private readonly Dictionary<SpawnAreaData, Coroutine> _clearJobs = new();
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
        CancelClearJob(area);
        BoxTrigger roomCollider = area.spawnArea.GetComponent<BoxTrigger>();
        if (area.activeEnimies.Count == 0 && roomCollider.isPlayerInside)
        {
            StartCoroutine(SpawnWavesInArea(area));
        }    
    }
    public void StopSpawnEnemy(SpawnAreaData area)
    {
        if (_clearJobs.ContainsKey(area)) return;
        _clearJobs[area] = StartCoroutine(DelayClearSpawn(area));
    }
    private void CancelClearJob(SpawnAreaData area)
    {
        if (_clearJobs.TryGetValue(area, out var co))
        {
            if (co != null) StopCoroutine(co);
            _clearJobs.Remove(area);
        }
    }
    IEnumerator DelayClearSpawn(SpawnAreaData area)
    {
        yield return new WaitForSeconds(5f);
        for (int i = area.activeEnimies.Count - 1; i >= 0; i--)
        {
            var enemy = area.activeEnimies[i];
            if (enemy != null)
            {
                Destroy(enemy);
            }
            area.activeEnimies.RemoveAt(i);
        }
        _clearJobs.Remove(area);
    }
    private IEnumerator SpawnWavesInArea(SpawnAreaData area)
    {
        foreach (var wave in area.enemyWaves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                Vector2 pos = GetRandomSpawnPosition(area.spawnArea);
                if(indicatorAnimator != null)
                {
                    GameObject warning = Instantiate(indicatorAnimator, pos, Quaternion.identity);
                    Destroy(warning, 0.5f);
                }
                yield return new WaitForSeconds(warningDuration);

                GameObject enemy = Instantiate(wave.enemyPrefab, pos, Quaternion.identity);
                Enemy_Health enemyhp = enemy.GetComponent<Enemy_Health>();
                if (enemyhp != null)
                {
                    enemyhp.maxHealth = 10 + area.levelEnemy;
                }

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
    private IEnumerator DelayRespawn(SpawnAreaData area, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartSpawnInArea(area);
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
            //Debug.Log("All enemies in area " + area.areaName + " have been defeated!");
            StartCoroutine(DelayRespawn(area, 5f));
        }
    }


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
