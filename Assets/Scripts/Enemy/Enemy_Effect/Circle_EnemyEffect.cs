using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Trap/CircleEffect")]
public class Circle_EnemyEffect : AttackEffectSO
{
    [SerializeField] private GameObject[] bulletPrefab;
    private GameObject bullet;
    public float bulletSpeed = 20f;
    public int bulletCount = 12;
    public GameObject effectAttack;
    public override void ApplyAttack(Transform enemy, Transform player)
    {
        SpawnBullets(enemy.position, enemy);
    }
    private void SpawnBullets(Vector3 position, Transform enemy)
    {
        int angleStep = 360 / bulletCount;
        float angle = 0;

        for (int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 moveDir = new Vector2(dirX, dirY).normalized;
            
            Instantiate(effectAttack, position, Quaternion.identity, enemy);
            //SpriteRenderer spriteBullet = bullet.GetComponent<SpriteRenderer>();
            if (i % 2 == 0)
            {
                Quaternion rotation = Quaternion.Euler(Vector3.forward.x, Vector3.forward.y, Vector3.forward.z + angle + 90);
                bullet = Instantiate(bulletPrefab[0], position, rotation);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(Vector3.forward.x, Vector3.forward.y, Vector3.forward.z + angle + 45);
                bullet = Instantiate(bulletPrefab[1], position, rotation);
            }
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = moveDir * bulletSpeed;
            }
            angle += angleStep;
        }
    }
}
