using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            collision.gameObject.GetComponent<Player_KnockBack>()?.KnockBack(transform, 10f);
            Destroy(gameObject);
        }
    }
}
