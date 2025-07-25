﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance { get; private set; } // Singleton instance
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private BoxCollider2D spawnArea;
    private float spawnInterval = 2f; // Khoảng thời gian giữa các lần spawn
    private float enemyPerWave = 5f;
    public float currentEnemyCount = 0; // Biến để đếm số lượng enemy đã spawn
    private bool isSpawning = false; // Biến để kiểm tra xem đã spawn chưa
    private Coroutine spawnCoroutine;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this; // Thiết lập singleton instance
        }
    }
    public void StartSpawnEnemy()
    {
        
        if(!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnWave());
        }
    }
    private IEnumerator SpawnWave()
    {
        currentEnemyCount = 0;
        for (int i = 0; i < enemyPerWave; i++)
        {
            SpawnEnemy();
            currentEnemyCount++;
            yield return new WaitForSeconds(0.1f);
        }
         // Chờ trước khi spawn wave tiếp theo
        isSpawning = false; // wave đã spawn xong
    }
    public void EnemyDied(GameObject enemy)
    {
        if(spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            currentEnemyCount--; // Giảm số lượng enemy đã spawn
            if (currentEnemyCount <= 0)
            {
                currentEnemyCount = 0; // Đảm bảo không âm
                StartCoroutine(SpawnNewWave()); // Bắt đầu spawn wave mới nếu không còn enemy nào
                
            }
            Destroy(enemy); // Xóa enemy khỏi danh sách và hủy đối tượng
        }
    }
    private IEnumerator SpawnNewWave()
    {
        yield return new WaitForSeconds(spawnInterval); // Chờ một chút trước khi spawn wave mới
        StartSpawnEnemy();
    }
    public void StopSpawnEnemy()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        foreach (var enemy in spawnedEnemies)
        {
            if(enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();

    }
    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(enemy);
    }
    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        return new Vector2(x, y);
    }
}
