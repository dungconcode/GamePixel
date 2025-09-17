using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private List<IEnemyTick> enemyTickList = new List<IEnemyTick>();
    private List<IEnemyPatrolTick> enemyPathTickList = new List<IEnemyPatrolTick>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void LateUpdate()
    {
        foreach (var enemy in enemyPathTickList)
        {
            enemy.OnPatrolTick();
        }
        foreach (var enemy in enemyTickList)
        {
            enemy.Ontick();
        }
        
    }
    public void Register(IEnemyTick enemy)
    {
        if (!enemyTickList.Contains(enemy))
        {
            enemyTickList.Add(enemy);
        }
    }
    public void Unregister(IEnemyTick enemy)
    {
        if (enemyTickList.Contains(enemy))
        {
            enemyTickList.Remove(enemy);
        }
    }
    public void RegisterPatrol(IEnemyPatrolTick enemy)
    {
        if (!enemyPathTickList.Contains(enemy))
        {
            enemyPathTickList.Add(enemy);
        }
    }
    public void UnregisterPatrol(IEnemyPatrolTick enemy)
    {
        if (enemyPathTickList.Contains(enemy))
        {
            enemyPathTickList.Remove(enemy);
        }
    }
}
