using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public int damage = 1; // sẽ được set từ bên ngoài
    [SerializeField] private GameObject iceEffectPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy: " + collision.name);
            collision.GetComponent<Enemy_Health>()?.TakeDamage(damage);
            StartCoroutine(FreezeEnemy(collision.gameObject, 2f));
            //Destroy(gameObject);
        }
    }
    IEnumerator FreezeEnemy(GameObject enemy, float freezeDuration = 2f)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = rb ? rb.velocity : Vector2.zero;
        Enemy_Controller controller = enemy.GetComponent<Enemy_Controller>();
        if(controller != null)
            controller.enabled = false;
        if(rb != null)
            rb.velocity = Vector2.zero;
        GameObject ice = null;
        ice = Instantiate(iceEffectPrefab, enemy.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(freezeDuration);
        if (controller != null)
            controller.enabled = true;
        if (rb != null) 
            rb.velocity = originalVelocity;
        if (ice != null)
            Destroy(ice);

    }
}
