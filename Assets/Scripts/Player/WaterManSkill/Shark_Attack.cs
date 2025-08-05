using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Attack : MonoBehaviour
{
    private CircleCollider2D sharkCollider;
    private void Start()
    {
        sharkCollider = GetComponent<CircleCollider2D>();
        sharkCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Health>()?.TakeDamage(5);
        }
    }
    public void SharkEating()
    {
        sharkCollider.enabled = true;
    }
    public void DestroyPrefabs()
    {
        sharkCollider.enabled = false;
        Destroy(gameObject);
    }
}
