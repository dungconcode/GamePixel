using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBullet : MonoBehaviour
{
    public int damage = 1; // sẽ được set từ bên ngoài
    [SerializeField] private GameObject iceEffectPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Health>()?.TakeDamage(damage);
            StartCoroutine(FreezeEnemy(collision.gameObject, 1f));
            //Destroy(gameObject);
        }
    }
    IEnumerator FreezeEnemy(GameObject enemy, float freezeDuration = 1f)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = rb ? rb.velocity : Vector2.zero;
        Enemy_Controller controller = enemy.GetComponent<Enemy_Controller>();
        AI_Path aiPath = enemy.GetComponent<AI_Path>();

        if (aiPath != null)
        {
            aiPath.isFrozen = true;
        }
        if (controller != null)
            controller.enabled = false;
        
        
        if (rb != null)
            rb.velocity = Vector2.zero;
        GameObject ice = null;
        ice = Instantiate(iceEffectPrefab, enemy.transform.position, Quaternion.identity);
        ice.transform.SetParent(enemy.transform);
        yield return new WaitForSeconds(freezeDuration);
        if (aiPath != null)
        {
            aiPath.isFrozen = false;
        }
        if (controller != null)
            controller.enabled = true;
        if (rb != null) 
            rb.velocity = originalVelocity;
        if (ice != null)
            Destroy(ice);

    }
}
