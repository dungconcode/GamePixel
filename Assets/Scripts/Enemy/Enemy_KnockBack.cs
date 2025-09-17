using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KnockBack : MonoBehaviour
{
    public float knockBackForce;
    public void KnockBack(Transform playerTransform)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        }
    }
    public void FreezeEnemy(GameObject effectFreeze, float freezeDuration)
    {
        StartCoroutine(FreezeCouroutine(effectFreeze, freezeDuration));
    }
    IEnumerator FreezeCouroutine(GameObject effectFreeze, float freezeDuration)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = rb ? rb.velocity : Vector2.zero;
        Enemy_Controller controller = GetComponent<Enemy_Controller>();
        AI_Path aiPath = GetComponent<AI_Path>();
        if (aiPath != null)
        {
            aiPath.isFrozen = true;
            aiPath.player = null;
            aiPath.isMoving = false;
        }
        if(controller != null)
            controller.enabled = false;

        GameObject ice = Instantiate(effectFreeze, transform.position, Quaternion.identity);
        ice.transform.SetParent(transform);
        yield return new WaitForSeconds(freezeDuration);
        if (aiPath != null)
        {
            aiPath.isFrozen = false;
            aiPath.player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        if (controller != null)
            controller.enabled = true;
        if (rb != null)
            rb.velocity = originalVelocity;
        if (ice != null)
            Destroy(ice);
    }    
}
