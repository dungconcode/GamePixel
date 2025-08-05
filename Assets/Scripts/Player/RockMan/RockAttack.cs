using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    private CircleCollider2D rockCollider;

    private void Start()
    {
        rockCollider = GetComponent<CircleCollider2D>();
        rockCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Health>()?.TakeDamage(5);
        }
    }
    public void RockAttackEnable()
    {
        rockCollider.enabled = true;
    }
    public void DestroyPrefabs()
    {
        rockCollider.enabled = false;
        Destroy(gameObject);
    }
}
