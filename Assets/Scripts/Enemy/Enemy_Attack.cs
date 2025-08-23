using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public Transform attackPoint;
    private int damage = 1;
    public float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayer;

    PlayerHealth playerhp;
    AI_Path aiPath;
    public bool isTouching = false;
    Coroutine damageCoroutine;

    Animator anim;
    private void Start()
    {
        playerhp = PlayerHealth.instance;
        aiPath = GetComponent<AI_Path>();
        anim = GetComponent<Animator>();
    }
    //private void Update()
    //{
    //    aiPath.isEnemyAttacking = isTouching;
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && playerhp != null)
    //    {
    //        isTouching = true;
    //        if (damageCoroutine == null)
    //        {
    //            damageCoroutine = StartCoroutine(DelayTakeDamage());
    //        }
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && playerhp != null)
    //    {
    //        isTouching = true;
    //        if (damageCoroutine == null)
    //        {
    //            damageCoroutine = StartCoroutine(DelayTakeDamage());
    //        }
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && playerhp != null)
    //    {
    //        isTouching = false;
    //        if (damageCoroutine != null)
    //        {
    //            StopCoroutine(damageCoroutine);
    //            damageCoroutine = null;
    //        }
    //    }
    //}
    public void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        if(hitPlayers.Length > 0)
        {
            hitPlayers[0].GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
    //IEnumerator DelayEnemyAttack()
    //{
    //    while (aiPath.isEnemyAttacking)
    //    {
            
    //        yield return new WaitForSeconds(1.5f);
    //    }
    //}
    //IEnumerator DelayTakeDamage()
    //{
    //    while (isTouching)
    //    {
    //        playerhp.TakeDamage(1);
    //        yield return new WaitForSeconds(1.5f);
    //    }
        
    //}
}
