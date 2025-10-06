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
    public void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        if(hitPlayers.Length > 0)
        {
            hitPlayers[0].GetComponent<PlayerHealth>()?.TakeDamage(damage);
            hitPlayers[0].GetComponent<Player_KnockBack>()?.KnockBack(transform, 15f);
        }
    }
}
