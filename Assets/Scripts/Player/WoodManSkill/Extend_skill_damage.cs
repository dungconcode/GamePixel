using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend_skill_damage : MonoBehaviour
{
    private CircleCollider2D radiusCollision;
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
        }
    }
    public void RockAttackEnable()
    {
        radiusCollision.enabled = true;
    }
    public void DestroyPrefabs()
    {
        radiusCollision.enabled = false;
        Destroy(gameObject);
    }
}
