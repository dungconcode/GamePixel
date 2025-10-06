using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_KnockBack : MonoBehaviour
{
    public bool isKnockBack;
    //[SerializeField] private float knockBackForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void KnockBack(Transform enemyTransform, float knockBackForce)
    {
        if (!gameObject.activeInHierarchy) return;

        isKnockBack = true;
        Vector2 direction = (transform.position - enemyTransform.position).normalized;
        rb.velocity = direction * knockBackForce;
        StartCoroutine(DelayKnockBack(0.1f));
    }
    IEnumerator DelayKnockBack(float sunTime)
    {
        yield return new WaitForSeconds(sunTime);
        isKnockBack = false;
        rb.velocity = Vector2.zero;
    }
}
