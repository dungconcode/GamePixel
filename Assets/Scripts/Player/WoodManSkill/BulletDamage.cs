using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<Enemy_Health>()?.TakeDamage(PlayerHealth.instance.playerIndex.damage);
            collision.gameObject.GetComponent<Enemy_KnockBack>()?.KnockBack(PlayerHealth.instance.transform, 8f);
            Destroy(gameObject, 0.05f);
        }
    }
}
