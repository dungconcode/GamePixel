using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    public bool isPlayerInside = false; // Biến để kiểm tra xem đã spawn chưa
    private Coroutine removeEnemyCoroutine;
    public RoomSpawner roomSpawner;
    public string targetAreaName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            var area = TestEnemySpawn.Instance.spawnAreas.Find(a => a.areaName == targetAreaName);
            if (area != null && isPlayerInside)
            {
                TestEnemySpawn.Instance.StartSpawnInArea(area);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Area: " + targetAreaName);
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            Enemy_Patrol patrol = collision.gameObject.GetComponent<Enemy_Patrol>();
            if (patrol != null)
            {
                patrol.isEnemyInRoom = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            var area = TestEnemySpawn.Instance.spawnAreas.Find(a => a.areaName == targetAreaName);
            if (area != null && !isPlayerInside)
            {
                TestEnemySpawn.Instance.StopSpawnEnemy(area);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ResetEnemyPostion(collision.gameObject));
            }
        }
    }
    private IEnumerator RemoveEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(!isPlayerInside) // Kiểm tra nếu không còn trong vùng trigger
        {
            EnemySpawn.Instance.StopSpawnEnemy(); // Gọi phương thức dừng spawn enemy
        }
    }
    private IEnumerator ResetEnemyPostion(GameObject enemy)
    {
        AI_Path aiPath = enemy.GetComponent<AI_Path>();
        Enemy_Patrol patrol = enemy.GetComponent<Enemy_Patrol>();
        patrol.isEnemyInRoom = false;
        if (aiPath != null)
        {
            if (!patrol.isEnemyInRoom)
            {
                aiPath.hasPatrolPoint = false;
                aiPath.isFrozen = true;
            }
        }
        yield return new WaitForSeconds(0.5f);
        if (aiPath != null)
        {
            aiPath.isFrozen = false;
        }
        
        if (patrol != null)
        {
            
            patrol.targetPatrol = transform.position;
            patrol.Initialize(transform.position);
        }
    }
}
