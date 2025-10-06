using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slash : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private AI_Path aiPath;
    private Animator anim;

    private float attackRange = 0.5f;
    private int damage = 1;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        StartAttack();
    }
    private void StartAttack()
    {
        if (aiPath.isEnemyAttacking)
        {
            anim.SetBool("isAttacking", true);
        }

    }
    public void Attack()
    {
        Collider2D hitPlayers = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayers != null)
        {
            hitPlayers.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            hitPlayers.GetComponent<Player_KnockBack>()?.KnockBack(transform, 10f);
        }
    }
    public void StopShooting()
    {
        anim.SetBool("isAttacking", false);
        aiPath.isEnemyAttacking = false;
    }
}
