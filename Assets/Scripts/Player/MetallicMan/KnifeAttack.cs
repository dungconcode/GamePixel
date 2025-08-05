using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [SerializeField] private Animator knife_anim;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 0.1f;
    private float damage;
    private float timer;
    private float attackCooldown = 0.8f;

    private void Start()
    {
        knife_anim = GetComponent<Animator>();
        damage = PlayerHealth.instance.playerIndex.damage;
        attackPoint = GameObject.Find("AttackPoint");
    }
    public void Attack()
    {
        PlayerAttack();
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }
    public void PlayerAttack()
    {
        if (timer <= 0f)
        {
            timer = attackCooldown;
            knife_anim.SetBool("Attack", true);
        }
    }
    public void DamageEnemy()
    {
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayerMask);
        foreach (Collider2D enemy in hitenemy)
        {
            Enemy_Health enemyHealth = enemy.GetComponent<Enemy_Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
    public void ResetAttack()
    {
        knife_anim.SetBool("Attack", false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
    }
}
