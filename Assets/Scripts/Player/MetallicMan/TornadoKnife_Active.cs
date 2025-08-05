using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoKnife_Active : MonoBehaviour
{
    private CircleCollider2D tornadoCollider;

    private void Start()
    {
        tornadoCollider = GetComponent<CircleCollider2D>();
        tornadoCollider.enabled = false;
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
        tornadoCollider.enabled = true;
    }
    public void DestroyPrefabs()
    {
        tornadoCollider.enabled = false;
        Destroy(gameObject);
    }
}
