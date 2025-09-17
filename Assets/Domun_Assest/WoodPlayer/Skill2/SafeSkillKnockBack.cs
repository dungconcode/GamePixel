using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSkillKnockBack : MonoBehaviour
{
    [SerializeField] private float forcwe = 5f;
    [SerializeField] private CircleCollider2D circleCollider2D;
    public float maxradius = 2;
    private void Start()
    {
        if (circleCollider2D == null)
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = false;
            circleCollider2D.radius = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                rb.AddForce(direction * forcwe * Time.deltaTime, ForceMode2D.Impulse);
                StartCoroutine(KnockBackCounter(rb, 0.5f));
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                rb.AddForce(direction * forcwe * Time.deltaTime, ForceMode2D.Impulse);
                StartCoroutine(KnockBackCounter(rb, 0.5f)); 
            }
        }
    }
    IEnumerator KnockBackCounter(Rigidbody2D rb, float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector2.zero;
    }
    IEnumerator ExpaindRadius(float targetRadius, float duration)
    {
        float startRadius = 0;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            circleCollider2D.radius = Mathf.Lerp(startRadius, targetRadius, elapsed / duration);
            yield return null;
        }
        circleCollider2D.radius = targetRadius;
        Debug.Log("Radius expanded to: " + circleCollider2D.radius);
    }
    IEnumerator TimeToKnockBack()
    {
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = true;
            StartCoroutine(ExpaindRadius(maxradius, 0.5f)); 
            yield return new WaitForSeconds(1f);
            circleCollider2D.radius = 0f;
            circleCollider2D.enabled = false;
        }
    }
    public void StartKnockBack()
    {
        StartCoroutine(TimeToKnockBack());
    }
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
