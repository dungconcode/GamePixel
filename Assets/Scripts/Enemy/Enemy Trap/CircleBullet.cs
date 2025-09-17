using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private void OnEnable()
    {
        Enemy_Health.OnEnemyDeath += HandleEnemyDeath;
    }
    private void OnDisable()
    {
        Enemy_Health.OnEnemyDeath -= HandleEnemyDeath;
    }
    private void HandleEnemyDeath(Vector3 position, EnemyType enemyType)
    {
        switch(enemyType)
        {
            case EnemyType.TrapCircleBullet:
                SpawnBulletCircle(position);
                break;
            default:
                break;
        }
    }
    private void SpawnBulletCircle(Vector3 position)
    {
        int bulletCount = 12;
        int angleStep = 360 / bulletCount;
        float angle = 0f;

        for(int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 moveDir = new Vector2(dirX, dirY).normalized;
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = moveDir * bulletSpeed;
            }
            angle += angleStep;
        }
    }    
}
