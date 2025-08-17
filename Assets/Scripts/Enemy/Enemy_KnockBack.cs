using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KnockBack : MonoBehaviour
{
    public void KnockBack(Transform playerTransform, float knockBackForce)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        }
    }
}
