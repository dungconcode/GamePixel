using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend_skill_damage : MonoBehaviour
{
    private CircleCollider2D radiusCollision;
    [SerializeField] private GameObject freezeEffectPrefab;
    private void Start()
    {
        radiusCollision = GetComponent<CircleCollider2D>();
        radiusCollision.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Health>()?.TakeDamage(2);
            StartCoroutine(DamageEnemy(collision.gameObject));
        }
    }
    //IEnumerator StartFreeze(GameObject enemy, float freezeDuration = 1f)
    //{
    //    enemy.GetComponent<Enemy_KnockBack>()?.KnockBack(PlayerHealth.instance.transform, 8f);
    //    yield return new WaitForSeconds(freezeDuration);
    //    Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
    //    Vector2 originalVelocity = rb ? rb.velocity : Vector2.zero;
    //    Enemy_Controller controller = enemy.GetComponent<Enemy_Controller>();
    //    AI_Path aiPath = enemy.GetComponent<AI_Path>();
    //    if (aiPath != null)
    //    {
    //        aiPath.isFrozen = true;
    //    }
    //    if (controller != null)
    //        controller.enabled = false;
    //    yield return new WaitForSeconds(1f);
    //    if (aiPath != null)
    //    {
    //        aiPath.isFrozen = false;
    //    }
    //    if (controller != null)
    //        controller.enabled = true;
    //    if (rb != null)
    //        rb.velocity = originalVelocity;
    //}
    IEnumerator DamageEnemy(GameObject enemy)
    {
        if(enemy == null) yield break;
        enemy.GetComponent<Enemy_KnockBack>()?.KnockBack(PlayerHealth.instance.transform);
        yield return new WaitForSeconds(0.2f);
        if(enemy == null) yield break;
        enemy.GetComponent<Enemy_KnockBack>()?.FreezeEnemy(freezeEffectPrefab, 1f);
        yield return new WaitForSeconds(0.2f);
        radiusCollision.enabled = false;
        Destroy(gameObject);
    }
    public void WoodAttackEnable()
    {
        radiusCollision.enabled = true;
    }
    public void DestroyPrefabs()
    {
        radiusCollision.enabled = false;
        Destroy(gameObject);
    }
}
