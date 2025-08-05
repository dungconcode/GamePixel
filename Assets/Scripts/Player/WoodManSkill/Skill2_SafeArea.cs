using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_SafeArea : SkillBase
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
    public override void Activate()
    {
        ActiveSkill(1f); // Call ActiveSkill with a delay of 2 seconds
    }
    public void ActiveSkill(float timeToDelaySkill)
    {
        StartCoroutine(WaitForNextSkill(timeToDelaySkill));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered safe area!");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                rb.AddForce(direction * forcwe * Time.deltaTime, ForceMode2D.Impulse);
                StartCoroutine(KnockBackCounter(rb, 0.5f)); // Knockback duration of 0.5 seconds
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
                rb.AddForce(direction * forcwe * Time.deltaTime, ForceMode2D.Impulse); // nhỏ hơn để tránh đẩy quá mạnh
                StartCoroutine(KnockBackCounter(rb, 0.5f));
            }
        }
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
    }
    IEnumerator KnockBackCounter(Rigidbody2D rb,float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector2.zero;
    }

    IEnumerator WaitForNextSkill(float timeToDelay)
    {
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = true;
            StartCoroutine(ExpaindRadius(maxradius,0.5f));
            yield return new WaitForSeconds(timeToDelay);
            circleCollider2D.radius = 0f;
            circleCollider2D.enabled = false;
        }
    }
}
