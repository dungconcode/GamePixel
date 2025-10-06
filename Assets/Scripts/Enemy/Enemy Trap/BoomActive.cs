using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomActive : MonoBehaviour
{
    private float maxRadius = 2f;
    public float expansionSpeed = 2f;
    public float forceKockback = 50f;
    private CircleCollider2D circleCollider;

    private bool isExpanded = false;
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0f;
        circleCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isExpanded) return;
        if (collision.CompareTag("Player"))
        {
            isExpanded = true;
            collision.GetComponent<PlayerHealth>().TakeDamage(2);
            collision.GetComponent<Player_KnockBack>()?.KnockBack(transform, forceKockback);
        }
    }
    public void ExtendBoomArea()
    {
        circleCollider.enabled = true;
        circleCollider.radius = 0f;
        if (circleCollider != null && circleCollider.radius <= maxRadius)
        {
            circleCollider.radius += expansionSpeed * Time.deltaTime;
        }
        circleCollider.radius = maxRadius;
    }
    public void DestroyBoom()
    {
        Destroy(gameObject);
        circleCollider.enabled = false;
    }
}
