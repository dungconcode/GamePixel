using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float suckForce = 10f;
    private float duration = 2f;
    private float explosionRadius = 5f;
    [SerializeField] private float damage = 1f;
    private List<Rigidbody2D> enemies = new List<Rigidbody2D>();
    private void Start()
    {
        StartCoroutine(ExecuteBlackHole());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            if (col.CompareTag("Enemy"))
            {
                Rigidbody2D enemyRb = col.GetComponent<Rigidbody2D>();
                if (enemyRb != null && !enemies.Contains(enemyRb))
                    enemies.Add(enemyRb);
            }
        }
    }
    private IEnumerator ExecuteBlackHole()
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            foreach (Rigidbody2D enemy in enemies)
            {
                if (enemy != null)
                {
                    Vector2 dir = (transform.position - enemy.transform.position).normalized;
                    enemy.AddForce(dir * suckForce, ForceMode2D.Force);
                }
            }
            yield return null;
        }
        Explored();
    }
    private void Explored()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var col in hit)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy_Health hp = col.GetComponent<Enemy_Health>();
                if (hp != null)
                    hp.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
