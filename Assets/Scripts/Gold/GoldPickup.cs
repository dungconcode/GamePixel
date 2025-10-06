using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class GoldPickup : MonoBehaviour
{
    [Header("Value")]
    public int amount = 1;

    [Header("Movement")]
    private float attactionRadius = 3f;
    private float minSpeed = 6f;
    private float maxSpeed = 12f;


    Rigidbody2D rb;
    Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void Update()
    {
        if (target == null) target = FindClosesPlayer();
        if (target == null) return;

        float d = Vector2.Distance(transform.position, target.position);
        if (d <= attactionRadius)
        {
            float t = Mathf.InverseLerp(attactionRadius, 0f, d);
            float speed = Mathf.Lerp(minSpeed, maxSpeed, t);
            Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
            rb.velocity = dir * speed;
        }
    }
    private Transform FindClosesPlayer()
    {
        var players = GameObject.FindGameObjectWithTag("Player");
        Transform best = null;
        float bestD = Mathf.Infinity;
        float d = Vector2.Distance(transform.position, players.transform.position);
        if (d < bestD)
        {
            bestD = d;
            best = players.transform;
        }
        return best;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        GoldService.Instance.AddGold(amount);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attactionRadius);
    }
}
