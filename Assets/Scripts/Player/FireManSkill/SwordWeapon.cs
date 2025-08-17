using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponBase
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 1f;
    public float damage;
    private Animator anim;
    private void Start()
    {
        damage = PlayerHealth.instance.playerIndex.damage;
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Attack()
    {
        anim.SetBool("isAttacking", true);
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
    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
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
