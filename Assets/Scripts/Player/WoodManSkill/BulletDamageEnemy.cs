using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damage;
    private bool hasDealtDamage = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasDealtDamage) return;
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            hasDealtDamage = true;
            collision.gameObject.GetComponent<Enemy_Health>()?.TakeDamage(PlayerHealth.instance.playerIndex.damage);
            collision.gameObject.GetComponent<Enemy_KnockBack>()?.KnockBack(PlayerHealth.instance.transform);
            Destroy(gameObject, 0.05f);
        }
    }
}
