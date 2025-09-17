using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAnim : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 0.1f;
    public float damage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        damage = PlayerHealth.instance.playerIndex.damage;
    }
    public void DamageEnemy()
    {
        if (attackPoint == null) return;
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
    public void ResetPunch()
    {
        animator.SetBool("Punch", false);
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
