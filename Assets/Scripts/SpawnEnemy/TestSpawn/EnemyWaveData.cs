using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveData
{
    public GameObject enemyPrefab;   // Prefab enemy
    public int count = 1;            // Số lượng enemy trong wave
}

[Serializable]
public class SpawnAreaData
{
    public string areaName = "Room"; // để dễ phân biệt
    public BoxCollider2D spawnArea;                // Khu vực spawn
    public List<EnemyWaveData> enemyWaves = new List<EnemyWaveData>(); // Danh sách loại enemy trong khu vực này

    [HideInInspector] public List<GameObject> activeEnimies = new List<GameObject>();
}
