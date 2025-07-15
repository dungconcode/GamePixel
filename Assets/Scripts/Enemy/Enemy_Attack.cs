using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    PlayerHealth playerhp;
    bool isTouching = false;
    Coroutine damageCoroutine;
    private void Start()
    {
        playerhp = PlayerHealth.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerhp != null)
        {
            isTouching = true;
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DelayTakeDamage());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerhp != null)
        {
            isTouching = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
    IEnumerator DelayTakeDamage()
    {
        while (isTouching)
        {
            playerhp.TakeDamage(1);
            yield return new WaitForSeconds(1.5f);
        }
        
    }
}
