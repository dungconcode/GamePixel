using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Trap/CircleBullet")]
public class CircleBullet : ScriptableObject, ITrapEffect
{
    [SerializeField] private GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletCount = 12;
    public void ApplyEffect(Vector3 position)
    {
        SpawnBullets(position);
    }

    private void SpawnBullets(Vector3 position)
    {
        int angleStep = 360 / bulletCount;
        float angle = Random.Range(0f,360f);

        for (int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 moveDir = new Vector2(dirX, dirY).normalized;
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = moveDir * bulletSpeed;
            }
            angle += angleStep;
        }
    }

}
